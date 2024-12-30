// For more information see https://aka.ms/fsharp-console-apps
open Elmish
open Elmish.React
open Feliz

let IDSEQ =
    seq {
        for i in 1..200000 do
            yield i
    }
    |> Seq.cache

let nextID =
    let enum = IDSEQ.GetEnumerator()

    let foo () =
        if enum.MoveNext() then
            enum.Current
        else
            failwith "No more ids."

    foo

type Card =
    { id: int
      image: string
      selected: bool
      shake: bool
      guessed: bool }

    static member makeCard(image: string) =

        { id = nextID ()
          image = image
          selected = false
          shake = false
          guessed = false }

type Game = { cardsToGuess: Card list }

type State =
    | Initial of Game
    | InitialGoodGuess of Game
    | InitialWrongGuess of Game
    | Guessed1 of Game

type Message =
    | TileClick of Card
    | DeselectAll

let selectCard (card: Card) (cards: Card list) : Card list =
    let foo (c: Card) =
        if c.id = card.id then { c with selected = true } else c

    cards |> List.map foo

let shakeCard (cards: Card list) : Card list =
    let foo (c: Card) =
        if c.selected then { c with shake = true } else c

    cards |> List.map foo

let deselectCards (cards: Card list) : Card list =
    let foo (c: Card) = { c with selected = false }

    cards |> List.map foo

let countSelected (cards: Card list) : int =
    cards |> List.filter (fun x -> x.selected) |> List.length

let init () : State * Cmd<'Msg> =
    let cards =
        [ Card.makeCard "fudge.png"
          Card.makeCard "fudge.png"
          Card.makeCard "fudge.png"
          Card.makeCard "fudge.png" ]

    Initial { cardsToGuess = cards }, Cmd.none

let update (msg: Message) (state: State) : State * Cmd<'Msg> =
    match state with
    | Initial gameState ->
        match msg with
        | TileClick(card) ->
            let newCards = selectCard card gameState.cardsToGuess

            if countSelected gameState.cardsToGuess < 3 then
                Initial
                    { gameState with
                        cardsToGuess = newCards },
                Cmd.none
            else
                let shakenCards = shakeCard newCards

                let wait2sec () =
                    async {
                        do! Async.Sleep 2000
                        printfn "Delayed print"
                    }

                let cmdfoo () = DeselectAll

                Initial
                    { gameState with
                        cardsToGuess = shakenCards },
                Cmd.OfAsync.perform wait2sec () cmdfoo
        | DeselectAll ->
            let newCards = gameState.cardsToGuess |> deselectCards |> shakeCard

            Initial
                { gameState with
                    cardsToGuess = newCards },
            Cmd.none
    | _ -> state, Cmd.none

let render (state: State) (dispatch: Message -> unit) : ReactElement =
    let card (c: Card) =
        let cardClasses =
            let res = [ "card" ]

            let res1 =
                if c.selected then
                    List.append res [ "selected-card" ]
                else
                    res

            if c.shake then List.append res1 [ "shake-card" ] else res1

        Html.div
            [ prop.classes cardClasses
              prop.children [ Html.img [ prop.src $"/public/{c.image}"; prop.alt "Card" ] ]
              prop.onClick (fun _ -> dispatch (TileClick c)) ]

    let cards () : ReactElement list =
        let guessingCards =
            match state with
            | Initial c -> c.cardsToGuess
            | InitialGoodGuess c -> c.cardsToGuess
            | InitialWrongGuess c -> c.cardsToGuess
            | Guessed1 c -> c.cardsToGuess
            |> List.map card

        guessingCards


    let grid () =
        Html.div [ prop.className "grid"; cards () |> prop.children ]

    Html.div [ prop.className "container"; prop.children [ grid () ] ]

Program.mkProgram init update render
|> Program.withReactSynchronous "app"
|> Program.run

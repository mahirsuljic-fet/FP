open Elmish.React
open Elmish
open Feliz

type Opearacija =
    | Add
    | Sub
    | Mul
    | Div

type Poruka =
    | Broj of char
    | Jednako
    | Clear
    | Operator of Opearacija

type State =
    | UnosimPrviBroj of string
    | UnioOperator of string
    | UnioDrugiBroj of string

let init () : State = UnosimPrviBroj ""

let update (msg: Poruka) (state: State) : State =
    match msg with
    | Broj(c) -> UnosimPrviBroj(string c)
    | _ -> UnosimPrviBroj "X" // nesto random

let render (state: State) (dispatch: (Poruka -> unit)) =

    let clickFoo (broj: char) (me: Browser.Types.MouseEvent) : unit = dispatch (Broj broj)

    let brojBtn (broj: char) =
        Html.button
            [ prop.className ("broj-btn")
              prop.text (string broj)
              prop.onClick (clickFoo broj) ]

    let opBtn (op: char) =
        Html.button [ prop.className ("op-btn"); prop.text (string op) ]

    let eqBtn = Html.button [ prop.className ("eq-btn"); prop.text ("=") ]

    let clrBtn = Html.button [ prop.className ("clr-btn"); prop.text ("C") ]

    let br = Html.br []

    let text =
        match state with
        | UnosimPrviBroj(s) -> "b1 -> " + s
        | UnioOperator(s) -> "op -> " + s
        | UnioDrugiBroj(s) -> "b2 -> " + s


    let display = Html.div [ prop.text (text) ]

    Html.div
        [ display
          brojBtn ('1')
          brojBtn ('2')
          brojBtn ('3')
          opBtn ('+')
          br
          brojBtn ('4')
          brojBtn ('5')
          brojBtn ('6')
          opBtn ('-')
          br
          brojBtn ('7')
          brojBtn ('8')
          brojBtn ('9')
          opBtn ('*')
          br
          clrBtn
          brojBtn ('0')
          eqBtn
          opBtn ('/') ]

Program.mkSimple init update render
|> Program.withReactSynchronous "app"
|> Program.run

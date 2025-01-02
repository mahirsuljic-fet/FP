open Elmish.React
open Elmish
open Feliz

type Operation =
    | Add
    | Sub
    | Mul
    | Div
    | Invalid

type Msg =
    | Number of char
    | Evaluate
    | Clear
    | Operator of Operation

type State =
    | InputFirstNumber of string
    | GotOperator of string * Operation
    | InputSecondNumber of string * Operation * string


let strToOp (str: string) : Operation =
    match str.ToLower() with
    | "+" -> Add
    | "-" -> Sub
    | "*" -> Mul
    | "/" -> Div
    | "add" -> Add
    | "sub" -> Sub
    | "mul" -> Mul
    | "div" -> Div
    | _ -> Invalid

let charToOp (ch: char) : Operation = string ch |> strToOp

let opToChar (op: Operation) : char =
    match op with
    | Add -> '+'
    | Sub -> '-'
    | Mul -> '*'
    | Div -> '/'
    | Invalid -> '?'

let opToStr (op: Operation) : string = opToChar op |> string

let eval (lhs: string) (op: Operation) (rhs: string) : string =
    let result =
        match op with
        | _ when lhs = "NaN" || rhs = "NaN" -> "NaN"
        | _ when lhs = "-∞" || rhs = "-∞" -> "-∞"
        | _ when lhs = "∞" || rhs = "∞" -> "∞"
        | Div when int64 rhs = 0 -> "NaN"
        | Add -> int64 lhs + int64 rhs |> string
        | Sub -> int64 lhs - int64 rhs |> string
        | Mul -> int64 lhs * int64 rhs |> string
        | Div -> int64 lhs / int64 rhs |> string
        | Invalid -> "Invalid operation"

    if result.Length <= 10 then result
    elif int64 result < 0 then "-∞"
    else "∞"

let initState = InputFirstNumber ""

let init () : State = initState

let update (msg: Msg) (state: State) : State =
    match state with
    | InputFirstNumber(str) ->
        match msg with
        | Number(n) ->
            if str.Length < 10 then
                InputFirstNumber(str + string n)
            else
                state
        | Evaluate -> state
        | Clear -> initState
        | Operator(op) -> GotOperator(str, op)
    | GotOperator(num, op) ->
        match msg with
        | Number(n) -> InputSecondNumber(num, op, string n)
        | Evaluate -> state
        | Clear -> initState
        | Operator(newOp) -> GotOperator(num, newOp)
    | InputSecondNumber(lhs, op, rhs) ->
        match msg with
        | Number(n) ->
            if rhs.Length < 10 then
                InputSecondNumber(lhs, op, rhs + string n)
            else
                state
        | Evaluate -> InputFirstNumber(eval lhs op rhs)
        | Clear -> initState
        | Operator(newOp) -> GotOperator(eval lhs op rhs, newOp)

let render (state: State) (dispatch: (Msg -> unit)) =

    let click_num (num: char) (_: Browser.Types.MouseEvent) : unit = dispatch (Number num)
    let click_op (op: char) (_: Browser.Types.MouseEvent) : unit = dispatch (Operator(charToOp op))
    let click_eq (_: Browser.Types.MouseEvent) : unit = dispatch (Evaluate)
    let click_clr (_: Browser.Types.MouseEvent) : unit = dispatch (Clear)

    let btn_num (num: char) =
        Html.button
            [ prop.className ("btn-num")
              prop.text (string num)
              prop.onClick (click_num num) ]

    let btn_op (op: char) =
        Html.button [ prop.className ("btn-op"); prop.text (string op); prop.onClick (click_op op) ]

    let btn_eq =
        Html.button [ prop.className ("btn-eq"); prop.text ("="); prop.onClick (click_eq) ]

    let btn_clr =
        Html.button [ prop.className ("btn-clr"); prop.text ("C"); prop.onClick (click_clr) ]

    let br = Html.br []

    let text =
        match state with
        | InputFirstNumber(s) -> s
        | GotOperator(num, op) -> string num + " " + opToStr op
        | InputSecondNumber(lhs, op, rhs) -> string lhs + " " + opToStr op + " " + string rhs


    let display = Html.div [ prop.className "display"; prop.text (text) ]

    Html.div
        [ prop.className "container"
          prop.children
              [ display
                btn_num ('1')
                btn_num ('2')
                btn_num ('3')
                btn_op ('+')
                br
                btn_num ('4')
                btn_num ('5')
                btn_num ('6')
                btn_op ('-')
                br
                btn_num ('7')
                btn_num ('8')
                btn_num ('9')
                btn_op ('*')
                br
                btn_clr
                btn_num ('0')
                btn_eq
                btn_op ('/') ] ]


Program.mkSimple init update render
|> Program.withReactSynchronous "app"
|> Program.run

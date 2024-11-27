printfn "Podržani tipovi unosa: int"
printfn "Podržane operacije: +, -, *, /"
printfn "Unesite izraz u formatu a op b (npr. 2 + 3), razmak je obavezan\n"
printf "Unos: "
let input = System.Console.ReadLine()

let (a, op, b) =
    let arr = input.Split(' ')
    (arr.[0] |> int, arr.[1] |> char, arr.[2] |> int)

type Result =
    | Value of int
    | Error of string

let calculate a op b : Result =
    match op with
    | '+' -> Value(a + b)
    | '-' -> Value(a - b)
    | '*' -> Value(a * b)
    | '/' when b <> 0 -> Value(a / b)
    | '/' -> Error "Dijeljenje sa nulom nije definisano"
    | _ -> Error "Operacija nije validna"

match calculate a op b with
| Value x -> printfn $"{a} {op} {b} = {x}"
| Error e -> printfn $"Error: {e}"

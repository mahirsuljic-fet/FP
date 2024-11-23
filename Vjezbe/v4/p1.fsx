// Napisati funkciju calcNums koja uzima dva broja (double tipa) i
// string koji predstavlja operaciju koju treba izvesti.
// Funkcija nazad vraca string koji predstavlja izvedenu operaciju
// ili error poruku ukoliko operacija nije uspjela (dijeljenje sa 0
// ili nevalidna operacija).
// Funkcija treba da podrzava operacije "add", "sub", "mul" i "div"
// Primjer: calcNums 10.0 5.0 mul -> "10.0 * 5.0 = 50.0"

// let calcNums a b op =
//   if op = "add" then
//     sprintf $"{a} + {b} = {a + b}"
//   else if op = "sub" then
//     sprintf $"{a} - {b} = {a - b}"
//   else if op = "mul" then
//     sprintf $"{a} * {b} = {a * b}"
//   else if op = "div" then 
//     if b <> 0.0 then
//       sprintf $"{a} / {b} = {a / b}"
//     else "Dijeljenje sa nulom nije dozvoljeno"
//   else "Nevalidna operacija"

let calcNums a b op =
  match op with
  | "add" -> sprintf $"{a} + {b} = {a + b}"
  | "sub" -> sprintf $"{a} - {b} = {a - b}"
  | "mul" -> sprintf $"{a} * {b} = {a * b}"
  | "div" when b <> 0.0 -> sprintf $"{a} / {b} = {a / b}"
  | "div" -> "Dijeljenje sa nulom nije dozvoljeno"
  | _ -> "Nevalidna operacija"

let resultAdd = calcNums 10.0 5.0 "add"
let resultSub = calcNums 10.0 5.0 "sub"
let resultMul = calcNums 10.0 5.0 "mul"
let resultDiv = calcNums 10.0 5.0 "div"
let resultDivByZero = calcNums 10.0 0.0 "div"
let resultInvalid = calcNums 10.0 5.0 "mod" // Invalid operation

printfn "%s" resultAdd
printfn "%s" resultSub
printfn "%s" resultMul
printfn "%s" resultDiv
printfn "%s" resultDivByZero
printfn "%s" resultInvalid

calcNums 10.0 5.0 "div" |> ignore

let aaa = ignore
let bbb = printf

calcNums 6.0 4.0 "add"
calcNums 6.0 4.0 "add"

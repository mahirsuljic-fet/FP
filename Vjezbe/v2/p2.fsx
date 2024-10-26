// Napisati izraz koji treba kvadrira broj 5, a nakon
// toga da ga uveca za 1. Rezultat generisati prvo koristeci
// operator aplikacija funkcije, nakon toga forward pipe
// i konacno backward pipe.

let num = 5
let square x = x * x
let inc x = x + 1

// let res = inc (square num)
// let res = inc <| square <| num // NE RADI jer je <| lijevo asocijativan
let res = inc <| square num
printfn "%d" (res)
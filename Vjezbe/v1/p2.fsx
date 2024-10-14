// Napisati let izraz koji ce dokazati da operator left shift
// radi operaciju p<<q <==> p = p * 2 ^ q (koristiti ** operator)

let p = 8
let q = 2

let lhs = p <<< q
let rhs = int32 (float32 p * 2f ** float32 q)
printfn "%b" (lhs = rhs)

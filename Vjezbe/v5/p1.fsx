// Napisati funkciju koja uzima 2 argumenta: divisor:int i listu integer-a.
// Funkcija treba nazad da vrati listu svih integer-a djeljivih sa
// prvim argumentom.

let list = [1; 2; 3; 4]

let rec filter (divisor : int) (list : List<int>) : List<int> =
    match list with
    | [] -> []
    | el :: xs when el % divisor = 0 -> el :: filter divisor xs
    | _ :: xs -> filter divisor xs

printfn "%A" (filter 2 list)

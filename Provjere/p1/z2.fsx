// Dodati svoj kod tako da se kod ispod kompajlira i radi.
// Program treba ispisati random broj od 0 do unesenog.
// Potrebno je anotirati svaki simbol u kodu.
// HINT: System.Random ima funkciju Next koja uzima jedan parametar maxValue i vraca random broj iz [0, maxValue)

let n: int = System.Console.ReadLine() |> int
let random: System.Random = System.Random()

// VAS KOD OVDJE
let getRandom (x: int) : (unit -> int) =
    let foo ((): unit) : int = random.Next x
    foo
// VAS KOD OVDJE

let k: int = n |> getRandom <| ()
printfn $"Random vrijednost od 0 do {n}: {k}"

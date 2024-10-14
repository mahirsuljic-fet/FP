// Pozvati funkciju enterNumber koja ce zaustaviti tok programa dok korisnik ne
// unese broj sa tastature. Povratna vrijednost funkcije je uneseni broj. Nakon
// toga uporediti uneseni broj sa randomNum brojem, te ukoliko su brojevi isti
// ispisati "TACNO" na ekran, u suprotnom program treba da izgenerise MatchFailure
// iznimku.

let rangeMin = 1
let rangeMax = 10

let enterNumber () =
    printf $"Unesite broj od {rangeMin} do {rangeMax}: "
    System.Console.ReadLine() |> int

let random = System.Random()
let randomNum = random.Next(rangeMin, rangeMax)

// Vas kod ovdje:
let x = enterNumber ()
let result = x = randomNum
let true = result
printfn "%s" "TACNO"

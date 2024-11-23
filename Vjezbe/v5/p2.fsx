// Digitalni korijen predstavlja vrijednost koju dobijemo rekurzivno
// sabirajuci cifre broja sve dok rezultat sabiranja cifara nije
// jednocifren broj.
//
// Primjer:
// Digitalni korijen broja 9876:
//  Suma cifara: 9 + 8 + 7 + 6 = 30
//  30 nije jednocifren broj, idemo ponovno u rekurziju:
//  3 + 0 = 3
//
// Rezultat je 3

// Uporediti rezultat sa modulom broja 9. :)

// Nacin 1
let rec saberi (cifre: char list) : int =
    match cifre with
    | [] -> 0
    | x :: xs -> (int x - int '0') + saberi xs

let rec droot (n: int) =
    let cifre = n |> string |> Seq.toList
    match cifre.Length with
    | 1 -> n
    | _ -> saberi cifre |> droot

// Nacin 2
let rec digroot (n: int) : int =
    match n with
    | 0 -> 0
    | n when n / 10 = 0 -> n
    | n -> n % 10 + digroot (n / 10)

let rec getroot (n: int) =
   match n with
   | 0 -> 0
   | n when n / 10 = 0 -> n
   | n -> digroot n |> digroot
 

let num = 9876

printfn "%d" (droot num)
printfn "%d" (getroot num)
printfn "%d | %d" (droot num) (num % 9)
printfn "%d | %d" (getroot num) (num % 9)

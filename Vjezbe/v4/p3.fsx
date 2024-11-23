// Napisati discriminated union Oblik koji ima 3 varijante konstruktora:
// Krug, Kvadrat i Pravougaonik
// Svaki od konstruktora treba da uzme odgovarajuci broj parametara potrebnih
// za izracunavanje obima i povrsine oblika.
// Napisati funkcije obim i povrsina koje izracunavaju obim i povrsinu oblika.

type Oblik =
  | Krug of double
  | Kvadrat of double
  | Pravougaonik of (double * double)

let krug = Krug 1.0
let kvadrat = Kvadrat 2.0
let pravougaonik = Pravougaonik (3.0, 4.0)

let PI = 3.14159

let obim (oblik: Oblik) : double =
  match oblik with
  | Krug r -> 2.0 * r * PI
  | Kvadrat a -> 4.0 * a
  | Pravougaonik (a, b) -> 2.0 * (a + b)

let povrsina (oblik: Oblik) : double =
  match oblik with
  | Krug r -> r * r * PI
  | Kvadrat a -> a * a
  | Pravougaonik (a, b) -> a * b

printfn "%A" krug
printfn "%A" kvadrat
printfn "%A" pravougaonik
printfn ""
printfn "%s" $"Obim kruga         -> {krug |> obim}"
printfn "%s" $"Obim kvadrata      -> {kvadrat |> obim}"
printfn "%s" $"Obim pravougaonika -> {pravougaonik |> obim}"
printfn ""
printfn "%s" $"Povrsina kruga         -> {krug |> povrsina}"
printfn "%s" $"Povrsina kvadrata      -> {kvadrat |> povrsina}"
printfn "%s" $"Povrsina pravougaonika -> {pravougaonik |> povrsina}"

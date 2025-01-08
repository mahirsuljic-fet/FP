Ne piše kakve su funkcije bile, pa ne mogu riješiti **b)** i **c)**, ali evo ispod otprilike kako riješiti **a)**.

### a)
Najlakše je odrediti pomoću nekih konstanti, npr.:
``` fsharp
let foo = 2
```
znamo da je `foo` tipa `int`
``` fsharp
let bar = []
```
znamo da je `bar` tipa `list` nečega
``` fsharp
let tar = None
```
znamo da je `tar` tipa `option` nečega

Pa ako imamo funkciju npr:
``` fsharp
let f a b =
 let x = a + 5
 let y = Some (b + "nesto")
 match y with
 | None -> None
 | Some k -> string k + string x |> Some
```

Možemo zakljućiti sljedeće:
- `a` i `x` su tipa `int`, jer se `a` sabira sa `5`, što je tipa `int`,
  `a` rezultat se veže sa `x`, pa i ono mora biti tipa `int`
- `b` se sabira sa `string`-om `"nesto"`, pa i b mora biti `string`.
  Rezultat tog sabiranja mora biti `string`.
  Od tog rezultata se pravi `option` type koji se veže za `y`.
  Pa `y` mora biti tipa `string option` (ili `option<string>`, isto je)
- Zadnji izraz u funkciji je `match`, tako da ono što `match` 
  vrati je povratna vrijednost cijele funkcije.
  Odma iz prvog slučaja vidimo da funkcija vraća `option` tip, ali `option` čega?
  U drugom slucaju, vidimo da se pravi `Some` `option` od sume dva `string`-a.
  To znači da povratna vrijednost mora biti `string option`.

Konačno, potpis funkcije je `int -> string -> option<string>`.

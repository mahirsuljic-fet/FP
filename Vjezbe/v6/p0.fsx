// Napisati rekurzivnu polimorfnu data strukturu V5L koja treba da radi sljedece operacije:

// foldl - funkcije viseg reda koje uzimaju folding funkciju kao prvi parametar, inicijalno stanje fold-a kao drugi parametar te listu kao treci parametar. Funkcija treba da aplicira folding funkciju na inicijalno stanje i prvi parametar liste, zatim rezultat prosljedjuje folding funkciji sa drugim argumentom i tako dalje.
// foldr - funkcija viseg reda koja uzim folding funkciju kao prvi parametar, inicijalno stanje fold-a kao drugi parametar te listu kao treci parametar. Funkcija dreba da aplicira folding funkciju na posljednji element liste i inicijalno stanje, zatim aplicira predposljednji element liste i rezultat folding funkcije i tako dalje.
// append - funkcija koja uzima dvije liste istog tipa te vraca nazad novu listu gdje se na pocetku nalaze prvo elementi prve liste, zatim elementi druge liste. Napraviti da operator @@ append-a liste.
// filter - funkcija koja uzima filter funkciju ('a->bool), listu, a nazad vraca samo one elemente liste za koje filter funkcija vrati tacno.
// head - funkcija koja mozda vrati nazad prvi element liste
// tail - funkcija koja mozda vrati nazad posljednji element liste
// pushFront - funkcija koja dodaje element na pocetak liste. Napraviti da operator ++ radi pushFront
// take - funkcija koja uzima integer i listu, a nazad vraca listu koja se sastoji od prvih n elemenata liste.
// zip - Uzima dvije liste kao argument, te nazad vraca listu tuple-ova gdje je svaki element par prvi element prve liste, prvi element druge liste. Funkcija vraca min(l1.Length, l2.Length) clanova.




type V5L<'a> =
    | Kraj
    | V5El of 'a * V5L<'a>

let lst = V5El(4, V5El(2, V5El(4, Kraj)))

let rec foldl (f: ('a -> 'b -> 'a)) (a: 'a) (lst: V5L<'b>) =
    match lst with
    | Kraj -> a
    | V5El(x, xs) ->
        let rez = f a x
        foldl f rez xs

lst |> foldl (/) 64 |> printfn "%A"


let rec foldr (f: ('a -> 'b -> 'b)) (b: 'b) (lst: V5L<'a>) : 'b =
    match lst with
    | Kraj -> b
    | V5El(x, xs) -> f x (foldr f b xs)


let firstVsLast (lst: List<'a>) : bool =
    match lst with
    | [] -> failwith "Prazna lista"
    | prvi :: xs ->
        let rec getZadnji (l0: List<'a>) : 'a =
            match l0 with
            | [] -> failwith "Nema zadnjeg"
            | zadnji :: [] -> zadnji
            | x :: xs -> getZadnji xs

        prvi > getZadnji xs

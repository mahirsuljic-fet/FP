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

let lst1 = V5El(1, V5El(7, V5El(2, V5El(6, V5El(3, V5El(4, Kraj))))))
let lst2 = V5El(5, V5El(9, V5El(11, Kraj)))

printfn "%A" lst1
printfn "%A" lst2

let rec v5foldl (f: ('a -> 'b -> 'a)) (a: 'a) (list: V5L<'b>) : 'a =
    match list with
    | Kraj -> a
    | V5El(x, xs) ->
        let resF = f a x
        v5foldl f resF xs

let rec v5foldr (f: ('a -> 'b -> 'a)) (a: 'a) (list: V5L<'b>) : 'a =
    match list with
    | Kraj -> a
    | V5El(x, xs) when xs = Kraj -> f a x
    | V5El(x, xs) ->
        let res = v5foldr f a xs
        f res x

let rec v5append (list1: V5L<'a>) (list2: V5L<'a>) : V5L<'a> =
    match list1 with
    | Kraj -> list2
    | V5El(x, xs) -> V5El(x, v5append xs list2)

let rec v5filter (f: ('a -> bool)) (list: V5L<'a>) : V5L<'a> =
    match list with
    | Kraj -> Kraj
    | V5El(x, xs) -> if f x then V5El(x, v5filter f xs) else v5filter f xs

let (@@) a b = v5append a b

// quick sort, zbog jednostavnosti uzimamo da je pivot prvi element
let rec v5sort (f: ('a -> bool)) (list: V5L<'a>) : V5L<'a> =
    match list with
    | Kraj -> Kraj
    | V5El(x, xs) ->
        let manji = v5filter (f x) xs
        let veci = v5filter (f x |> not) xs
        v5sort f manji @@ V5El(x, Kraj) @@ v5sort f veci
        // DZ

let sumList2 = v5foldl (+) 0 lst2

printfn "%d" sumList2
printfn "%A" (v5append lst1 lst2)

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

let (@@) a b = v5append a b

let rec v5filter (f: ('a -> bool)) (list: V5L<'a>) : V5L<'a> =
    match list with
    | Kraj -> Kraj
    | V5El(x, xs) -> if f x then V5El(x, v5filter f xs) else v5filter f xs

// quick sort, zbog jednostavnosti uzimamo da je pivot prvi element
let rec v5sort (f: ('a -> 'a -> bool)) (list: V5L<'a>) : V5L<'a> =
    match list with
    | Kraj -> Kraj
    | V5El(x, xs) ->
        let manji = v5filter (f x >> not) xs
        let veci = v5filter (f x) xs
        v5sort f manji @@ V5El(x, Kraj) @@ v5sort f veci

let v5head (list: V5L<'a>) : option<'a> =
    match list with
    | V5El(x, _) -> Some x
    | Kraj -> None

let rec v5tail (list: V5L<'a>) : option<'a> =
    match list with
    | Kraj -> None
    | V5El(x, Kraj) -> Some x
    | V5El(_, xs) -> v5tail xs

let v5pushfront (list: V5L<'a>) (value: 'a) : V5L<'a> = V5El(value, list)
let (++) = v5pushfront

let rec v5take (n: int) (list: V5L<'a>) : V5L<'a> =
    match list with
    | Kraj -> Kraj
    | V5El(_, _) when n <= 0 -> Kraj
    | V5El(x, _) when n = 1 -> V5El(x, Kraj)
    | V5El(x, xs) -> V5El(x, v5take (n - 1) xs)

let rec v5zip (list1: V5L<'a>) (list2: V5L<'b>) : V5L<'a * 'b> =
    match list1 with
    | Kraj -> Kraj
    | V5El(x1, xs1) ->
        match list2 with
        | Kraj -> Kraj
        | V5El(x2, xs2) -> V5El((x1, x2), v5zip xs1 xs2)

let rec v5print (pf: 'a -> unit) (list: V5L<'a>) : unit =
    match list with
    | V5El(x, xs) ->
        pf x
        v5print pf xs
    | Kraj -> printfn ""

let lst1 = V5El(1, V5El(7, V5El(2, V5El(6, V5El(3, V5El(4, Kraj))))))
let lst2 = V5El(5, V5El(9, V5El(11, Kraj)))
let inline v5printany list = v5print (printf "%A ") list

printfn "%A" lst1
printfn "%A" lst2

v5printany lst1
v5printany lst2

let sumList2 = v5foldl (+) 0 lst2
let append = v5append lst1 lst2
let sort = v5sort (fun x y -> x < y) lst1

let head =
    match v5head lst1 with
    | Some x -> x
    | None -> failwith "No head"

let tail =
    match v5tail lst1 with
    | Some x -> x
    | None -> failwith "No tail"

let pushfront = lst1 ++ 5 ++ 0
let take = v5take 3 lst1
let zip = v5zip lst1 lst2

printf "Sum    -> %d\n" sumList2
printf "Append -> ", v5printany append
printf "Sort   -> ", v5printany sort
printf "Head   -> %d\n" head
printf "Tail   -> %d\n" tail
printf "PushF  -> ", v5printany pushfront
printf "Take   -> ", v5printany take
printf "Zip    -> ", v5printany zip

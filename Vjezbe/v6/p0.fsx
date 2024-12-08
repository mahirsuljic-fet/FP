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
  | El of 'a * V5L<'a>

let lst = El (8, El(12, El(24, El(4, Kraj))))

// Prvi foldr:
//  [8, 12, 24, 4, []] b = 2
//  x = 8 xs = [12, 24, 4, []] b = 2 
//  nesto = 1
//  Vrati 8
// Drugi foldr:
//  [12, 24, 4, []] b =2
//  x = 12 xs=[24, 4, []] b = 2
//   nesto= 12
//  Vrati 1
// Treci foldr:
//  [24, 4, []] b = 2
//  x=24 xs=[4, []] b=2
//  nesto = 2
//  Vrati 12
// Cetvrti foldr:
//  [4, []] b=2
//  x=4 xs=[] b=2
//  nesto=2
//  rez=2
//  Vrati 2
// Peti foldr:
//  Vrati b=2
let rec foldr (f: ('a -> 'b -> 'b)) (b: 'b) (lst: V5L<'a>) : 'b =
  match lst with
  | Kraj ->
    b
  | El (x, xs) ->
    let nesto = foldr f b xs // nesto = 2
    f x nesto

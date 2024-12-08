// Napisati rekurzivnu strukturu podataka "BinaryTree". Struktura
// treba da bude polimorfna i da sadrzi comparison constraint nad
// tipom. 
// Napisati funkcije push, traverse, foldl i foldr. Elementi treba
// da se dodaju u binarno stablo koristeci operator "<".
// Traverse treba da prolazi kroz binarno stablo i ispisuje elemente
// in-order koristeci prnt funkciju koja treba da bude parametar
// travers-a.
// foldl treba uzme 3 parametra: funkciju koja uzima dva parametra
// pocetnu vrijednost i BinaryTree strukturu.
// Potrebno je pocetnu vrijednost apply-ati funkcijom sa elementom na 
// kranjoj lijevoj strani i rezultat date operacije apply-ati kao
// pocetnu vrijednost stabla sa desne strane   
// foldr treba da radi istu stvar u suprotnom smijeru.

type BT<'a> =
  | End
  | Node of left: BT<'a> * value: 'a * right: BT<'a>

let rec push (v: 'a) (bt: BT<'a>) : BT<'a> =
  match bt with
  | End -> Node(End, v, End)
  | Node (left, tv, right) ->
    if v < tv then
      Node (push v left, tv, right)
    else
      Node (left, tv, push v right)

let rec printTree (bt: BT<'a>) : unit =
  match bt with
  | End ->
    ()
  | Node (left, v, right) ->
    printTree left
    printfn "%A" v
    printTree right

let rec foldl (f: 'a -> 'b -> 'a) (a: 'a) (bt: BT<'b>) : 'a =
  match bt with 
  | End ->
    a
  | Node (left, tv, right) ->
    let nestoLijevo = foldl f a left
    let rez = f nestoLijevo tv
    foldl f rez right 

let rec foldr (f: 'a -> 'b -> 'b) (b: 'b) (bt: BT<'a>) : 'b =
  match bt with
  | End ->
    b
  | Node (left, tv, right) ->
    let nestoDesno = foldr f b right
    let rez = f tv nestoDesno
    foldr f rez left


let stablo = push 5 End |> push 2 |> push 7 |> push 1 |> push 3 |> push 9 |> push 6

stablo |> foldl (/) 100000 |> printfn "%d"
stablo |> foldr (+) 100000 |> printfn "%d"

//                5
//             2      7
//           1  3   6  9
//
// (/) 1000

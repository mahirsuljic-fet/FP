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

type BinaryTree<'a> =
    | End
    | Node of left: BinaryTree<'a> * value: 'a * right: BinaryTree<'a>

let rec push (value: 'a) (tree: BinaryTree<'a>) : BinaryTree<'a> =
    match tree with
    | End -> Node(End, value, End)
    | Node(left, current, right) ->
        if value < current then
            Node(push value left, current, right)
        else
            Node(left, current, push value right)

let rec printTree (tree: BinaryTree<'a>) : unit =
    match tree with
    | End -> ()
    | Node(left, current, right) ->
        printTree left
        printfn "%A" current
        printTree right

let rec foldl (f: ('a -> 'b -> 'a)) (a: 'a) (tree: BinaryTree<'a>) : 'a =
    match tree with
    | End -> a
    | Node(left, current, right) ->
        let fold_left = foldl f a left
        let fold_middle = f fold_left current
        foldl f fold_middle right


let rec foldr (f: ('a -> 'b -> 'a)) (b: 'a) (tree: BinaryTree<'a>) : 'a =
    match tree with
    | End -> b
    | Node(left, current, right) ->
        let fold_right = foldr f b right
        let fold_middle = f current fold_right
        foldr f fold_middle left

let tree = End |> push 5 |> push 2 |> push 9 |> push 7 |> push 1
printTree tree

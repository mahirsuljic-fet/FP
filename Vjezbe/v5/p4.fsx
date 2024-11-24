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
    | BTE of (BinaryTree<'a> * 'a * BinaryTree<'a>) // Binary Tree Element

let rec BTPush (value: 'a) (tree: BinaryTree<'a>) : BinaryTree<'a> =
    match tree with
    | End -> BTE(End, value, End)
    | BTE(_, head, _) when value = head -> tree
    | BTE(left, head, right) when value < head -> BTE(BTPush value left, head, right)
    | BTE(left, head, right) when value > head -> BTE(left, head, BTPush value right)
    | _ -> End

let rec BTTraverse (pf: 'a -> unit) (tree: BinaryTree<'a>) : unit =
    match tree with
    | BTE(left, head, right) ->
        BTTraverse pf left
        pf head
        BTTraverse pf right
    | End -> ()

let rec BTFoldl (f: 'a -> 'b -> 'a) (a: 'a) (tree: BinaryTree<'b>) : 'a =
    match tree with
    | End -> a
    | BTE(left, head, right) ->
        let aLeft = BTFoldl f a left
        let aHead = f aLeft head
        let aRight = BTFoldl f aHead right
        aRight

let rec BTFoldr (f: 'a -> 'b -> 'a) (a: 'a) (tree: BinaryTree<'b>) : 'a =
    match tree with
    | End -> a
    | BTE(left, head, right) ->
        let aRight = BTFoldr f a right
        let aHead = f aRight head
        let aLeft = BTFoldr f aHead left
        aLeft

let (@@) = BTPush

// let tree = BTE(End, 10, End) |> BTPush 5 |> BTPush 15 |> BTPush 2 |> BTPush 8
let intTree = 10 @@ 5 @@ 15 @@ 2 @@ 8 @@ End
let strTree = "d" @@ "c" @@ "e" @@ "a" @@ "b" @@ End

printfn "%A" intTree
BTTraverse (printf "%d ") intTree
printfn ""
printfn "%d" (BTFoldl (+) 0 intTree)
printfn "%d" (BTFoldr (-) 0 intTree)
printfn "%s" (BTFoldl (+) "" strTree)
printfn "%s" (BTFoldr (+) "" strTree)

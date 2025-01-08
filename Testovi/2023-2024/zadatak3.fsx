// generalno, ovako se implementira binarno stablo
type Tree<'a> =
    | Empty
    | Node of Tree<'a> * 'a * Tree<'a>

// funkcija g nemam pojma sta je, pa ne znam ni kakvu funkciju treba implementirati pod c)
// tako da evo nesto proizvoljno, sto daje sljedeci rezultat:
//          7
//         / \
//        /   \
//       2     4
//      / \     \
//     5   6     9
//        /
//       2

let napraviCudnoStablo () : int Tree =
    // stablo pravimo odozdo prema gore
    //          n0
    //         /  \
    //        /    \
    //       n1     n2
    //      /  \     \
    //     n3   n4    n5
    //         /
    //        n6

    let n6 = Node(Empty, 2, Empty)
    let n5 = Node(Empty, 9, Empty)
    let n4 = Node(n6, 6, Empty)
    let n3 = Node(Empty, 5, Empty)
    let n2 = Node(Empty, 4, n5)
    let n1 = Node(n3, 2, n4)
    let n0 = Node(n1, 7, n2)

    n0

// ako bi se trazilo BST (Binary Search Tree),
// njegova full implementacija ima u ~/Random/MyBST/MyBST.fs
// ovdje evo ukratko implementiran BST

type BST<'a> =
    | Empty
    | Node of BST<'a> * 'a * BST<'a>

let addElement (element: 'a) (tree: 'a BST) : 'a BST =
    let rec addEl (tree: 'a BST) : 'a BST =
        match tree with
        | Empty -> Node(Empty, element, Empty)
        | Node(left, current, right) ->
            if element < current then
                Node(addEl left, current, right)
            else
                Node(left, current, addEl right)

    addEl tree

let listToBST (list: 'a list) : 'a BST =
    let foldFun (acumulator: 'a BST) (element: 'a) : 'a BST = addElement element acumulator
    List.fold foldFun Empty list

let list = [ 5; 3; 7; 1; 20; 4; 11 ]
let bst = list |> listToBST

printfn "%A" bst

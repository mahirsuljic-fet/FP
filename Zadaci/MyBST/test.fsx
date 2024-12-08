#load "MyBST.fs"
open MyBST

let bst = 25 @@ 20 @@ 5 @@ 3 @@ 12 @@ 18 @@ 16 @@ 4 @@ 10 @@ End

printfn "%A" bst
printfn "%A" (MyBST.Root bst)
printfn "%A" (MyBST.Min bst)
printfn "%A" (MyBST.Max bst)
printfn "%A" (MyBST.Exists 12 bst)
printfn "%A" (MyBST.Exists 6 bst)

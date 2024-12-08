#load "list.fs"
open MyList

let list1 = 1 @@ 2 @@ 3 @@ End
let list2 = End |@ 3 |@ 2 |@ 1
let listStr = "123" @@ "12345" @@ "12" @@ "1234" @@ End
let bigList = 6 @@ 3 @@ 9 @@ 0 @@ 7 @@ -2 @@ 2 @@ -3 @@ 5 @@ End

printfn "list1 -> %A" list1
printfn "list2 -> %A" list2

(*
printfn "%A" (list1.isEmpty())
printfn "%A" (MyList.isEmpty list2)
printfn "%A" (End.isEmpty())
*)

(*
printfn "%A" (list1.PushFront(0))
printfn "%A" (list2.PushBack(0))
printfn "%A" (0 @@ list1)
printfn "%A" (list2 |@ 0)
*)

(*
printfn "%A" (MyList.Head list1)
printfn "%A" (MyList.tryHead list2)
printfn "%A" (MyList.tryHead End)
printfn "%A" (MyList.Head End)
*)

(*
printfn "%A" (list1.Tail())
printfn "%A" (list2.tryTail ())
printfn "%A" (End.tryTail ())
printfn "%A" (End.Tail())
*)

(*
printfn "%A" (MyList.Foldl (fun x y -> printf "%A " y; x) 1 list1)
printfn "%A" (MyList.Foldr (fun x y -> printf "%A " x; y) 0 list1)
*)

(*
printfn "%A" (MyList.Exists 1 list1)
printfn "%A" (MyList.Exists 0 list1)
*)

(*
printfn "%A" (MyList.Take 0 list1)
printfn "%A" (MyList.Take 1 list1)
printfn "%A" (MyList.Take 2 list1)
printfn "%A" (MyList.Take 3 list1)
printfn "%A" (MyList.Take 4 list1)
*)

(*
printfn "%A" (MyList.ForAll (fun x -> x % 2 = 0) list1)
printfn "%A" (MyList.ForAll (fun x -> x < 4) list1)
*)

(*
printfn "%A" (MyList.Append list1 list2)
printfn "%A" (MyList.Append list1 End)
printfn "%A" (MyList.Append End list2)
printfn "%A" (MyList.Append End End)
*)

(*
printfn "%A" (MyList.Filter (fun x -> x % 2 <> 0) list1)
printfn "%A" (MyList.Filter (fun x -> x <= 2) list2)
*)

(*
printfn "%A" (MyList.Length list1)
printfn "%A" (MyList.Length (0 @@ list1))
printfn "%A" (MyList.Length End)
*)

(*
printfn "%A" (MyList.Map (fun x -> float x * 2.5) list1)
*)

(*
printfn "%A" (MyList.Min list1)
printfn "%A" (MyList.MinBy (fun (x: string) -> x.Length) listStr)
printfn "%A" (MyList.tryMin End)
printfn "%A" (MyList.Min End)
*)

(*
printfn "%A" (MyList.Max list1)
printfn "%A" (MyList.MaxBy (fun (x: string) -> x.Length) listStr)
printfn "%A" (MyList.tryMax End)
printfn "%A" (MyList.Max End)
*)

(*
printfn "%A" (Sort (<) bigList)
printfn "%A" (Sort (>) bigList)
printfn "%A" (Sort (fun (x: string) (y: string) -> x.Length < y.Length) listStr)
printfn "%A" (Sort (fun (x: string) (y: string) -> x.Length > y.Length) listStr)
*)

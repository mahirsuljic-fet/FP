module MyList

[<StructuredFormatDisplay("[ {printString}]")>]
type MyList<'a> =
    | End
    | El of 'a * MyList<'a>

    override this.ToString() =
        let rec buildString (list: MyList<'a>) : string =
            match list with
            | End -> ""
            | El(x, End) -> string x + " "
            | El(x, xs) -> string x + ", " + buildString xs

        buildString this

    member this.printString = this.ToString()

    member this.PushFront(el: 'a) : MyList<'a> = El(el, this)
    static member (@@)(el: 'a, list: MyList<'a>) : MyList<'a> = list.PushFront(el)

    member this.PushBack(el: 'a) : MyList<'a> =
        match this with
        | End -> El(el, End)
        | El(x, xs) -> El(x, xs.PushBack(el))

    static member (|@)(list: MyList<'a>, el: 'a) : MyList<'a> = list.PushBack(el)

    member this.isEmpty() : bool =
        match this with
        | End -> true
        | _ -> false

    member this.tryHead() : option<'a> =
        match this with
        | El(x, _) -> Some x
        | End -> None

    member this.Head() : 'a =
        match this.tryHead () with
        | Some x -> x
        | None -> failwith "List is empty"

    member this.tryTail() : option<'a> =
        match this with
        | El(x, End) -> Some x
        | El(x, xs) -> Some(xs.Tail())
        | End -> None

    member this.Tail() : 'a =
        match this.tryTail () with
        | Some x -> x
        | None -> failwith "List is empty"

let isEmpty (list: MyList<'a>) : bool = list.isEmpty ()
let tryHead (list: MyList<'a>) : option<'a> = list.tryHead ()
let Head (list: MyList<'a>) : 'a = list.Head()
let tryTail (list: MyList<'a>) : option<'a> = list.tryTail ()
let Tail (list: MyList<'a>) : 'a = list.Tail()

let rec Foldl (f: 'a -> 'b -> 'a) (a: 'a) (list: MyList<'b>) : 'a =
    match list with
    | End -> a
    | El(x, xs) -> Foldl f (f a x) xs

let rec Foldr (f: 'a -> 'b -> 'b) (a: 'b) (list: MyList<'a>) : 'b =
    match list with
    | End -> a
    | El(x, xs) -> f x (Foldr f a xs)

let rec Exists (el: 'a) (list: MyList<'a>) : bool =
    match list with
    | End -> false
    | El(x, _) when x = el -> true
    | El(_, xs) -> Exists el xs

let rec tryTake (n: int) (list: MyList<'a>) : option<MyList<'a>> =
    match list with
    | _ when n = 0 -> Some End
    | _ when n < 0 -> None
    | End -> None
    | El(x, xs) ->
        let other = tryTake (n - 1) xs

        match other with
        | None -> None
        | Some other_list -> Some(El(x, other_list))

let rec Take (n: int) (list: MyList<'a>) : MyList<'a> =
    match tryTake n list with
    | Some l -> l
    | None -> failwith "List does not have that many elements"

let ForAll (f: 'a -> bool) (list: MyList<'a>) : bool =
    let foldFun (acc: bool) (list_el: 'a) = acc && f list_el
    Foldl foldFun true list

let rec Append (listL: MyList<'a>) (listR: MyList<'a>) : MyList<'a> =
    match listL with
    | End -> listR
    | El(x, xs) -> El(x, Append xs listR)

let rec Filter (f: 'a -> bool) (list: MyList<'a>) : MyList<'a> =
    match list with
    | End -> End
    | El(x, xs) when f x -> El(x, Filter f xs)
    | El(_, xs) -> Filter f xs

let rec Length (list: MyList<'a>) : int =
    match list with
    | End -> 0
    | El(_, xs) -> 1 + Length xs

let rec Map (f: 'a -> 'b) (list: MyList<'a>) : MyList<'b> =
    match list with
    | End -> End
    | El(x, xs) -> El(f x, Map f xs)


let rec tryCompareBy (comp: 'b -> 'b -> bool) (f: 'a -> 'b) (list: MyList<'a>) : option<'a> =
    match list with
    | End -> None
    | El(x, xs) ->
        let min_xs = tryCompareBy comp f xs

        match min_xs with
        | None -> Some x
        | Some min_xs when comp (f x) (f min_xs) -> Some x
        | Some min_xs -> Some min_xs

let CompareBy (comp: 'b -> 'b -> bool) (f: 'a -> 'b) (list: MyList<'a>) : 'a =
    match tryCompareBy comp f list with
    | Some x -> x
    | None -> failwith "List can not be empty"

let tryMinBy (f: 'a -> 'b) (list: MyList<'a>) : option<'a> = tryCompareBy (<) f list
let MinBy (f: 'a -> 'b) (list: MyList<'a>) : 'a = CompareBy (<) f list
let tryMin (list: MyList<'a>) : option<'a> = tryMinBy (fun x -> x) list
let Min (list: MyList<'a>) : 'a = MinBy (fun x -> x) list
let tryMaxBy (f: 'a -> 'b) (list: MyList<'a>) : option<'a> = tryCompareBy (>) f list
let MaxBy (f: 'a -> 'b) (list: MyList<'a>) : 'a = CompareBy (>) f list
let tryMax (list: MyList<'a>) : option<'a> = tryMaxBy (fun x -> x) list
let Max (list: MyList<'a>) : 'a = MaxBy (fun x -> x) list

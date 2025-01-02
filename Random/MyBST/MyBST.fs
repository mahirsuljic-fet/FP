module MyBST

[<StructuredFormatDisplay("< {printString}>")>]
type MyBST<'a when 'a: comparison> =
    | End
    | El of MyBST<'a> * 'a * MyBST<'a>

    override this.ToString() =
        let rec buildString (bst: MyBST<'a>) : string =
            match bst with
            | End -> ""
            | El(End, middle, End) -> string middle + " "
            | El(left, middle, right) -> buildString left + buildString (El(End, middle, End)) + string right

        buildString this

    member this.printString = this.ToString()

    member this.Push(value: 'a) : MyBST<'a> =
        match this with
        | End -> El(End, value, End)
        | El(left, current, right) ->
            if value < current then
                El(left.Push value, current, right)
            else if value > current then
                El(left, current, right.Push value)
            else // dodavanje "duplikata" u ovaj BST je nop operacija
                this

    static member (@@)(value: 'a, bst: MyBST<'a>) : MyBST<'a> = bst.Push value

let rec tryRoot (bst: MyBST<'a>) : option<'a> =
    match bst with
    | End -> None
    | El(_, value, _) -> Some value

let rec Root (bst: MyBST<'a>) : 'a =
    match tryRoot bst with
    | Some x -> x
    | None -> failwith "BST is empty"

let rec tryMin (bst: MyBST<'a>) : option<'a> =
    match bst with
    | End -> None
    | El(End, value, _) -> Some value
    | El(left, _, _) -> tryMin left

let Min (bst: MyBST<'a>) : 'a =
    match tryMin bst with
    | Some x -> x
    | None -> failwith "BST is empty"

let rec tryMax (bst: MyBST<'a>) : option<'a> =
    match bst with
    | End -> None
    | El(_, value, End) -> Some value
    | El(_, _, right) -> tryMax right

let Max (bst: MyBST<'a>) : 'a =
    match tryMax bst with
    | Some x -> x
    | None -> failwith "BST is empty"

let rec Exists (value: 'a) (bst: MyBST<'a>) : bool =
    match bst with
    | End -> false
    | El(left, middle, right) ->
        if middle = value then
            true
        else
            Exists value left || Exists value right

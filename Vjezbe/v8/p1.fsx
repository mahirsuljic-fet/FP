// Dodati staticku map funkciju u Maybe tip.
// Napisati podrsku za computation expression maybe.

type Maybe<'a> =
    | Nothing
    | Something of 'a

    static member map (f: 'a -> 'b) (myb: Maybe<'a>) : Maybe<'b> =
        match myb with
        | Something(x) -> Something(f x)
        | Nothing -> Nothing

type MaybeBuilder() =
    member this.Bind(mtip: Maybe<'a>, foo: 'a -> Maybe<'a>) : Maybe<'a> =
        match mtip with
        | Something m -> foo m
        | Nothing -> Nothing

    member this.Return(foo: 'a) : Maybe<'a> = Something foo


let maybe = MaybeBuilder()


// "rucno"
let sum1 ao bo co =
    match ao with
    | Something(a) ->
        match bo with
        | Something(b) ->
            match co with
            | Something(c) -> a + b + c |> Something
            | Nothing -> Nothing
        | Nothing -> Nothing
    | Nothing -> Nothing


// malo "ljepse"
let sum2 ao bo co : Maybe<int> = 
  maybe.Bind(ao, fun a -> 
    maybe.Bind(bo, fun b -> 
      maybe.Bind(co, fun c ->
        a + b + c |> Something)))

  
// computational expression
let sum3 ao bo co : Maybe<int> = 
  maybe {
    let! a = ao
    let! b = bo
    let! c = co
    return a + b + c
  }


let ao = Something 1
let bo = Something 2
let co = Something 3

sum1 ao bo co |> Maybe.map (fun x -> printfn "%d" x)
sum2 ao bo co |> Maybe.map (fun x -> printfn "%d" x)
sum3 ao bo co |> Maybe.map (fun x -> printfn "%d" x)

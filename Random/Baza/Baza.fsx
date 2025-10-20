type Baza =
    { ime: string; staz: int; info: string }

let lista =
    [ { ime = "Baza1"
        staz = 1
        info = "prva" }
      { ime = "Baza2"
        staz = 2
        info = "druga" }
      { ime = "Baza3"
        staz = 3
        info = "treca" }
      { ime = "Baza4"
        staz = 4
        info = "cetvrta" }
      { ime = "Baza5"
        staz = 5
        info = "peta" } ]

let trySearchName (name: string) (lista : Baza list) : Baza option =
  let rec search (list: Baza list) =
    match list with
    | [] -> None
    | x :: _ when x.ime = name -> Some x
    | _ :: xs -> search xs

  search lista

let searchName (name: string) (lista : Baza list) : Baza =
  let result = trySearchName name lista
  match result with
  | Some x -> x
  | None -> raise(System.NotSupportedException "Name not found")

printfn "%A" (searchName "Baza1" lista)

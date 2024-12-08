// Napisati fju divide koja uzima fju (t->bool) i listu, a vraca
// nazad tuple sa dvije liste. Prva sadrzi elemente za koje je
// predicate vratio true.

let lista = [1;2;3;4;5;6;7;8;9]

let divide (predicate: ('a -> bool)) (lst: List<'a>) :
  List<'a>*List<'a> =

    let rec foo (lijeva: List<'a>) (desna: List<'a>) (l: List<'a>) : List<'a>*List<'a> =
      match l with
      | [] ->
        (lijeva, desna)
      | x :: xs ->
        if predicate x then
          foo (x :: lijeva) desna xs
        else 
          foo lijeva (x::desna) xs
    foo [] [] lst

let firstVsLast (lst: List<'a>) : bool =
  match lst with
  | [] -> failwith "Nema prvog"
  | prvi :: xs ->
    let rec getZadnji (l: List<'a>) : 'a =
      match l with
      | [] -> failwith "Nema zadnjeg"
      | zadnji :: [] -> zadnji
      | x :: xs -> getZadnji xs
    let zadnji = getZadnji xs
    prvi < zadnji


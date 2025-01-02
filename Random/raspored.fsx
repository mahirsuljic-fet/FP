type Predmet =
    | FP
    | OS
    | BP

type VrstaCasa =
    | Pred
    | AV
    | LV

type Dan =
    | Pon
    | Uto
    | Sri
    | Cet
    | Pet

type Cas =
    { pred: Predmet
      vrsta: VrstaCasa
      dan: Dan }

let raspored: List<Cas> =
    [ { pred = FP; vrsta = AV; dan = Pon }
      { pred = FP; vrsta = LV; dan = Pon }
      { pred = FP; vrsta = Pred; dan = Cet }
      { pred = OS; vrsta = AV; dan = Sri }
      { pred = OS; vrsta = LV; dan = Sri }
      { pred = OS; vrsta = Pred; dan = Sri }
      { pred = BP; vrsta = LV; dan = Uto }
      { pred = BP; vrsta = AV; dan = Cet }
      { pred = BP; vrsta = Pred; dan = Cet } ]

let getDan (n: int) : option<Dan> =
    match n with
    | 1 -> Some Pon
    | 2 -> Some Uto
    | 3 -> Some Sri
    | 4 -> Some Cet
    | 5 -> Some Pet
    | _ -> None

let formatCas (cas: Cas) : string = sprintf $"{cas.pred} ({cas.vrsta})"

let printRaspored (dan: Dan) : unit =
    let casovi =
        raspored |> List.filter (fun { pred = _; vrsta = _; dan = d } -> d = dan)

    match casovi with
    | [] -> printfn "Slobodan dan :)"
    | casovi -> casovi |> List.map formatCas |> List.iter (printfn "%s")

printf
    "RASPORED\n\
    1 - Ponedjeljak\n\
    2 - Utorak\n\
    3 - Srijeda\n\
    4 - Četvrtak\n\
    5 - Petak\n\
    Unos: "

let input =
    match System.Console.ReadLine() |> int with
    | n when n >= 1 && n <= 5 -> n
    | _ -> 0

match getDan input with
| None -> printfn "Unos nije validan"
| Some dan -> printRaspored dan

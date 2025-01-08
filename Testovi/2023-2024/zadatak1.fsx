// Postavka zadatka iz notes nije bas jasna, ne kontam tacno sta treba,
// ali evo otprilike kako bi se rijesio zadatak

// izvinjavam se unaprije jer mijesam engleski i bosanski u implementacijama :)

// telefon i mail su alias za string
// ne znam na sta se misli u notes, pa evo ovako nek bude
type telefon = string
type mail = string

type Baza =
    { ime: string
      staz: int
      info: telefon * mail }



////////
// a) //
////////

// nacin 1
let lista1 =
    [ { ime = "baza1"
        staz = 1
        info = ("060111111", "baza1@mail.com") }
      // ako se elementi liste navode u odvojenim redovime ne moraju se odvajati sa ;
      { ime = "baza2"
        staz = 2
        info = "060222222", "baza2@mail.com" } // moze a i ne mora zagrada oko tuple-a
      { ime = "baza3"
        staz = 3
        info = "060333333", "baza3@mail.com" } // primjer tuple bez zagrade
      { ime = "baza4"
        staz = 4
        info = "060444444", "baza4@mail.com" }
      { ime = "baza5"
        staz = 5
        info = "060555555", "baza5@mail.com" } ]


// nacin 2
let lista2 =
    { ime = "baza1"
      staz = 1
      info = "060111111", "baza1@mail.com" }
    :: { ime = "baza2"
         staz = 2
         info = "060222222", "baza2@mail.com" }
    :: { ime = "baza3"
         staz = 3
         info = "060333333", "baza3@mail.com" }
    :: { ime = "baza4"
         staz = 4
         info = "060444444", "baza4@mail.com" }
    :: { ime = "baza5"
         staz = 5
         info = "060555555", "baza5@mail.com" }
    :: [] // obavezno kao zadnji element dodati praznu listu []


// nacin 3 i 4
// ako se u jednoj liniji definise record, polja treba odvojiti sa ;
let baza1 =
    { ime = "baza1"
      staz = 1
      info = ("060111111", "baza1@mail.com") }

// zagrada oko tuple-a moze ali ne mora
let baza2 =
    { ime = "baza2"
      staz = 2
      info = "060222222", "baza2@mail.com" }

// ako se polja recorda definisu u zasebnim redovima nije potrebno ;
let baza3 = // pri cemu nije odzvoljeno definisati polje ovdje
    { ime = "baza3"
      staz = 3
      info = "060333333", "baza3@mail.com" }

let baza4 =
    { // pozicija {} nije toliko bitna
      ime = "baza4"
      staz = 4
      info = "060444444", "baza4@mail.com" }

let baza5 =
    // ovo je takodjer dozvoljeno, ali proklinjat ce vas ko god procita ovaj kod
    { ime = "baza5"
      staz = 5 // bitni su ;
      info = "060555555", "baza5@mail.com" }

// nacin 3 (kao nacin 1)
let lista3 = [ baza1; baza2; baza3; baza4; baza5 ]

// nacin 4 (kao nacin 2)
let lista4 = baza1 :: baza2 :: baza3 :: baza4 :: baza5 :: []



////////
// b) //
////////

// cista funkcija je funkcija koja za neki input uvijek proizvodi isti output
// pri cemu nema nikakvih popratnih efekata

// potpuna funkcija je funkcija koja za svaki output moze proizvesti output (nema iznimki)


// funkcija za testiranje search funkcija, ovo ignorisati
// i da, parametri funkcije se mogu odvojiti u nove redove
let testSearch (searchFunction: string -> Baza list -> Baza option) (ime: string) (lista: Baza list) =
    let result = lista |> searchFunction ime

    match result with
    | Some x -> printfn "Pronadjen element sa imenom %s" x.ime
    | None -> printfn "Nije pronadjen element sa imenom %s" ime


// pretpostavljam da je profesor htio da se funkcija implementira
// cisto funkcionalnim pristupom (bez mutacija), sto cini funkciju cistom
// medjutim, takodjer pretpostavljam da je htio da se implementira tako da vraca
// option, a ne "cisti" tip, sto bi je cinilo i potpunom
// ovo kazem jer, u slucaju da ne vracamo option, onda bi morali vratiti
// "prazan" Base record (sva polja na "default" vrijednostima, string-ovi na "", int-ovi na 0)
// ili da bacimo iznimku, u kojem slucaju ne mozemo bas rec da je ta funkcija 100% cista,
// ako podrazumijevano iznimku kao popratni efekat
//
// u svakom slucaju, eto sta je moguce, najbolje pitat profesora/asistenta u toku testa


// ako bi nam dopustili da koristimo funkcije iz modula List,
// tada mozemo koristiti funkciju List.tryFind
// zasto tryFind, a ne find?
// problem sa find je sto ona baza iznimku u slucaju da trazeni element ne postoji
// funkcija tryFind vraca tip option, pa ako ne nadje element vraca None

// Baza list   je isto sto i list<Baza>
// Baza option je isto sto i option<Baza>
let trySearchList (ime: string) (lista: Baza list) : Baza option =
    let predicate (element: Baza) : bool = element.ime = ime
    List.tryFind predicate lista


// nacin 1 - rekurzivno i vracamo option
let trySearchRecursive (ime: string) (lista: Baza list) : Baza option =
    // mozemo iskoristiti closure umjesto da napravimo jos jedan parametar za ime koje pretrazujemo
    let rec searchList (currentList: Baza list) =
        match currentList with
        // ako je lista [] to znaci da je lista
        // proslijedjena u trySearchRecursive prazna
        // ili da smo dosli do kraja iste
        | [] -> None
        // provjeravamo da li je polje ime trenutnog elementa Baza
        // isto kao i ime argument proslijedjen u trySearchRecursive
        // ako jest onda vracamo x
        // u ovom slucaju ostatak liste nas ne zanima,
        // jer ako se ovo izvrsi to znaci da smo nasli trazeni element
        | x :: _ when x.ime = ime -> Some x
        // ako prosli uslov nije prosao, znaci da nismo nasli trazeni element
        // pa pretrazujemo ostatak liste
        // u ovom slucaju nas treutni element ne zanima jer znamo da nije trazeni
        | _ :: xs -> searchList xs

    searchList lista


// nacin 2 - koristeci fold, vracamo option
let trySearchFold (ime: string) (lista: Baza list) : Baza option =
    let foldFunc (acumulator: Baza option) (rhs: Baza) =
        match acumulator with
        | Some _ -> acumulator
        | None -> if rhs.ime = ime then Some rhs else None


    let initialAcumulator = None

    List.fold foldFunc initialAcumulator lista


testSearch trySearchList "baza1" lista1
testSearch trySearchList "baza5" lista1
testSearch trySearchList "baza0" lista1

testSearch trySearchRecursive "baza1" lista1
testSearch trySearchRecursive "baza5" lista1
testSearch trySearchRecursive "baza0" lista1

testSearch trySearchFold "baza1" lista1
testSearch trySearchFold "baza5" lista1
testSearch trySearchFold "baza0" lista1



////////
// c) //
////////

// ako bi nam dozvolili da koristimo funkcije iz modula List
// onda se ovo moze uraditi sa funkcijom List.map na sljedeci nacin

let removeTelephoneList (ime: string) (lista: Baza list) : Baza list =
    let mapFunc (element: Baza) : Baza =
        if element.ime = ime then
            { element with
                info = "", (element.info |> snd) } // snd vraca drugi element tuple-a
        else
            element

    lista |> List.map mapFunc


// u suprotnom, moze se rijesiti pomocu funkcije foldBack (foldr) ili rekurzivno


// nacin 1 - koristeci fold
let removeTelephoneFold (ime: string) (lista: Baza list) : Baza list =
    let foldFun (element: Baza) (acumulator: Baza list) : Baza list =
        let newElement =
            if element.ime = ime then
                { element with
                    info = "", (element.info |> snd) }
            else
                element

        newElement :: acumulator // zbog ovog koristimo foldBack sto je isto sto i foldr

    let initialAcumulator: Baza list = []

    List.foldBack foldFun lista initialAcumulator

// nacin 2 - koristeci rekurziju
let removeTelephoneRecursive (ime: string) (lista: Baza list) : Baza list =
    let rec removeFromList (currentList: Baza list) =
        match currentList with
        | [] -> []
        | element :: ostatakListe ->
            let noviElement =
                if element.ime = ime then
                    { element with
                        info = "", (element.info |> snd) } // snd vraca drugi element tuple-a
                else
                    element

            noviElement :: removeFromList ostatakListe

    removeFromList lista



let testRemoveTelephone (removeFunction: string -> Baza list -> Baza list) (ime: string) (lista: Baza list) : unit =
    printfn "\nBrisanje telefona elementa sa imenom %s" ime
    printfn "%A" (lista |> removeFunction ime)


testRemoveTelephone removeTelephoneList "baza1" lista1
testRemoveTelephone removeTelephoneList "baza5" lista1
testRemoveTelephone removeTelephoneList "baza0" lista1

testRemoveTelephone removeTelephoneFold "baza1" lista1
testRemoveTelephone removeTelephoneFold "baza5" lista1
testRemoveTelephone removeTelephoneFold "baza0" lista1

testRemoveTelephone removeTelephoneRecursive "baza1" lista1
testRemoveTelephone removeTelephoneRecursive "baza5" lista1
testRemoveTelephone removeTelephoneRecursive "baza0" lista1

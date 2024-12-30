// Napisati program koji trazi od korisnika unos stringova
// Nakon unesenih svih stringova (korisnik unio Ctrl D),
// program na ekran ispisuje svaki string te informaciju
// da li je dati string palindrom ili ne. Ispis treba biti
// odgodjen za 2s za svaki string.

let rec readInput () : string list =
    let line = System.Console.ReadLine()
    if line = null then [] else line :: readInput ()

readInput ()
|> Seq.map (fun x -> x.ToUpper())
|> Seq.map (fun x ->
    let rev: string = Seq.rev x |> Seq.fold (fun s c -> s + (string c)) ""

    if rev = x then x, "Palindrom" else x, "Nije palindrom")
|> Seq.map (fun (word, isPalindrome) ->
    async {
        do!
            printfn "%s -> %s" word isPalindrome
            Async.Sleep 2000
    })
|> Async.Sequential
|> Async.RunSynchronously

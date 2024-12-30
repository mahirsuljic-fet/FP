// Napisati funkciju readInput koja ce citati input sa
// standardnog ulaza. Svaki puta kada korisnik unese rijec
// na ekranu se treba ispisati da li je unesena rijec
// palindrom. Nakon ispisa, program treba da ceka na unos
// nove rijeci. Kada korisnik unese EOF, program terminira
// bez ispisa. Prilozeni kod nije dozvoljeni mijenjati.

let readInput () : string seq =
    seq {
        let mutable input = System.Console.ReadLine()

        while input <> null do

            yield input
            input <- System.Console.ReadLine()
    }

readInput ()
|> Seq.map (fun x -> x.ToUpper())
|> Seq.iter (fun x ->
    let rev: string = Seq.rev x |> Seq.fold (fun s c -> s + (string c)) ""

    if rev = x then
        printfn "Palindrom"
    else
        printfn "Nije palindrom")

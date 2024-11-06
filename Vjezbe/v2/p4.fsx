// Funkcija loadMeasurements učitava mjerenja operacija u nanosekundama 
// sa standardnog ulaza. Potrebno je na ekran ispisati koliko je trajalo 
// najbržih 5 operacija u mikrosekundama.

// Funkcije koje je potrebno koristiti:
// Map.toList; List.map; snd; nanoSecondsToMicroSeconds(napisati ovu fju);
// List.take; printfn "%A" list

let loadMeasurements () : Map<string, float> =
    let rec loadMeasurements_ (ms : Map<string, float>) : Map<string, float> =
        let line = System.Console.ReadLine ()
        match line with
        | null -> ms
        | _ ->
            let rArg = 
                match line.Split('|') with
                | [|"I"; uid; startTime|] ->
                    (uid, float startTime) |> ms.Add
                | [|"O"; uid; endTime|] ->
                    match ms.TryFind uid with
                    | Some startTime ->
                        (uid, float endTime - startTime) |> ms.Add
                    | None ->
                        ms
                | _ -> ms
            loadMeasurements_ rArg
    loadMeasurements_ Map.empty

let nanoSecondsToMicroSeconds (x : float) : float = x / 1000.0

let measurements = loadMeasurements()
printfn "%A" measurements
// dotnet fsi p4.fsx < <(cat measures.log)

let result = measurements |> Map.toList |> List.map snd |> List.map nanoSecondsToMicroSeconds |> List.sort |> List.take 5
printfn "%A" result

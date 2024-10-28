// Konvertovati listu stringova datu ispod u jedan string koristeći sljedeća pravila:
// 1. Konačan string treba da ima sva velika slova (koristiti funkciju uppercase)
// 2. Konačan string treba da ima samo stringove veće od 5 karaktera (List.filter)
// Za spajanje stringova koristiti String.concat koji uzima separator kao prvi argument
// te listu stringova za spajanje kao drugi argument.

let phrases = ["Zdravo"; "F#"; "Pipe"; "Operator"; "Primjer"; "Lab"; "Vježbe"]

let rec uppercase (str : string) =
    match str.Length with
    | 0 -> ""
    | _ -> (System.Char.ToUpper str[0]).ToString () + uppercase str[1..]

let result = phrases |> List.map uppercase |> List.filter (fun str -> String.length str > 5) |> String.concat " "

printfn "Processed Phrases: %s" result

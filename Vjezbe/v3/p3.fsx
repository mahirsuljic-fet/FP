// Zasto se kod ispod ne kompajlira?

let people = [("Ena", 20); ("Sara", 25); ("Denis", 30); ("Mak", 35)]
let format name age = sprintf $"{name} ima {age} godina."

// let formattedPeople = people |> 
//                       List.map ( fun x -> x |> format )

let formattedPeople = people |> 
                      List.map ( fun x -> x ||> format ) // x je tuple, pa se mora koristiti ||>, a ne |>

formattedPeople |> List.iter (printfn "%s")

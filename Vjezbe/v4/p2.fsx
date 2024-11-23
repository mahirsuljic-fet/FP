// Napisati funkciju koja racuna cijenu potrosene elektricne energije.
// Cijena po kWh se racuna na osnovu kolicine potrosene energije
// Postoje 4 opsega potrosnje:
// Opseg A: 0 - 100 kWh - 0.1 KM po kWh
// Opseg B: 101 - 200 kWh - 0.15 po KWh
// Opseg C: 201 - 500 kWh - 0.2 po KWh
// Opseg D: preko 500 kWh - 0.25 po KWh
// Dodatno za preko 300 kWh potrosnje dodati 5 KM na ukupnu cijenu

// let cijena potrosnja =
//   if potrosnja < 0.0 then
//     0.0
//   else if potrosnja < 100.0 then
//     potrosnja * 0.1
//   else if potrosnja < 200.0 then
//     potrosnja * 0.15
//   else if potrosnja < 500.0 then
//     if potrosnja < 300.0 then
//       potrosnja * 0.2
//     else
//       potrosnja * 0.2 + 5.0
//   else
//     potrosnja * 0.25 + 5.0

let cijena potrosnja =
  match potrosnja with
  | x when x >= 0.0 && x < 100.0 -> x * 0.1
  | x when x < 200.0 -> x * 0.15
  | x when x < 300.0 -> x * 0.2
  | x when x < 500.0 -> x * 0.2 + 5.0
  | _ -> potrosnja * 0.25 + 5.0

let input = System.Console.ReadLine() |> float
printfn "%f" (cijena input)

// Napisati program koji definira tip Student koji sadrzi:
// ime, prezime, godine i najdrazi_predmet
// Program treba da instancira 3 studenta te ih ispise na ekran
// ime studenta te najdrazi predmet koristeci funkciju "ispisiStudenta"
// Funkcija treba da ispise studenta na sljedeci nacin:
//
// |Student|Godine|Najdrazi predmet|?|
// |Meho|Funkcionalno programiranje|:)|
// ili
// |Student|Godine|Najdrazi predmet|?|
// |Ivica|Objektno orijentirano programiranje|:(|

type Student =
    { ime: string
      prezime: string
      godine: int
      najdrazi_predmet: string }

let stud =
    { ime = "Mahir"
      prezime = "Suljic"
      godine = 20
      najdrazi_predmet = "Funkcionalno programiranje" }

// let foo (st: Student) : unit =
//   let emoji = if st.najdrazi_predmet.ToLower() = "funkcionalno programiranje" then ":)" else ":("
//   printfn $"|{st.ime}|{st.najdrazi_predmet}|{emoji}"

let foo ({ime=ime: string; najdrazi_predmet=predmet: string} : Student) : unit =
  let emoji = if predmet.ToLower() = "funkcionalno programiranje" then ":)" else ":("
  printfn $"|{ime}|{predmet}|{emoji}"

foo stud

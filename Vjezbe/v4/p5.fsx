// Napisati program koji učitava podatke o vremenu u New Yorku iz 2019-e godine.
// Podaci se nalaze u file-u "nyc_temperature.csv". Svaka linija file-a je data
// u sljedećem formatu:
//
//date,tmax,tmin,tavg,departure,HDD,CDD,precipitation,new_snow,snow_depth
// date - datum mjerenja
// tmax - maksimalna zabiljezena temperatura u Fahrenheit-ima
// tmin - minimalna zabiljezena temperatura u Fahrenheit-ima
// tavg - prosjecna temperatura u Fahrenheit-ima
// departure - temperaturno odstupanje u odnosu na historijski prosjek
// HDD (Heating Degree Day) - mjera koja se koristi za estimiranje potrebne energije
//                              za zagrijavanje objekata
// CDD (Cooling Degree Day) - mjera koja se koristi za estimiranje potrebne energije
//                              za hladjenje objekata
// precipitation - kolicina padavina u incima
// new_snow - kolicina snijeznih padavina u incima
// snow_depth - dubina snijega u incima
//
// Izgraditi sljedeće strukture podataka na osnovu podataka iz file-a:
//    - Datum
//    - Padavine (Kolicina, Zanemarivo, Nikako)
//    - TempRecord - sadrzi informacije date u jednoj liniji mjerenja
//
// Na osnovu podataka iz file-a neophodno je izracunati:
//    - Kog dana je zabiljezena najvisa temperatura i njena vrijednost
//    - Kog dana je zabiljezena najmanja temperatura i njena vrijednost
//    - Prosjecna temperatura u toku godine
//    - Prosjecno temperaturno odstupanje u toku godine
//    - Kog dana je zabiljezeno najvece (apsolutno) temperaturno odstupanje i njena vrijednost
//    - Kog dana je palo najvise snijega i kolicina u cm
//    - Kog dana je zabiljezena najveca dubina snijega u cm
//    - U kom mjesecu je zabiljezena najvisa prosjecna temperatura i njena vrijednost
//    - U kom mjesecu je zabiljezena najniza prosjecna temperatura i njena vrijednost
//    - U kom mjesecu je estimiran najvisi prosjek neophodne energije
//    - Kog dana je zabiljezena najveca temperaturna razlika
//
// Podatke prikazivati u metrickim jedinicama, a temperaturu u stepenima Celzija.


// Kod sam pisao sa ciljem da sto vise koristim record-e, tuple-e, match, fold, rekurziju, itd.
// Htio sam da na sto vise nacina pokusam iskoristiti pomenute koncepte
// Zadatak se moze puno ljepse uraditi koristenjem ekstenzija tipa List kao sto su maxBy, minBy i slicno
// Dakle, kod nije bas citljiv, ali nadam se da ce nekome pomoc bar malo


// TIPOVI
type Datum = { dan: int; mjesec: int; godina: int }

type Padavine =
    | Kolicina of float
    | Zanemarivo
    | Nikako

type TempRecord =
    { datum: Datum
      tmax: float
      tmin: float
      tavg: float
      departure: float
      hdd: float
      cdd: float
      padavine: Padavine
      noviSnijeg: Padavine
      dubinaSnijega: Padavine }


// POMOCNE FUNKCIJE (konverzija i formatiranje)
let imeMjeseca (brMjeseca: int) : string =
    match brMjeseca with
    | 1 -> "Januar"
    | 2 -> "Februar"
    | 3 -> "Mart"
    | 4 -> "April"
    | 5 -> "Maj"
    | 6 -> "Juni"
    | 7 -> "Juli"
    | 8 -> "August"
    | 9 -> "Septembar"
    | 10 -> "Oktobar"
    | 11 -> "Novembar"
    | 12 -> "Decembar"
    | _ -> "???"

let toCelsius (temp: float) : float = (temp - 32.0) / 1.8

let toCm (len: float) : float = len * 2.54

let stringToDatum (str: string) : Datum =
    match str.Split(separator = '/') with
    | [| danStr: string; mjStr: string; godStr: string |] ->
        { dan = int danStr
          mjesec = int mjStr
          godina = int godStr }
    | _ -> failwith "Nevalidan format datuma"

let stringToPadavine (str: string) : Padavine =
    match str with
    | "T" -> Zanemarivo
    | "0" -> Nikako
    | n when float n > 0 -> Kolicina(toCm (float n))
    | _ -> failwith "Nevalidne padavine"

let formatDatum (datum: Datum) : string =
    sprintf $"{datum.dan}. {datum.mjesec |> imeMjeseca} 20{datum.godina}."

let formatPadavine padavine =
    match padavine with
    | Kolicina n -> sprintf "%.2f" n + "cm"
    | Zanemarivo -> "zanemarivo"
    | Nikako -> "nikako"

let formatRecord (temp: TempRecord) : string =
    match temp with
    | { datum = datum
        tmax = tmax
        tmin = tmin
        tavg = tavg
        departure = departure
        hdd = hdd
        cdd = cdd
        padavine = padavine
        noviSnijeg = noviSnijeg
        dubinaSnijega = dubinaSnijega } ->
        let maxTemp = sprintf "%.2f" tmax
        let minTemp = sprintf "%.2f" tmin
        let avgTemp = sprintf "%.2f" tavg

        let perc = formatPadavine padavine
        let newSnow = formatPadavine noviSnijeg
        let snowDepth = formatPadavine dubinaSnijega

        $"\
	Date:       {datum.dan}. {imeMjeseca datum.mjesec} 20{datum.godina}.\n\
	Max temp:   {maxTemp}°C\n\
	Min temp:   {minTemp}°C\n\
	Avg temp:   {avgTemp}°C\n\
	Departure:  {departure}\n\
	HDD:        {hdd}\n\
	CDD:        {cdd}\n\
	Perc:       {perc}\n\
	New snow:   {newSnow}\n\
	Snow depth: {snowDepth}\n"


// FUNKCIJA ZA PARSIRANJE PODATAKA IZ FAJLA nyc_temperature.csv
let ucitajPodatke () : list<TempRecord> =
    let _ = System.Console.ReadLine() // odbacujemo header

    let rec processLine () : list<TempRecord> =
        let line: string = System.Console.ReadLine()

        if line = null then
            []
        else
            match line.Split(separator = ',') with
            | [| date: string
                 tmax: string
                 tmin: string
                 tavg: string
                 departure: string
                 hdd: string
                 cdd: string
                 precipitation: string
                 new_snow: string
                 snow_depth: string |] ->
                let tempRecord =
                    { datum = stringToDatum date
                      tmax = toCelsius (float tmax)
                      tmin = toCelsius (float tmin)
                      tavg = toCelsius (float tavg)
                      departure = float departure
                      hdd = float hdd
                      cdd = float cdd
                      padavine = stringToPadavine precipitation
                      noviSnijeg = stringToPadavine new_snow
                      dubinaSnijega = stringToPadavine snow_depth }

                tempRecord :: (processLine ())
            | _ -> []

    processLine ()


// Lista sa parsiranim podacima pohranjenim u TempRecord tip
let podaci: List<TempRecord> = ucitajPodatke ()



// ZADACI
//
// nisam imao puno inspiracije za imena varijabli
// provjera je sutra, nenaspavan sam
// ako je kod tezak za citat izvinjavam se
//
// rjesenja nisam puno provjeravao, ali imaju logike
// ako neko nadje neku gresku neka mi javi

// foldFun je koristeno kao ime za funkciju koja ce se koristiti u List.fold
// soFar  - koristeno u imenima varijabli koje "zapamte" najvecu/najmanju vrijednost do sada (so far)
// Record - koristeno u imenima varijabli da se naglasi da varijabla "cuva" neki record
// Month - mjesec, koristeno u imenima varijabli koje oznacavaju redni broj nekog mjeseca
// listElement - jedan clan iz liste koja je koristena u List.fold, onaj do kojeg smo dosli
// lista[n] - je zapravo ekstenzija [] na tipu List (ekvivalentno je sa lista.[n]) i radi kao c operator [] na nizu
// impl - unutarnja implementacija vanjske funkcije, koristeno da se izbjegne potreba za prosljedjivanjem argumenata vanjskoj funkciji
// current - trenutno, koristeno u imenima varijabli koje se odnose na nesto sto se trenutno analizira
// Temp - temperatura, koristeno u imenima varijabli
// avg - average, prosjek, koristeno u imenima varijabli
// min - minimum, najmanje, koristeno u imenima varijabli
// max - maximum, najvece, koristeno u imenima varijabli


// Kog dana je zabiljezena najvisa temperatura i njena vrijednost
let (maxTempDatum, maxTemp) =
    let foldFun (maxTempRecordSoFar: TempRecord) (listElement: TempRecord) : TempRecord =
        match maxTempRecordSoFar with
        | maxSoFar when maxSoFar.tmax > listElement.tmax -> maxSoFar
        | _ -> listElement

    let maxTempRecord = List.fold foldFun podaci[0] podaci
    (maxTempRecord.datum, maxTempRecord.tmax)


// Kog dana je zabiljezena najmanja temperatura i njena vrijednost
let (minTempDatum, minTemp): (Datum * float) =
    let foldFun (minTempRecordSoFar: TempRecord) (listElement: TempRecord) : TempRecord =
        match minTempRecordSoFar with
        | minSoFar when minSoFar.tmin < listElement.tmin -> minSoFar
        | _ -> listElement

    let minTempRecord = List.fold foldFun podaci[0] podaci
    (minTempRecord.datum, minTempRecord.tmin)


// Prosjecna temperatura u toku godine
let avgTempFullYear: float =
    let foldFun (sum: float) (listElement: TempRecord) : float = sum + listElement.tavg

    let sumRecord = List.fold foldFun 0.0 podaci
    sumRecord / float podaci.Length


// Prosjecno temperaturno odstupanje u toku godine
let avgTempDepartureFullYear: float =
    let foldFun (sum: float) (listElement: TempRecord) : float = sum + listElement.departure

    let sum = List.fold foldFun 0.0 podaci
    sum / float podaci.Length


// Kog dana je zabiljezeno najvece (apsolutno) temperaturno odstupanje i njena vrijednost
let (maxDepartureDatum, maxDeparture): (Datum * float) =
    let foldFun (maxDepartureRecordSoFar: TempRecord) (listElement: TempRecord) : TempRecord =
        match maxDepartureRecordSoFar with
        | maxSoFar when abs (maxSoFar.departure) > abs (listElement.departure) -> maxSoFar
        | _ -> listElement

    let maxDepartureRecord = List.fold foldFun podaci[0] podaci
    (maxDepartureRecord.datum, maxDepartureRecord.tmin)


// Kog dana je palo najvise snijega i kolicina u cm
// Kog dana je zabiljezena najveca dubina snijega u cm
let (maxNewSnowRecord, maxSnowDepthRecord): (TempRecord * TempRecord) =
    let foldFun
        (maxNewSoFar: TempRecord, maxDepthSoFar: TempRecord)
        (listElement: TempRecord)
        : (TempRecord * TempRecord) =
        let maxNew =
            match listElement with
            | listElement when listElement.noviSnijeg > maxNewSoFar.noviSnijeg -> listElement
            | _ -> maxNewSoFar

        let maxDepth =
            match listElement with
            | listElement when listElement.dubinaSnijega > maxDepthSoFar.dubinaSnijega -> listElement
            | _ -> maxDepthSoFar

        (maxNew, maxDepth)

    List.fold foldFun (podaci[0], podaci[0]) podaci

let (maxNewSnowDatum, maxNewSnowPadavine): (Datum * string) =
    (maxNewSnowRecord.datum, maxNewSnowRecord.noviSnijeg |> formatPadavine)

let (maxSnowDepthDatum, maxSnowDepthPadavine): (Datum * string) =
    (maxSnowDepthRecord.datum, maxSnowDepthRecord.dubinaSnijega |> formatPadavine)


// U kom mjesecu je zabiljezena najvisa prosjecna temperatura i njena vrijednost
// U kom mjesecu je zabiljezena najniza prosjecna temperatura i njena vrijednost

// Ako trazimo temperaturu samo jednog dana (u kojem je mjesecu)
let (maxAvgTemp, maxAvgTempMjesec, minAvgTemp, minAvgTempMjesec): (float * int * float * int) =
    let foldFun
        (maxSoFar: float, maxSoFarMjesec: int, minSoFar: float, minSoFarMjesec: int)
        (listElement: TempRecord)
        : (float * int * float * int) =
        match listElement with
        | { tavg = currentAvg; datum = datum } when currentAvg > maxSoFar ->
            (currentAvg, datum.mjesec, minSoFar, minSoFarMjesec)
        | { tavg = currentAvg; datum = datum } when currentAvg < minSoFar ->
            (maxSoFar, maxSoFarMjesec, currentAvg, datum.mjesec)
        | _ -> (maxSoFar, maxSoFarMjesec, minSoFar, minSoFarMjesec)

    List.fold foldFun (podaci[0].tavg, podaci[0].datum.mjesec, podaci[0].tavg, podaci[0].datum.mjesec) podaci

// Ako trazimo temperaturu u cijelom mjesecu
let getMonthAvgTemp (mjesec: int) : float =
    let foldFun (sum: float, count: int) (listElement: TempRecord) =
        match listElement with
        | { datum = datum; tavg = tavg } when datum.mjesec = mjesec -> (sum + tavg, count + 1)
        | _ -> (sum, count)

    let (sum: float, count: int) = List.fold foldFun (0.0, 0) podaci
    sum / float count

let getMonthTempList () : List<(float * int)> =
    let startMonth = 12

    let rec impl (currentMonth: int) : List<(float * int)> =
        match currentMonth with
        | n when n > 0 -> (getMonthAvgTemp n, n) :: impl (n - 1)
        | _ -> []

    impl startMonth

let monthTempList: List<(float * int)> = getMonthTempList ()

let (maxMonthTemp, maxTempMonth): (float * int) =
    let getMax (maxTempSoFar: float, maxTempMonthSoFar: int) (listElement: (float * int)) =
        match listElement with
        | (temp, month) when temp > maxTempSoFar -> (temp, month)
        | _ -> (maxTempSoFar, maxTempMonthSoFar)

    List.fold getMax monthTempList[0] monthTempList

let (minMonthTemp, minTempMonth): (float * int) =
    let getmin (minTempSoFar: float, minTempMonthSoFar: int) (listElement: (float * int)) =
        match listElement with
        | (temp, month) when temp < minTempSoFar -> (temp, month)
        | _ -> (minTempSoFar, minTempMonthSoFar)

    List.fold getmin monthTempList[0] monthTempList


// U kom mjesecu je estimiran najvisi prosjek neophodne energije

// Ako trazimo u kojem mjesecu je dan kada je najvise energije trebalo
let maxAvgEnergyMonth: int =
    let foldFun (maxAvgEnergySoFar: float, maxMonthSoFar: int) (listElement: TempRecord) : (float * int) =
        match listElement with
        | { hdd = hdd; cdd = cdd; datum = datum } when hdd + cdd > maxAvgEnergySoFar -> (hdd + cdd, datum.mjesec)
        | _ -> (maxAvgEnergySoFar, maxMonthSoFar)

    let (_, mjesec) =
        List.fold foldFun (podaci[0].hdd + podaci[0].cdd, podaci[0].datum.mjesec) podaci

    mjesec

// Ako trazimo u cijelom mjesecu kada je prosjecno najvise energije trebalo
let getMonthAvgEnergy (mjesec: int) : float =
    let foldFun (sum: float, count: int) (listElement: TempRecord) =
        match listElement with
        | { datum = datum; hdd = hdd; cdd = cdd } when datum.mjesec = mjesec -> (sum + (hdd + cdd), count + 1)
        | _ -> (sum, count)

    let (sum: float, count: int) = List.fold foldFun (0.0, 0) podaci
    sum / float count

let getMonthEnergyList () : List<(float * int)> =
    let startMonth = 12

    let rec impl (currentMonth: int) : List<(float * int)> =
        match currentMonth with
        | n when n > 0 -> (getMonthAvgEnergy n, n) :: impl (n - 1)
        | _ -> []

    impl startMonth

let monthEnergyList: List<(float * int)> = getMonthEnergyList ()

let maxEnergyMonth: int =
    let getMax (maxEnergySoFar: float, maxEnergyMonthSoFar: int) (listElement: (float * int)) =
        match listElement with
        | (energy, month) when energy > maxEnergySoFar -> (energy, month)
        | _ -> (maxEnergySoFar, maxEnergyMonthSoFar)

    let (_, month) = List.fold getMax monthEnergyList[0] monthEnergyList
    month


// Kog dana je zabiljezena najveca temperaturna razlika
let maxTempDiffDatum: Datum =
    let foldFun (maxDiffSoFar: float, maxDiffDatumSoFar: Datum) (listElement: TempRecord) : (float * Datum) =
        match listElement with
        | { datum = datum
            tmax = tmax
            tmin = tmin } when tmax - tmin > maxDiffSoFar -> (tmax - tmin, datum)
        | _ -> (maxDiffSoFar, maxDiffDatumSoFar)

    let (_, datum) =
        List.fold foldFun (podaci[0].tmax - podaci[0].tmin, podaci[0].datum) podaci

    datum


// Ispis svih podataka
podaci |> List.map formatRecord |> List.iter (printfn "%s")

// Ispis rjesenja zadataka
printfn "MaxTemp              -> %.2f°C" maxTemp
printfn "MaxTempDate          -> %s" (maxTempDatum |> formatDatum)

printfn "MinTemp              -> %.2f°C" minTemp
printfn "MinTempDate          -> %s" (minTempDatum |> formatDatum)

printfn "AvgTemp              -> %.2f°C" avgTempFullYear
printfn "AvgTempDeparture     -> %.2f°C" avgTempDepartureFullYear

printfn "MaxDeparture         -> %.2f°C" maxDeparture
printfn "MaxDepartureDate     -> %s" (maxDepartureDatum |> formatDatum)

printfn "MaxNewSnowDate       -> %s" (maxNewSnowDatum |> formatDatum)
printfn "MaxNewSnow           -> %s" maxNewSnowPadavine

printfn "MaxSnowDepthDate     -> %s" (maxSnowDepthDatum |> formatDatum)
printfn "MaxSnowDepth         -> %s" maxSnowDepthPadavine

printfn "MaxAvgTempInMonth_v1 -> %.2f°C" maxAvgTemp
printfn "MaxAvgTempMonth_v1   -> %s" (maxAvgTempMjesec |> imeMjeseca)

printfn "MinAvgTempInMonth_v1 -> %.2f°C" minAvgTemp
printfn "MinAvgTempMonth_v1   -> %s" (minAvgTempMjesec |> imeMjeseca)

printfn "MaxAvgTempInMonth_v2 -> %.2f°C" maxMonthTemp
printfn "MaxAvgTempMonth_v2   -> %s" (maxTempMonth |> imeMjeseca)

printfn "MinAvgTempInMonth_v2 -> %.2f°C" minMonthTemp
printfn "MinAvgTempMonth_v2   -> %s" (minTempMonth |> imeMjeseca)

printfn "MaxAvgEnergyMonth_v1 -> %s" (maxAvgEnergyMonth |> imeMjeseca)
printfn "MaxAvgEnergyMonth_v2 -> %s" (maxEnergyMonth |> imeMjeseca)

printfn "MaxTempDiffDay       -> %s" (maxTempDiffDatum |> formatDatum)

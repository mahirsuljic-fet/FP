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

type Datum = { dan: int; mjesec: int; godina: int }

let imeMjeseca ({ mjesec = m: int }: Datum) : string =
    match m with
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

        let formatPadavine padavine =
            match padavine with
            | Kolicina n -> sprintf "%.2f" n + "cm"
            | Zanemarivo -> "zanemarivo"
            | Nikako -> "nikako"

        let perc = formatPadavine padavine
        let newSnow = formatPadavine noviSnijeg
        let snowDepth = formatPadavine dubinaSnijega

        $"\
	Date:       {datum.dan}. {imeMjeseca datum} 20{datum.godina}.\n\
	Max temp:   {maxTemp}°C\n\
	Min temp:   {minTemp}°C\n\
	Avg temp:   {avgTemp}°C\n\
	Departure:  {departure}\n\
	HDD:        {hdd}\n\
	CDD:        {cdd}\n\
	Perc:       {perc}\n\
	New snow:   {newSnow}\n\
	Snow depth: {snowDepth}\n"

ucitajPodatke () |> List.map formatRecord |> List.iter (printfn "%s")

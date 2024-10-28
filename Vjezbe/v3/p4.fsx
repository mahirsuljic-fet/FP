// Napisati funkcije na odgovarajućim mjestima
let pricesInUSD = [100.0; 250.0; 75.0; 300.0]

// 1. Implementirati funkciju konverzije (closure)
let convertToBAM (exchangeRate : float) : (float -> float) = 
    let foo x = x * exchangeRate in foo

// 2. Implementirati funkciju popusta (closure)
let applyDiscount (discountRate : float) : (float -> float) =  
    let foo x = x - x * discountRate in foo

// 3. Implementirati funkciju provizije
let addProvisionFee (provisionFee : float) : (float -> float) = 
    let foo x = x + x * provisionFee in foo

// 3. Implementirati funkciju formatiranja
let formatBAM (price : float) : string =
    sprintf $"{price}KM"


// 4. Kompozicija funkcija
let processPrices = 
    convertToBAM 1.81
    >> applyDiscount 0.01
    >> addProvisionFee 0.02
    >> formatBAM

// 5. Transformisati i ispisati rezultate
let finalPrices = List.map processPrices pricesInUSD
List.iter (printfn "%s") finalPrices

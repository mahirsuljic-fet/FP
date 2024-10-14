// Napisati let izraz koji veze "isEven" simbol za lambda
// izraz koji uzima broj te ispituje da li je dati broj
// paran. Lambda nazad vraca boolean. Lambda izraz je
// potrebno anotirati odgovarajucim tipovima.
// Nakon ovoga napisati funkciju isEven koja radi istu 
// stvar kao lambda izraz te je takodjer anotirati tipovima

let n = 4

// let isEven = fun x -> x % 2 = 0
let isEven x = x % 2 = 0

printfn "%b" (isEven n)

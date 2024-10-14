// Napisati let izraz koji ce rezultirati stringom "SAMOGLASNIK"
// ukoliko expr1 (char) sadrzi samoglasnik. U suprotnom, izraz treba
// da generise runtime iznimku.

let expr1 = 'a'
printfn "%s" (let 'a' | 'e' | 'i' | 'o' | 'u' = expr1 in "SAMOGLASNIK")

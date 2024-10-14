// Napisati let izraz koji ce rezultirati stringom "PARAN"
// ukoliko je broj unutar expr1 zaista paran. U suprotnom izraz treba
// da generise runtime iznimku. 

let expr1 = 2
printfn "%s" (let 0 = expr1 % 2 in "PARAN")

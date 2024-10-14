// Napisati let izraz koji veze simbol concat za lambda izraz
// koji uzima 3 parametra. Prva dva parametra su stringovi
// koje treba spojiti, a treci parametar je delimiter izmedju
// dva stringa. Pozvati lambdu u expr2 dijelu let izraza:
// concat "Hello" "World" "_" ispisuje Hello_World

// let concat = fun s1 s2 s3 -> s1 + s3 + s2 

let concat a b c = a + c + b

printfn "%s" (concat "Hello" "World" "_")

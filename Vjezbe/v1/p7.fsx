// Zasto se dati izraz ne kompajlira?

let foo = fun x y -> x * y
let _ = foo 5.4f 6.1f         
printfn "%f" (foo 7.2f 8.4f) // 5.4f i 5.4 nisu isti tip

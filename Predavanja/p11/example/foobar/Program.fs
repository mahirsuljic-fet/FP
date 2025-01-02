open Browser.Dom

let foo = document.getElementById "foo"
let bar = document.getElementById "bar"
let tar = document.getElementById "tar"

let mutable count = 0

foo.innerText <- "+"
bar.innerText <- string count
tar.innerText <- "-"

foo.onclick <-
    fun _ ->
        count <- count + 1
        bar.innerText <- string count
        printfn "increment"

tar.onclick <-
    fun _ ->
        count <- count - 1
        bar.innerText <- string count
        printfn "decrement"

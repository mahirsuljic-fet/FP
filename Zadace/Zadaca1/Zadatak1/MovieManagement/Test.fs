module Test

open DatabaseAPI
open MovieTypes
open MovieFunctions

let test (db: Database) : unit =
    printfn "Creating movies..."

    let mutable movie1 = Movie("Movie1", Action, 2001, 1)
    let mutable movie2 = Movie("Movie2", Adventure, 2002, 2)
    let mutable movie3 = Movie("Movie3", Comedy, 2003, 3)
    let mutable movie4 = Movie("Movie4", Thriller, 2004, 4)
    let mutable movie5 = Movie("Movie5", Sci_Fi, 2005, 5)
    let mutable movie6 = Movie("Movie6", Fantasy, 2006, 6)
    let mutable movie7 = Movie("Movie7", Action, 2007, 7)
    let mutable movie8 = Movie("Movie8", Adventure, 2008, 8)
    let mutable movie9 = Movie("Movie9", Action, 2009, 9)
    let mutable movie10 = Movie("Movie10", Comedy, 2001, 10)
    let mutable movie11 = Movie("Movie11", Documentary, 1999, 2)
    let mutable movie12 = Movie("Movie12", Sci_Fi, 1998, 7)

    printfn "Movies created\n"


    //////////
    // SAVE //
    //////////

    let moviesToSave =
        [ movie1
          movie2
          movie3
          movie4
          movie5
          movie6
          movie7
          movie8
          movie9
          movie10
          movie11
          movie12 ]

    printfn "Saving movies..."
    saveMovies db moviesToSave
    printfn "Movies saved\n"


    ////////////
    // SEARCH //
    ////////////

    let arg_searchTitle: string = "Movie2"
    let arg_searchGenre: Genre = Action
    let arg_searchRatingBelow: float = 5
    let arg_searchRatingAbove: float = 5
    let arg_searchRatingRange: float * float = (4, 6)

    printfn "All movies"

    getAllMovies db |> MovieFunctions.printSearchResults

    printfn ""
    printfn $"Searching for movies with Title \"{arg_searchTitle}\""

    SearchTitle arg_searchTitle
    |> searchMovie db
    |> MovieFunctions.printSearchResults

    printfn ""
    printfn $"Searching for movies with Genre {arg_searchGenre}"

    SearchGenre arg_searchGenre
    |> searchMovie db
    |> MovieFunctions.printSearchResults

    printfn ""
    printfn $"Searching for movies with Rating below {arg_searchRatingBelow}"

    SearchRatingBelow arg_searchRatingBelow
    |> searchMovie db
    |> MovieFunctions.printSearchResults

    printfn ""
    printfn $"Searching for movies with Rating above {arg_searchRatingAbove}"

    SearchRatingAbove arg_searchRatingAbove
    |> searchMovie db
    |> MovieFunctions.printSearchResults

    printfn ""
    printfn $"Searching for movies with Rating in range [{fst arg_searchRatingRange}, {snd arg_searchRatingRange}]"

    SearchRatingRange arg_searchRatingRange
    |> searchMovie db
    |> MovieFunctions.printSearchResults


    ////////////
    // UPDATE //
    ////////////

    let arg_updateTitle: string = "Movie2"
    let arg_updateGenre: Genre = Mystery

    printfn ""
    printfn $"Updating Genre of movie with Title \"{arg_updateTitle}\" to {arg_updateGenre}"

    movie2.Genre <- arg_updateGenre
    (movieToISaveable db movie2).Save()
    printfn $"Movie with title \"{arg_updateTitle}\" updated\n"

    SearchTitle arg_updateTitle
    |> searchMovie db
    |> MovieFunctions.printSearchResults


    ////////////
    // DELETE //
    ////////////

    let arg_deleteTitle: string = "Movie3"

    printfn ""
    printfn $"Deleting movies with Title \"{arg_deleteTitle}\""

    (movieToIDeletable db movie3).Delete()

    SearchTitle arg_deleteTitle
    |> searchMovie db
    |> MovieFunctions.printSearchResults

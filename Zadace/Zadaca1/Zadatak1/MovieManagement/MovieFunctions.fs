module MovieFunctions

open DatabaseAPI
open MovieTypes

///////////////////
// DATABASE INIT //
///////////////////

let private createDB =
    "CREATE TABLE IF NOT EXISTS Movies (
        Id TEXT PRIMARY KEY,
        Title TEXT NOT NULL,
        Genre TEXT NOT NULL,
        ReleaseYear INTEGER NOT NULL,
        Rating REAL NOT NULL
    );"

let getDatabase () : Database =

    new Database("baza.db", createDB)


//////////
// SAVE //
//////////

let saveMovies (db: Database) (movieList: Movie list) : unit =
    let movieToISaveable (movie: Movie) = CommitMovie(db, movie) :> ISaveable
    movieList |> List.map movieToISaveable |> List.iter (fun x -> x.Save())


////////////
// SEARCH //
////////////

let getAllMovies (db: Database) : System.Collections.Generic.List<Movie> =
    let selectQuery = "SELECT * FROM Movies;"
    let dict = System.Collections.Generic.Dictionary<string, obj>()
    db.Search<Movie>(selectQuery, dict)

let private searchMovieByTitle (db: Database) (title: string) : System.Collections.Generic.List<Movie> =
    let selectQuery =
        "SELECT * FROM Movies 
            WHERE Title = @Title 
            ORDER BY Title ASC;"

    let dict = System.Collections.Generic.Dictionary<string, obj>()

    dict.Add("@Title", title)
    db.Search<Movie>(selectQuery, dict)

let private searchMovieByGenre (db: Database) (genre: Genre) : System.Collections.Generic.List<Movie> =
    let selectQuery =
        "SELECT * FROM Movies 
            WHERE Genre = @Genre 
            ORDER BY Title ASC;"

    let dict = System.Collections.Generic.Dictionary<string, obj>()

    dict.Add("@Genre", genre |> genreToString)
    db.Search<Movie>(selectQuery, dict)

let private searchMovieByRating
    (db: Database)
    (lowerBound: float)
    (upperBound: float)
    : System.Collections.Generic.List<Movie> =
    let selectQuery =
        "SELECT * FROM Movies 
            WHERE Rating >= @RatingLower 
            AND Rating <= @RatingUpper 
            ORDER BY Rating DESC, Title ASC;"

    let dict = System.Collections.Generic.Dictionary<string, obj>()

    dict.Add("@RatingLower", lowerBound)
    dict.Add("@RatingUpper", upperBound)
    db.Search<Movie>(selectQuery, dict)

let private searchMovieByRatingBelow (db: Database) (upperBound: float) : System.Collections.Generic.List<Movie> =
    searchMovieByRating db Movie.MinRating upperBound

let private searchMovieByRatingAbove (db: Database) (lowerBound: float) : System.Collections.Generic.List<Movie> =
    searchMovieByRating db lowerBound Movie.MaxRating

let searchMovie (db: Database) (searchType: SearchType) : System.Collections.Generic.List<Movie> =
    match searchType with
    | SearchTitle(title) -> searchMovieByTitle db title
    | SearchGenre(genre) -> searchMovieByGenre db genre
    | SearchRatingAbove(lowerBound) -> searchMovieByRatingAbove db lowerBound
    | SearchRatingBelow(upperBound) -> searchMovieByRatingBelow db upperBound
    | SearchRatingRange(lowerBound, upperBound) -> searchMovieByRating db lowerBound upperBound


// ispod je kod za "lijep" ispis tabela za koji mi je trebalo previse vremena i istrazivanja :)
// ovo sam napravio prije nego sto sam dodao NanoID, sa NanoID svi ID imaju istu velicinu,
// ali ostavio sam kod kako bi ostala podrska za slucaj da se format ID-a promijeni

// labeli za header
let private labelId = "ID"
let private labelTitle = "Title"
let private labelGenre = "Genre"
let private labelReleaseYear = "ReleaseYear"
let private labelRating = "Rating"

// sirine kolona
// wGenre je sirina Genre kolone, itd.
// ispod je slika sta koji width predstavlja
let private wReleaseYear = labelReleaseYear.Length
let private wRating = labelRating.Length
let private wPadding = 6 * 3 - 2 // " | " 6 puta, minus razmak na pocetku i kraju reda


// | labelId | labelTitle | labelGenre | labelReleaseYear | labelRating | <-- header
// |--------------------------------------------------------------------| <-- seperatorString
// | <-wId-> | <-wTitle-> | <-wGenre-> | <-wReleaseYear-> | <-wRating-> | <-- row
// ##       ###          ###          ###                ###           ## <-- # je padding ; wPadding = count(#)


// nalazi duzinu najduzed Id-a kako bi se mogao odrediti dovoljno velik wId za sve Id-ove
let private getLongestIdLength (list: List<Movie>) : int =
    let longestIdPredicate (movie: Movie) : int = (string movie.ID).Length
    let longestIdLength = ((list |> List.maxBy longestIdPredicate).ID |> string).Length
    longestIdLength


// nalazi duzinu najduzed Title-a kako bi se mogao odrediti dovoljno velik wTitle za sve Title-ove
let private getLongestTitleLength (list: List<Movie>) : int =
    let longestTitlePredicate (movie: Movie) : int = movie.Title.Length
    let longestTitleLength = (list |> List.maxBy longestTitlePredicate).Title.Length
    longestTitleLength


let private getLongestGenreLength (list: List<Movie>) : int =
    let longestGenrePredicate (movie: Movie) : int = (movie.Genre |> genreToString).Length
    let longestGenre = (list |> List.maxBy longestGenrePredicate).Genre
    let longestGenreLength = (longestGenre |> genreToString).Length
    longestGenreLength


// odredjuje dovoljno velik wId za sve Id-ove
let private getIdW (list: List<Movie>) : int =
    if list.IsEmpty then
        labelId.Length
    else
        let longestIdLength: int = getLongestIdLength list

        if longestIdLength > labelId.Length then
            longestIdLength
        else
            labelId.Length


// odredjuje dovoljno velik wTitle za sve Title-ove
let private getTitleW (list: List<Movie>) : int =
    if list.IsEmpty then
        labelTitle.Length
    else
        let longestTitleLength: int = getLongestTitleLength list

        if longestTitleLength > labelTitle.Length then
            longestTitleLength
        else
            labelTitle.Length


// odredjuje dovoljno velik wGenre za sve Genre-ove
let private getGenreW (list: List<Movie>) : int =
    if list.IsEmpty then
        labelGenre.Length
    else
        let longestGenreLength: int = getLongestGenreLength list

        if longestGenreLength > labelGenre.Length then
            longestGenreLength
        else
            labelGenre.Length


// sources za Printf.TextWriterFormat :
// https://stackoverflow.com/questions/62799947/how-can-i-build-a-format-string-with-sprintf-in-f
// https://stackoverflow.com/questions/19202773/how-do-i-use-a-variable-as-the-formatting-string-with-sprintf


// pravi Printf.TextWriterFormat na osnovu sirina kolona (w* varijabli) za header
let private getHeaderFormat (list: List<Movie>) : Printf.TextWriterFormat<_> =

    let wId = getIdW list
    let wTitle = getTitleW list
    let wGenre = getGenreW list

    let formatStringHeader =
        $"| %%-{wId}s | %%-{wTitle}s | %%-{wGenre}s | %%-{wReleaseYear}s | %%-{wRating}s |"

    let formatHeader =
        Printf.TextWriterFormat<string -> string -> string -> string -> string -> unit>(formatStringHeader)

    formatHeader


// pravi Printf.TextWriterFormat na osnovu sirina kolona (w* varijabli) za redove
let private getRowFormat (list: List<Movie>) : Printf.TextWriterFormat<_> =
    let wId = getIdW list
    let wTitle = getTitleW list
    let wGenre = getGenreW list

    let formatStringRow =
        $"| %%{wId}s | %%-{wTitle}s | %%-{wGenre}s | %%{wReleaseYear}d | %%{wRating}.2f |"

    let formatRow =
        Printf.TextWriterFormat<string -> string -> string -> int -> float -> unit>(formatStringRow)

    formatRow


// source za String.replicate :
// https://zetcode.com/fsharp/string/


let private getSeparatorString (list: List<Movie>) : string =
    let wId = getIdW list
    let wTitle = getTitleW list
    let wGenre = getGenreW list
    let rowLength = wId + wTitle + wGenre + wReleaseYear + wRating + wPadding
    String.replicate (rowLength - 2) "-"

let private getHeaderSeparatorString (list: List<Movie>) : string =
    getSeparatorString list |> sprintf "|%s|"

let private getTableLimitString (list: List<Movie>) : string =
    getSeparatorString list |> sprintf "+%s+"


// source za Generic.List -> List :
// https://stackoverflow.com/questions/3105165/convert-net-generic-list-to-f-list


let printSearchResults (genList: System.Collections.Generic.List<Movie>) : unit =
    let list: List<Movie> = List.ofSeq (genList)

    let headerFormat = getHeaderFormat list
    let rowFormat = getRowFormat list
    let tableLimitString = getTableLimitString list
    let headerSeperatorString = getHeaderSeparatorString list

    printfn "%s" tableLimitString
    printfn headerFormat labelId labelTitle labelGenre labelReleaseYear labelRating
    printfn "%s" headerSeperatorString

    for movie in list do
        printfn rowFormat movie.ID movie.Title (movie.Genre |> genreToString) movie.ReleaseYear movie.Rating

    printfn "%s" tableLimitString

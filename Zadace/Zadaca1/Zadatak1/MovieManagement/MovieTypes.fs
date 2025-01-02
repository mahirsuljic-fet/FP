module MovieTypes

open DatabaseAPI
open Microsoft.Data.Sqlite
open NanoidDotNet

// Dodan tip za zanrove kako bi se izbjegla situacija
// tipa da je jedan film zanra sci-fi, a drugi science fiction
// a program ih ne izbaci oba pri pretrazivanju
type Genre =
    | Action
    | Adventure
    | Animation
    | Comedy
    | Crime
    | Documentary
    | Drama
    | Family
    | Fantasy
    | Horror
    | Musical
    | Mystery
    | Reality
    | Romance
    | Sci_Fi
    | Sport
    | Thriller
    | Western
    | Unknown


//////////////////////////
// CONVERSION FUNCTIONS //
//////////////////////////

let genreToString (genre: Genre) : string =
    match genre with
    | Action -> "Action"
    | Adventure -> "Adventure"
    | Animation -> "Animation"
    | Comedy -> "Comedy"
    | Crime -> "Crime"
    | Documentary -> "Documentary"
    | Drama -> "Drama"
    | Family -> "Family"
    | Fantasy -> "Fantasy"
    | Horror -> "Horror"
    | Musical -> "Musical"
    | Mystery -> "Mystery"
    | Reality -> "Reality"
    | Romance -> "Romance"
    | Sci_Fi -> "Sci_Fi"
    | Sport -> "Sport"
    | Thriller -> "Thriller"
    | Western -> "Western"
    | Unknown -> "Unknown"

let stringToGenre (str: string) : Genre =
    match str.ToLower() with
    | "action" -> Action
    | "adventure" -> Adventure
    | "animation" -> Animation
    | "comedy" -> Comedy
    | "crime" -> Crime
    | "documentary" -> Documentary
    | "drama" -> Drama
    | "family" -> Family
    | "fantasy" -> Fantasy
    | "horror" -> Horror
    | "musical" -> Musical
    | "mystery" -> Mystery
    | "reality" -> Reality
    | "romance" -> Romance
    | "science fiction" -> Sci_Fi
    | "scifi" -> Sci_Fi
    | "sci-fi" -> Sci_Fi
    | "sci_fi" -> Sci_Fi
    | "sport" -> Sport
    | "thriller" -> Thriller
    | "western" -> Western
    | _ -> Unknown


type SearchType =
    | SearchTitle of title: string
    | SearchGenre of genre: Genre
    | SearchRatingAbove of lowerBound: float
    | SearchRatingBelow of upperBound: float
    | SearchRatingRange of lowerBound: float * upperBound: float


type Movie(id: string, title: string, genre: Genre, release_year: int, rating: float) =

    static let IDsize = 10
    static let IDtype = Nanoid.Alphabets.LowercaseLettersAndDigits

    let mutable _id = id
    let mutable _title = title
    let mutable _genre = genre
    let mutable _release_year = release_year
    let mutable _rating = rating

    static member MinRating = 1.0
    static member MaxRating = 10.0

    member _.ID
        with get (): string = _id
        and set (newID: string): unit = _id <- newID

    member _.Title
        with get (): string = _title
        and set (newTitle: string): unit = _title <- newTitle

    member _.Genre
        with get (): Genre = _genre
        and set (newGenre: Genre): unit = _genre <- newGenre

    member _.ReleaseYear
        with get (): int = _release_year
        and set (newReleaseYear: int): unit = _release_year <- newReleaseYear

    member _.Rating
        with get (): float = _rating
        and set (newRating: float): unit =
            let _newRating =
                if newRating < Movie.MinRating then Movie.MinRating
                elif newRating > Movie.MaxRating then Movie.MaxRating
                else newRating

            _rating <- _newRating

    new() = Movie("", "", Unknown, 0, 0)

    new(title: string, genre: Genre, release_year: int, rating: float) =
        Movie(Nanoid.Generate(IDtype, IDsize), title, genre, release_year, rating)

    interface ISearchable with
        member this.FromReader(reader: SqliteDataReader) : unit =
            this.ID <- reader.GetString(0)
            this.Title <- reader.GetString(1)
            this.Genre <- reader.GetString(2) |> stringToGenre
            this.ReleaseYear <- reader.GetInt32(3)
            this.Rating <- reader.GetDouble(4)


type CommitMovie(db: Database, movie: Movie) =
    inherit DatabaseEntity(db)

    let _movie = movie

    override _.BindCommitParameters(cmd: SqliteCommand) : unit =
        cmd.Parameters.AddWithValue("@Id", _movie.ID) |> ignore
        cmd.Parameters.AddWithValue("@Title", _movie.Title) |> ignore
        cmd.Parameters.AddWithValue("@Genre", _movie.Genre |> genreToString) |> ignore
        cmd.Parameters.AddWithValue("@ReleaseYear", _movie.ReleaseYear) |> ignore
        cmd.Parameters.AddWithValue("@Rating", _movie.Rating) |> ignore

    interface ISaveable with
        member this.Save() : unit =
            let sqlQuery =
                "INSERT OR REPLACE INTO 
                    Movies(Id, Title, Genre, ReleaseYear, Rating) 
                    VALUES(@Id, @Title, @Genre, @ReleaseYear, @Rating);"

            this.Commit(sqlQuery)

    interface IDeletable with
        member this.Delete() : unit =
            let sqlQuery =
                "DELETE FROM Movies 
                    WHERE Id = @Id 
                    AND Title = @Title 
                    AND Genre = @Genre 
                    AND ReleaseYear = @ReleaseYear 
                    AND Rating = @Rating;"

            this.Commit(sqlQuery)

let movieToISaveable (db: Database) (movie: Movie) : ISaveable = CommitMovie(db, movie) :> ISaveable
let movieToIDeletable (db: Database) (movie: Movie) : IDeletable = CommitMovie(db, movie) :> IDeletable

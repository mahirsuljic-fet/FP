open DatabaseAPI
open Microsoft.Data.Sqlite

type Film(db: Database, Title: string, Genre: string) =
    inherit DatabaseEntity(db)

    member this.Title = Title
    member this.Genre = Genre

    override this.BindCommitParameters(cmd: SqliteCommand) : unit =
        cmd.Parameters.AddWithValue("@Title", this.Title) |> ignore
        cmd.Parameters.AddWithValue("@Genre", this.Genre) |> ignore
        ()

    interface ISaveable with
        member this.Save() : unit =
            let insertQuery = "INSERT OR REPLACE INTO Film VALUES (@Title, @Genre);"
            this.Commit(insertQuery)

type FilmByGenre(title: string) =
    let mutable _title: string = title

    member this.Title
        with get () = _title
        and set (title) = _title <- title

    new() = FilmByGenre("")

    interface ISearchable with
        member this.FromReader(reader: SqliteDataReader) : unit = this.Title <- reader.GetString(0)

let createTables =
    @"
  CREATE TABLE IF NOT EXISTS Film(
    Title TEXT PRIMARY KEY,
    Genre TEXT
  );
"

let selectQuery = "SELECT Title FROM Film WHERE Genre = 'Sci-Fi';"

let db = Database("baza.db", createTables)

let film1 = Film(db, "Gladiator 2", "Romance")
let film2 = Film(db, "Mad Max", "Sci-Fi")
let film3 = Film(db, "The Notebook", "Romance")
let film4 = Film(db, "Star Trek", "Sci-Fi")

let saveFilms (films: List<ISaveable>) : unit = films |> List.iter (fun x -> x.Save())

saveFilms [ film1; film2; film3; film4 ]

let dict = System.Collections.Generic.Dictionary<string, obj>()
dict.Add("@Genre", "Sci-Fi")

let sciFis = db.Search<FilmByGenre>(selectQuery, dict)

printfn "Sci-Fi movies:"

for movie in sciFis do
    printfn "- %s" movie.Title

module MovieManagement

open DatabaseAPI
open MovieFunctions
open Test

let db: Database = getDatabase ()
test(db)

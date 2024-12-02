type IRide =
    abstract member GetCost: unit -> float
    abstract member GetStatus: unit -> string
    abstract member UpdateStatus: string -> unit

type Ride(id: int, pickup: string, dropoff: string, cost: double) =
    // inherit NekaKlasa(njeni_parametri) // na ovaj nacin se radi nasljedjivanje

    let mutable status = "Available"

    member this.Id = id
    member this.Pickup = pickup
    member this.Dropoff = dropoff
    member this.Cost = cost

    member this.Status
        with get () = status
        and set (value: string) = status <- value

    // interface se moze implementirati bilo gdje
    interface IRide with
        override this.GetCost() = this.Cost
        override this.GetStatus() = this.Status
        override this.UpdateStatus(status: string) = this.Status <- status

type PremiumRide(id: int, pickup: string, dropoff: string, cost: double, vehicleType: string) =
    inherit Ride(id, pickup, dropoff, cost)

    member this.VehicleType = vehicleType

    interface IRide with
        override this.GetCost() = this.Cost + 10.0
        override this.GetStatus() = this.Status
        override this.UpdateStatus(status: string) = this.Status <- status

let main () =
    let rides: IRide list = [
        Ride(1, "Downtown", "Airport", 20.0);
        PremiumRide(2, "Mall", "Stadium", 30.0, "SUV") 
    ]

    let printRides (list: IRide list) : unit =
        match list with
        | [] -> ()
        | x :: xs -> printfn "%A" x

    printRides rides

main ()

let x = Ride(1, "Downtown", "Airport", 20.0)
let px = x :> IRide // promovisanje u interface
let x1 = px :?> Ride // democija u Ride
px.GetCost()

// public interface IRide
// {
//     double GetCost();
//     string GetStatus();
//     void UpdateStatus(string status);
// }


type IRide =
  abstract member GetCost: unit -> double
  abstract member GetStatus: unit -> string
  abstract member UpdateStatus: string -> unit


  // public Ride(int id, string pickup, string dropoff, double cost)

type Ride(id: int, pickup: string, dropoff: string, cost: double) =
  let mutable status = "Available" 

  member this.Id 
    with get () = id

  member this.Pickup = pickup
  member this.Dropoff = dropoff
  member this.Cost = cost
  member this.Status
    with get () = status
    and set(value) = status <- value
    
  interface IRide with
    member this.GetCost () = this.Cost
    member this.GetStatus() = this.Status
    member this.UpdateStatus(st: string) = this.Status <- st

type PremiumRide(id: int, pickup: string, dropoff: string, cost: double, vehicleType: string) =
  inherit Ride(id, pickup, dropoff, cost * 1.5)

  member this.VehicleType = vehicleType 

  interface IRide with
    member this.GetCost () = this.Cost + 10.

let ride = Ride(1, "Downtown", "Airport", 20.0)
let iride = ride :> IRide
let nride = iride :?> Ride
nride.Cost
iride.GetCost()


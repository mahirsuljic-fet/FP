using System;
using System.Collections.Generic;

public interface IRide
{
    double GetCost();
    string GetStatus();
    void UpdateStatus(string status);
}
public class Ride : IRide
{
    public int Id { get; }
    public string PickupLocation { get; }
    public string DropoffLocation { get; }
    private double Cost { get; }
    private string Status { get; set; }

  public Ride(int id, string pickup, string dropoff, double cost)
    {
  Id = id;
        PickupLocation = pickup;
        DropoffLocation = dropoff;
        Cost = cost;
        Status = "Available";
  }

    public double GetCost() => Cost;
    public string GetStatus() => Status;
    public void UpdateStatus(string status) => Status = status;
}

public class PremiumRide : Ride
{
    public string VehicleType { get; }

    public PremiumRide(int id, string pickup, string dropoff, double cost, string vehicleType)
        : base(id, pickup, dropoff, cost * 1.5)
  {
        VehicleType = vehicleType;
    }

    public new double GetCost() => base.GetCost() + 10; // Adds a luxury fee
}

class Program
{
    static void Main()
    {
        List<IRide> rides = new List<IRide>
        {
            new Ride(1, "Downtown", "Airport", 20.0),
            new PremiumRide(2, "Mall", "Stadium", 30.0, "SUV")
        };

        foreach (var ride in rides)
        {
            Console.WriteLine($"Ride Cost: ${ride.GetCost()}, Status: {ride.GetStatus()}");
            ride.UpdateStatus("Booked");
            Console.WriteLine($"After booking: Status: {ride.GetStatus()}");
        }
    }
}

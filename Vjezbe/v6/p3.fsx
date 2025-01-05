// using System;

open System

// public interface ISolid
// {
//     double GetVolume();
//     double GetSurfaceArea();
//     string GetDescription();
// }

type ISolid =
    abstract member GetVolume: unit -> float
    abstract member GetSurfaceArea: unit -> float
    abstract member GetDescription: unit -> string

// public abstract class Solid : ISolid
// {
//     public string Name { get; }
//     public int Id { get; }
//
//     protected Solid(int id, string name)
//     {
//         Id = id;
//         Name = name;
//     }
//
//     public abstract double GetVolume();
//     public abstract double GetSurfaceArea();
//
//     public string GetDescription()
//     {
//         return $"{Name} (ID: {Id})";
//     }
// }

[<AbstractClass>]
type Solid(id: int, name: string) =
    member this.Name: string = name
    member this.Id: int = id

    abstract member _getVolume: unit -> float
    abstract member _getSurfaceArea: unit -> float

    interface ISolid with
        member this.GetVolume() : float = this._getVolume ()
        member this.GetSurfaceArea() : float = this._getSurfaceArea ()
        member this.GetDescription() : string = $"{this.Name} (ID: {this.Id})"


// public class Sphere : Solid
// {
//     public double Radius { get; }
//
//     public Sphere(int id, string name, double radius)
//         : base(id, name)
//     {
//         Radius = radius;
//     }
//
//     public override double GetVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
//     public override double GetSurfaceArea() => 4 * Math.PI * Math.Pow(Radius, 2);
// }

type Sphere(id: int, name: string, radius: float) =
    inherit Solid(id, name)

    member this.Radius: float = radius

    override this._getVolume() : float =
        (4.0 / 3.0) * Math.PI * Math.Pow(this.Radius, 3)

    override this._getSurfaceArea() : float =
        4.0 * Math.PI * Math.Pow(this.Radius, 2)

// public class Cylinder : Solid
// {
//     public double Radius { get; }
//     public double Height { get; }
//
//     public Cylinder(int id, string name, double radius, double height)
//         : base(id, name)
//     {
//         Radius = radius;
//         Height = height;
//     }
//
//     public override double GetVolume() => Math.PI * Math.Pow(Radius, 2) * Height;
//     public override double GetSurfaceArea() => 2 * Math.PI * Radius * (Radius + Height);
// }

type Cylinder(id: int, name: string, radius: float, height: float) =
    inherit Solid(id, name)

    member this.Radius: float = radius
    member this.Height: float = height

    override this._getVolume() : float =
        Math.PI * Math.Pow(this.Radius, 2) * this.Height

    override this._getSurfaceArea() : float =
        2.0 * Math.PI * this.Radius * (this.Radius + this.Height)

// class Program
// {
//     static void Main()
//     {
//         ISolid sphere = new Sphere(1, "Sphere", 5);
//         ISolid cylinder = new Cylinder(2, "Cylinder", 3, 7);
//
//         ISolid[] solids = { sphere, cylinder };
//
//         foreach (var solid in solids)
//         {
//             Console.WriteLine(solid.GetDescription());
//             Console.WriteLine($"Volume: {solid.GetVolume():F2}");
//             Console.WriteLine($"Surface Area: {solid.GetSurfaceArea():F2}");
//             Console.WriteLine();
//         }
//     }
// }

type Program() =
    static member Main() =
        let sphere: ISolid = Sphere(1, "Sphere", 5)
        let cylinder: ISolid = Cylinder(2, "Cylinder", 3, 7)

        let solids: ISolid list = [ sphere; cylinder ]

        for solid in solids do
            printfn "%s" (solid.GetDescription())
            printfn "%s" $"Volume: {solid.GetVolume():F2}"
            printfn "%s" $"Surface Area: {solid.GetSurfaceArea():F2}"

Program.Main()

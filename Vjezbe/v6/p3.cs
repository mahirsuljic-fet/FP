using System;

public interface ISolid
{
    double GetVolume();
    double GetSurfaceArea();
    string GetDescription();
}

public abstract class Solid : ISolid
{
    public string Name { get; }
    public int Id { get; }

    protected Solid(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public abstract double GetVolume();
    public abstract double GetSurfaceArea();

    public string GetDescription()
    {
        return $"{Name} (ID: {Id})";
    }
}

public class Sphere : Solid
{
    public double Radius { get; }

    public Sphere(int id, string name, double radius)
        : base(id, name)
    {
        Radius = radius;
    }

    public override double GetVolume() => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
    public override double GetSurfaceArea() => 4 * Math.PI * Math.Pow(Radius, 2);
}

public class Cylinder : Solid
{
    public double Radius { get; }
    public double Height { get; }

    public Cylinder(int id, string name, double radius, double height)
        : base(id, name)
    {
        Radius = radius;
        Height = height;
    }

    public override double GetVolume() => Math.PI * Math.Pow(Radius, 2) * Height;
    public override double GetSurfaceArea() => 2 * Math.PI * Radius * (Radius + Height);
}

class Program
{
    static void Main()
    {
        ISolid sphere = new Sphere(1, "Sphere", 5);
        ISolid cylinder = new Cylinder(2, "Cylinder", 3, 7);

        ISolid[] solids = { sphere, cylinder };

        foreach (var solid in solids)
        {
            Console.WriteLine(solid.GetDescription());
            Console.WriteLine($"Volume: {solid.GetVolume():F2}");
            Console.WriteLine($"Surface Area: {solid.GetSurfaceArea():F2}");
            Console.WriteLine();
        }
    }
}

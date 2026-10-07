using Starship_Mini.Models;

namespace Starship_Mini;

public class Program
{
    // Main = Startpunkt des Programms
    public static void Main(string[] args)
    {
        // --- 1) Konstruktor ---
        // Variable vom Typ der Basis-Klasse, Objekt vom Typ der Kind-Klasse
        BaseShip starship1 = new Starship("  Bounty  ", 8, new DateOnly(2026, 1, 1));

        // --- 2) Object Initializer Syntax ---
        Starship starship2 = new Starship()
        {
            Name = "Enterprise",
            CrewMembers = 10,
            BuildAt = new DateOnly(2020, 5, 17),
            IsBattleShip = true,
        };

        Starship starship3 = new Starship("Voyager", 3, new DateOnly(2024, 3, 9));

        // Polymorphie: Describe() => kommt aus Starship, obwohl der Typ BaseShip ist
        Console.WriteLine(starship1);
        Console.WriteLine(starship2);
        Console.WriteLine(starship3);
        Console.WriteLine();


        // --- 3) Equals ---
        Starship copy = new Starship("Bounty", 5, new DateOnly(2025, 1, 1));

        Console.WriteLine($"== (gleiche Referenz?):  {starship1 == copy}");      // False
        Console.WriteLine($"Equals (gleicher Name?): {starship1.Equals(copy)}"); // True
        Console.WriteLine();


        // --- 4) Mothership ---
        Mothership mothership = new Mothership();
        mothership.DockOn(starship1);
        mothership.DockOn(starship2);
        mothership.DockOn(starship3);

        Console.WriteLine($"Docked ships:      {mothership.Baseships.Count}");
        Console.WriteLine($"Sum crew members:  {mothership.SumCrewMembers()}");
        Console.WriteLine($"Ships with crew > 5: {mothership.NumberLargeCrew(5)}");

        foreach (BaseShip ship in mothership.NumberLargeShips(5))
        {
            Console.WriteLine($"  - {ship.Name}");
        }
        Console.WriteLine();


        try
        {
            mothership.DockOn(copy);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        try
        {
            starship2.CrewMembers = 11;                     // > 10
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        try
        {
            starship2.Name = "   ";
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        try
        {
            starship2.BuildAt = new DateOnly(2010, 1, 1);   // älter als 10 Jahre
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}
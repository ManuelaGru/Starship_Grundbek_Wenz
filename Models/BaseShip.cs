namespace Starship_Mini.Models;

// BASIS-KLASSE
// abstract = man kann KEIN "new BaseShip()" machen, nur Kind-Klassen.
// Enthält alles, was JEDES Schiff hat.
public abstract class BaseShip
{
    // --- Backing Fields ---

    private string _name = string.Empty;

    private int _crewMembers;


    // --- Properties (2 Props for both) ---

    // Invariants: not null, not empty, should be trimmed
    public string Name
    {
        get { return _name; }
        set
        {
            // GUARD
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(Name));
            _name = value.Trim();
        }
    }

    // Invariants: [1, 10]
    // Muss in BaseShip sein, weil Mothership damit rechnet (SumCrewMembers, ...)
    public int CrewMembers
    {
        get { return _crewMembers; }
        set
        {
            // GUARD
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value, nameof(CrewMembers));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, 10, nameof(CrewMembers));

            _crewMembers = value;
        }
    }


    // --- Ctor ---

    public BaseShip(string name, int crewMembers)
    {
        // Über die Properties setzen -> Guards werden ausgeführt
        Name = name;
        CrewMembers = crewMembers;
    }

    // Für die Object Initializer Syntax: new Starship() { ... }
    public BaseShip() {}


    // --- 1 virtual method ---

    // virtual = Kind-Klasse DARF die Methode überschreiben (override)
    public virtual string Describe()
    {
        return "Base ship";
    }


    // --- Equals ---
    // Zwei Schiffe sind gleich, wenn sie gleich heißen (names are unique).
    // Wird von Mothership.DockOn -> Contains() verwendet.

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        BaseShip other = (BaseShip)obj;
        return Name == other.Name;
    }

    // Wer Equals überschreibt, muss auch GetHashCode überschreiben
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }


    // --- ToString ---

    public override string ToString()
    {
        // Describe() kommt zur Laufzeit von der Kind-Klasse (Polymorphie)
        return $"[{Describe()}] Name: {Name}, CrewMembers: {CrewMembers}";
    }
}

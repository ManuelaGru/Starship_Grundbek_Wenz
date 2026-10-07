namespace Starship_Mini.Models;

// is-a relationship
// has-a relationship (1:n)
public class Mothership
{
    // --- Backing Field ---

    // internal mutable list
    private List<BaseShip> _baseships = [];

    // exposed as read only
    public IReadOnlyList<BaseShip> Baseships => _baseships.AsReadOnly();


    // Invariants: not null, size [0, 10], names are unique
    // add
    public void DockOn(BaseShip baseship)
    {
        ArgumentNullException.ThrowIfNull(baseship);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(Baseships.Count, 10);

        if (Baseships.Contains(baseship))
            throw new ArgumentException("Cannot contain duplicates");

        _baseships.Add(baseship);
    }

    // remove
    public void DockOff(BaseShip baseship)
    {
        ArgumentNullException.ThrowIfNull(baseship);

        _baseships.Remove(baseship);
    }


    // sum all crew members
    public int SumCrewMembers()
    {
        return Baseships.Sum(baseship => baseship.CrewMembers);
    }


    // declarative programming (what i want)
    public int NumberLargeCrew(int limit)
    {
        return _baseships.Count(baseship => baseship.CrewMembers > limit);
    }


    // declarative
    // LINQ, where, toList
    public IReadOnlyList<BaseShip> NumberLargeShips(int limit)
    {
        return _baseships
            .Where(baseship => baseship.CrewMembers > limit)
            .ToList();
    }
}
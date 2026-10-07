namespace Starship_Mini.Models;

// KIND-KLASSE
// is-a relationship: ein Starship IST EIN BaseShip.
// Name und CrewMembers kommen aus BaseShip.

public class Starship : BaseShip
{
    // --- Backing Fields ----

    private DateOnly _buildAt;


    // --- Properties ---

    // Auto-Property: keine Regel nötig -> kein Backing Field nötig
    public bool IsBattleShip { get; set; }

    // Invariants: [date.now - 10 years, now]
    public DateOnly BuildAt
    {
        get { return _buildAt; }
        set
        {
            DateOnly nowDate = DateOnly.FromDateTime(DateTime.UtcNow);
            DateOnly minDate = nowDate.AddYears(-10);

            // GUARD
            ArgumentOutOfRangeException.ThrowIfLessThan(value, minDate, nameof(BuildAt));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(value, nowDate, nameof(BuildAt));

            _buildAt = value;
        }
    }


    // --- Ctor ---

    // : base(...) ruft zuerst den Konstruktor der Basis-Klasse auf
    public Starship(string name, int crewMembers, DateOnly buildAt, bool isBattleShip = false)
        : base(name, crewMembers)
    {
        BuildAt = buildAt;
        IsBattleShip = isBattleShip;
    }

    public Starship()
        : base()
    {
    }


    // --- Override der virtual method ---

    public override string Describe()
    {
        return IsBattleShip ? "Battle starship" : "Starship";
    }


    // --- ToString ---

    public override string ToString()
    {
        // base.ToString() = Text aus BaseShip wiederverwenden
        return $"{base.ToString()}, BuildAt: {BuildAt}, IsBattleShip: {IsBattleShip}";
    }
}
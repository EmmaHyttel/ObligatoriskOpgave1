namespace ObligatoriskOpgave1;

public class Trophy
{
   
    public int Id { get; set; }
    public string Competition { get; set; }
    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Id}, {Competition}, {Year}";
    }
    public Trophy()
    {
    }

    public Trophy(int id, string competition, int year)
    {
        Id = id;
        Competition = competition;
        Year = year;
    }
    
    // copy constructor
    public Trophy(Trophy trophy)
    {
        Id = trophy.Id;
        Competition = trophy.Competition;
        Year = trophy.Year;
    }

    public void ValidateCompetition()
    {
        if (Competition == null)
            throw new ArgumentNullException("Must enter competition");
        if (Competition.Length < 3)
            throw new ArgumentException("Competition must be at least 3 characters: " + Competition);
    }

    public void ValidateYear()
    {
        if (Year < 1970 || Year > DateTime.Now().YearOnly())
            throw new ArgumentOutOfRangeException("Year must be between 1970 and 2024: " + Year);
    }

    public void Validate()
    {
        ValidateCompetition();
        ValidateYear();
    }
}

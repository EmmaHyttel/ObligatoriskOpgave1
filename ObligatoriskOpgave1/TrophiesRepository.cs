
namespace ObligatoriskOpgave1;

public class TrophiesRepository : ITrophiesRepository
{
    private int _nextId = 1;
    private readonly List<Trophy> _trophies = new();
    public Trophy Add(Trophy trophy)
    {
        trophy.Validate();
        trophy.Id = _nextId++;
        _trophies.Add(trophy);
        return trophy;
    }

    public IEnumerable<Trophy> Get(int? yearAfter = null, int? yearOnly = null, string? competitionIncludes = null, string? orderBy = null)
    {
        IEnumerable<Trophy> result = new List<Trophy>(_trophies);

        // hvordan laver jeg en copy contructor til at returnere en kopi af listen af mine trophies? 
        
        if (yearAfter != null)
        {
            result = result.Where(t => t.Year > yearAfter);
        }
        if (yearOnly != null)
        {
            result = result.Where(t => t.Year == yearOnly);
        }
        if (competitionIncludes != null)
        {
            result = result.Where(t => t.Competition.Contains(competitionIncludes));
        }

        if (orderBy != null)
        {
            orderBy = orderBy.ToLower();
            switch (orderBy)
            {
                case "competition": // fall through to next case
                case "competition_asc":
                    result = result.OrderBy(t => t.Competition);
                    break;
                case "competition_desc":
                    result = result.OrderByDescending(t => t.Competition);
                    break;
                case "year":
                case "year_asc":
                    result = result.OrderBy(t => t.Year);
                    break;
                case "year_desc":
                    result = result.OrderByDescending(a => a.Year);
                    break;
                default:
                    break;
            }
        }
        return result;
    }

    public Trophy? GetById(int id)
    {
        return _trophies.Find(trophy => trophy.Id == id);
    }

    public Trophy? Remove(int id)
    {
        Trophy? trophy = GetById(id);
        if (trophy == null)
        {
            return null;
        }
        _trophies.Remove(trophy);
        return trophy;
    }

    public Trophy? Update(int id, Trophy trophy)
    {
        trophy.Validate();
        Trophy? existingTrophy = GetById(id);
        if (existingTrophy == null)
        {
            throw new ArgumentException($"No trophy found with ID {id}");
        }
        existingTrophy.Competition = trophy.Competition;
        existingTrophy.Year = trophy.Year;
        return existingTrophy;
    }
}

namespace ObligatoriskOpgave1;

public interface ITrophiesRepository
{
    IEnumerable<Trophy> Get(int? yearAfter = null, int? yearOnly = null, string? competitionIncludes = null, string? orderBy = null);

    Trophy? GetById(int id);
    Trophy Add(Trophy trophy);
    Trophy? Remove(int id);
    Trophy? Update(int id, Trophy trophy);
}

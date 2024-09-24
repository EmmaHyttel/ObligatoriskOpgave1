using ObligatoriskOpgave1;

namespace TrophyTest;

[TestClass]
public class TrophiesRepositoryTest
{
    private ITrophiesRepository _repo;

    private readonly Trophy competitionNullTrophy = new() { Id = 2, Competition = null, Year = 1990 };
    private readonly Trophy competitionShortTrophy = new() { Id = 3, Competition = "Te", Year = 1990 };
    private readonly Trophy yearInvalid = new() { Id = 4, Competition = "Tennis", Year = 2025 };
    private readonly Trophy yearToOld = new() { Id = 5, Competition = "Tennis", Year = 1969 };

    [TestInitialize]
    public void Init()
    {
        _repo = new TrophiesRepository();

        _repo.Add(new Trophy() { Competition = "CPH Half Marathon", Year = 2024 });
        _repo.Add(new Trophy() { Competition = "DM i Skak", Year = 1991 });
        _repo.Add(new Trophy() { Competition = "Roskilde Tennisklub juniormesterskab", Year = 2001 });
        _repo.Add(new Trophy() { Competition = "Tour de France", Year = 2020 });   
    }

    [TestMethod()]
    public void GetTest()
    {
        IEnumerable<Trophy> trophie = _repo.Get();
        Assert.AreEqual(4, trophie.Count());
        Assert.AreEqual(trophie.First().Competition, "CPH Half Marathon");
    }

    [TestMethod()]
    public void GetTestYearAfterFilter()
    {
        IEnumerable<Trophy> trophies = _repo.Get(yearAfter: 2019);
        Assert.AreEqual(2, trophies.Count()); 
        Assert.IsTrue(trophies.All(t => t.Year > 2019));
    }

    [TestMethod()]
    public void GetTestYearOnlyFilter()
    {
        IEnumerable<Trophy> trophies = _repo.Get(yearAfter: 2020);
        Assert.AreEqual(1, trophies.Count());
        Assert.AreEqual(2020, trophies.First().Year);
    }

    [TestMethod()]
    public void GetTestCompetitionIncludesFilter()
    {
       IEnumerable<Trophy> trophies = _repo.Get(competitionIncludes: "Marathon");
        Assert.AreEqual(1, trophies.Count());
        Assert.AreEqual(trophies.First().Competition, "CPH Half Marathon");
    }

    [TestMethod()]
    public void GetTestOrderByYear()
    {
        IEnumerable<Trophy> sortedTrophies = _repo.Get(orderBy: "year");
        Assert.AreEqual(sortedTrophies.First().Year, 1991);
    }

    [TestMethod()]
    public void GetTestOrderByCompetition()
    {
        IEnumerable<Trophy> sortedTrophies2 = _repo.Get(orderBy: "competition");
        Assert.AreEqual(sortedTrophies2.First().Competition, "CPH Half Marathon");
    }

    [TestMethod()]
    public void GetByIdTest()
    {
        Assert.IsNotNull(_repo.GetById(1));
        Assert.IsNull(_repo.GetById(100));
    }

    [TestMethod()]
    public void AddTest()
    {
        Trophy t = new() { Competition = "Test", Year = 2021 };
        Assert.AreEqual(5, _repo.Add(t).Id);
        Assert.AreEqual(5, _repo.Get().Count());

        Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(competitionNullTrophy));
        Assert.ThrowsException<ArgumentException>(() => _repo.Add(competitionShortTrophy));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(yearToOld));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(yearInvalid));
    }

    [TestMethod()]
    public void RemoveTest()
    {
        Assert.IsNull(_repo.Remove(100));
        Assert.AreEqual(1, _repo.Remove(1)?.Id);
        Assert.AreEqual(3, _repo.Get().Count());
    }

    public void UpdateTest()
    {
        Assert.AreEqual(4, _repo.Get().Count());
        Trophy t = new() { Competition = "Test", Year = 2021 };
        Assert.IsNull(_repo.Update(100, t));
        Assert.AreEqual(1, _repo.Update(1, t)?.Id);
        Assert.AreEqual(_repo.Get().First().Competition, "Test");
        Assert.AreEqual(4, _repo.Get().Count());

        Assert.ThrowsException<ArgumentNullException>(() => _repo.Update(1, competitionNullTrophy));
        Assert.ThrowsException<ArgumentException>(() => _repo.Update(1, competitionShortTrophy));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, yearToOld));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(1, yearInvalid));
    }
}

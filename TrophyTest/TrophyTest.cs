using ObligatoriskOpgave1;

namespace TrophyTest;

[TestClass]
public class TrophyTest
{ 
    private Trophy validTrophy = new Trophy { Id = 1, Competition = "Tennis", Year = 1990 };
    private Trophy competitionNullTrophy = new Trophy { Id = 2, Competition = null, Year = 1990 };
    private Trophy competitionShortTrophy = new Trophy { Id = 3, Competition = "Te", Year = 1990 };
    private Trophy yearInvalid = new Trophy { Id = 4, Competition = "Tennis", Year = 2025 };
    private Trophy yearToOld = new Trophy { Id = 5, Competition = "Tennis", Year = 1969 };

    [TestMethod]
    public void ToStringTest()
    {
        string str = validTrophy.ToString();
        Assert.AreEqual("1, Tennis, 1990", str);
    }

    [TestMethod]
    public void ValidateCompetitionTest()
    {
        validTrophy.ValidateCompetition();
        Assert.ThrowsException<ArgumentNullException>(() => competitionNullTrophy.ValidateCompetition());
        Assert.ThrowsException<ArgumentException>(() => competitionShortTrophy.ValidateCompetition());
    }

    [TestMethod]
    public void ValidateYearTest()
    {
        validTrophy.ValidateYear();
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearToOld.ValidateYear());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearInvalid.ValidateYear());
    }

    [TestMethod()]
    public void ValidateTest()
    {
        validTrophy.Validate();
        Assert.ThrowsException<ArgumentNullException>(() => competitionNullTrophy.Validate());
        Assert.ThrowsException<ArgumentException>(() => competitionShortTrophy.Validate());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearToOld.Validate());
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => yearInvalid.Validate());
    }
}
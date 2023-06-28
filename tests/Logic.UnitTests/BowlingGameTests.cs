using FluentAssertions;

namespace Katas.Logic.UnitTests;

public class BowlingGameTests
{
    private readonly BowlingGame _game;

    public BowlingGameTests()
    {
        _game = new BowlingGame();
    }

    [Fact]
    public void TestGutterGame()
    {
        RollMany(20, 0);
        _game.Score().Should().Be(0);
    }

    private void RollMany(int n, int pins)
    {
        for (var i = 0; i < n; i++)
        {
            _game.Roll(pins);
        }
    }

    [Fact]
    public void TestAllOnes()
    {
        RollMany(20, 1);

        _game.Score().Should().Be(20);
    }

    [Fact]
    public void TestOneSpare()
    {
        RollSpare();
        _game.Roll(5);
        RollMany(17, 0);
        
        _game.Score().Should().Be(20);
    }

    private void RollSpare()
    {
        _game.Roll(8);
        _game.Roll(2);
    }

    [Fact]
    public void TestOneStrike()
    {
        RollStrike();
        _game.Roll(5);
        _game.Roll(3);
        RollMany(16, 0);

        _game.Score().Should().Be(26);
    }

    private void RollStrike()
    {
        _game.Roll(10);
    }

    [Fact]
    public void TestPerfectGame()
    {
        RollMany(12, 10);
        _game.Score().Should().Be(300);
    } 
}
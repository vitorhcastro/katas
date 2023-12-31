namespace Katas.Logic;

public class BowlingGame
{
    private readonly int[] _rolls = new int[21];
    private int _currentRoll;

    public void Roll(int pins)
    {
        _rolls[_currentRoll++] = pins;
    }

    public int Score()
    {
        var score = 0;
        var frameIndex = 0;
        for (var frame = 0; frame < 10; frame++)
        {
            if (IsStrike(frameIndex)) //strike
            {
                score += 10 + StrikeBonus(frameIndex);
                frameIndex++;
            }
            else if (IsSpare(frameIndex))
            {
                score += 10 + SpareBonus(frameIndex);
                frameIndex += 2;
            }
            else
            {
                score += SumOfBallsInFrame(frameIndex);
                frameIndex += 2;
            }
        }

        return score;
    }

    private bool IsStrike(int frameIndex)
    {
        return _rolls[frameIndex] == 10;
    }

    private int StrikeBonus(int frameIndex)
    {
        return _rolls[frameIndex + 1] + _rolls[frameIndex + 2];
    }

    private int SpareBonus(int frameIndex)
    {
        return _rolls[frameIndex + 2];
    }

    private int SumOfBallsInFrame(int frameIndex)
    {
        return _rolls[frameIndex] + _rolls[frameIndex + 1];
    }

    private bool IsSpare(int frameIndex)
    {
        return SumOfBallsInFrame(frameIndex) == 10;
    }
}
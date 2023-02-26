namespace Dart.Model;

public class Round
{
    public int RoundNumber { get; set; }

    public int FirstShot { get; set; }

    public int SecondShot { get; set; }

    public int ThirdShot { get; set; }

    public int Result { get; private set; }

    public Round() { }
    public Round(int roundNumber, int firstShot, int secondShot, int thirdShot)
    {
        RoundNumber = roundNumber;
        FirstShot = firstShot;
        SecondShot = secondShot;
        ThirdShot = thirdShot;
        Result = GetResult();
    }

    public int GetResult()
    {
        return FirstShot + SecondShot + ThirdShot;
    }

    public bool isEmptyRound()
    {
        if (FirstShot == 0 && SecondShot == 0 && ThirdShot == 0)
            return true;
        else return false;
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}, {2}", FirstShot, SecondShot, ThirdShot);
    }
}

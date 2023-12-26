namespace monogametest;

public interface ISideGiving
{
    public Side Side(FlatAngle value);
    // нужно добавить Side.Center
    public (AngleRange Left, AngleRange Right) Side(AngleRange value);
}

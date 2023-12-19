namespace monogametest;

// так как прямая может дать только FlatAngle, в названии можно использовать слово "Angle"
public interface IAngleGiveable
{
    FlatAngle Angle { get; set; }
}

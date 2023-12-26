using System.Collections.Generic;

namespace monogametest;

public interface ISidesGiving
{
    public IEnumerable<Side> Sides(FlatAngle value);
    public IEnumerable<(AngleRange Left, AngleRange Right)> Sides(AngleRange value);
}

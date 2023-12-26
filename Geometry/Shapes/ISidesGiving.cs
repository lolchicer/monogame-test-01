using System.Collections.Generic;

namespace MonogameTest01;

public interface ISidesGiving
{
    public IEnumerable<Side> Sides(FlatAngle value);
    public IEnumerable<(AngleRange Left, AngleRange Right)> Sides(AngleRange value);
}

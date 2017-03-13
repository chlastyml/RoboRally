using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public class TransportBelt : Enviroment
  {
    public TransportBelt(Position position) : base(position) { }

    public TransportBelt(int x, int y, Orientation orientation) : this(new Position(new Coordinates(x,y), orientation)) { }

    public override void Move(Robot robot)
    {
      if (robot != null)
      {
        robot.IsMovedEnvironment = true;
        robot.Move(Orientation, true);
      }
    }
  }
}

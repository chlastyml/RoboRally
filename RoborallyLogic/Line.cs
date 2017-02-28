using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public class Line : Enviroment
  {
    public Line(Position position) : base(position) { }

    public Line(int x, int y, Orientation orientation) : this(new Position(orientation, new Coordinates(x,y))) { }

    public override void Move(Robot robot)
    {
      if (robot != null)
      {
        robot.Move(Orientation);
        robot.IsMovedEnvironment = true;
      }
    }
  }
}

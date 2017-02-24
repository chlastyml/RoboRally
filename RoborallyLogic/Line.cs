using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public class Line : IEnviromentMove
  {
    public Line(Position position)
    {
      Position = position.Copy();
    }

    public Line(int x, int y, Orientation orientation) : this(new Position(orientation, new Coordinates(x,y))) { }

    public ILogicMap Map { get; set; }

    public Position Position { get; set; }
    public int X { get { return Position.Coordinates.X; } }
    public int Y { get { return Position.Coordinates.Y; } }
    public Orientation Orientation { get { return Position.Orientation; } }

    public void Move(Robot robot)
    {
      if (robot != null)
      {
        robot.Move(Orientation);
        robot.IsMovedEnvironment = true;
      }
    }
  }
}

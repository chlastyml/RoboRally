using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public class Target : LogicObject
  {
    public int Count { get; set; }

    public Target(Coordinates coordinates, int count) : base(new Position(coordinates))
    {
      Count = count;
    }

    public virtual void DoSomething(Robot robot)
    {
      if (robot != null)
      {
        robot.StartPosition = (Position) robot.Position.Clone();
      }
    }
  }

  class FinishTarget : Target
  {
    public FinishTarget(Coordinates coordinates, int count) : base(coordinates, count) { }
  }
}

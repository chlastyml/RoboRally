using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public class Goal : LogicObject
  {
    public int Count { get; set; }

    public Goal(Coordinates coordinates, int count) : base(new Position(Orientation.Up, coordinates))
    {
      Count = count;
    }

    public virtual void DoSomething()
    {
      
    }
  }

  class FinishGoal : Goal
  {
    public FinishGoal(Coordinates coordinates, int count) : base(coordinates, count) { }

    public override void DoSomething()
    {
      base.DoSomething();

    }
  }
}

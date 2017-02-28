using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic.Instruction
{
  public interface IInstruction
  {
    int Priority { get; set; }

    void DoInstuction(Robot robot);
  }

  class Move1Forward : IInstruction
  {
    public Move1Forward(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.MoveForward(1);
    }
  }

  class Move2Forward : IInstruction
  {
    public Move2Forward(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.MoveForward(2);
    }
  }

  class Move3Forward : IInstruction
  {
    public Move3Forward(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.MoveForward(3);
    }
  }

  class TurnLeft : IInstruction
  {
    public TurnLeft(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.TurnLeft();
    }
  }

  class TurnRight : IInstruction
  {
    public TurnRight(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.TurnRight();
    }
  }

  class MoveBack : IInstruction
  {
    public MoveBack(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.MoveBack();
    }
  }

  class TurnBack : IInstruction
  {
    public TurnBack(int priority)
    {
      Priority = priority;
    }

    public int Priority { get; set; }

    public void DoInstuction(Robot robot)
    {
      robot.TurnRight();
    }
  }
}

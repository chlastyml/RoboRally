using System;
using System.Collections.Generic;
using System.IO;
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

  public abstract class Instruction : IInstruction
  {
    protected Instruction(int priority)
    {
      Priority = priority;
    }

    public abstract int Priority { get; set; }
    public abstract void DoInstuction(Robot robot);
  }

  public class Move1Forward : Instruction
  {
    public Move1Forward(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.MoveForward(1);
    }
  }

  public class Move2Forward : Instruction
  {
    public Move2Forward(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.MoveForward(2);
    }
  }

  public class Move3Forward : Instruction
  {
    public Move3Forward(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.MoveForward(3);
    }
  }

  public class TurnLeft : Instruction
  {
    public TurnLeft(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.TurnLeft();
    }
  }

  public class TurnRight : Instruction
  {
    public TurnRight(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.TurnRight();
    }
  }

  public class MoveBack : Instruction
  {
    public MoveBack(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.MoveBack();
    }
  }

  public class TurnBack : Instruction
  {
    public TurnBack(int priority) : base(priority) { }

    public override int Priority { get; set; }

    public override void DoInstuction(Robot robot)
    {
      robot.TurnRight();
    }
  }

  public class InstructionHelper
  {
    private static readonly Random Random = new Random();

    public static IInstruction GetRandomInstruction()
    {
      int randomNumber = Random.Next(0, 7);
      switch (randomNumber)
      {
        case 0: return new MoveBack(550);
        case 1: return new Move1Forward(570);
        case 2: return new Move2Forward(600);
        case 3: return new Move3Forward(670);
        case 4: return new TurnLeft(480);
        case 5: return new TurnRight(480);
        case 6: return new TurnBack(480);
      }

      throw new InvalidDataException();
    }
  }
}

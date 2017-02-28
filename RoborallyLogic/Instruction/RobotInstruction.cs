using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic.Instruction
{
  interface IRobotIstruction
  {
    IList<IInstruction> Instructions { get; set; }

    int CurrentInstructionNumber { get; set; }

    void AddInstruction(IInstruction instruction);
    void RemoveInstruction(IInstruction instruction);
    void ResetInstructionSet();

    void DoInstruction(int instractionNumber, Robot robot);
    void DoNextInstruction(Robot robot);
  }

  //public class RobotInstruction : Robot, IRobotIstruction
  //{
  //  public RobotInstruction(int damagedCount, int lifeTokenCount, string name, Position position, bool player = false)
  //    : base(damagedCount, lifeTokenCount, name, position, player)
  //  {
  //    CurrentInstructionNumber = 0;
  //    Instructions = new List<IInstruction>();
  //  }
    
  //  public IList<IInstruction> Instructions { get; set; }
  //  public int CurrentInstructionNumber { get; set; }

  //  public virtual void AddInstruction(IInstruction instruction)
  //  {
  //    Instructions.Add(instruction);
  //  }

  //  public virtual void RemoveInstruction(IInstruction instruction)
  //  {
  //    Instructions.Remove(instruction);
  //  }

  //  public virtual void ResetInstructionSet()
  //  {
  //    CurrentInstructionNumber = 0;
  //    Instructions.Clear();
  //  }

  //  public void DoInstruction(int instractionNumber, Robot robot)
  //  {
  //    Instructions[instractionNumber].DoInstuction(robot);
  //  }

  //  public void DoNextInstruction(Robot robot)
  //  {
  //    Instructions[CurrentInstructionNumber].DoInstuction(robot);
  //  }
  //}
}

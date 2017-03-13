using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic.Instruction
{
  public interface IRobotIstruction
  {
    IList<IInstruction> Instructions { get; set; }

    int CurrentInstructionNumber { get; set; }

    void AddInstruction(IInstruction instruction);
    void RemoveInstruction(IInstruction instruction);
    void ResetInstructionSet();

    void DoInstruction(int instractionNumber);
    void DoNextInstruction();
  }

  public class RobotInstruction : Robot, IRobotIstruction
  {
    public RobotInstruction(string name, Position position) : base(9, 3, name, position, false)
    {
      CurrentInstructionNumber = 0;
      Instructions = new List<IInstruction>();
    }

    public IList<IInstruction> Instructions { get; set; }
    public int CurrentInstructionNumber { get; set; }

    public virtual void AddInstruction(IInstruction instruction)
    {
      Instructions.Add(instruction);
    }

    public virtual void RemoveInstruction(IInstruction instruction)
    {
      Instructions.Remove(instruction);
    }

    public virtual void ResetInstructionSet()
    {
      CurrentInstructionNumber = 0;
      Instructions.Clear();
    }

    public void DoInstruction(int instractionNumber)
    {
      Instructions[instractionNumber].DoInstuction(this);
    }

    public void DoNextInstruction()
    {
      Instructions[CurrentInstructionNumber].DoInstuction(this);
      CurrentInstructionNumber++;
    }
  }
}

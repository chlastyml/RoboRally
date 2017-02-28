using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoborallyLogic
{
  public abstract class Enviroment : LogicObject
  {
    public abstract void Move(Robot robot);

    protected Enviroment(Position position) : base(position)
    {
    }
  }
}

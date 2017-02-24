using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoborallyLogic;

namespace RoboRallyeConsole
{
  class ConsoleRobot : Robot
  {
    public string Symbol { get; set; }

    public ConsoleRobot(string name, string symbol, Position position) : base(name, position)
    {
      Symbol = symbol;
    }
  }
}
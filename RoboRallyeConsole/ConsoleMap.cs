using System;
using System.Linq;
using System.Text;
using RoborallyLogic;

namespace RoboRallyeConsole
{
  class ConsoleMap : Map
  {
    public override void Draw()
    {
      Console.WriteLine(ToStringMap());
    }

    private string ToStringMap()
    {
      StringBuilder sb = new StringBuilder();

      for (int y = 0; y < MaxY; y++)
      {
        for (int x = 0; x < MaxX; x++)
        {
          ConsoleRobot robot = (ConsoleRobot) Robots.FirstOrDefault(r => r.Position.Coordinates.X == x && r.Position.Coordinates.Y == y);

          sb.Append(robot != null ? robot.Symbol : " ");
        }

        sb.AppendFormat("| {0} {1}", GetRobotByIndex(y), Environment.NewLine);
      }
      for (int x = 0; x < MaxX; x++)
      {
        sb.Append("-");
      }
      sb.Append("|");

      return sb.ToString();
    }

    public ConsoleMap(int x, int y) : base(x, y)
    {
    }
  }
}

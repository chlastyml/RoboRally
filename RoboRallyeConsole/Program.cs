using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoborallyLogic;

namespace RoboRallyeConsole
{
  class Program
  {
    private static void Main(string[] args)
    {
      Console.SetWindowPosition(0,0);

      Map map = new ConsoleMap(60, 30);

      Robot robot2 = new ConsoleRobot("Twinky", "X", new Position(Orientation.Up, 10, 10));
      Robot robot = new ConsoleRobot("Hammer", "H", new Position(Orientation.Up, 5, 5));
      robot.Weapon = new BasicWeapon();

      map.AddRobot(robot);
      map.AddRobot(robot2);

      bool ukroky = false;
      string command = string.Empty;
      while (true)
      {
        Console.Clear();
        map.Draw();
        Console.WriteLine("Úkroky: {0}", ukroky);
        Console.WriteLine(command);
        ConsoleKeyInfo key = Console.ReadKey();
        switch (key.Key)
        {
          case ConsoleKey.UpArrow:
            if (ukroky)
            {
              robot.MoveUp();
              command = string.Format("{0} - MOVE UP", robot.Name);
            }
            else
            {
              robot.MoveForward();
              command = string.Format("{0} - MOVE FORWARD", robot.Name);
            }
            break;
          case ConsoleKey.LeftArrow:
            if (ukroky)
            {
              robot.MoveLeft();
              command = string.Format("{0} - MOVE LEFT", robot.Name);
            }
            else
            {
              robot.TurnLeft();
              command = string.Format("{0} - TURN LEFT", robot.Name);
            }
            break;
          case ConsoleKey.RightArrow:
            if (ukroky)
            {
              robot.MoveRight();
              command = string.Format("{0} - MOVE RIGHT", robot.Name);
            }
            else
            {
              robot.TurnRight();
              command = string.Format("{0} - TURN RIGHT", robot.Name);
            }
            break;
          case ConsoleKey.DownArrow:
            if (ukroky)
            {
              robot.MoveDown();
              command = string.Format("{0} - MOVE DOWN", robot.Name);
            }
            else
            {
              robot.MoveBack();
              command = string.Format("{0} - MOVE BACK", robot.Name);
            }
            break;
          case ConsoleKey.N:
            {
              while (true)
              {
                Random nh = new Random();
                Coordinates c = new Coordinates(nh.Next(0, map.MaxX), nh.Next(0, map.MaxY));
                Robot rrr;
                if (!map.TryGetRobot(c, out rrr))
                {
                  Robot r = new ConsoleRobot(nh.Next(0, 100000).ToString(), nh.Next(0, 10).ToString(), new Position(Orientation.Up, c));
                  map.AddRobot(r);
                  command = string.Format("{0} - ADD", r.Name);
                  break;
                }
              }
              break;
            }
          case ConsoleKey.C:
            ukroky = !ukroky;
            break;
          case ConsoleKey.Spacebar:
            robot.Fire();
            command = string.Format("{0} - FIRE", robot.Name);
            break;
          case ConsoleKey.Q:
            robot.Weapon = new BasicWeapon();
            command = string.Format("{0} - BasicWeapon", robot.Name);
            break;
          case ConsoleKey.W:
            robot.Weapon = new Cannon();
            command = string.Format("{0} - Cannon", robot.Name);
            break;
          case ConsoleKey.E:
            robot.Weapon = new Ultimate();
            command = string.Format("{0} - Ultimate", robot.Name);
            break;
        }
      }
    }
  }
}

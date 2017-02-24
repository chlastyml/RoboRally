using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using RoborallyLogic;

namespace Roborallye
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private XamlMap map;

    public MainWindow()
    {
      InitializeComponent();
      GlobalSetting.Rectangle = MainRectangle;
      GlobalSetting.RobotTemplate = RobotTemplateGrid;
      GlobalSetting.WallTemplate = WallGrid;
      GlobalSetting.Line = LineGrid;

      Restart();
    }

    private XamlRobot robot;
    private bool ukroky = false;
    private bool ignoreEnvironment = false;
    private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      string command = string.Empty;

      switch (e.Key)
      {
        case Key.Enter:
          Restart();
          command = "RESTART";
          break;
        case Key.Up:
          if (ukroky)
          {
            robot.Move(Orientation.Up, ignoreEnvironment);
            command = string.Format("{0} - MOVE UP", robot.Name);
          }
          else
          {
            robot.MoveForward();
            command = string.Format("{0} - MOVE FORWARD", robot.Name);
          }
          break;
        case Key.Left:
          if (ukroky)
          {
            robot.Move(Orientation.Left, ignoreEnvironment);
            command = string.Format("{0} - MOVE LEFT", robot.Name);
          }
          else
          {
            robot.TurnLeft();
            command = string.Format("{0} - TURN LEFT", robot.Name);
          }
          break;
        case Key.Right:
          if (ukroky)
          {
            robot.Move(Orientation.Right, ignoreEnvironment);
            command = string.Format("{0} - MOVE RIGHT", robot.Name);
          }
          else
          {
            robot.TurnRight();
            command = string.Format("{0} - TURN RIGHT", robot.Name);
          }
          break;
        case Key.Down:
          if (ukroky)
          {
            robot.Move(Orientation.Down, ignoreEnvironment);
            command = string.Format("{0} - MOVE DOWN", robot.Name);
          }
          else
          {
            robot.MoveBack();
            command = string.Format("{0} - MOVE BACK", robot.Name);
          }
          break;
        case Key.N:
        {
          command = AddRobot();
          break;
        }
        case Key.A: // Wall
        {
          command = AddWall();
          break;
        }
        case Key.L: // Wall
        {
          command = AddLine();
          break;
        }
        case Key.H: // Enviroment
        {
          map.EnviromentMove();
          break;
        }
        case Key.S: // Walls
        {
          ukroky = !ukroky;

          XamlWall wall;
          if (ukroky)
          {
            Coordinates c = new Coordinates(2, 2);
            wall = new XamlWall(c, Orientation.Right, GlobalSetting.WallTemplate.DeepCopy());
          }
          else
          {
            Coordinates c = new Coordinates(3, 2);
            wall = new XamlWall(c, Orientation.Left, GlobalSetting.WallTemplate.DeepCopy());
          }
          map.AddWall(wall);

          command = "NASRAT";
          break;
        }
        case Key.C:
          ukroky = !ukroky;
          break;
        case Key.V:
          ignoreEnvironment = !ignoreEnvironment;
          break;
        case Key.Space:
          robot.Fire();
          command = string.Format("{0} - FIRE", robot.Name);
          break;
        case Key.Q:
          robot.Weapon = new BasicWeapon();
          command = string.Format("{0} - BasicWeapon", robot.Name);
          break;
        case Key.W:
          robot.Weapon = new Cannon();
          command = string.Format("{0} - Cannon", robot.Name);
          break;
        case Key.E:
          robot.Weapon = new Ultimate();
          command = string.Format("{0} - Ultimate", robot.Name);
          break;
        case Key.R:
          robot.Weapon = new Penetrator();
          command = string.Format("{0} - Penetrator", robot.Name);
          break;
        case Key.Escape:
          this.Close();
          return;
      }

      // TODO: Další fáze
      CommandTextBlock.Text = string.Format("{0}{1}{1}{2}", command, Environment.NewLine, map.GetRobotsStats());
    }

    private int startLine = 2;
    private string AddLine()
    {
      XamlLine enviromentMove = new XamlLine(startLine, 5, Orientation.Right, GlobalSetting.Line.DeepCopy());
      map.AddEnviromentalMove(enviromentMove);
      
      startLine++;

      return string.Format("{0} - ADD LINE", enviromentMove);
    }

    private void Restart()
    {
      MainGrid.Children.Clear();
      map = new XamlMap(20, 20, MainGrid);
      robot = new XamlRobot("Twonky", new Position(Orientation.Down, 5, 5), GlobalSetting.RobotTemplate.DeepCopy());
      robot.Player = true;
      map.AddRobot(robot);
    }

    private string AddWall()
    {
      Random nh = new Random();
      Coordinates c = new Coordinates(nh.Next(0, map.MaxX), nh.Next(0, map.MaxY));
      XamlWall wall = new XamlWall(c, new Orientation().Random(), GlobalSetting.WallTemplate.DeepCopy());
      map.AddWall(wall);

      return string.Format("{0} - ADD WALL", wall);      
    }

    private string AddRobot()
    {
      while (true)
      {
        Random nh = new Random();
        Coordinates c = new Coordinates(nh.Next(0, map.MaxX), nh.Next(0, map.MaxY));
        Robot rrr;
        if (!map.TryGetRobot(c, out rrr))
        {
          Robot r = new XamlRobot(nh.Next(0, 100000).ToString(), new Position(Orientation.Up, c), GlobalSetting.RobotTemplate.DeepCopy(true));
          map.AddRobot(r);
          return string.Format("{0} - ADD ROBOT", r.Name);
        }
      }
    }
  }
}

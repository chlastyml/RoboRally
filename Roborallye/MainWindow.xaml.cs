using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using RoborallyLogic;

namespace Roborallye
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private bool ignoreEnvironment;
    private Map map;
    private Robot robot;
    private int startLine = 2;
    private bool ukroky;

    public MainWindow()
    {
      InitializeComponent();
      GlobalSetting.MainGrid = MainGrid;
      GlobalSetting.Rectangle = MainRectangle;
      GlobalSetting.RobotTemplate = RobotTemplateGrid;
      GlobalSetting.WallTemplate = WallGrid;
      GlobalSetting.Line = LineGrid;
      GlobalSetting.GoalTemplate = GoalGrid;
      Restart();
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      string command = string.Empty;

      switch (e.Key)
      {
        case Key.Enter:
          command = Restart();
          break;
        case Key.Up:
          command = Move(Orientation.Up);
          break;
        case Key.Left:
          command = Move(Orientation.Left);
          break;
        case Key.Right:
          command = Move(Orientation.Right);
          break;
        case Key.Down:
          command = Move(Orientation.Down);
          break;
        case Key.N:
          command = AddRobot();
          break;
        case Key.A:
          command = AddWall();
          break;
        case Key.L:
          command = AddLine();
          break;
        case Key.H:
          map.EnviromentMove();
          break;
        case Key.S:
          ukroky = !ukroky;
          command = string.Format("Úkroky: {0}", ukroky);
          break;
        case Key.C:
          ukroky = !ukroky;
          break;
        case Key.V:
          ignoreEnvironment = !ignoreEnvironment;
          command = string.Format("Ignorovat prostředí: {0}", ignoreEnvironment);
          break;
        case Key.Space:
          robot.Fire();
          command = string.Format("{0} - FIRE - {1}", robot.Name, robot.Weapon);
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
          Close();
          return;
      }
      // TODO: Další fáze

      CommandTextBlock.Text = string.Format("{0}{1}{1}{2}", command, Environment.NewLine, map.GetRobotsStats());
    }

    private string Move(Orientation orientation)
    {
      if (ukroky)
      {
        robot.Move(orientation, ignoreEnvironment);
        return string.Format("{0} - MOVE {1}", robot.Name, orientation);
      }

      switch (orientation)
      {
        case Orientation.Up:
          robot.MoveForward();
          return string.Format("{0} - MOVE FORWARD", robot.Name);
        case Orientation.Left:
          robot.TurnLeft();
          return string.Format("{0} - TURN LEFT", robot.Name);
        case Orientation.Right:
          robot.TurnRight();
          return string.Format("{0} - TURN RIGHT", robot.Name);
        case Orientation.Down:
          robot.MoveBack();
          return string.Format("{0} - MOVE BACK", robot.Name);
      }

      throw new Exception();
    }

    private string AddLine()
    {
      Enviroment line = new Line(new Position(Orientation.Right, new Coordinates(startLine, 5)));
      line.Draw = new XamlDraw(GlobalSetting.Line.DeepCopy(), line);
      map.Add(line);

      startLine++;

      return string.Format("{0} - ADD LINE", line);
    }

    private string Restart()
    {
      MainGrid.Children.Clear();
      map = new Map(20, 20);
      map.Draw = new MapXaml(MainGrid);

      robot = RobotFactory.CreateRobot(map.GetFreePosition(), "Twonky", false); //new XamlRobot("Twonky", new Position(Orientation.Down, 5, 5), GlobalSetting.RobotTemplate.DeepCopy());
      robot.Player = true;
      map.Add(robot);
      return "RESTART";
    }

    private string AddWall()
    {
      Random nh = new Random();
      Coordinates c = new Coordinates(nh.Next(0, map.MaxX), nh.Next(0, map.MaxY));
      Wall wall = new Wall(new Position(Orientation.Up.Random(), c));
      wall.Draw = new XamlDraw(GlobalSetting.WallTemplate.DeepCopy(), wall);
      //XamlWall wall = new Xaml(c, new Orientation().Random(), GlobalSetting.WallTemplate.DeepCopy());
      map.AddWall(wall);

      return string.Format("{0} - ADD WALL", wall);
    }

    private string AddRobot()
    {
      Position c = map.GetFreePosition();
      Robot r = RobotFactory.CreateRobot(c, "XXX");
      map.Add(r);
      return string.Format("{0} - ADD ROBOT", r.Name);
    }
  }

  internal static class RobotFactory
  {
    public static Robot CreateRobot(Position position, string name, bool random = true)
    {
      Robot robot = new Robot(name, position);
      robot.Draw = new XamlDraw(GlobalSetting.RobotTemplate.DeepCopy(random), robot);
      return robot;
    }
  }
}
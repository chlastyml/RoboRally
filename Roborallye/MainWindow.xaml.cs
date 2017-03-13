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
      GlobalSetting.TransportBelt = LineGrid;
      GlobalSetting.TargetTemplate = TargetGrid;
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
        case Key.A:
          command = AddRobot();
          break;
        case Key.S:
          command = AddWall();
          break;
        case Key.D:
          command = AddTransportBelt();
          break;
        case Key.F:
          command = AddTarget();
          break;
        case Key.H:
          map.EnviromentMove();
          break;
        case Key.C:
          ukroky = !ukroky;
          command = string.Format("Úkroky: {0}", ukroky);
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
        robot.Move(orientation);
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

    private string AddTransportBelt()
    {
      TransportBelt transportBelt = Factory.CreateTransportBelt(startLine);
      map.Add(transportBelt);

      startLine++;

      return string.Format("{0} - ADD Transport belt", transportBelt);
    }

    private string Restart()
    {
      MainGrid.Children.Clear();
      //map = new Map(20, 20);
      map = new Map(GlobalSetting.MapSizeX, GlobalSetting.MapSizeY);
      map.Draw = new MapXaml(MainGrid);

      robot = Factory.CreateRobot(map.GetRandomPosition(true), "Twonky", false);
      robot.Player = true;
      map.Add(robot);
      return "RESTART";
    }

    private string AddWall()
    {
      Wall wall = Factory.CreateWall(map.GetRandomPosition(false));
      map.Add(wall);

      return string.Format("{0} - ADD WALL", wall);
    }

    private string AddRobot()
    {
      Position c = map.GetRandomPosition(true);
      Robot r = Factory.CreateRobot(map.GetRandomPosition(true), "XXX");
      map.Add(r);
      return string.Format("{0} - ADD ROBOT", r.Name);
    }

    private string AddTarget()
    {
      Target target = Factory.CreateTarget(map.GetRandomPosition(true));
      map.Add(target);

      return string.Format("{0} - ADD Target", target);
    }
  }

  internal static class Factory
  {
    public static Robot CreateRobot(Position position, string name, bool randomColor = true)
    {
      Robot robot = new Robot(name, position);
      robot.Draw = new XamlDraw(GlobalSetting.RobotTemplate.DeepCopy(randomColor), robot);
      return robot;
    }

    public static Wall CreateWall(Position position)
    {
      Wall wall = new Wall(position);
      wall.Draw = new XamlDraw(GlobalSetting.WallTemplate.DeepCopy(), wall);

      return wall;
    }

    public static TransportBelt CreateTransportBelt(int startLine)
    {
      TransportBelt transportBelt = new TransportBelt(new Position(new Coordinates(startLine, 5), Orientation.Right));
      transportBelt.Draw = new XamlDraw(GlobalSetting.TransportBelt.DeepCopy(), transportBelt);
      
      return transportBelt;
    }

    public static Target CreateTarget(Position position)
    {
      Target target = new Target(position.Coordinates, 0);
      target.Draw = new XamlDraw(GlobalSetting.TargetTemplate.DeepCopy(), target);

      return target;
    }
  }
}
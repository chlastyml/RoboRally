using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using RoborallyLogic;
using RoborallyLogic.Instruction;

namespace Roborallye
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private Map map;
    private RobotInstruction robot;
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
      GlobalSetting.KeyHelper = new KeyHelper();
      Restart();
    }

    private bool _instrukce = false;

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
      string command = string.Empty;

      // Obecne
      switch (e.Key)
      {
        case Key.T: command = Restart(); break;
        case Key.A: command = AddRobot(); break;
        case Key.S: command = AddWall(); break;
        case Key.D: command = AddTransportBelt(); break;
        case Key.F: command = AddTarget(); break;
        case Key.H: map.EnviromentMove(); break;
        case Key.C: ukroky = !ukroky; command = string.Format("Úkroky: {0}", ukroky); break;
        case Key.Q: robot.Weapon = new BasicWeapon(); command = string.Format("{0} - BasicWeapon", robot.Name); break;
        case Key.W: robot.Weapon = new Cannon(); command = string.Format("{0} - Cannon", robot.Name); break;
        case Key.E: robot.Weapon = new Ultimate(); command = string.Format("{0} - Ultimate", robot.Name); break;
        case Key.R: robot.Weapon = new Penetrator(); command = string.Format("{0} - Penetrator", robot.Name); break;
        case Key.Escape: if (_tread !=null && _tread.IsAlive) ExecuteInstructions(); CloseApplication(); return;
        case Key.L: _instrukce = !_instrukce; command = "Instrukce : " + _instrukce; break;
        case Key.Enter: command = ExecuteInstructions(); break;
      }


      if(_instrukce)
        switch (e.Key)
        {
          case Key.Up: command = AddPlayerIstruction(Orientation.Up); break;
          case Key.Left: command = AddPlayerIstruction(Orientation.Left); break;
          case Key.Right: command = AddPlayerIstruction(Orientation.Right); break;
          case Key.Down: command = AddPlayerIstruction(Orientation.Down); break;
          case Key.P: command = ExecuteInstructionsAll(); break;
        }
      else
        switch (e.Key)
        {
          case Key.Up: command = Move(Orientation.Up); break;
          case Key.Left: command = Move(Orientation.Left); break;
          case Key.Right: command = Move(Orientation.Right); break;
          case Key.Down: command = Move(Orientation.Down); break;
        }

      // TODO: Další fáze

      CommandTextBlock.Text = string.Format("{0}{1}{1}{2}", command, Environment.NewLine, map.GetRobotsStats());
    }

    private void CloseApplication()
    {
      Close();
      GlobalSetting.KeyHelper.Close();
    }

    private bool _instructTread = true;
    private Thread _tread;

    private string ExecuteInstructionsAll()
    {
      Thread tt = new Thread(ExecuteInstructionsPrivate);
      tt.Start();

      return "Vyhodnocení";
    }

    private string ExecuteInstructions()
    {
      if(_tread == null)
        _tread = new Thread(ExecuteInstructionsNekonecny);
      if (_instructTread)
        _tread.Start();
      else
        _tread.Abort();

      _instructTread = !_instructTread;
      return "Vyhodnocení";
    }

    private void ExecuteInstructionsNekonecny()
    {
      while (true)
      {
        foreach (RobotInstruction robotInstruction in map.Robots.Cast<RobotInstruction>())
        {
          robotInstruction.CurrentInstructionNumber = 0;
          robotInstruction.Instructions.Clear();
          robotInstruction.AddInstruction(InstructionHelper.GetRandomInstruction());
        }

        map.DoNextInstruction();
        map.EnviromentMove();
        map.Fire();

        CommandTextBlock.Dispatcher.Invoke(
          () =>
          {
            CommandTextBlock.Text = string.Format("{0}", map.GetRobotsStats());
          }
          );

        Thread.Sleep(500);
      }
    }

    private void ExecuteInstructionsPrivate()
    {
      for (int i = 0; i < robot.Instructions.Count; i++)
      {
        map.DoNextInstruction();
        map.EnviromentMove();
        map.Fire();
        Thread.Sleep(500);
      }


      foreach (RobotInstruction robotInstruction in map.Robots.Cast<RobotInstruction>())
      {
        robotInstruction.CurrentInstructionNumber = 0;
        robot.Instructions.Clear();
      }
    }

    private string AddPlayerIstruction(Orientation orientation)
    {
      foreach (RobotInstruction robotInstruction in map.Robots.Where(r => !r.Equals(robot)).Cast<RobotInstruction>())
      {
        robotInstruction.AddInstruction(InstructionHelper.GetRandomInstruction());
      }

      switch (orientation)
      {
          case Orientation.Up: robot.AddInstruction(new Move1Forward(1000)); return "Move1Forward";
          case Orientation.Down: robot.AddInstruction(new MoveBack(1000)); return "MoveBack";
          case Orientation.Left: robot.AddInstruction(new TurnLeft(1000)); return "TurnLeft";
          case Orientation.Right: robot.AddInstruction(new TurnRight(1000)); return "TurnRight";
      }

      throw new NotSupportedException();
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

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      GlobalSetting.KeyHelper.OpenWindow();
    }
  }

  internal static class Factory
  {
    public static RobotInstruction CreateRobot(Position position, string name, bool randomColor = true)
    {
      RobotInstruction robot = new RobotInstruction(name, position);
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
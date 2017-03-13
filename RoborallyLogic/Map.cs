using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RoborallyLogic.Instruction;

namespace RoborallyLogic
{
  public class Map : ILogicMap
  {
    private IMapDraw _draw;

    public Map(int x, int y)
    {
      Walls = new List<Wall>();
      Robots = new List<Robot>();
      Enviroments = new List<Enviroment>();
      Targets = new List<Target>();
      MaxX = x;
      MaxY = y;
    }

    public IList<Robot> Robots { get; private set; }
    protected IList<Wall> Walls { get; set; }
    protected IList<Enviroment> Enviroments { get; set; }
    protected IList<Target> Targets { get; set; }

    public IMapDraw Draw
    {
      get { return _draw; }
      set
      {
        _draw = value;
        _draw.Inicializace(MaxX, MaxY);
      }
    }

    public int MaxX { get; set; }
    public int MaxY { get; set; }

    public virtual void Add(LogicObject logicObject)
    {
      if (logicObject == null)
      {
        throw new ArgumentNullException();
      }

      // Zda přidáváme robota
      if (logicObject is Robot)
      {
        AddRobot((Robot)logicObject);
      }

      // Zda přidáváme zeď
      if (logicObject is Wall)
      {
        AddWall((Wall) logicObject);
      }

      // Zda přidáváme prvky prostředí
      if (logicObject is Enviroment)
      {
        AddEnviromental((Enviroment) logicObject);
      }

      // Zda přidáváme cíl
      if (logicObject is Target)
      {
        AddTarget((Target) logicObject);
      }
    }

    public void Remove(LogicObject logicObject)
    {
      if (logicObject is Robot)
      {
        RemoveRobot((Robot) logicObject);
      }

      if (logicObject is Wall)
      {
        RemoveWall((Wall) logicObject);
      }

      if (logicObject is Enviroment)
      {
        RemoveEnviromental((Enviroment) logicObject);
      }

      if (logicObject is Target)
      {
        RemoveTarget((Target) logicObject);
      }

      logicObject.Remove();
    }
    
    public bool TryGetRobot(Coordinates coordinates, out Robot robot)
    {
      robot = Robots.FirstOrDefault(r => r.Position.Coordinates.Equals(coordinates));
      return robot != null;
    }

    public bool MovingRobot(Coordinates newCoordinates, Orientation orientation, bool isEnviromental = false)
    {
      //if (ignoreEnviroment) return IsOnMap(newCoordinates);

      Wall wall = Walls.FirstOrDefault(w =>
        (w.Coordinates.Equals(newCoordinates) &&
         w.Coordinates.GetOneNearby(w.Orientation).Equals(newCoordinates.GetOneNearby(orientation.Oposite()))) ||
        (w.Coordinates.GetOneNearby(w.Orientation).Equals(newCoordinates) &&
         w.Coordinates.Equals(newCoordinates.GetOneNearby(orientation.Oposite()))));

      if (wall != null) return false;

      Robot robot;
      if (TryGetRobot(newCoordinates, out robot))
        return robot.Move(orientation, isEnviromental);
      return IsOnMap(newCoordinates);
    }

    private bool IsOnMap(Coordinates coordinates)
    {
      if (0 <= coordinates.X && coordinates.X < MaxX && 0 <= coordinates.Y && coordinates.Y < MaxY)
        return true;
      return false;
    }

    public Position GetRandomPosition(bool mustBeFreePosition)
    {
      // TODO: optimalizovat
      while (true)
      {
        Random nh = new Random();
        Coordinates coordinates = new Coordinates(nh.Next(0, MaxX), nh.Next(0, MaxY));
        if (GetRobotByCoordinates(coordinates) == null || !mustBeFreePosition)
        {
          return new Position(coordinates, Helper.RandomOrientation);
        }
      }
    }

    public Robot GetRobotFromRobotOnLine(Position position, int wallPenetration = 0)
    {
      // Výpočet souřadnic dalšího kontrolovaného pole
      Position nextPosition = position.NextPosition();

      //Kontrola jsou nové souřadnice ještě v poli
      if (!IsOnMap(nextPosition.Coordinates))
      {
        return null;
      }
      Wall wall = GetWallByCoordinates(nextPosition.Coordinates);
      if (wall != null)
      {
        if (wall.Orientation.Equals(position.Orientation.Oposite()))
        {
          if (wallPenetration <= 0)
          {
            return null;
          }
          return GetRobotFromRobotOnLine(nextPosition, wallPenetration - 1);
        }
        if (wall.Orientation.Equals(position.Orientation))
        {
          if (wallPenetration <= 0)
          {
            return GetRobotByCoordinates(nextPosition.Coordinates);
          }
          return GetRobotFromRobotOnLine(nextPosition, wallPenetration - 1);
        }
      }
      return GetRobotByCoordinates(nextPosition.Coordinates) ?? GetRobotFromRobotOnLine(nextPosition, wallPenetration);
    }

    private void AddWall(Wall wall)
    {
      wall.Map = this;
      Walls.Add(wall);
      wall.Add();
    }

    private void AddRobot(Robot robot)
    {
      robot.Map = this;
      Robots.Add(robot);
      robot.Add();
    }

    private void RemoveRobot(Robot robot)
    {
      Robots.Remove(robot);
      robot.Remove();
    }

    private void RemoveWall(Wall wall)
    {
      Walls.Remove(wall);
      wall.Remove();
    }

    public void EnviromentMove()
    {
      foreach (Robot robot in Robots)
      {
        robot.IsMovedEnvironment = false;
      }

      foreach (Enviroment enviroment in Enviroments)
      {
        Robot robot;
        
        if(TryGetRobot(enviroment.Coordinates, out robot) && !robot.IsMovedEnvironment)
        {
          enviroment.Move(robot);
        }
      }
    }

    private void AddEnviromental(Enviroment enviroment)
    {
      enviroment.Map = this;
      Enviroments.Add(enviroment);
      enviroment.Add(true);
    }

    private void RemoveEnviromental(Enviroment enviroment)
    {
      Enviroments.Remove(enviroment);
      enviroment.Remove();
    }

    private void AddTarget(Target target)
    {
      target.Map = this;
      Targets.Add(target);
      target.Add();
    }

    private void RemoveTarget(Target target)
    {
      Targets.Remove(target);
      target.Remove();
    }

    public void UpdateRobots()
    {
      foreach (Target target in Targets)
      {
        target.DoSomething(Robots.FirstOrDefault(robot => robot.Coordinates.Equals(target.Coordinates)));
      }
    }

    public bool IsSomeRobotOnPosition(Coordinates coordinates)
    {
      return Robots.Any(robot => robot.Coordinates.Equals(coordinates));
    }

    private Wall GetWallByCoordinates(Coordinates nextCoordinates)
    {
      return Walls.FirstOrDefault(w => w.Coordinates.Equals(nextCoordinates));
    }

    private Robot GetRobotByCoordinates(Coordinates nextCoordinates)
    {
      return Robots.FirstOrDefault(r => r.Position.Coordinates.Equals(nextCoordinates));
    }

    public string GetRobotsStats()
    {
      StringBuilder sb = new StringBuilder();

      if (Robots != null && Robots.Count != 0)
      {
        sb.AppendFormat("{0}{1}", string.Format("ROBOTS:{0}{1}{0}", Environment.NewLine, string.Join(Environment.NewLine, Robots.Select(r => r.ToString()))), Environment.NewLine);
      }

      if (Walls != null && Walls.Count != 0)
      {
        sb.AppendFormat("{0}{1}", string.Format("WALLS:{0}{1}{0}", Environment.NewLine, string.Join(Environment.NewLine, Walls.Select(r => r.ToString()))), Environment.NewLine);
      }

      if (Enviroments != null && Enviroments.Count != 0)
      {
        sb.AppendFormat("{0}{1}", string.Format("ENVIROMENTALS:{0}{1}{0}", Environment.NewLine, string.Join(Environment.NewLine, Enviroments.Select(r => r.ToString()))), Environment.NewLine);
      }

      if (Targets != null && Targets.Count != 0)
      {
        sb.AppendFormat("{0}{1}", string.Format("TARGETS:{0}{1}{0}", Environment.NewLine, string.Join(Environment.NewLine, Targets.Select(r => r.ToString()))), Environment.NewLine);
      }


      return sb.ToString();
    }

    public void Fire()
    {
      IList<Robot> robotsCopy = new List<Robot>(Robots.Count);
      foreach (Robot robot in Robots)
      {
        robotsCopy.Add(robot);
      }

      foreach (Robot robot in robotsCopy)
      {
        robot.Fire();
      }
    }

    public void DoNextInstruction()
    {
      foreach (RobotInstruction robotInstruction in Robots.Cast<RobotInstruction>())
      {
        robotInstruction.DoNextInstruction();
      }
    }
  }
}
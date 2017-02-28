using System;
using System.Collections.Generic;
using System.Linq;

namespace RoborallyLogic
{
  public class Map : ILogicMap
  {
    private IMapDraw _draw;

    public Map(int x, int y)
    {
      Walls = new List<Wall>();
      Robots = new List<Robot>();
      EnviromentMoves = new List<Enviroment>();
      Goals = new List<Goal>();
      MaxX = x;
      MaxY = y;
    }

    protected IList<Robot> Robots { get; set; }
    protected IList<Wall> Walls { get; set; }
    protected IList<Enviroment> EnviromentMoves { get; set; }
    protected IList<Goal> Goals { get; set; }

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
      if (logicObject is Robot)
      {
        AddRobot((Robot) logicObject);
      }

      if (logicObject is Wall)
      {
        AddWall((Wall) logicObject);
      }

      if (logicObject is Enviroment)
      {
        AddEnviromental((Enviroment) logicObject);
      }

      if (logicObject is Goal)
      {
        AddGoal((Goal) logicObject);
      }

      logicObject.Add();
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

      if (logicObject is Goal)
      {
        RemoveGoal((Goal) logicObject);
      }

      logicObject.Remove();
    }

    public string GetRobotByIndex(int i)
    {
      return i < Robots.Count ? Robots[i].ToString() : string.Empty;
    }

    public bool TryGetRobot(Coordinates coordinates, out Robot robot)
    {
      robot = Robots.FirstOrDefault(r => r.Position.Coordinates.Equals(coordinates));
      return robot != null;
    }

    public bool MovingRobot(Coordinates newCoordinates, Orientation orientation, bool ignoreEnviroment = false)
    {
      if (ignoreEnviroment) return IsOnMap(newCoordinates);

      Wall wall = Walls.FirstOrDefault(w =>
        (w.Coordinates.Equals(newCoordinates) && w.Coordinates.GetOneNearby(w.Orientation).Equals(newCoordinates.GetOneNearby(orientation.Oposite()))) ||
        (w.Coordinates.GetOneNearby(w.Orientation).Equals(newCoordinates) && w.Coordinates.Equals(newCoordinates.GetOneNearby(orientation.Oposite()))));

      if (wall != null) return false;

      Robot robot;
      if (TryGetRobot(newCoordinates, out robot))
        return robot.Move(orientation);
      return IsOnMap(newCoordinates);
    }

    public bool IsOnMap(Coordinates coordinates)
    {
      if (0 <= coordinates.X && coordinates.X < MaxX && 0 <= coordinates.Y && coordinates.Y < MaxY)
        return true;
      return false;
    }

    public Position GetFreePosition()
    {
      // TODO: optimalizovat
      while (true)
      {
        Random nh = new Random();
        Coordinates c = new Coordinates(nh.Next(0, MaxX), nh.Next(0, MaxY));
        if (GetRobotByCoordinates(c) == null)
        {
          return new Position(Orientation.Up.Random(), c);
        }
      }
    }

    public Robot GetRobotFromRobot(Position position, int wallPenetration = 0)
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
          return GetRobotFromRobot(nextPosition, wallPenetration - 1);
        }
        if (wall.Orientation.Equals(position.Orientation))
        {
          if (wallPenetration <= 0)
          {
            return GetRobotByCoordinates(nextPosition.Coordinates);
          }
          return GetRobotFromRobot(nextPosition, wallPenetration - 1);
        }
      }
      return GetRobotByCoordinates(nextPosition.Coordinates) ?? GetRobotFromRobot(nextPosition, wallPenetration);
    }

    public virtual void AddWall(Wall wall)
    {
      wall.Map = this;
      Walls.Add(wall);

      wall.Add();
    }

    private void AddRobot(Robot robot)
    {
      robot.Map = this;
      Robots.Add(robot);
    }

    private void RemoveRobot(Robot robot)
    {
      Robots.Remove(robot);
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

      foreach (Enviroment enviromentMove in EnviromentMoves)
      {
        enviromentMove.Move(
          Robots.FirstOrDefault(
            r => r.Position.Coordinates.Equals(enviromentMove.Position.Coordinates) && !r.IsMovedEnvironment));
      }
    }

    private void AddEnviromental(Enviroment enviroment)
    {
      enviroment.Map = this;
      EnviromentMoves.Add(enviroment);
    }

    private void RemoveEnviromental(Enviroment enviroment)
    {
      EnviromentMoves.Remove(enviroment);
    }

    private void AddGoal(Goal goal)
    {
      goal.Map = this;
      Goals.Add(goal);
    }

    private void RemoveGoal(Goal goal)
    {
      Goals.Remove(goal);
    }

    public void UpdateRobots()
    {
      foreach (Goal goal in Goals)
      {
      }
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
      return string.Format("{0}", string.Join(Environment.NewLine, Robots.Select(r => r.ToString())));
    }
  }
}
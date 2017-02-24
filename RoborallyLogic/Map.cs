using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoborallyLogic
{
  public class Map : ILogicMap
  {
    public Map(int x, int y)
    {
      Walls = new List<Wall>();
      Robots = new List<Robot>();
      EnviromentMoves = new List<IEnviromentMove>();
      MaxX = x;
      MaxY = y;
    }

    protected IList<Robot> Robots { get; set; }
    protected IList<Wall> Walls { get; set; }
    protected IList<IEnviromentMove> EnviromentMoves { get; set; }

    public int MaxX { get; set; }
    public int MaxY { get; set; }
    
    public virtual void AddRobot(Robot robot)
    {
      robot.Map = this;
      Robots.Add(robot);
    }

    public virtual void Draw() { }

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

    public virtual void AddWall(Wall wall)
    {
      wall.Map = this;
      Walls.Add(wall);
    }

    public virtual void RemoveWall(Wall wall)
    {
      Walls.Remove(wall);
    }

    public void EnviromentMove()
    {
      foreach (Robot robot in Robots)
      {
        robot.IsMovedEnvironment = false;
      }

      foreach (IEnviromentMove enviromentMove in EnviromentMoves)
      {
        enviromentMove.Move(Robots.FirstOrDefault(r => r.Position.Coordinates.Equals(enviromentMove.Position.Coordinates) && !r.IsMovedEnvironment));
      }
    }


    public virtual void AddEnviromentalMove(IEnviromentMove enviromentMove)
    {
      enviromentMove.Map = this;
      EnviromentMoves.Add(enviromentMove);
    }

    public virtual void RemoveEnviromentalMove(IEnviromentMove enviromentMove)
    {
      EnviromentMoves.Remove(enviromentMove);
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
      Wall wall = this.GetWallByCoordinates(nextPosition.Coordinates);
      if (wall != null)
      {
        if (wall.Orientation.Equals(position.Orientation.Oposite()))
        {
          if (wallPenetration <= 0)
          {
            return null;
          }
          else
          {
            return GetRobotFromRobot(nextPosition, wallPenetration - 1);
          }
        }
        if (wall.Orientation.Equals(position.Orientation))
        {
          if (wallPenetration <= 0)
          {
            return GetRobotByCoordinates(nextPosition.Coordinates);
          }
          else
          {
            return GetRobotFromRobot(nextPosition, wallPenetration - 1);
          }
        }
      }
      return GetRobotByCoordinates(nextPosition.Coordinates) ?? GetRobotFromRobot(nextPosition, wallPenetration);
    }

    private Wall GetWallByCoordinates(Coordinates nextCoordinates)
    {
      return Walls.FirstOrDefault(w => w.Coordinates.Equals(nextCoordinates));
    }

    private Robot GetRobotByCoordinates(Coordinates nextCoordinates)
    {
      return Robots.FirstOrDefault(r => r.Position.Coordinates.Equals(nextCoordinates));
    }

    public virtual void RemoveRobot(Robot robot)
    {
      Robots.Remove(robot);
    }
  }
}
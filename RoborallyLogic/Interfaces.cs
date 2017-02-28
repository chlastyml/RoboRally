using System.Collections.Generic;
using System.ComponentModel;

namespace RoborallyLogic
{
  public interface ILogicMap
  {
    int MaxX { get; set; }
    int MaxY { get; set; }

    string GetRobotByIndex(int i);
    bool TryGetRobot(Coordinates coordinates, out Robot robot);
    bool MovingRobot(Coordinates newCoordinates, Orientation orientation, bool ignoredEnviroment = false);
    bool IsOnMap(Coordinates coordinates);
    Robot GetRobotFromRobot(Position position, int wallPenetration = 0);

    void Add(LogicObject logicObject);
    void Remove(LogicObject logicObject);

    //void AddRobot(Robot robot);
    //void RemoveRobot(Robot robot);

    //void AddWall(Wall wall);
    //void RemoveWall(Wall wall);

    //void EnviromentMove();
    //void AddEnviromentalMove(IEnviroment enviromentMove);
    //void RemoveEnviromentalMove(IEnviroment enviromentMove);

    //void AddGoal(Goal goal);
    //void RemoveGoal(Goal goal);
    //void UpdateRobots();

    Position GetFreePosition();
  }

  public interface IMapDraw
  {
    void Inicializace(int x, int y);
  }

  public interface IDraw
  {
    void SynchronizacePosition();

    void Add();
    void Remove();
  }

  public interface IPosition
  {
    Position Position { get; }
  }

  public interface IWeapon
  {
    void Fire(ILogicMap map, Robot robot);
  }

  interface IFireAble
  {
    IWeapon Weapon { get; set; }

    void Fire();
    void TakeDamage();
    void TakeDamage(int damage);
  }

  internal interface IMoveAble
  {
    void MoveUp();
    void MoveLeft();
    void MoveDown();
    void MoveRight();
  }
}
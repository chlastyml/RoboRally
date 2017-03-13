using System.Collections.Generic;
using System.ComponentModel;

namespace RoborallyLogic
{
  public interface ILogicMap
  {
    int MaxX { get; set; }
    int MaxY { get; set; }

    bool TryGetRobot(Coordinates coordinates, out Robot robot);
    bool MovingRobot(Coordinates newCoordinates, Orientation orientation, bool isEnviromental = false);
    Robot GetRobotFromRobotOnLine(Position position, int wallPenetration = 0);

    void Add(LogicObject logicObject);
    void Remove(LogicObject logicObject);

    Position GetRandomPosition(bool mustBeFreePosition = true);
    void UpdateRobots();
    bool IsSomeRobotOnPosition(Coordinates coordinates);
  }

  public interface IMapDraw
  {
    void Inicializace(int x, int y);
  }

  public interface IDraw
  {
    void SynchronizacePosition();

    void Add(bool isInsert = false);
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
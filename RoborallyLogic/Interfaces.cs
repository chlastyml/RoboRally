using System.Collections.Generic;

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

    void AddRobot(Robot robot);
    void RemoveRobot(Robot robot);

    void AddWall(Wall wall);
    void RemoveWall(Wall wall);

    void EnviromentMove();
    void AddEnviromentalMove(IEnviromentMove enviromentMove);
    void RemoveEnviromentalMove(IEnviromentMove enviromentMove);
  }

  public interface IDraw
  {
    ILogicMap LogicMap { get; set; }
    void Draw();
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

  public interface IEnviromentMove
  {
    ILogicMap Map { get; set; }

    Position Position { get; set; }
    int X { get; }
    int Y { get; }
    Orientation Orientation { get; }

    void Move(Robot robot);
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoborallyLogic
{
  public class Ultimate : IWeapon
  {
    public void Fire(ILogicMap map, Robot robot)
    {
      Robot robotOnTarget = map.GetRobotFromRobot(robot.Position);
      if (robotOnTarget != null)
      {
        robotOnTarget.TakeDamage(9);
      }
    }
  }

  public class BasicWeapon : IWeapon
  {
    public void Fire(ILogicMap map, Robot robot)
    {
      Robot robotOnTarget = map.GetRobotFromRobot(robot.Position);
      if (robotOnTarget != null)
      {
        robotOnTarget.TakeDamage();
      }
    }
  }

  public class Cannon : IWeapon
  {
    public void Fire(ILogicMap map, Robot robot)
    {
      Robot robotOnTarget = map.GetRobotFromRobot(robot.Position);
      if (robotOnTarget != null)
      {
        robotOnTarget.TakeDamage();
        robotOnTarget.Move(robot.Orientation);
      }
    }
  }

  public class Penetrator : IWeapon
  {
    public void Fire(ILogicMap map, Robot robot)
    {
      Robot robotOnTarget = map.GetRobotFromRobot(robot.Position, 1);
      if (robotOnTarget != null)
      {
        robotOnTarget.TakeDamage(3);
      }
    }
  }
}
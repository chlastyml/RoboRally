using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using RoborallyLogic;
using Orientation = RoborallyLogic.Orientation;

namespace Roborallye
{
  interface IXamlDraw
  {
    Grid Template { get; set; }
    void SynchronizacePosition();
  }

  class XamlRobot : Robot
  {
    public Grid Template { get; set; }

    public XamlRobot(string name, Position position, Grid template) : base(name, position)
    {
      Template = template;
    }

    public void SynchronizacePosition()
    {
      ActualizaceMargin();
      ActualizaceOrientation();
    }

    private void ActualizaceMargin()
    {
      Template.Margin = new Thickness(GlobalSetting.Rectangle.Width * X, GlobalSetting.Rectangle.Height * Y, 0, 0);
    }

    private void ActualizaceOrientation()
    {
      switch (Orientation)
      {
          case Orientation.Up:
          Template.LayoutTransform = new RotateTransform(-180);
          break;
          case Orientation.Left:
          Template.LayoutTransform = new RotateTransform(90);
          break;
          case Orientation.Down:
          Template.LayoutTransform = new RotateTransform(0);
          break;
          case Orientation.Right:
          Template.LayoutTransform = new RotateTransform(-90);
          break;
      }
    }

    public override bool Move(Orientation orientation, bool ignoreEnvironment = false)
    {
      if (base.Move(orientation, ignoreEnvironment))
      {
        ActualizaceMargin();
        return true;
      }
      return false;
    }

    public override void TurnLeft()
    {
      base.TurnLeft();

      ActualizaceOrientation();
    }

    public override void TurnRight()
    {
      base.TurnRight();

      ActualizaceOrientation();
    }
  }
}

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
  class XamlWall : Wall, IXamlDraw
  {
    public XamlWall(Coordinates coordinates, Orientation orientation, Grid wallTemplate) : base(coordinates, orientation)
    {
      Template = wallTemplate;
    }

    public Grid Template { get; set; }

    public void SynchronizacePosition()
    {
      Template.Margin = new Thickness(GlobalSetting.Rectangle.Width * X, GlobalSetting.Rectangle.Height * Y, 0, 0);

      switch (Orientation)
      {
        case Orientation.Up:
          Template.LayoutTransform = new RotateTransform(270);
          break;
        case Orientation.Left:
          Template.LayoutTransform = new RotateTransform(180);
          break;
        case Orientation.Down:
          Template.LayoutTransform = new RotateTransform(90);
          break;
        case Orientation.Right:
          Template.LayoutTransform = new RotateTransform(0);
          break;
      }
    }
  }
}

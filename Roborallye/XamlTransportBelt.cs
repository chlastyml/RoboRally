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
  class XamlTransportBelt : TransportBelt, IXamlDraw
  {
    public XamlTransportBelt(Position position) : base(position) { }

    public XamlTransportBelt(int x, int y, Orientation orientation, Grid deepCopy) : base(x, y, orientation)
    {
      Template = deepCopy;
    }

    public Grid Template { get; set; }

    public override void SynchronizacePosition()
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

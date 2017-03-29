using System.Text;

namespace Roborallye
{
  class KeyHelper
  {
    private StatusWindow StatusWindow { get; set; }

    private void Redraw()
    {
      StringBuilder info = new StringBuilder();

      info.AppendLine("T - Restart");
      info.AppendLine("A - AddRobot");
      info.AppendLine("S - AddWall");
      info.AppendLine("D - TransportBelt");
      info.AppendLine("F - AddTaget");
      info.AppendLine("H - Enviroment move");

      info.AppendLine("C - Ukroky");
      info.AppendLine("Q - Basic weapon");
      info.AppendLine("W - Cannon");
      info.AppendLine("E - Ultimate");
      info.AppendLine("R - Penetrator");

      info.AppendLine("Escape - Close");
      info.AppendLine("Enter - Vyhodnot instrukce všechny");
      info.AppendLine("L - Instrukce");
      info.AppendLine("P - Vyhodnot instrukci (pokud žadná není tak random)");

      StatusWindow.MainTextBox.Text = info.ToString();
    }

    public void OpenWindow()
    {
      if (StatusWindow == null)
      {
        StatusWindow = new StatusWindow();
        StatusWindow.Closed += (sender, args) => this.StatusWindow = null;
        StatusWindow.Show();
        Redraw();
      }
    }

    public void Close()
    {
      if (StatusWindow != null)
      {
        StatusWindow.Close();
      }
    }
  }
}

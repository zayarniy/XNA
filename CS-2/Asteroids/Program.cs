using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main()
        {
            Form form = new Form();
            form.Width = 1240;
            form.Height = 720;
            form.Show();
            Game.Init(form);
            Application.Run(form);
        }
    }
}

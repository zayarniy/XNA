using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Asteroids.Objects;

namespace Asteroids
{
    static class Game
    {
        static event Action<string> Log;
        static BufferedGraphicsContext context;
        static public BufferedGraphics Buffer { get; private set; }

        static public Random Random { get; } = new Random();

        static private Image background = Image.FromFile("Images\\fon.jpg");
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        static private BaseObject[] baseObjects = new BaseObject[20];
        static private Timer timer = new Timer();
        //static private Bullet bullet;
        static private List<Bullet> bullets;
        static private Ship ship;

        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            form.MouseMove += Form_MouseMove;
            form.MouseClick += Form_MouseClick;
            form.KeyDown += Form_KeyDown;
            Log += Game_LogToConsole;
            Bullet.Die += Bullet_Die;
            
        }

        private static void Bullet_Die(Bullet obj)
        {
           bullets.Remove(obj);
        }

        private static void Game_LogToConsole(string message)
        {
            Console.WriteLine(message);
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Up)
            {
                ship.Up();
            }
            
        }

        private static void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                bullets.Add(new Bullet(new Point(ship.Rect.X + 20, ship.Rect.Y + 10), new Point(5), new Size(10, 10)));

            }
        }

        private static void Form_MouseMove(object sender, MouseEventArgs e)
        {
            ship.Update(e.Location);
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static private void Load()
        {
            for (int i = 0; i < 10; i++)
                baseObjects[i] = new Asteroid(new Point(Random.Next(0,Width),Random.Next(0,Height)), new Point(-Random.Next(1,20), 0), new Size(i + 50, i + 50));
            for (int i = 10; i < 20; i++)
                baseObjects[i] = new Star(new Point(Random.Next(0, Width), Random.Next(0, Height)), new Point(-Random.Next(1, 20), 0), new Size(i + 1, i + 1));
            bullets = new List<Bullet>();            
            //new Bullet(new Point(0, 200), new Point(5, 0), new Size(10, 4));
            ship = new Ship(new Point(0,300));
        }

        static public void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(background, 0, 0);
            Buffer.Graphics.DrawString("Life:", SystemFonts.DefaultFont, Brushes.White, 0, 0);
            //Buffer.Graphics.DrawRectangle(Pens.White, 10, 10, 40, 60);
            foreach (BaseObject baseObject in baseObjects)
            {
                baseObject?.Draw();
                //if (baseObject is BaseObject) baseObject.Draw();
                //if (baseObject is Star) (baseObject as Star).Draw();
            }
            foreach(Bullet bullet in bullets) 
                bullet.Draw();
            ship.Draw();
            Buffer.Render();
        }

        static public void Update()
        {
            //foreach (BaseObject baseObject in baseObjects)
            for (int i = 0; i < bullets.Count; i++)
                bullets[i].Update();
                

            for (int i=0;i<baseObjects.Length;i++)
            {
                baseObjects[i]?.Update();
                for(int j=0;j<bullets.Count;j++)
                if (baseObjects[i]!=null && baseObjects[i] is Asteroid && baseObjects[i].Collision(bullets[j]))
                {
                    Log.Invoke("Asteroid number " + i + " clashed");
                    baseObjects[i] = null;
                        bullets.RemoveAt(j);
                        j--;
                }
            }

        }
    }
}

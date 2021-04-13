using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids.Objects
{
    interface ICollision
    {
        Rectangle Rect { get; }

        bool Collision(ICollision obj);
    }

    abstract class BaseObject: ICollision
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public Rectangle Rect => new Rectangle(Pos, Size);


        public BaseObject(Point pos, Point dir, Size size)
        {
            this.Pos = pos;
            this.Dir = dir;
            this.Size = size;
        }

        abstract public void Update();

        abstract public void Draw();


        public bool Collision(ICollision obj)
        {
            return this.Rect.IntersectsWith(obj.Rect);
        }
    }

    class Star:BaseObject
    {

        static Image Image { get; } = Image.FromFile("Images\\star.png");

        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {
           
        }

        public override void Update()
        {
            Pos.X += Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + 20;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y);
            //Game.Buffer.Graphics.DrawImage(Image, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

    }

    class Asteroid:BaseObject
    {
        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Update()
        {
            Pos.X += Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + 20;
        }


        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Gray, this.Rect);
        }

    }

    class Bullet:BaseObject
    {
        static public event Action<Bullet> Die;

        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }
        public override void Update()
        {
            Pos.X += Dir.X;
            if (Pos.X > Game.Width) Die?.Invoke(this);
        }


        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.Gray, this.Rect);
        }

        public void Reset()
        {
            Pos.X = 0;
            Pos.Y = Game.Random.Next(0, Game.Height);
        }

    }

    class Ship:BaseObject
    {
        Image image = Image.FromFile("Images\\ship.png");

        public Ship(Point pos) : base(pos, new Point(0,0), new Size(0,0))
        {
            this.Size = new Size(image.Width, image.Height);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(image, Pos);
        }

        public override void Update()
        {
            
        }

        public void Update(Point pos)
        {
            Pos = pos;
        }

        public void Up()
        {
            Pos.Y -= 5;
        }


    }
}

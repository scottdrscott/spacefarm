using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
/* this code is a stub to make a copy 
 * of the game as implemented on a pdp10 which was in the stanford coffeehouse from about 1970
 * was orginally from MIT
 * The coffehouse version had joysticks that allowed the players to rotate their ships 
 * the ships could fire 20 torpedos per game and the torpedos could anhilate each other or explode a ship
 * the ship had a deceleartion when firing torpedoes. I can't recall ibut I dont think torpedos were effected by gravity?
 *              http://infolab.stanford.edu/pub/voy/museum/galaxy.html
 *   SAB 
 * */

namespace spaceFarm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PointF shipPos = new PointF(122, 122);        
        PointF shipV = new PointF(.1f, .3f);
        PointF shipA = new PointF(.000001f, .000003f);
        double theta = 20;
        PointF[] ship = null;
        internal PointF FF(PointF a) { return new PointF(-a.X, a.Y); }

        private void Form1_Load(object sender, EventArgs ee)
        {
            PointF c = new PointF(0, 20); PointF C = FF(c);
            PointF d = new PointF(3, 15); PointF D = FF(d);
            PointF e = new PointF(3, 4); PointF E = FF(e);
            PointF f = new PointF(5, 2); PointF F = FF(f);
            PointF g = new PointF(5, -2); PointF G = FF(g);
            Point h = new Point(0, 0);
            ship = new PointF[] { c, d, e, f, g, h, G, F,E, D, C};
        }


        void drawShip(PaintEventArgs e)
        {
            List<PointF> nu = new List<PointF>();
            for (int j = 0; j < ship.Length; j++)
            {
                PointF f = ship[j];
                f.X += shipPos.X;
                f.Y += shipPos.Y;
                nu.Add(f);
            }
            Pen pp = new Pen(Color.BlueViolet);
            e.Graphics.DrawPolygon(pp, nu.ToArray());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawShip(e);
            drawSun(e);
        }

        private void drawSun(PaintEventArgs e)
        {
            float sx = panel1.Width / 2;
            float sy = panel1.Height / 2;
            SolidBrush sb = new SolidBrush(Color.Gold);
            e.Graphics.FillEllipse(sb, sx, sy, 8, 8);

        }
        void DoSun(float g)
        {
            float sx = panel1.Width / 2;
            float sy = panel1.Height / 2;

            float dx = shipPos.X - sx;
            float dy = shipPos.Y - sy;
            float r2 = dx * dx + dy * dy;

            shipV.X += g * dx / r2;
            shipV.Y += g * dy / r2;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            panel1.Invalidate();
            shipPos.X += shipV.X;
            shipPos.Y += shipV.Y;

            shipV.X += shipA.X;
            shipV.Y += shipA.Y;

            shipPos.X = shipPos.X % panel1.Width;
            shipPos.Y = shipPos.Y % panel1.Height;
            DoSun(-.31f);
        }
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            shipV.Y += (e.Button == MouseButtons.Left) ? -.3f : .3f;
        }

    }
}

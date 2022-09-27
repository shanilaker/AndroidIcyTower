using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class Player//אחראית על השחקן שלנו
    {
        private int a{ get; set; }
        public float x { get; set; }
        public float y { get; set; }
        private float deltax { get; set; }
        private float deltay { get; set; }
        public float vy { get; set; }
        private float vx { get; set; }
        private double t { get; set; }
        public Bitmap b { get; set; }
        public Player(Bitmap b,Stone s)
        {
            a = 1;
            this.x = s.x;
            this.y = s.y;
            vy = -1;
            vx = 0;
            t = 0.5;
            this.b = b;
            this.deltax = 10;
            this.deltay = 10;
            this.b=b;
            Console.WriteLine("in Player constructor ");//למטרות דיבוג ב-Output
        }
        public void Move(Stone s, Canvas c)//פעולה שאחראית על תנועת השחקן
        {
            x = (float)(this.x + (this.vx * this.t));//ע"פ נוסחת הכבידה
            y = (float)(this.y + (this.vy * this.t) + (0.5 * this.a * this.t * this.t));//ע"פ נוסחת הכבידה

            if (s != null && (y + b.Height) <= s.y)//שתי תנאים שיזיזו את השחקן כל עוד האבן עוד לא התחילו לזוז
            {
                this.y = y - 10;//מעט את הקפיצה
            }
            if (s != null && (y + b.Height) >= s.y)
            {
                this.vy = -30;//מגדיל את המהירות שהוא נופל שהוא מתחת לאבן
            }

            vy = (float)(vy + (a * t));// ע"פ נוסחת הכבידה 
            
        }

        public void SensorChanged(SensorEvent e, DrawSurfaceView ds)//אחראית על תזוזת השחקן בעזרת החיישנים הפעולה הראשי בהפעלת המשחק
        {
            if (e.Values[0] > 0.4)//אם התנאי מתקבל משמה שהזזנו את הטלפון שמאלה
            {
                ds.Player.x = ds.Player.x - e.Values[0] * 15;//מזיז שמאלה בערכי ה-X

                if (ds.Player.x < 0)//שלא יעבור את גבולות המסך הנראות לעין
                {
                    ds.Player.x = 0;
                }
                // Console.WriteLine(" +++++++++++++++++++");
            }
            else if (e.Values[0] < -0.4)//אם התנאי מתקבל משמה שהזזנו את הטלפון ימינה
            {
                ds.Player.x = ds.Player.x - e.Values[0] * 15;// מזיז ימינה בערכי ה-X באותו היחס
                // Console.WriteLine("-------------------");
                if (ds.Player.x > ds.canvasRight - ds.Player.b.Width)//שלא יצא מגבולות המסך
                {
                    ds.Player.x = ds.canvasRight - ds.Player.b.Width;
                }
            }
            
            ds.stones.TouchPlayer(ds.Player); //בודק אם יש איזשהו חיכוך בין האבנים          
        }

        public void Draw (Canvas c)//מציירת את השחקן
        {
                c.DrawBitmap(b, this.x, this.y, null);
        }
    }
}
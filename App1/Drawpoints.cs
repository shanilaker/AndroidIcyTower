using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class Drawpoints//מטפלת בציור הניקוד על המסך
    {
        public int points{get;set;}
        private float x { get; set; }
        private float y { get; set; }
        private Paint p;
        
        public Drawpoints(int points,float x, float y)
        {
            this.points = points;
            this.x = x;
            this.y = y;
            p = new Paint();
            p.Color=Color.White;
            p.TextSize = 55;
        }
         
        public void Draw(Canvas c)//מציירת
        {
            c.DrawText(this.points.ToString(), this.x, this.y, p);
        }
    }
}
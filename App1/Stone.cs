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
    public class Stone//אחראית אבן במשחק
    {
        public float x { get; set; }
        public float y { get; set; }
        private float Width { get; set; }
        private float Height { get; set; }
        private Bitmap bitmap { get; set; }
        bool start {get;set;}
        public static bool faster = false;//האם להוריד את האבן יותר  מהר

        public Stone(float x, float y,  Bitmap bitmap, float Height, float Width, bool start)
        {
            this.start = start;
            this.x = x;
            this.y = y;
            this.bitmap = bitmap;
            this.Width = Width;
            this.Height = Height;
        }
        
        public void Draw(Canvas c)//מציירת את האבן
        {
            if (start)
            {
               c.DrawBitmap(bitmap, new Rect(0, 0, bitmap.Width, bitmap.Height),
                                new Rect((int)x, (int)y, (int)(x + Width), (int)(y + Height)), null);
            }
            else
                c.DrawBitmap(bitmap,x, y, null);
        }
        public Stone Collission (Player p)//בודקת האם האבן מתנגש עם ההשחקן
        {
            if (InsideXY(p))//האם השחקן בתחומי האבן
            {
                if (p.vy >5)//האם המהירות של השחקן מהירה מידי
                {
                    p.y = this.y - p.b.Height;//מציבה את השחקן מעל האבן
                    p.vy *= (float)(-1.164);//להקפיץ את השחקן לכיוון השני
                    if (p.vy<-27)//שהמהירות לא תהיה מהירה מידי
                    {
                        p.vy = -27;//מקסימום -27
                    }
                    if (faster)
                    {
                        DrawSurfaceView.dp.points += 2;
                    }
                    else
                        DrawSurfaceView.dp.points += 1;
                    Console.WriteLine(" !!!!!!!!!!!!!!!!!{0}!!!!!!!!!!!!", p.vy);//לצורכי דיבוג
                }
            }
            return this;
        }
        public void Move(int dy, Player p, Canvas c,DrawSurfaceView ds,GameActivity gameActivity)//אחראית על תנועת האבן
        {
            if(p.y<=0.25*c.Height)//בודקת האם השחקן עבר את מרבית המסך כדי להתחיל הוריד את האבנים יותר מהר
            {
                faster = true;
                 Rect s = new Rect(0, 0, ds.clock.Width, ds.clock.Height);
                 Rect t = new Rect(0, 0,150, 150);
                 c.DrawBitmap(ds.clock, s, t, null);//מצייר את השעון
                gameActivity.mp.Start();//מפעיל את הקול של השעון
            }
            
            if (faster)//מגדיל את קצב הורדת האבן
            {
                this.y += 4 * dy;
                //מגדיל את גודל הורדת האבן
            }
            else
            {
                this.y += dy;//מוריד את האבן בגודל קבוע
                
            }
            

        }
        public bool InsideXY(Player p)//בודקת האם השחקן בתחמי האבן
        {
            if(p.x >= this.x && p.x+p.b.Width <= this.x+this.bitmap.Width)
            {
                if (p.y + p.b.Height >= this.y && p.y + p.b.Height <= this.y +this.bitmap.Height )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
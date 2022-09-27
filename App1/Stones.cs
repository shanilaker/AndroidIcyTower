using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text.Format;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class Stones//אחראית על כל האבנים כמכלול
    {
        public List<Stone> stoneArr { get; }// רשימה של כל האבנים במשחק
        Canvas c;
        private Bitmap[] bitmaps { get; set; }//מערך של התמונות האפשריות
        int numb, randX;
        Random rnd;
        bool start;
        public bool canMoveStones { get; set; }//האם האבנים יכולות להתחיל לזוז
        int dy;

        public Stones (Canvas c , int dy)
        {
           this.c = c;
            bitmaps = new Bitmap[3];
            this.stoneArr = new List <Stone> ();
            rnd = new Random();
            this.dy = 1;
            canMoveStones = false;
        }
        public bool Move(Player p, DrawSurfaceView ds,GameActivity ga)//תנועת האבנים כלפי מטה
        {
            int i = 0;
            if (canMoveStones)//בודק שהשחקן עלה על אבן ראשונה כדי להתחיל להוריד את האבנים
            {
                while (i < stoneArr.Count)//עובר על כל האבנים ברשימה
                {
                    stoneArr[i].Move(dy,p,c,ds, ga);
                    
                   if(stoneArr[i].y>=c.Height)//בודק אם זאת האבן הראשונה ומוחק אותה ומוסיף חדשה למסך
                    {
                        this.Add(true);
                        stoneArr.Remove(stoneArr[i]);//מסיר מהרשימה
                    }
                    else// מחפש את האבן שאותה יש למחוק ולבן מקדם לאיבר הבא ברשימה
                    {
                        i++;
                    }
                   

                }
                return true;
            }
            return false;
        }

        public void SetBitmap(Context c)//מזין את ערכי התמונות במערך
        {
            bitmaps[0] = BitmapFactory.DecodeResource(c.Resources, Resource.Drawable.brick_floor);
            bitmaps[1] = BitmapFactory.DecodeResource(c.Resources, Resource.Drawable.midium_stone);
            bitmaps[2] = BitmapFactory.DecodeResource(c.Resources, Resource.Drawable.small_brick2);
        }

        public void Add(bool bCanAddPoints)//מוסיף אבן לרשימה ולמסך בצורה רנדןמאלית מבחינת תמונה ומיקום
        {
            
            Stone s1;
            int rndx;
            Bitmap bitmap;
            if (stoneArr.Count==0)// חייב שהאבןהראשונה תהיה לאורך כל המסך
            {
                s1 = new Stone(0, (float)c.Height - bitmaps[0].Height, bitmaps[0], 100, c.Width,true);
                stoneArr.Add(s1);
                
            }
            else
            {
                start = false;
                numb = rnd.Next(0, 3);//מגריל מספר שיהווה את מיקום התמונה במערך
                bitmap = bitmaps[numb];
                rndx = (c.Width - bitmaps[numb].Width) + 1;
                randX = rnd.Next(0, rndx);
                s1 = new Stone((float)randX, (float)(stoneArr [stoneArr.Count-1].y-220), bitmaps[numb], 50, (float)bitmaps[numb].Width,false);//מרכיבים את האבן הרנדומאלית
                stoneArr.Add(s1);
                
            }
        }

        public void Draw(Canvas c)// מצייר את כל רשימת האבנים במסך
        {
            for (int i = 0; i < stoneArr.Count; i++)//עבר על כל הרשימה
            {
                stoneArr[i].Draw(c);
            }
        }
        
        public Stone TouchPlayer(Player p)//בודק עם יש חיכוך בין אחת מהאבנים לשחקן
        {
            Stone s = null;
            int i = 0;
            while(i!= stoneArr.Count)//עוברת על כל רשימת האבנים
            {
                 s= stoneArr[i].Collission(p);// שולח לפעולה שבודקת חיכוך עם השחקן
                i++;
            }
            if(s!= null)//אם יש חיכוך והשחקן עלה על אבן אפשר להתחיל להוריד את האבנים
            {
                canMoveStones=true;
            }
            return s;
        }
    }
}
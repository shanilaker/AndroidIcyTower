using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public class DrawSurfaceView : SurfaceView//מחלקה שאחראית על ציור מסך המשחק
    {
        public bool threadRunning = true;
        public bool isRunning = true;
        Bitmap background;
        public Bitmap clock;
        static public Drawpoints dp;
        public Player Player{get; set;}
        int counter;
        private Context context;//מסך המשחק שלנו,Activity
        Canvas c;
        public int canvasLeft { get; set; }
        public int canvasRight { get; set; }
        public Thread t;
        ThreadStart ts;
        public Stones stones;
        int dy;//קובע את מהירות ירידת האבנים
        Bitmap playerImage;
        bool bStart;// משתנה שקובע האם רק התחלנו את המשחק
        GameActivity gameActivity;
        
        public DrawSurfaceView(Context context, GameActivity gameActivity) : base(context)
        {
            this.gameActivity = gameActivity;
            dy = 1;
            this.context = context;
            playerImage = BitmapFactory.DecodeResource(Context.Resources, Resource.Drawable.IcyBoy);
            background = BitmapFactory.DecodeResource(Context.Resources, Resource.Drawable.brick_wall);
            clock = BitmapFactory.DecodeResource(Context.Resources, Resource.Drawable.Clock);
            ts = new ThreadStart(Run);
            t = new Thread(ts);
            bStart = true;//רק יצרנו את המשחק ולכן זה עוד הההתחלה
        }

        public void destroy()
        {
            isRunning = false;
            ((GameActivity)context).Finish();
        }

        public void pause()
        {
            isRunning = false;
        }


        public void resume()
        {
            isRunning = true;
        }


        public void startGame()
        {
            isRunning = true;
        }

        public void MyDrawbitmap(Canvas canvas, Bitmap bitmap, int x, int y, int w, int h)//מצייר תמונה על פי שיטת הריבועים
        {
            Rect s = new Rect(0, 0, bitmap.Width, bitmap.Height);//מאיפה רוצים לצייר בתמונה
            Rect t = new Rect(x, y, x + w, y + h);//מאיפה רוצה לצייר במסך
            canvas.DrawBitmap(bitmap, s, t, null);//שיטת הריבועים
        }
        public void Run()
        {
            
            while (threadRunning)
            {

                if (isRunning)
                {
                    if (!this.Holder.Surface.IsValid)//בודק אם הקנבס ריק
                        continue;
                    c = null;
                    try
                    {
                        c = this.Holder.LockCanvas();//נועל את הקנבס
                        c.DrawColor(Color.Transparent, PorterDuff.Mode.Clear);
                        if (bStart)//האם התחלנו את המשחק כי יש לצייר אותם פעם אחת
                        {
                            stones = new Stones(c,counter);//בניית רשימת האבנים
                            canvasLeft = 0;
                            canvasRight = c.Width;
                            stones.SetBitmap(this.context);//מגדיר את שלושת האופציות היחידות לתמונות
                            stones.Add(false);//מוסיף אבנים למסך
                            stones.Add(false);
                            stones.Add(false);
                            stones.Add(false);
                            stones.Add(false);
                            stones.Add(false);
                            Player = new Player(Activity_ChoosePic.pic, stones.stoneArr[0]);//בונים את השחקן
                            dp = new Drawpoints(0, c.Width- 200, 50);
                            dp.Draw(c);
                            bStart = false;//כבר לא הפעם הראשונה
                        }
                        
                      
                        counter = counter % Height;//הממשתנה שקובע כמה הרקע ירד
                        counter += dy;//מורידים את המסך לאיפה שיצרו עוד אבנים כלומר מתאימים את התזוזות בין האבנים לרקע עצמו
                        DrawBackground();
                        stones.Move(Player,this, gameActivity);//מזיז 
                        stones.Draw(c);
                        dp.Draw(c);
                        if (!stones.canMoveStones)// אם האבנים עוד לא יורדות אז מקפיצים את השחקן על האבן ההתחלתית כאשר האבנים עוד לא התחילו לרדת
                            Player.Move(stones.stoneArr[0], c);
                        else//אם האבנים תחילו לרדת יש רק להקפיץ אותו לכי חוקי המשיכה ובפעולת ההתנגשות נקפיץ אותו על האבן
                            Player.Move(null, c);
                        Player.Draw(c);
                        Lose(c);//בודק תמיד אם השחקן נפל והמשחק נגמר
                    }

                    catch (Exception e)//אם יש בעיה בסראד אז יציג זאת ב- Output 
                    {
                        Console.WriteLine("error");
                        Console.WriteLine("error");
                        Console.WriteLine("error");
                        Console.WriteLine("error");
                        Console.WriteLine("error");
                    }
                    
                    finally
                    {
                        if (c != null)
                        {
                            this.Holder.UnlockCanvasAndPost(c);//משחרר את הקנבס ליצירת המסך הבא
                        }
                    }
                    

                }
               
                }
            }
        public void Lose(Canvas c)//פעולה שקובעת מה קורה כאשר נגמר המשחק 
        {
            if (Player.y+Player.b.Width >= c.Height)//אם השחקן נפל במעלה המגדל כלומר,אם השחקן עבר את הגבול התחתון של המסך
            {
                Intent intent = new Intent(context, typeof(Activity_GameOver));
                context.StartActivity(intent);
                Activity_GameOver.Save(dp.points, Activity_ChoosePic.pic);

            }
        }
        public void DrawBackground()//פעולה שמציירת לנו את הרקע של המשחק
        {

            MyDrawbitmap(c, background, 0, -counter, Width, Height);// שליחה לפעולה שמצייר לנו את הרקע בשיטת המלבנים עם חלק שלא מופיע במסך
            MyDrawbitmap(c, background, 0, -counter + Height, Width, Height);//מצייר לנו את הרקע בחלק החסר עקב שנק התחלת הציור של הרקע מחוץ למסך וכך ממשיך בתנועה מעגלית  
        }
      }
    }


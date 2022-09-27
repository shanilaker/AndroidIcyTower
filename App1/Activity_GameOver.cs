using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace App1
{
    [Activity(Label = "Activity_GameOver")]
    public class Activity_GameOver : Activity// מסך סוף המשחק
    {
        private MediaPlayer mp3;
        AudioManager am;
        ISharedPreferences sp;
        private static int pos = -1;//משתנה כאשר עברו למסך הזה

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); 
            pos = Intent.GetIntExtra("pos", -1);//-1 is default
            SetContentView(Resource.Layout.layout_GameOver);
            mp3 = MediaPlayer.Create(this, Resource.Raw.end);
            am = (AudioManager)GetSystemService(Context.AudioService);
            sp = GetSharedPreferences("setting", FileCreationMode.Private);
            int volume = sp.GetInt(Utils.VOLUME, 40);//קובע את ווליום המוזיקה
            am.SetStreamVolume(Stream.Music, volume, VolumeNotificationFlags.PlaySound);
            if(!GameActivity.bMute)//אם לחצו על ההשתק גם הקול הזה יושתק
            {
                mp3.Start();//מתחיל את המוזיקה כאשר אנחנו יוצרים את המסך 
            }
           
            
        }

        public override bool OnTouchEvent(MotionEvent e)// מפעילה את המוזיקה של סוף המשחק
        {
            if (MotionEventActions.Up == e.Action)
            {
                Intent intent = new Intent(this, typeof(Activity_Menu));
                StartActivity(intent);
            }
            return true;
        }

        public static void Save(int score, Bitmap bitmap)//שומרת את השיא של המשחק
        {
            Score t = null;
            var db = new SQLiteConnection(Helper.Path());

            if (pos != -1)//update 
            {
                t = Activity_Scores.scoreList[pos];
                Activity_Scores.scoreList[pos].SetScore( score, Helper.BitmapToBase64(bitmap));
                Activity_Scores.scoreList[pos] = t;
                db.Update(t);
            }
            else
            {
                //db.DeleteAll<Score>();
                t = new Score(score, Helper.BitmapToBase64(bitmap), Activity_Profile.name);
                db.Insert(t);//שומרת במסד הנתונים את השיא
                Activity_Scores.scoreList.Add(t);//מוסיפים את השיא חדש לרשימת השיאים
                
            }


        }
    }
}
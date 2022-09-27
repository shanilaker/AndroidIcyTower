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
using Java.Lang;
using Java.Util;
using SQLite;

namespace App1
{
    [Activity(Label = "Activity_Scores")]
    public class Activity_Scores : Activity//מסך טבלת השיאים
    {
        public static List<Score> scoreList { get; set; }//רשימת השיאים
        public string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "db");//יוצר את הדרך למסד הנתונים
        ScoreAdapter scoreAdapter;
        ListView lv;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_Scores);
            scoreList = getAllGamesInfo();
            scoreAdapter = new ScoreAdapter(this, scoreList);
            lv = FindViewById<ListView>(Resource.Id.lv);
            lv.Adapter = scoreAdapter;
        }

        public override bool OnTouchEvent(MotionEvent e)// מחזירה לתפריט הראשי
        {
            if (MotionEventActions.Up == e.Action)
            {
                Intent intent = new Intent(this, typeof(Activity_Menu));
                StartActivity(intent);
            }
            return true;
        }

        public static List<Score> getAllGamesInfo()     // פעולה שמחזירה רשימה עם כל השיאים בסדר עולה 
        {
            var db = new SQLiteConnection(Helper.Path());
            string strSql = "SELECT  * FROM Scores";  // שולף כל הרשומות   
            var gamesInfo = db.Query<Score>(strSql);
            scoreList = new List<Score>();
            if (gamesInfo.Count > 0)//אם יש כבר שיאים
            {
                foreach (var item in gamesInfo)
                {
                    scoreList.Add(item);

                }
            }
            List<Score> scoreList2 = new List<Score>();
            scoreList2 = OrderUP(scoreList);
            return scoreList2; 
        }
        private static List<Score> OrderUP(List<Score> scoreList)//מסדרת את השיאים בסדר עולה
        {
            Score[] arr = new Score[scoreList.Count];
            List<Score> s = new List<Score>();
            for (int i = 0; i < scoreList.Count; i++)
            {
                arr[i] = scoreList[i];
            }
            
            for (int i = 0; i < arr.Length; i++)
            {
                int max = arr[i].Points;//בודקים כל פעם מה האיבר מהקסימלי בשאר האיברים במערך כלומר שבאים אחרי
                int index = i;

                for (int J = i+1; J < arr.Length; J++)
                {
                    if (arr[J].Points > max)
                    {
                        max = scoreList[J].Points;
                        index = J;
                    }
                }
                Score scr = arr[index];
                arr[index] =arr[i];
                arr[i] = scr;

                s.Insert(i, scr);
            }
            
            return s;
        }
    }
}
   

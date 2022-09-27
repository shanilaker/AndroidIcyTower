using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Android.Graphics;

namespace App1
{
    [Activity(Label = "Activity_starting")]
    public class Activity_Menu : Activity, View.IOnClickListener//מסך התפריט הראשי
    {
        Button btnGame;
        Button btnInstruc;
        Button btnScores;
        Button btnProfile;
        TextView tv;
        BroadcastBattery broadCastBattery;//יוצרת האזנה לסוללה במסך זה 
        public string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "db");//בונים את המעבר למסד הנתונים
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_menu);
            btnInstruc = FindViewById<Button>(Resource.Id.btnInstruc);
            btnProfile = FindViewById<Button>(Resource.Id.btnProfile);
            btnScores = FindViewById<Button>(Resource.Id.btnScores);
            btnGame = (Button)FindViewById(Resource.Id.btnGame);
            btnGame.SetOnClickListener(this);
            btnProfile.SetOnClickListener(this);
            btnInstruc.SetOnClickListener(this);
            btnScores.SetOnClickListener(this);
            var db = new SQLiteConnection(Helper.Path());//בונים את מסד הנתונים
            db.CreateTable<Score>();// יוצרת את טבלת מסד הנתונים
            tv = FindViewById<TextView>(Resource.Id.tv);
            broadCastBattery = new BroadcastBattery(tv);
        }

        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(broadCastBattery, new IntentFilter(Intent.ActionBatteryChanged));//שיעדכן את מצב הסוללה כאשר חוזרים או פותחים את המניו מחדש
        }

        protected override void OnPause()
        {
            UnregisterReceiver(broadCastBattery);   //שיפסיק להאזין לסוללה
            base.OnPause();
        }


        public void OnClick(View v)//פעולה שמטפלת בשליחה למסכים על ידי לחיצה על כפתורים
        {
            if((Button)v ==  btnInstruc)
            {
                Intent intent = new Intent(this, typeof(Activity_Instruction));
                StartActivity(intent);
            }
            if ((Button)v == btnGame)
            {
                if (Activity_Profile.name != null)
                {
                    Intent intent = new Intent(this, typeof(GameActivity));
                    StartActivity(intent);
                }
                else//שלא יוכלו להיכנס למשחק אם לא יצרו פרופיל
                {
                    
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);//יוצרים דיאלוג
                    builder.SetMessage("you need to add your nickname;)");
                    builder.SetCancelable(true);
                    builder.SetPositiveButton("OK", OkAction);
                    builder.SetNegativeButton("NO", CancelAction);
                    AlertDialog dialog = builder.Create();
                    dialog.Show();
                }
            }
            if ((Button)v == btnScores)
            {
                Intent intent = new Intent(this, typeof(Activity_Scores));
                StartActivity(intent);
            }
            if ((Button)v == btnProfile)
            {
                Intent intent = new Intent(this, typeof(Activity_Profile));
                StartActivity(intent);
            }
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activity_Profile));
            StartActivity(intent);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(Activity_Menu));
            StartActivity(intent);
        }
    }
}
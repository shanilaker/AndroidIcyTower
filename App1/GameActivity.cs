using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Hardware;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using SQLite;
using Android.Graphics;

namespace App1
{
    [Activity(Label = "GameActivity1")]
    public class GameActivity : Activity, ISensorEventListener//אחראית על מסך המשחק
    {
        DrawSurfaceView ds;
        bool userAskBack = false;
        static readonly object _syncLock = new object();//סטטי כי לא צריך להגדירו יותר מפעם אחת
        SensorManager sensorManager;
        public MediaPlayer mp;//להשמעת השעון
        private MediaPlayer mp2;//להשמעת מוזיקת רקע
        AudioManager am;//שולט במוזיקה
        ISharedPreferences sp;
        LinearLayout ll;
        public static bool bMute = false;//האם לחצו בתפריט על האופציה להשתק
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Game_layout);
            sensorManager = (SensorManager)GetSystemService(Context.SensorService);//אחראי על החיישנים           
            ds = new DrawSurfaceView(this,this);
            ll = FindViewById<LinearLayout>(Resource.Id.ll);
            ll.AddView(ds);//מעצב את המסך לפי ה-ds
            ds.t.Start();
            mp = MediaPlayer.Create(this, Resource.Raw.alarm);//שומרת את קובץ המוזיקה של השעון
            mp2 = MediaPlayer.Create(this, Resource.Raw.game);//שומרת את קובץ המוזיקה שברקע של המשחק
            am = (AudioManager)GetSystemService(Context.AudioService);
            sp = GetSharedPreferences("setting", FileCreationMode.Private);
            int volume = sp.GetInt(Utils.VOLUME, 5);//קובע את מידת הווליום של המוזיקה
            am.SetStreamVolume(Stream.Music, volume, VolumeNotificationFlags.PlaySound);
        }
       
        protected override void OnResume()//מוודא שלמרות שאנחנו נכנסים ויוצאים המשחק ירוץ כשניכנס
        {
            base.OnResume();
            if (ds != null)
            {
                ds.resume();
                Toast.MakeText(this, "OnResume", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "ds is null OnResume", ToastLength.Long).Show();
            }
            sensorManager.RegisterListener(this, sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                                            SensorDelay.Ui);//כך נרשמים למערכת החיישנים
        }

        protected override void OnStart()//מתחיל את הרצת המשחק
        {
            base.OnStart(); ;
            Toast.MakeText(this, "OnStart", ToastLength.Long).Show();
            
        }
        protected override void OnDestroy()//עוצרת את המשחק
        {
            base.OnDestroy();
            Toast.MakeText(this, "OnDestroy", ToastLength.Long).Show();
        }

        protected override void OnStop()//מפסיקה את המשחק לגמרי ואת המוזיקה
        {
            base.OnStop();
            Toast.MakeText(this, "OnStop", ToastLength.Long).Show();
            mp.Stop();
            mp2.Stop();
        }


        protected override void OnPause()//משעה את המשחק 
        {
            base.OnPause();
            mp.Pause();
            mp2.Pause();
            if (userAskBack)
            {
                Toast.MakeText(this, "OnPause1", ToastLength.Long).Show();
            }
            else if (ds != null)
            {
                ds.pause();
                Toast.MakeText(this, "OnPause2", ToastLength.Long).Show();
            }
            Toast.MakeText(this, "OnPause3", ToastLength.Long).Show();
            sensorManager.UnregisterListener(this);//מפסיקים את ההזנה לחיישנים כי אם אני לא באפליקציה זה סתם יוריד מהבטריה
        }

        public override void Finish()
        {
            base.Finish();
            userAskBack = true;
            ds.threadRunning = false;
            while (true)
            {
                try
                {
                    ds.t.Join();
                }
                catch (InterruptedException e)
                {
                }
                break;
            }
            Toast.MakeText(this, "Finish" + ds.threadRunning, ToastLength.Long).Show();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)//מציג על המסך את המניו
        {
            MenuInflater.Inflate(Resource.Menu.game_menu, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)//משתיק
        {
            if (item.ItemId == Resource.Id.action_mute)
            {
                mp.SetVolume(0f, 0f);
                mp2.SetVolume(0f, 0f);
                bMute = true;
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {
            //throw new NotImplementedException();
        }

        public void OnSensorChanged(SensorEvent e)//אחראית על תנועה במסך לפי החיישנים
        {
            if (ds.Player == null)
                return;
            lock (_syncLock)
            {
                ds.Player.SensorChanged(e, ds);//שולח את נתוני החיישן לפעולה במחלקה של השחקן על מנת לגרום לו לנוע ביחס לחיישנים
               mp2.Start();//מתחיל את מוזיקת הרקע
            }
        }
    }
}

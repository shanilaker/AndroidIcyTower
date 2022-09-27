using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;
using SQLite;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity//אחראית על מסך הפתיחה
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Activity_Scores.scoreList = Activity_Scores.getAllGamesInfo();
        }

        public override bool OnTouchEvent(MotionEvent e)//עוברת לתפריט הראשי כשלוחצים על המסך
        {
            if (MotionEventActions.Up == e.Action)
            {
                Intent intent = new Intent(this, typeof(Activity_Menu));
                StartActivity(intent);
            }
            return true;
        }

    }
}
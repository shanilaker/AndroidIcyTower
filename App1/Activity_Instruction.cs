using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    [Activity(Label = "Activity_Instruction")]
    public class Activity_Instruction : Activity//מסך ההוראות
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_Instruction);
        }
        public override bool OnTouchEvent(MotionEvent e)//אחראית לכך שנגיעה במקום אקראי במסך תעביר לתפריט הראשי
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
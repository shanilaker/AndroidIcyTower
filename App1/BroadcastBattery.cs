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
    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBatteryChanged })]//מאזין לסוללה
    public class BroadcastBattery : BroadcastReceiver//אחראי על האזנה למצב הסוללה
    {
        TextView tv;
        public BroadcastBattery()
        {
        }
        public BroadcastBattery(TextView tv)
        {
            this.tv = tv;
        }
        public override void OnReceive(Context context, Intent intent)//מציג את מצב הסוללה
        {
            int battery = intent.GetIntExtra("level", 0);//לוקח את המידע של כמות הסוללה ומציב שם
            tv.Text = "your Battery is" + battery + "%";
        }
    }
}
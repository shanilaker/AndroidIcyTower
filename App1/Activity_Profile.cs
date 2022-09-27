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
using SQLite;

namespace App1
{
    [Activity(Label = "Activity_Profile")]
    public class Activity_Profile : Activity,Android.Views.View.IOnClickListener//מסך יצירת פרופיל המשתמש
    {
        Button btnBack;
        Button btnSave;
        public EditText etName;
        public static string name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_Profile);
            btnBack= FindViewById<Button>(Resource.Id.btnback);
            etName = FindViewById<EditText>(Resource.Id.etNname);
            btnSave = FindViewById<Button>(Resource.Id.btnsave);
            btnBack.SetOnClickListener(this);
            btnSave.SetOnClickListener(this);
        }
        public void OnClick(View v)
        {
          if((Button)v== btnBack)
            {
                Intent intent = new Intent(this, typeof(Activity_Menu));
                StartActivity(intent);
            }
            if (btnSave == (Button)v)//שומרת את הכינוי של השחקן
            {
                name = etName.Text;
                Intent intent = new Intent(this, typeof(Activity_ChoosePic));
                StartActivity(intent);
            }

        }
        
    }
}
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

namespace App1
{
    public class Character//מחלקה של דמויות במשחק
    {
        public Bitmap bitmap;
        private String des { get; set; }//תיאור השחקן

        public Character(String des, Bitmap bitmap)
        {
            this.des = des;
            this.bitmap = bitmap;
        }
        public Android.Graphics.Bitmap getBitmap()
        {
            return bitmap;
        }
        public String getdes()
        {
            return des;
        }
    }
}
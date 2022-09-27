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
    class ScoreAdapter : BaseAdapter<Score>//מטפלת בהשמת המידע של הטבלה
    {
        Android.Content.Context context;
        List<Score> objects;

        public ScoreAdapter(Android.Content.Context context, System.Collections.Generic.List<Score> objects)
        {
            this.context = context;
            this.objects = objects;
           
        }
        public List<Score> GetList()
        {
            return this.objects;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override int Count

        {
            get { return this.objects.Count; }
        }
        public override Score this[int position]
        {
            get { return this.objects[position]; }
        }
        
        public override View GetView(int position, View convertView, ViewGroup parent)

        {
            Android.Views.LayoutInflater layoutInflater = ((Activity_Scores)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.custom_layout, parent, false);
            TextView tvName = view.FindViewById<TextView>(Resource.Id.tvName);
            TextView score = view.FindViewById<TextView>(Resource.Id.tvScore);
            ImageView iv = view.FindViewById<ImageView>(Resource.Id.iv);
            Score temp = objects[position];
            if (temp != null)
            {
                score.Text = "" + temp.Points;
                iv.SetImageBitmap(Helper.Base64ToBitmap(temp.Bitmap));
                tvName.Text =temp.Name;
            }
            return view;
        }

    }
    }
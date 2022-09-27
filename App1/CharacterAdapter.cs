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
    class CharacterAdapter: BaseAdapter<Character>//מטפלת השמת המידע על הדמויות בכל תא בטבלה
    {
        Android.Content.Context context;//המסך שבו תופיע הטבלה
        public List<Character> objects;

        public CharacterAdapter(Android.Content.Context context, System.Collections.Generic.List<Character> objects)
        {
            this.context = context;
            this.objects = objects;
        }



        public List<Character> GetList()
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



        public override Character this[int position]//מחזיר את הדמות במקום הנ"ל
        {
            get { return this.objects[position]; }
        }




        public override View GetView(int position, View convertView, ViewGroup parent)//שמה את המידע על כל דמות בטבלה
        {
            Android.Views.LayoutInflater layoutInflater = ((Activity_ChoosePic)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.layoutcustom2, parent, false);
            TextView tvTitle = view.FindViewById<TextView>(Resource.Id.tvTitle);
            ImageView ivProduct = view.FindViewById<ImageView>(Resource.Id.ivProduct);
            Character temp = objects[position];
            if (temp != null)
            {
                ivProduct.SetImageBitmap(temp.getBitmap());
                tvTitle.Text = temp.getdes();
            }
            return view;
        }
    }
}
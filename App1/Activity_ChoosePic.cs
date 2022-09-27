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
    [Activity(Label = "Activity_ChoosePic")]
    public class Activity_ChoosePic : Activity, ListView.IOnItemClickListener//מסך בחירת השחקן
    {
        public static List<Character> characterList { get; set; }        //רשימה של הדמויות
        CharacterAdapter characterAdapter;
        ListView lv;
        ImageView iv;
        public static Bitmap pic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout_ChoosePic);
            Bitmap icyBoy = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.IcyBoy);
            Bitmap linux = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.linux2);
            Bitmap marge = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.marge2);
            Bitmap mickey = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.mickey2);
            Bitmap moncky = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.moncky2);
            Bitmap spidey = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.spidey2);
            Bitmap kirby = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.Kirby2);
            Bitmap dexter = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.dexter2);
            Bitmap pikachu = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.pikachu2);
            Bitmap ironman = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.ironman2);
            Bitmap bunny = Android.Graphics.BitmapFactory.DecodeResource(Application.Context.Resources, Resource.Drawable.Bunny2);
            Character t1 = new Character("The original character off Icy Tower", icyBoy);
            Character t2 = new Character("Linux company logo", linux);
            Character t3 = new Character("The mather of the Simpsons", marge);
            Character t4 = new Character("The oldest player", mickey);
            Character t5 = new Character("The one and only pikachu!", pikachu);
            Character t6 = new Character("The amazing spiderman", spidey);
            Character t7 = new Character("The original character Nintendo", kirby);
            Character t8 = new Character("The youngest scientist", dexter);
            Character t9 = new Character("The naughtiest player", moncky);
            Character t10 = new Character("The cutest player", bunny);
            Character t11= new Character("The stronger player", ironman);
            //phase 2 - add to array list
            Activity_ChoosePic.characterList = new System.Collections.Generic.List<Character>();
            Activity_ChoosePic.characterList.Add(t1); Activity_ChoosePic.characterList.Add(t2); Activity_ChoosePic.characterList.Add(t3);
            Activity_ChoosePic.characterList.Add(t4); Activity_ChoosePic.characterList.Add(t5); Activity_ChoosePic.characterList.Add(t6);
            Activity_ChoosePic.characterList.Add(t7); Activity_ChoosePic.characterList.Add(t8); Activity_ChoosePic.characterList.Add(t9);
            Activity_ChoosePic.characterList.Add(t10); Activity_ChoosePic.characterList.Add(t11);
            //phase 3 - create adapter
            characterAdapter = new CharacterAdapter(this, characterList);
            //phase 4 reference to listview
            iv = FindViewById<ImageView>(Resource.Id.iv);
            lv = FindViewById<ListView>(Resource.Id.lv);
            lv.Adapter = characterAdapter;
            lv.OnItemClickListener = this;
        }

        protected override void OnResume()
        {
            base.OnResume();
            characterAdapter.NotifyDataSetChanged();//  חזרו או פתחו שוב את המסך ולכן יש לעדכן את הנתונים בתאים ברשימה
        }

        void AdapterView.IOnItemClickListener.OnItemClick(AdapterView parent, View view, int position, long id)//מה קורה כאשר השחקן לוחץ על תא ברשימה
        {
            pic = characterAdapter.objects[position].bitmap;
            Intent intent = new Intent(this, typeof(Activity_Menu));
            StartActivity(intent);
        }
    }
}
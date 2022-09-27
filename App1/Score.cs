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
using SQLite;

namespace App1
{
    [Table("Scores")]
    public class Score//מחלקה של השיאים של השחקנים במשחק
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }
        public int Points { get; set; }
        public string Bitmap { get; set; }
        public string Name { get; set; }

        public Score( int score,string b, string name)
        {
            this.Points = score;
            this.Bitmap = b;
            this.Name = name;
        }

        public Score()
        {
            
        }
        public void SetScore(int score, String b)
        {
            this.Points = score;
            this.Bitmap = b;
        }
    }
}
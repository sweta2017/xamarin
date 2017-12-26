using Android.App;
using Android.Widget;
using Android.OS;
using SqliteDemo.SQLitedb;
using SqliteDemo.Adapter;
using System;
using System.Collections.Generic;
using Java.Util;
using Android.Database;
using Android.Content;

namespace SqliteDemo
{
    [Activity(Label = "SqliteDemo", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/MyCustomTheme")]
    public class MainActivity : Activity
    {
        EditText ename;
        EditText eage;
        Button btnsubmit, btnshow,btnpick;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            ename = FindViewById<EditText>(Resource.Id.ename);
            eage = FindViewById<EditText>(Resource.Id.eage);
            btnsubmit = FindViewById<Button>(Resource.Id.btnsubmit);
            btnshow = FindViewById<Button>(Resource.Id.btnshow);
            btnpick = FindViewById<Button>(Resource.Id.myButton);
            btnpick.Click += Btnpick_Click;

            btnsubmit.Click += Btnsubmit_Click;
            btnshow.Click += Btnshow_Click;


        }

        private void Btnpick_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(ProfileActivity));
            StartActivity(i);
        }
       

        private void Btnshow_Click(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(ShowActivity));
            StartActivity(i);

        }

        private void Btnsubmit_Click(object sender, System.EventArgs e)
        {
            DBAdapter db = new DBAdapter(this);
            db.openDb();
            string name = ename.Text;
            int age = Convert.ToInt32(eage.Text);
            long i = db.insert(name, age);
            if (i > 0)
            {
                Toast.MakeText(this, "Insert successfully", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Failed Insert", ToastLength.Short).Show();
            }
            db.closeDb();
        }


    }
}


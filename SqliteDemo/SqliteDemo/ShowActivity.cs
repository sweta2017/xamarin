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
using SqliteDemo.Adapter;
using Android.Database;

namespace SqliteDemo
{
    [Activity(Label = "ShowActivity",Theme = "@style/MyCustomTheme")]
    public class ShowActivity : Activity
    {
        ListView lv;
        List<string> lst;
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Show);
            lv = FindViewById<ListView>(Resource.Id.lstview);
         
            lst = new List<string>();
            DBAdapter db = new DBAdapter(this);
            db.openDb();
            ICursor c = db.show();
            while (c.MoveToNext())
            {
                lst.Add(c.GetInt(0) + " " + c.GetString(1));
            }
            ArrayAdapter adp = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, lst);
            lv.Adapter = adp;

        }
    }
}
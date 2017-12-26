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
using Android.Database.Sqlite;
using SqliteDemo.SQLitedb;
using Android.Database;
using Java.Sql;

namespace SqliteDemo.Adapter
{
    class DBAdapter
    {
        private Context c;
        private SQLiteDatabase db;
        public MyHelper helper;

        public DBAdapter(Context c)
        {
            this.c = c;
            helper = new MyHelper(c);
        }
        public DBAdapter openDb()
        {
            try
            {
                db = helper.WritableDatabase;
            }
            catch(Exception e)
            {
               Console.WriteLine(e.Message);
            }
            return this;
            
        }
        public DBAdapter closeDb()
        {
            try
            {
                helper.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return this;

        }

        public long insert(string name,int age)
        {
            ContentValues cv = new ContentValues();
            cv.Put("name", name);
            cv.Put("age", age);
            return db.Insert(MyHelper.TBLNAME, null, cv);
        }
        public ICursor show()
        {
            return db.Query(MyHelper.TBLNAME, null, null, null, null, null, null);

        }

        public long insertImage(Byte[] name)
        {
            ContentValues cv = new ContentValues();
            cv.Put("imagename", name);
            return db.Insert(MyHelper.TBLNAME2, null, cv);
        }
        public ICursor showImage()
        {
            string id = "2";
            String where = MyHelper.col_id + " = ?";
            String[] whereArgs = { id };
            return db.Query(MyHelper.TBLNAME2, null,where,whereArgs, null,null,null);
          
        }
    }
   
}
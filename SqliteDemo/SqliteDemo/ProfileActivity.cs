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
using Android.Graphics;
using Java.IO;
using System.IO;
using SqliteDemo.Adapter;
using Android.Database;
using Android.Provider;
using static System.Net.Mime.MediaTypeNames;
using Java.Nio;

namespace SqliteDemo
{
    [Activity(Label = "ProfileActivity",Theme = "@style/MyCustomTheme")]
    public class ProfileActivity : Activity
    {
        ImageView image;
        byte[] img1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Profile);
            image = FindViewById<ImageView>(Resource.Id.image);
            image.Click += Image_Click;

            // Create your application here
        }

        private void Image_Click(object sender, EventArgs e)
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(
                Intent.CreateChooser(imageIntent, "Select photo"), 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
               // var imageView = FindViewById<ImageView>(Resource.Id.image);
              
                image.BuildDrawingCache(true);
                Bitmap bmap = image.GetDrawingCache(true);
                //  image.SetImageURI(data.Data);

             



                MemoryStream stream = new MemoryStream();

               bmap.Compress(Bitmap.CompressFormat.Png, 100, stream);

                byte[] img = stream.ToArray();

              
                DBAdapter db = new DBAdapter(this);
                db.openDb();
               long i= db.insertImage(img);
                if(i>0)
                {
                    Toast.MakeText(this, "Insert", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Insert Failed..", ToastLength.Short).Show();
                }

                ICursor c = db.showImage();
                if (c != null)
                {

                    c.MoveToFirst();

                    do
                    {

                        img1 = c.GetBlob(1);
                   
                    } while (c.MoveToNext());

                }

                MemoryStream ms = new MemoryStream();
             
                Bitmap n1 = BitmapFactory.DecodeByteArray(img1, 0, img1.Length);
              //  n1.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                image.SetImageBitmap(n1);
              
                db.closeDb();




            }
           
        }
   

    }
}
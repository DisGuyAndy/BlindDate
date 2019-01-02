using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace BlindDate.Droid
{
    [Activity(Label = "LoginPage")]
    public class LoginPage : Activity
    {
        Button btn_login;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //SetContentView
            SetContentView(Resource.Layout.login);
            //Get btn_Hello Button control from the Manin.axml Layout.
            btn_login = FindViewById<Button>(Resource.Id.btn_login);
            //Creating Click event of btn_Hello
            btn_login.Click += btn_login_Click;
        }
        //btn_Hello Click Event
        void btn_login_Click(object sender, EventArgs e)
        {
            //Display a Toast Message on Clicking the btn_Hello
            Toast.MakeText(this, "test", ToastLength.Short).Show();
        }
    }
}
using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using System.Net;
using Plugin.CurrentActivity;

namespace UN1App.Droid
{
    [Activity(Label = "UN1App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            //Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.Camera }, 1);
            //Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.ReadExternalStorage }, 1);
            //Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.WriteExternalStorage }, 1);

            LoadApplication(new cApp());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }

}
//using Com.OneSignal;
using Delysoft.Apps;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Delysoft
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //OneSignal.Current.StartInit("90a9d020-87d2-4dd4-9884-72b404d341b2").EndInit();
            MainPage = new Login();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

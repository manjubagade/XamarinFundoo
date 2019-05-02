//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP
{
    using System;
    using FUNDOOAPP.views;
    using Microsoft.AppCenter;
    using Microsoft.AppCenter.Push;
    using Xamarin.Forms;

    /// <summary>
    /// this app instance
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Application" />
    public partial class App : Application
    {

        public static double ScreenWidth;
        public static double ScreenHeight;
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.MainPage = new NavigationPage(new Login());
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnStart()
        {
            AppCenter.Start("b607533a-045d-49bb-b67e-107b8ff43c7b", typeof(Push));
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added
        /// </remarks>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks> 
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="Login.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.ViewModel;
    using FUNDOOAPP.views.Dashbord;
    using Plugin.Toast;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The firebase
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Login : ContentPage
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private Firebasedata firebase = new Firebasedata();

        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            try
            {
                var result = DependencyService.Get<IFirebaseAuthenticator>().Status();
                if (result)
                {
                    Navigation.PushModalAsync(new Masterpage());
                }
                else
                {
                    this.InitializeComponent();
                    this.BindingContext = new MainPageViewModel(this);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the handle event of the Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Click_hanndle1(object sender, EventArgs e)
        {
            if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            {
              await this.DisplayAlert("No Internet", "check your network connection", "ok");
                return;
            }

            loading.IsEnabled = true;
            loading.IsRunning = true;
            loading.IsVisible = true;
            string email = username.Text;
            string password = upassword.Text;

            var item = await DependencyService.Get<IFirebaseAuthenticator>().LoginwithEmailPassword(email, password);
            if (item)
            {
                CrossToastPopUp.Current.ShowToastMessage("SUCCESS");

                await this.Navigation.PushModalAsync(new Masterpage());
            }
            else
            {
                await this.DisplayAlert("Invalid User", "Invalid user Details", "close");
            }
        }

        /// <summary>
        /// Handles the handle event of the Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Click_hanndle(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new Signup());
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PushModalAsync(new Forgotpage());
        }
    }
}
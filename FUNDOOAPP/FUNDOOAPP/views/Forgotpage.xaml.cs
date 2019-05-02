//-----------------------------------------------------------------------
// <copyright file="Forgotpage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using FUNDOOAPP.Interfaces;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="Forgotpage" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Forgotpage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Forgotpage"/> class.
        /// </summary>
        public Forgotpage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().Forgotpassword(emailss.Text);
            await this.DisplayAlert("Forgot password", "check your mail", "ok");
            await Navigation.PushModalAsync(new Login());
        }
    }
}

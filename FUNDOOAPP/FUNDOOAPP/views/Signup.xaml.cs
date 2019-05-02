//-----------------------------------------------------------------------
// <copyright file="signup.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using System.Text.RegularExpressions;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using FUNDOOAPP.ViewModel;
    using Plugin.Toast;
    using Xamarin.Forms;

    /// <summary>
    /// sign up class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Signup : ContentPage
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private Firebasedata firebase = new Firebasedata();

        /// <summary>
        /// Initializes a new instance of the <see cref="Signup"/> class.
        /// </summary>
        public Signup()
        {
            this.InitializeComponent();
            this.BindingContext = new MainPageViewModel1(this);
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserRepository userRepository = new UserRepository();
                if (this.CheckValidation())
                {
                    var user = await userRepository.AddUserAsync(first.Text, last.Text, emailid.Text, password.Text, cpassword.Text);
                    first.Text = string.Empty;
                    last.Text = string.Empty;
                    emailid.Text = string.Empty;
                    password.Text = string.Empty;
                    cpassword.Text = string.Empty;
                    CrossToastPopUp.Current.ShowToastMessage(" REGISTRATION SUCCESS");
                    DependencyService.Get<IFirebaseAuthenticator>().Sigout();
                    await Navigation.PushModalAsync(new Login());
                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Invalid Details", ex.Message, "Close");
            }
        }

        /// <summary>
        /// Checks the validation.
        /// </summary>
        /// <returns>return task</returns>
        public bool CheckValidation()
        {
            if (string.IsNullOrEmpty(first.Text) || Regex.IsMatch(first.Text, @"[0-9]") || first.Text.Length < 3)
            {
                this.DisplayAlert("FirstName", "First name should not Empty and  less then  3 character ", "ok");
                return false;
            }

            if (string.IsNullOrEmpty(last.Text) || Regex.IsMatch(last.Text, @"[0-9]") || last.Text.Length < 3)
            {
                this.DisplayAlert("LastName", "Last name should not Empty and  less then 3 character  ", "ok");
                return false;
            }

            if (string.IsNullOrEmpty(emailid.Text))
            {
                this.DisplayAlert("E-mail", "Email Address should not Empty", "ok");
                return false;
            }

            if (string.IsNullOrEmpty(password.Text) || (password.Text.Length < 6))
            {
                this.DisplayAlert("Password", "password  should not Empty and less than 6 character or number", "ok");
                return false;
            }

            if (!password.Text.Equals(cpassword.Text))
            {
                this.DisplayAlert("Confirm password", "Confirm password  should not Match your password", "ok");
                return false;
            }

            return true;
        }

    }
}
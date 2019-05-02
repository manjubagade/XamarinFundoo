//-----------------------------------------------------------------------
// <copyright file="CameraPermition.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views.RemiderAndLocation
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
   
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// this Remainder instance
    /// </summary>
    public partial class Remainder
     {

        private FirebaseClient firebaseclient = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");
        /// <summary>
        /// this notesRepository instance
        /// </summary>
        NotesRepository notesRepository = new NotesRepository();

        /// <summary>
        /// this Remainder instance
        /// </summary>
        public Remainder()
         {
            this.InitializeComponent();
            mypicker.Items.Add("Does not repeat");
            mypicker.Items.Add("Daily");
            mypicker.Items.Add("Weekly");
            mypicker.Items.Add("Monthly");
            mypicker.Items.Add("Yearly");
            mypicker.Items.Add("Custom");
         }

        /// <summary>
        /// Handles the Clicked event of the Cencel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private async void Cencel_Clicked(object sender, System.EventArgs e)
        {

            await PopupNavigation.Instance.PopAsync(true);
            
        }

        /// <summary>
        /// Handles the Clicked event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private async void Save_Clicked(object sender, System.EventArgs e)
        {
            //var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            //var response = this.firebaseclient.Child("User").Child(uid).Child("TimeDate").
            //    PostAsync<Note>(new Note() { DateTime = datepicker.Date.ToString(); });

            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
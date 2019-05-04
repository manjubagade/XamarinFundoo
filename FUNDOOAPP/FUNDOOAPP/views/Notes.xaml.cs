//-----------------------------------------------------------------------
// <copyright file="Notes.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.views.Poppage;
    using FUNDOOAPP.views.RemiderAndLocation;
    using Plugin.Toast;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// firebase page
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Notes : ContentPage
    {
        public Color ColorNotes { get; set; }
        public string noteBackGroundColor = "White"; 
        /// <summary>
        /// The firebase client
        /// </summary>
        private FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// Initializes a new instance of the <see cref="Notes"/> class.
        /// </summary>
        public Notes()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Create notes this instance.
        /// </summary>
        /// <returns>return task</returns>
        public async Task<List<Note>> Createnotes()
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            return (await this.firebaseclint.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes
            }).ToList();
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        //protected override bool OnBackButtonPressed()
        //{
        //    var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
        //    var response = this.firebaseclint.Child("User").Child(uid).Child("Note").PostAsync<Note>(new Note() { Title = title.Text, Notes = notess.Text });
        //    base.OnBackButtonPressed();
        //    return false;
        //}

         protected override void OnDisappearing()
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            var response = this.firebaseclint.Child("User").Child(uid).Child("Note").PostAsync<Note>(new Note() { Title = title.Text, Notes = notess.Text, ColorNote=this.noteBackGroundColor });
            base.OnDisappearing();
            CrossToastPopUp.Current.ShowToastMessage("Notes Created");
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked(object sender, EventArgs e)
        {
            BackgroundColor = Color.Red;
            this.noteBackGroundColor = "Red";
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
           await PopupNavigation.Instance.PushAsync(new Popupmenupage());
        }

        /// <summary>
        /// Handles the Clicked event of the Aque control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Aque_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Aqua;
            this.noteBackGroundColor = "Aqua";
        }

        /// <summary>
        /// Handles the Clicked event of the DarkGoldenrod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DarkGoldenrod_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.DarkGoldenrod;
            this.noteBackGroundColor = "DarkGoldenrod";
        }

        /// <summary>
        /// Handles the Clicked event of the Green control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Green_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Green;
            this.noteBackGroundColor = "Green";
        }

        /// <summary>
        /// Handles the Clicked event of the Gold control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Gold_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Gold;
            this.noteBackGroundColor = "Gold";
        }

        /// <summary>
        /// Handles the Clicked event of the GreenYellow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void GreenYellow_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.GreenYellow;
            this.noteBackGroundColor = "GreenYellow";
        }

        /// <summary>
        /// Handles the Clicked event of the Gray control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Gray_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Gray;
            this.noteBackGroundColor = "Gray";
        }

        /// <summary>
        /// Handles the Clicked event of the Lavender control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Lavender_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Lavender;
            this.noteBackGroundColor = "Lavender";
        }

        /// <summary>
        /// Handles the Clicked event of the Red control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Red_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Red;
            this.noteBackGroundColor = "Red";
        }

        /// <summary>
        /// Handles the Clicked event of the Yellow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Yellow_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Yellow;
            this.noteBackGroundColor = "Yellow";
        }

        /// <summary>
        /// Handles the Clicked event of the Orange control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Orange_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Orange;
            this.noteBackGroundColor = "Orange";
        }

        /// <summary>
        /// Handles the Clicked event of the Teal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Teal_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Teal;
            this.noteBackGroundColor = "Teal";
        }

        /// <summary>
        /// Handles the Clicked event of the Brown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Brown_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Brown;
            this.noteBackGroundColor = "Brown";
        }

        /// <summary>
        /// Handles the Clicked event of the Purple control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Purple_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Purple;
            this.noteBackGroundColor = "Purple";
        }
    }
}
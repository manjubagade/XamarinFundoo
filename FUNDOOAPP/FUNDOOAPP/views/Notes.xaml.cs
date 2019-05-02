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

        private void Button_Clicked(object sender, EventArgs e)
        {
            BackgroundColor = Color.Red;
            this.noteBackGroundColor = "Red";
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
           await PopupNavigation.Instance.PushAsync(new Popupmenupage());
        }

        private void Aque_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Aqua;
            this.noteBackGroundColor = "Aqua";
        }

        private void DarkGoldenrod_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.DarkGoldenrod;
            this.noteBackGroundColor = "DarkGoldenrod";
        }

        private void Green_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Green;
            this.noteBackGroundColor = "Green";
        }

        private void Gold_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Gold;
            this.noteBackGroundColor = "Gold";
        }

        private void GreenYellow_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.GreenYellow;
            this.noteBackGroundColor = "GreenYellow";
        }

        private void Gray_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Gray;
            this.noteBackGroundColor = "Gray";
        }

        private void Lavender_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Lavender;
            this.noteBackGroundColor = "Lavender";
        }

        private void Red_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Red;
            this.noteBackGroundColor = "Red";
        }

        private void Yellow_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Yellow;
            this.noteBackGroundColor = "Yellow";
        }

        private void Orange_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Orange;
            this.noteBackGroundColor = "Orange";
        }

        private void Teal_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Teal;
            this.noteBackGroundColor = "Teal";
        }

        private void Brown_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Brown;
            this.noteBackGroundColor = "Brown";
        }

        private void Purple_Clicked(object sender, EventArgs e)
        {
            this.BackgroundColor = Color.Purple;
            this.noteBackGroundColor = "Purple";
        }
    }
}
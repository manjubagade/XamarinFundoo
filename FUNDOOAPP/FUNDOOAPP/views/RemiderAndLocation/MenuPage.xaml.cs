//-----------------------------------------------------------------------
// <copyright file="MenuPage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views.RemiderAndLocation
{
    using System;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using FUNDOOAPP.views.Dashbord;
    using Plugin.Toast;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FUNDOOAPP.DataFile.Enum;

    [XamlCompilation(XamlCompilationOptions.Compile)]
     public partial class MenuPage : PopupPage
    {
        
        private NotesRepository notesRepository = new NotesRepository();

        private string noteKeys = string.Empty;
        private string notes = string.Empty;

        private FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class.
        /// </summary>
        /// <param name="notekay">The notekay.</param>
        public MenuPage(string notekay)
         {
            this.noteKeys = notekay;
           this.InitializeComponent();
          }

        /// <summary>
        /// Deletes the notes.
        /// </summary>
        public async void DeleteNotes()
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            await this.firebaseclint.Child("User").Child(uid).Child("Note").Child(this.noteKeys).DeleteAsync();
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
                Note note = new Note();
                var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
                note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
                note.noteType = NoteType.isTrash;
                await this.notesRepository.UpdateNoteAsync(note, this.noteKeys, uid);
                await Navigation.PushModalAsync(new Masterpage());
                await PopupNavigation.Instance.PopAsync();
                CrossToastPopUp.Current.ShowToastMessage("Note Moved to Trash");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }      
        }

        /// <summary>
        /// Updates the notes.
        /// </summary>
        public async void UpdateNotes()
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note notes = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);

            await Xamarin.Essentials.Share.RequestAsync(new ShareTextRequest
            {
                Text = notes.Notes,
                Title = "Share!"
            });

            await PopupNavigation.Instance.PopAsync(true);
           //  PopupNavigation.Instance.PushAsync(new SharePage());     
        }

        /// <summary>
        /// Handles the 2 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new labelspage(noteKeys));
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
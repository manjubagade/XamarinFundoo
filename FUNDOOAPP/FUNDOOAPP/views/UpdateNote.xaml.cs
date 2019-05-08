//-----------------------------------------------------------------------
// <copyright file="UpdateNote.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using FUNDOOAPP.ViewModel;
    using FUNDOOAPP.views.Dashbord;
    using FUNDOOAPP.views.RemiderAndLocation;
    using Plugin.Toast;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FUNDOOAPP.DataFile.Enum;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// this class UpdateNote instance
    /// </summary>
    public partial class UpdateNote : ContentPage
    {
        /// <summary>
        /// The lists
        /// </summary>
        IList<string> lists = new List<string>();

        /// <summary>
        /// Gets or sets the color notes.
        /// </summary>
        /// <value>
        /// The color notes.
        /// </value>
        public Color ColorNotes { get; set; }

        /// <summary>
        /// The note back ground color
        /// </summary>
        public string noteBackGroundColor = "White";

        /// <summary>
        /// this class UpdateNote instance
        /// </summary>
        private string noteKeys = string.Empty;

        /// <summary>
        /// NotesRepository this instance
        /// </summary>
        private NotesRepository notesRepository = new NotesRepository();

        /// <summary>
        /// FirebaseClient this instance
        /// </summary>
        private FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// UpdateNote this instance
        /// </summary>
        /// <param name="noteKey">note Key</param>
        public UpdateNote(string noteKey)
        {
            this.noteKeys = noteKey;
            this.UpdateNotes();
            this.InitializeComponent();
        }

        /// <summary>
        /// UpdateNotes this instance
        /// </summary>
        public async void UpdateNotes()
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            editor.Text = note.Title;
            editorNote.Text = note.Notes;
            lists = note.LabelsList;
            this.BackgroundColor = Color.FromHex(FrameColorSetter.GetHexColor(note));
            ToolbarItems.Clear();
            if (note.noteType == NoteType.isNote)
            {
                ToolbarItems.Add(this.archived);
                ToolbarItems.Add(this.alaram);
                ToolbarItems.Add(this.pincard);
            }
            else if (note.noteType == NoteType.isArchive)
            {
                ToolbarItems.Add(this.unarchived);
                ToolbarItems.Add(this.alaram);
                ToolbarItems.Add(this.pincard);
            }
            else if (note.noteType == NoteType.isTrash)
            {
                ToolbarItems.Add(this.deleted);
                ToolbarItems.Add(this.Restoredata);
            }
            else if (note.noteType == NoteType.ispin)
            {
                ToolbarItems.Add(this.PinCard1);
            }
            
        }

        /// <summary>
        /// DeleteNotes this instance
        /// </summary>
        public async void DeleteNotes()
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            Delete delete = new Delete();
           //// delete.Trash(note);
            await this.firebaseclint.Child("User").Child(uid).Child("Note").Child(this.noteKeys).DeleteAsync();
        }

        /// <summary>
        /// OnBackButtonPressed this instance
        /// </summary>
        /// <returns>return task</returns>
        protected  override  bool OnBackButtonPressed()
        {
            if (Device.RuntimePlatform.Equals(Device.Android))
            {
                var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

                Note newnote = new Note()
                {
                    Title = editor.Text,
                    Notes = editorNote.Text,
                    ColorNote = this.noteBackGroundColor,
                    LabelsList = lists,
                };

        var results=this.notesRepository.UpdateNoteAsync(newnote, this.noteKeys, uid);
                return base.OnBackButtonPressed();
            }
            else
            {
                return false;
            }
        }
        //protected override async void OnDisappearing()
        //{
        //    var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

        //    Note newnote = new Note()
        //    {
        //        Title = editor.Text,
        //        Notes = editorNote.Text
        //    };
        //    await this.notesRepository.UpdateNoteAsync(newnote, this.noteKeys, uid);

        //    base.OnDisappearing();
        //}

        /// <summary>
        /// Delete_Clicked this instance
        /// </summary>
        /// <param name="sender">name</param>
        /// <param name="e">name p</param>
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            this.DeleteNotes();
            await Navigation.PushModalAsync(new Masterpage()); 
        }

        /// <summary>
        /// this Listview_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Listview_Clicked(object sender, EventArgs e)
        {
           // PopupNavigation.Instance.PushAsync(new MenuPage(noteKeys));
        }

        /// <summary>
        /// this ImageButton_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new MenuPage(this.noteKeys));
            Navigation.RemovePage(this);
        }

        /// <summary>
        /// this Bell_btn_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bell_btn_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new Remainder());
        }

        /// <summary>
        /// this Archived_Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Archived_Clicked(object sender, EventArgs e)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            note.noteType = NoteType.isArchive;
            await this.notesRepository.UpdateNoteAsync(note, this.noteKeys, uid);
            await Navigation.PushAsync(new Masterpage());
        }

        /// <summary>
        /// this ToolbarItem_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            note.noteType = NoteType.isArchive;
            note.ColorNote = this.noteBackGroundColor;
            await this.notesRepository.UpdateNoteAsync(note, this.noteKeys, uid);
            CrossToastPopUp.Current.ShowToastMessage("Note is Archived");
            // await Navigation.PushModalAsync(new Masterpage());
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// this Nodification_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"> EventArgs</param>
        private void Nodification_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new Remainder());
        }

        /// <summary>
        /// this Deleted_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">return task</param>
        private async void Deleted_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Question?", "Delete this note forever", "Delete", "Cancel");
           if (answer == true)
            {
                this.DeleteNotes();
                CrossToastPopUp.Current.ShowToastMessage("Notes is Deleted");
                await Navigation.PushModalAsync(new Masterpage());
            }
            else
            {
                await Navigation.PushModalAsync(new Masterpage());
            }
        }

        /// <summary>
        /// this Restoredata_Clicked instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">return task</param>
        private async void Restoredata_Clicked(object sender, EventArgs e)
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note newnote = new Note()
            {
                Title = editor.Text,
                Notes = editorNote.Text,
                ColorNote = this.noteBackGroundColor
            };
          await this.notesRepository.UpdateNoteAsync(newnote, this.noteKeys, uid);
            CrossToastPopUp.Current.ShowToastMessage("Note is Restore");
            await Navigation.PushModalAsync(new Masterpage());
        }

        /// <summary>
        /// Handles the 1 event of the Unarchived_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Unarchived_Clicked_1(object sender, EventArgs e)
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();

            Note newnote = new Note()
            {
                Title = editor.Text,
                Notes = editorNote.Text,
                ColorNote = this.noteBackGroundColor
                
            };
            await this.notesRepository.UpdateNoteAsync(newnote, this.noteKeys, uid);
            CrossToastPopUp.Current.ShowToastMessage("Note is UnArchived");
            //await Navigation.PushModalAsync(new Masterpage());
            await Navigation.PopAsync(true);
        }

        /// <summary>
        /// Handles the Clicked event of the Pincard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Pincard_Clicked(object sender, EventArgs e)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, uid);
            note.noteType = NoteType.ispin;
            note.ColorNote = this.noteBackGroundColor;
            await this.notesRepository.UpdateNoteAsync(note, this.noteKeys, uid);
            CrossToastPopUp.Current.ShowToastMessage("Note is Pinned");
            // await Navigation.PushModalAsync(new Masterpage());
        }

        /// <summary>
        /// Handles the Clicked event of the PinCard1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void PinCard1_Clicked(object sender, EventArgs e)
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Note newnote = new Note()
            {
                Title = editor.Text,
                Notes = editorNote.Text,
                ColorNote = this.noteBackGroundColor  
            };
            await this.notesRepository.UpdateNoteAsync(newnote, this.noteKeys, uid);
            CrossToastPopUp.Current.ShowToastMessage("Note is UnPinnned");
            // await Navigation.PushModalAsync(new Masterpage());
            //await Navigation.PopAsync(true);
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

        /// <summary>
        /// Lables the frames.
        /// </summary>
        /// <param name="list">The list.</param>
        public async void LableFrames(IList<string> list)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Firebasedata firebasedata = new Firebasedata();
            var alllabels = await firebasedata.GetAllLabels();
            foreach (LabelNotes model in alllabels)
            {
                //IList<string> lists = data.LabelsList;
                foreach (var labelId in list)
                {
                    if (model.LabelKey.Equals(labelId))
                    {
                        var labelName = new Label
                        {
                            Text = model.Label,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Start,
                            FontSize = 11
                        };
                        var labelFrame = new Frame();
                        labelFrame.CornerRadius = 28;
                        labelFrame.HeightRequest = 14;
                        labelFrame.Content = labelName;
                        labelFrame.BorderColor = Color.Gray;
                        labelFrame.BackgroundColor = this.BackgroundColor;
                        // labelFrame.BackgroundColor = Color.FromHex(FrameColorSetter.GetHexColor(data));
                        Note note = await notesRepository.GetNoteByKeyAsync(noteKeys, userid);
                        // labelFrame.BackgroundColor = this.noteBackGroundColor;
                        note.ColorNote = this.noteBackGroundColor;
                        layout.Children.Add(labelFrame);
                    }
                }
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            Firebasedata firebasedata = new Firebasedata();
            var note = await this.notesRepository.GetNoteByKeyAsync(this.noteKeys, userid);
            var list = note.LabelsList;
            this.LableFrames(list);
        }
    } 
}
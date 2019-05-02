

namespace FUNDOOAPP.views
{
    using System;
    using System.Collections.Generic;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using Plugin.InputKit.Shared.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="labelspage" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class labelspage : ContentPage
    {
        string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
        NotesRepository notesRepository = new NotesRepository();
        private string noteKey = string.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref="labelspage"/> class.
        /// </summary>
        public labelspage(string labelkeys)
        {
            noteKey = labelkeys;
            this.InitializeComponent();
        }
        Firebasedata firebasedata = new Firebasedata();

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                var alllabels = await this.firebasedata.GetAllLabels();
                lstLabels.ItemsSource = alllabels;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the CheckChanged event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CheckBox_CheckChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            IList<string> list = new List<string>();
            if (checkbox.IsChecked)
            {
                checkbox.Color = Color.Black;
                StackLayout stack = (StackLayout)sender;
                IList<View> view = stack.Children;
                Label temp = (Label)view[1];
                var value = temp.Text;
            }
        }

        private async void CheckBox_CheckChanged_1(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;
            if (checkbox.IsChecked)
            {
                checkbox.Color = Color.Black;
                var labKey = checkbox.Text;
                var getnode = await notesRepository.GetNoteByKeyAsync(noteKey, uid);
                getnode.LabelsList.Add(labKey);
                Note note = new Note
                {
                    Title=getnode.Title,
                    Notes=getnode.Notes,
                    ColorNote=getnode.ColorNote,
                    LabelsList=getnode.LabelsList
                };
                
                   firebasedata.Updatelabelstonotes(noteKey, note);
            }
        }
    }
}
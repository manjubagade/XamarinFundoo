//-----------------------------------------------------------------------
// <copyright file="SearchNotes.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System.Collections.Generic;
    using System.Linq;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using static FUNDOOAPP.DataFile.Enum;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchNotes : ContentPage
    {
        public List<Note> noteData;
        public SearchNotes()
        {
            this.InitializeComponent();
            this.Getdata();
            list.ItemsSource = this.noteData;
        }

        /// <summary>
        /// Getdatas this instance.
        /// </summary>
        public async void Getdata()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            NotesRepository notesRepository = new NotesRepository();
            List<Note> note = await notesRepository.GetNotesAsync(userid);
            note = note.Where(a => a.noteType != NoteType.isArchive && a.noteType != NoteType.isTrash).ToList();
            this.noteData = note;
        }

        /// <summary>
        /// Handles the TextChanged event of the SearchBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                list.ItemsSource = this.noteData;
            }
            else
            {
                list.ItemsSource = this.noteData.Where(x => (x.Title.ToLower().Contains(e.NewTextValue.ToLower())
                && x.Notes.ToLower().Contains(e.NewTextValue.ToLower())) || x.Title.ToLower().Contains(e.NewTextValue.ToLower()) 
                || x.Notes.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }
    } 
}
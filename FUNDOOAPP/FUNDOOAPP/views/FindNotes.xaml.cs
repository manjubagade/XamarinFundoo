//-----------------------------------------------------------------------
// <copyright file="FindNotes.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System.Collections.Generic;
    using System.Linq;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FUNDOOAPP.DataFile.Enum;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The note data
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class FindNotes : ContentPage
     {
        /// <summary>
        /// The note data
        /// </summary>
        public List<Note> noteData;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindNotes"/> class.
        /// </summary>
        public FindNotes()
         {
          this.InitializeComponent();
            this.GetNoteData();
            list.ItemsSource = this.noteData;
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
                list.ItemsSource = this.noteData.Where(x => x.Title.ToLower().Contains(e.NewTextValue.ToLower())
                && x.Notes.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        /// <summary>
        /// Gets the note data.
        /// </summary>
        public async void GetNoteData()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            NotesRepository notesRepository = new NotesRepository();
            List<Note> note = await notesRepository.GetNotesAsync(userid);
            note = note.Where(a => a.noteType != NoteType.isArchive && a.noteType != NoteType.isTrash).ToList();
            this.noteData = note;
        }
    }
}
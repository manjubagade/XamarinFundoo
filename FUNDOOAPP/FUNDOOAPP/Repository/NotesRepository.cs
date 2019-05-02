//-----------------------------------------------------------------------
// <copyright file="NotesRepository.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//---------------------------------------------------------------------
namespace FUNDOOAPP.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Models;
    using static FUNDOOAPP.DataFile.Enum;

    /// <summary>
    /// this NotesRepository class
    /// </summary>
    public class NotesRepository
    {
        /// <summary>
        /// The firebase client
        /// </summary>
        private FirebaseClient firebaseclient = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// Gets the notes asynchronous.
        /// </summary>
        /// <param name="uid">The user id </param>
        /// <returns>return task</returns>
        public async Task<List<Note>> GetNotesAsync(string uid)
        {
            List<Note> notesData = (await this.firebaseclient.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                noteType = item.Object.noteType,
                Key = item.Key,
                DateTime = item.Object.DateTime,
               ColorNote=item.Object.ColorNote,
               LabelsList = item.Object.LabelsList
     
            }).ToList();

            return notesData;
        }

        /// <summary>
        /// Gets the note by key asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="uid">The user id.</param>
        /// <returns>return task</returns>
        public async Task<Note> GetNoteByKeyAsync(string key, string uid)
        {
            Note note = await this.firebaseclient.Child("User").Child(uid).Child("Note").Child(key).OnceSingleAsync<Note>();
            return note;
        }

        /// <summary>
        /// Updates the note asynchronous.
        /// </summary>
        /// <param name="note">The note.</param>
        /// <param name="key">The key.</param>
        /// <param name="uid">The user id.</param>
        public async Task UpdateNoteAsync(Note note, string key, string uid)
        {
            await this.firebaseclient.Child("User").Child(uid).Child("Note").Child(key).PutAsync<Note>(new Note() { Title = note.Title, Notes = note.Notes, noteType = note.noteType,ColorNote=note.ColorNote,LabelsList=note.LabelsList });
        }

        /// <summary>
        /// Deletes the notes asynchronous.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="uid">The user id.</param>
        /// <returns>return task</returns>
        public async Task DeleteNotesAsync(string key, string uid)
        {
            await this.firebaseclient.Child("User").Child(uid).Child("Note").Child(key).DeleteAsync();
        }

        public async Task<IList<TrashData>> GetTrashAsync(string uid)
        {
            IList<TrashData> notesData = (await this.firebaseclient.Child("User").Child(uid).Child("Note").OnceAsync<TrashData>()).Select(item => new TrashData
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                key   = item.Object.key
            }).ToList();
    
            return notesData;
        }
    }
}
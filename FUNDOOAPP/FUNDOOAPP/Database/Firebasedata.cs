//-----------------------------------------------------------------------
// <copyright file="Firebasedata.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using Xamarin.Forms;

    /// <summary>
    /// Firebase data 
    /// </summary>
    public class Firebasedata
    {
        /// <summary>
        /// Firebase Client
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// Gets all persons.
        /// </summary>
        /// <returns>return task</returns>
        public async Task<List<Register>> GetAllPersons()
        {
            return (await this.firebase
              .Child("User")
              .OnceAsync<Register>()).Select(item => new Register
              {
                  Firstname = item.Object.Firstname,
                  Lastname = item.Object.Lastname,
                  Emailid = item.Object.Emailid,
                  Password = item.Object.Password,
                  Cpassword = item.Object.Cpassword
              }).ToList();
        }

        /// <summary>
        /// Adds the person
        /// </summary>
        /// <param name="firstname">The first name</param>
        /// <param name="lastname">The last name</param>
        /// <param name="emailid">The email id</param>
        /// <param name="password">The password</param>
        /// <param name="cpassword">The c password</param>
        /// <returns> return task</returns>
        public async Task AddPerson(string firstname, string lastname, string emailid, string password, string cpassword)
        {
            await this.firebase
              .Child("User")
              .PostAsync(new Register() { Firstname = firstname, Lastname = lastname, Emailid = emailid, Password = password, Cpassword = cpassword });
        }

        /// <summary>
        /// Gets the person.
        /// </summary>
        /// <param name="emailid">The email id.</param>
        /// <param name="password">The password.</param>
        /// <returns>return task</returns>
        public async Task<Register> GetPerson(string emailid, string password)
        {
            var allPersons = await this.GetAllPersons();
            await this.firebase
              .Child("Persons")
              .OnceAsync<Register>();
            return allPersons.Where(a => a.Emailid == emailid && a.Password == password).FirstOrDefault();
        }

        /// <summary>
        /// Create notes this instance.
        /// </summary>
        /// <returns>return task</returns>
        public async Task<List<Note>> Createnotes()
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            return (await this.firebase.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes
            }).ToList();
        }

        /// <summary>
        /// Creates the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns> return task</returns>
        public async Task CreateLabel(string label)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            await this.firebase.Child("User").Child(userid).Child("Lab").PostAsync(new LabelNotes { Label = label });
        }

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <returns>return task</returns>
        public async Task<List<LabelNotes>> GetAllLabels()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            //// returns all the person contained in the list
            return (await this.firebase
              .Child("User").Child(userid).Child("Lab").OnceAsync<LabelNotes>()).Select(item => new LabelNotes
              {
                  Label = item.Object.Label,
                  LabelKey = item.Key

              }).ToList();
        }

        public async Task<LabelNotes> GetLabel(string key, string uid)
        {
            LabelNotes labelNotes = await this.firebase.Child("User").Child(uid).Child("Lab").Child(key).OnceSingleAsync<LabelNotes>();
            return labelNotes;
        }

        public async void UpdateLable(LabelNotes note, string key, string uid)
        {
            await this.firebase.Child("User").Child(uid).Child("Lab").Child(key).PutAsync<LabelNotes>(new LabelNotes()
            {
                Label = note.Label
            });
        }
        public void DeleteLabel(string userId, string keyLabel)
        {
            this.firebase.Child("User").Child(userId).Child("Lab").Child(keyLabel).DeleteAsync();
        }

        public async void Updatelabelstonotes(string keyNote, Note note)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            await firebase.Child("User").Child(userid).Child("Note").Child(keyNote).PutAsync(new Note()
            {
                Title = note.Title,
                Notes = note.Notes,
                LabelsList=note.LabelsList,
                noteType=note.noteType,
                ColorNote=note.ColorNote
            });
        }
    }
}
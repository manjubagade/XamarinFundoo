using Firebase.Database;
using Firebase.Database.Query;
using FUNDOOAPP.Interfaces;
using FUNDOOAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FUNDOOAPP.Repository
{
   public class LabelRepository
    {
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        public async Task Createlabel(string label)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            await this.firebase.Child("User").Child(userid).Child("Labels").PostAsync<LabelNotes>(new LabelNotes
            {
                Label = label
            });
        }

        public async Task<List<LabelNotes>> Getalllabel()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            return (await firebase.Child("User").Child(userid).Child("Labels").OnceAsync<LabelNotes>()).Select(item => new
              LabelNotes
            {
                Label=item.Object.Label,
                LabelKey=item.Key,
            }).ToList();
        }

        public async Task<LabelNotes> GetLabelNotesAsync(string uid, string key)
        {
            LabelNotes labelNotes =await this.firebase.Child("User").Child(uid)
                .Child("Labels").Child(key).OnceSingleAsync<LabelNotes>();
            return labelNotes;
        }

        public async void UPdateLabel(LabelNotes note,string uid,string key)
        {
            var labels = this.firebase.Child("User").Child(uid).Child("Labels").Child(key).PutAsync<LabelNotes>(new LabelNotes
            {
                Label = note.Label
            });
        }

        public async void Updatelabelstonotes(string keyNote, Note note)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            await firebase.Child("User").Child(userid).Child("Note").Child(keyNote).PutAsync(new Note()
            {
                Title = note.Title,
                Notes = note.Notes,
                LabelsList = note.LabelsList,
                noteType = note.noteType,
                ColorNote = note.ColorNote
            });
        }
    }
}

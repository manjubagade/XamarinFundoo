//-----------------------------------------------------------------------
// <copyright file="Collabators.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Collabators : ContentPage
    {

        NotesRepository notesRepository = new NotesRepository();
        string id = null;
        string value = null;
        TapGestureRecognizer tapgester = new TapGestureRecognizer();

        ObservableCollection<string> source = new ObservableCollection<string>();

        FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        public Collabators(string key)
        {
            InitializeComponent();
            txtMail.ItemsSource = source;
            Data();
            this.value = key;
            var savenote = new TapGestureRecognizer();
            savenote.Tapped += Savenote_Tapped;
            savebtn.GestureRecognizers.Add(savenote);
            this.tapgesterrec();
        }

        public void tapgesterrec()
        {
            tapgester.Tapped += Tapgester_Tapped;
            exit.GestureRecognizers.Add(tapgester);
        }

        private void Tapgester_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("hello", "exit", "ok");
            txtMail.Text = string.Empty;
        }

        private void Savenote_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("hello", "thisnote", "ok");
        }

        public async void Data()
        {
            var users = await firebase.Child("User").OnceAsync<User>();

            IList<string> mail = new List<string>();

            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();

            foreach (var items in users)
            {
                if (items.ToString() != uid)
                {
                    var email = await firebase.Child("User").Child(items.Key).Child("Userinfo").OnceAsync<User>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.Email;
                        // var emailId = item.Key;
                        source.Add(emailDetails);
                    }
                }
            }
        }

        private async void Savebtn_Clicked(object sender, System.EventArgs e)
        {
            var users = await firebase.Child("User").OnceAsync<User>();

            IList<string> mail = new List<string>();

            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();

            foreach (var items in users)
            {
                if (items.ToString() != uid)
                {
                    var email = await firebase.Child("User").Child(items.Key).Child("Userinfo").OnceAsync<User>();
                    foreach (var item in email)
                    {
                        var emailDetails = item.Object.Email;

                        id = items.Key;
                        if (txtMail.Text == emailDetails)
                        {
                            Note notes = await notesRepository.GetNoteByKeyAsync(this.value, uid);

                            notes = new Note()
                            {
                                Title=notes.Title,
                                Notes=notes.Notes,
                                ColorNote=notes.ColorNote,
                                noteType=notes.noteType
                            };

                            await firebase.Child("User").Child(this.id).Child("Note").Child(value).PutAsync(new Note()
                            {
                                Title=notes.Title,
                                Notes=notes.Notes,
                                ColorNote=notes.ColorNote,
                                LabelsList=notes.LabelsList,
                                noteType=notes.noteType

                            });
                            await firebase.Child("User").Child(uid).Child("Note").Child(value).PutAsync(new Note()
                            {
                                Title = notes.Title,
                                Notes = notes.Notes,
                                ColorNote = notes.ColorNote,
                                LabelsList = notes.LabelsList,
                                noteType = notes.noteType

                            });

                            //source.Add(emailDetails);
                        }
                    }
                }
            }
        }
    }
}
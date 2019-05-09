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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Collabators : ContentPage
    {
        TapGestureRecognizer tapgester = new TapGestureRecognizer();

        ObservableCollection<string> source = new ObservableCollection<string>();

        FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        public Collabators()
        {
            InitializeComponent();
            txtMail.ItemsSource = source;
            Data();
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
    }
}
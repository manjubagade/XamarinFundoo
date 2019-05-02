using Firebase.Database;
using FUNDOOAPP.Database;
using FUNDOOAPP.Interfaces;
using FUNDOOAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FUNDOOAPP.views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLabels : ContentPage
	{
        Firebasedata firebasedata = new Firebasedata();

        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");
        private string keyLab = string.Empty;

        public EditLabels (string value)
		{
            this.keyLab = value;
            this.UpdateLable();
            InitializeComponent ();
		}


        public async void UpdateLable()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            LabelNotes labelNotes =await firebasedata.GetLabel(this.keyLab, userid);
            txtLabel.Text = labelNotes.Label;
        }

        public  void LabelEdits()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();

            LabelNotes labelNotes = new LabelNotes()
            {
                Label = txtLabel.Text
            };
            firebasedata.UpdateLable(labelNotes, keyLab, userid);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var alllabels = await firebasedata.GetAllLabels();
            lstLabels.ItemsSource = alllabels;
        }
        protected override bool OnBackButtonPressed()
        {
            LabelEdits();
            return base.OnBackButtonPressed();
        }

        private void Deletelabels_Clicked(object sender, EventArgs e)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            txtLabel.Text = null;
            firebasedata.DeleteLabel(userid, keyLab);
            Navigation.RemovePage(this);
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            LabelEdits();
            Navigation.RemovePage(this);
        }

        private void Deletedlabels_Clicked(object sender, EventArgs e)
        {
            LabelEdits();
            Navigation.RemovePage(this);
        }
    }
} 
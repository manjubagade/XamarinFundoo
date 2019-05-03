//-----------------------------------------------------------------------
// <copyright file="EditLabels.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using Firebase.Database;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The firebasedata
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class EditLabels : ContentPage
	{
        /// <summary>
        /// The firebasedata
        /// </summary>
        Firebasedata firebasedata = new Firebasedata();

        /// <summary>
        /// The firebase
        /// </summary>
        private FirebaseClient firebase = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// The key lab
        /// </summary>
        private string keyLab = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditLabels"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public EditLabels(string value)
         {
            this.keyLab = value;
            this.UpdateLable();
          this.InitializeComponent();
         }

        /// <summary>
        /// Updates the lable.
        /// </summary>
        public async void UpdateLable()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            LabelNotes labelNotes = await this.firebasedata.GetLabel(this.keyLab, userid);
            txtLabel.Text = labelNotes.Label;
        }

        /// <summary>
        /// Labels the edits.
        /// </summary>
        public void LabelEdits()
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();

            LabelNotes labelNotes = new LabelNotes()
            {
                Label = txtLabel.Text
            };
            this.firebasedata.UpdateLable(labelNotes, this.keyLab, userid);
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var alllabels = await this.firebasedata.GetAllLabels();
            lstLabels.ItemsSource = alllabels;
        }

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>
        /// To be added.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override bool OnBackButtonPressed()
        {
            this.LabelEdits();
            return base.OnBackButtonPressed();
        }

        /// <summary>
        /// Handles the Clicked event of the Deletelabels control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Deletelabels_Clicked(object sender, EventArgs e)
        {
            var userid = DependencyService.Get<IFirebaseAuthenticator>().User();
            txtLabel.Text = null;
            this.firebasedata.DeleteLabel(userid, this.keyLab);
            Navigation.RemovePage(this);
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            this.LabelEdits();
            Navigation.RemovePage(this);
        }

        /// <summary>
        /// Handles the Clicked event of the Deletedlabels control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Deletedlabels_Clicked(object sender, EventArgs e)
        {
            this.LabelEdits();
            Navigation.RemovePage(this);
        }
    }
}
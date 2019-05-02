//-----------------------------------------------------------------------
// <copyright file="CameraPermition.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Models;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="Labels" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Labels : ContentPage
    {
        /// <summary>
        /// The firebase
        /// </summary>
        Firebasedata firebase = new Firebasedata();

        /// <summary>
        /// Initializes a new instance of the <see cref="Labels"/> class.
        /// </summary>
        public Labels()
         {
            this.InitializeComponent();
         }

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
                //// Overiding base class on appearing 
                base.OnAppearing();

                //// Listing all the person in the list
                var allLabels = await this.firebase.GetAllLabels();
                lstLabels.ItemsSource = allLabels;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await this.firebase.CreateLabel(txtLabel.Text);
                //// Empty the placeholder
                txtLabel.Text = string.Empty;
                var allLabels = await this.firebase.GetAllLabels();
                lstLabels.ItemsSource = allLabels;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void LstLabels_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var entity = (LabelNotes)e.SelectedItem;
            var temp = entity.LabelKey;
            Navigation.PushAsync(new EditLabels(temp));
        }
    }
}
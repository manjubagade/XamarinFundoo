//-----------------------------------------------------------------------
// <copyright file="Drawingpage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views.RemiderAndLocation
{
    using System;
    using System.IO;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="Drawingpage" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Drawingpage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Drawingpage"/> class.
        /// </summary>
        public Drawingpage()
        {
          this.InitializeComponent();
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
          Stream image = await PadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg);
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            PadView.Clear();
        }
    } 
}
//-----------------------------------------------------------------------
// <copyright file="CameraPermition.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using Plugin.Media;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="CameraPermition" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class CameraPermition : ContentPage
      {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraPermition"/> class.
        /// </summary>
        public CameraPermition()
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
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsTakePhotoSupported && CrossMedia.Current.IsPickPhotoSupported)
            {
                await this.DisplayAlert("alert", "take photo not supported", "ok");
                return;
            }
            else
            {
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Images",
                    Name = DateTime.Now + "_test.jpg"
                });
                if (file == null)
                    return;

                await this.DisplayAlert("file path", file.Path, "ok");

                MyImage.Source = ImageSource.FromStream(() =>
                {
                    var steam = file.GetStream();
                    return steam;
                });
            }      
        }
    }
}
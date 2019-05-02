//-----------------------------------------------------------------------
// <copyright file="SharePage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views.RemiderAndLocation
{
    using System;
    using Plugin.Media;
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="SharePage" /> class.
    /// </summary>
    /// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
    public partial class SharePage : PopupPage
     {
        /// <summary>
        /// Initializes a new instance of the <see cref="SharePage"/> class.
        /// </summary>
        public SharePage()
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
       
           await Navigation.PushModalAsync(new gallarypermition());
            await PopupNavigation.Instance.PopAsync(true);   
        }

        /// <summary>
        /// Handles the 1 event of the Button_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked_1(object sender, EventArgs e)
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
            //await Navigation.PushModalAsync(new CameraPermition());
            await PopupNavigation.Instance.PopAsync(true);
        }
    }
}
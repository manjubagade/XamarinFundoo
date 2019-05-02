//-----------------------------------------------------------------------
// <copyright file="gallarypermition.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using Firebase.Database;
    using Firebase.Storage;
    using FUNDOOAPP.Repository;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;    
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="gallarypermition" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class gallarypermition : ContentPage
     {

        UserRepository userRepository = new UserRepository();
        /// <summary>
        /// The file
        /// </summary>
       private MediaFile file;

        /// <summary>
        /// Initializes a new instance of the <see cref="gallarypermition"/> class.
        /// </summary>
        public gallarypermition()
         {
            this.InitializeComponent();
         }

        /// <summary>
        /// Handles the Clicked event of the button Pick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnPick_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                this.file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });

                if (this.file == null)
                    return;

                imgChoosed.Source = ImageSource.FromStream(() =>
                {
                    var imageStram = this.file.GetStream();
                    return imageStram;
                });
                await this.StoreImages(this.file.GetStream());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the button Store control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnStore_Clicked(object sender, EventArgs e)
        {
            await this.StoreImages(this.file.GetStream());
        }

        /// <summary>
        /// Stores the images.
        /// </summary>
        /// <param name="imageStream">The image stream.</param>
        /// <returns>return task</returns>
        public async Task StoreImages(Stream imageStream)
        {
            FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");
           // string timeStamp = GetTimestamp(DateTime.Now);
            var stroageImage = await new FirebaseStorage("fundooapp-810e7.appspot.com")
                .Child("XamarinMonkeys").Child("image.jpg")
                //.Child("image_" + timeStamp + ".jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            // return imgurl;
            await userRepository.GetimageSouce(imgurl);
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>return task</returns>
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
    }
}
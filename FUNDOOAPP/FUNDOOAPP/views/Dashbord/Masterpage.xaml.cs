//-----------------------------------------------------------------------
// <copyright file="Masterpage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views.Dashbord
{
    using System;
    using System.Collections.Generic;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// Initializes a new instance of the <see cref="Masterpage" /> class.
    /// </summary>
    /// <seealso cref="Xamarin.Forms.MasterDetailPage" />
    public partial class Masterpage : MasterDetailPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Masterpage"/> class.
        /// </summary>
        public Masterpage()
        {
            this.InitializeComponent();
            ////var imgsource = new UriImageSource { Uri = new Uri("https://firebasestorage.googleapis.com/v0/b/fundooapp-810e7.appspot.com/o/XamarinMonkeys%2Fimage.jpg?alt=media&token=70743c7b-b1c7-472e-8af4-58c13b2da0ef") };
            ////imgsource.CachingEnabled = false;
            ////ProfilePic.Source = imgsource;
            ////ProfilePic.HeightRequest = 100;
            ////ProfilePic.WidthRequest = 100;
            OnAppearing();
            var userImage = new TapGestureRecognizer();
            //// Binding events 
            userImage.Tapped += this.userImage_Tapped;
            ///// Associating tap events to the image buttons    
            ProfilePic.GestureRecognizers.Add(userImage);
            this.MenuList = new List<MasterPageItem>();
            this.MenuList.Add(new MasterPageItem() { Title = "Notes", Icon = "keep.png", TargetType = typeof(Homepage) });
            this.MenuList.Add(new MasterPageItem() { Title = "Reminders", Icon = "rem.png", TargetType = typeof(Reminder) });
            this.MenuList.Add(new MasterPageItem() { Title = "Create new label", Icon = "plus.png", TargetType = typeof(Labels) });
            this.MenuList.Add(new MasterPageItem() { Title = "Archive", Icon = "archive.png", TargetType = typeof(Archive) });
            this.MenuList.Add(new MasterPageItem() { Title = "Trash", Icon = "delete.png", TargetType = typeof(Delete) });
            this.MenuList.Add(new MasterPageItem() { Title = "Setting", Icon = "setting.png", TargetType = typeof(Delete) });
            this.navigationDrawerList.ItemsSource = this.MenuList;
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Homepage)));
        }

        /// <summary>
        /// Gets or sets the menu list.
        /// </summary>
        /// <value>
        /// The menu list.
        /// </value>
        public IList<MasterPageItem> MenuList { get; set; }

        /// <summary>
        /// Called when [item selected].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectedItemChangedEventArgs"/> instance containing the event data.</param>
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (MasterPageItem)e.SelectedItem;
            Type page = item.TargetType;
            this.Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            this.IsPresented = false;
        }

        /// <summary>
        /// this userImage_Tapped instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void userImage_Tapped(object sender, EventArgs e)
        {
          await  Navigation.PushModalAsync(new gallarypermition());
        }

        /// <summary>
        /// this OnAppearing instance
        /// </summary>
        protected async override void OnAppearing()
        {
            UserRepository userRepository = new UserRepository();
            User user = await userRepository.GetUserById();
            if (user.Imageurl != null)
            {
                var imgsource = new UriImageSource { Uri = new 
                    Uri(user.Imageurl) };
                imgsource.CachingEnabled = false;
                ProfilePic.Source = imgsource;
                ProfilePic.HeightRequest = 70;
                ProfilePic.WidthRequest = 70;
            }

            base.OnAppearing();
        }
    }
}
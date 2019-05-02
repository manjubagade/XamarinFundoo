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
    }
}

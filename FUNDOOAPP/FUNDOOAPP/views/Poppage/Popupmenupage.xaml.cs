using FUNDOOAPP.Interfaces;
using FUNDOOAPP.Models;
using FUNDOOAPP.Repository;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FUNDOOAPP.DataFile.Enum;

namespace FUNDOOAPP.views.Poppage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Popupmenupage : PopupPage
    {
       
        private NotesRepository notesRepository = new NotesRepository();
        public Popupmenupage ()
		{
            InitializeComponent ();
		}

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
           await DisplayAlert("hello", "good", "ok");
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="MainPageViewModel.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Validation;
    using Xamarin.Forms;

    /// <summary>
    /// this MainPageViewModel instance
    /// </summary>
    public class MainPageViewModel
    {
        /// <summary>
        /// The page
        /// </summary>
        private Page page;

        /// <summary>
        /// Gets or sets the login model.
        /// </summary>
        /// <value>
        /// The login model.
        /// </value>
        public LoginModel LoginModel { get; set; } = new LoginModel();

        /// <summary>
        /// Gets the login in command.
        /// </summary>
        /// <value>
        /// The login in command.
        /// </value>
        public ICommand LoginInCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel"/> class.
        /// </summary>
        /// <param name="page">The page</param>
        public MainPageViewModel(Page page)
        {
            this.page = page;
            this.LoginInCommand = new Command(async () => await this.LoginAsync());
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <returns>return task</returns>
        private async Task LoginAsync()
        {
            if (!ValidationHelper.IsFormValid(LoginModel, this.page))
            {
                return;
            }

            await this.page.DisplayAlert("Success", "Validation Success!", "OK");
        }
    }
}
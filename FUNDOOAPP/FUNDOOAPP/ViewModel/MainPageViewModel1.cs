//-----------------------------------------------------------------------
// <copyright file="MainPageViewModel1.cs" company="BridgeLabz">
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
    /// this MainPageViewModel1 instance
    /// </summary>
    public class MainPageViewModel1
    {
        /// <summary>
        /// The page
        /// </summary>
        private Page page;

        /// <summary>
        /// Gets or sets the sign up model.
        /// </summary>
        /// <value>
        /// The sign up model.
        /// </value>
        public Signupmodel Signupmodel { get; set; } = new Signupmodel();

        /// <summary>
        /// Gets the login in command.
        /// </summary>
        /// <value>
        /// The login in command.
        /// </value>
        public ICommand LoginInCommand { get; }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <returns>return task</returns>
        private async Task LoginAsync()
        {
            if (!ValidationHelper.IsFormValid(Signupmodel, this.page))
            {
                return;
            }

            await this.page.DisplayAlert("Success", "Validation Success!", "OK");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPageViewModel1"/> class.
        /// </summary>
        /// <param name="page">The page.</param>
        public MainPageViewModel1(Page page)
        {
            this.page = page;
            this.LoginInCommand = new Command(async () => await this.LoginAsync());
        }
    }
}
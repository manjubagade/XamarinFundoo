//-----------------------------------------------------------------------
// <copyright file="IFirebaseAuthenticator.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// this IFirebaseAuthenticator instance
    /// </summary>
    public interface IFirebaseAuthenticator
    {
        /// <summary>
        /// Login with Email Password 
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>return task</returns>
        Task<bool> LoginwithEmailPassword(string email, string password);

        /// <summary>
        /// Adds the user with email password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>return task</returns>
        Task<string> AddUserWithEmailPassword(string email, string password);

        /// <summary>
        /// Forgot passwords the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        void Forgotpassword(string email);

        /// <summary>
        /// Sign outs this instance.
        /// </summary>
        /// <returns>return task</returns>
        string Sigout();

        /// <summary>
        /// Users this instance.
        /// </summary>
        /// <returns>return task</returns>
        string User();

        /// <summary>
        /// Statuses this instance.
        /// </summary>
        /// <returns>return task</returns>
        bool Status();
    }
}
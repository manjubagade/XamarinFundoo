using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Auth;
using FUNDOOAPP.Interfaces;
using FUNDOOAPP.Models;
using Firebase.Database.Query;
using FUNDOOAPP.Droid.Login;
using Xamarin.Forms;
using Firebase;

[assembly: Dependency(typeof(LoginUser))]
namespace FUNDOOAPP.Droid.Login
{


    public class LoginUser : IFirebaseAuthenticator
    {
        FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");
        public async Task<bool> LoginwithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var response = Status();
                return response;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Status()
        {
            try
            {
                var user = FirebaseAuth.Instance.CurrentUser;
                if (user != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static string DataT()
        {
            string user12 = FirebaseAuth.Instance.CurrentUser.Uid;
            return user12;
        }

        public static string PackageName { get; }

        public async Task<string> AddUserWithEmailPassword(string email, string password)
        {
            var response = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            using (var user = response.User)
            using (var actionCode = ActionCodeSettings.NewBuilder().SetAndroidPackageName(PackageName, true, "0").Build())
            {
                await user.SendEmailVerificationAsync(actionCode);
            }
            return response.User.Uid;
        }

        public void Forgotpassword(string email)
        {
            var respone = FirebaseAuth.Instance.SendPasswordResetEmail(email);
        }

        public string Sigout()
        {
            string user = null;
            try
            {
                FirebaseAuth.Instance.SignOut();
                user = FirebaseAuth.Instance.CurrentUser.Uid;
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string User()
        {
            string userId = FirebaseAuth.Instance.CurrentUser.Uid;
            return userId;
        }


    }
}
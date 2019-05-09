using Firebase.Database;
using Firebase.Database.Query;
using FUNDOOAPP.Interfaces;
using FUNDOOAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FUNDOOAPP.Repository
{
    public class ProfileRepository
    {

        private FirebaseClient firebaseclient = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        public async void UploadProfilePic(string imageurl)
        {
            string uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            await firebaseclient.Child("User").Child(uid).Child("Profile").PostAsync<User>(new User
            {
                Imageurl = imageurl
            });
        } 
    }
}

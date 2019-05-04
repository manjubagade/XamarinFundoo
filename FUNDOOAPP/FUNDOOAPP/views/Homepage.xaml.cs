//-----------------------------------------------------------------------
// <copyright file="homepage.xaml.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Firebase.Database;
    using Firebase.Database.Query;
    using FUNDOOAPP.Database;
    using FUNDOOAPP.Interfaces;
    using FUNDOOAPP.Models;
    using FUNDOOAPP.Repository;
    using FUNDOOAPP.ViewModel;
    using FUNDOOAPP.views.RemiderAndLocation;
    using Rg.Plugins.Popup.Services;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static FUNDOOAPP.DataFile.Enum;

    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// The homepage class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.ContentPage" />
    public partial class Homepage : ContentPage
    {
        /// <summary>
        /// The firebase
        /// </summary>
        private Firebasedata firebase = new Firebasedata();

        /// <summary>
        /// The firebase client
        /// </summary>
        private FirebaseClient firebaseclint = new FirebaseClient("https://fundooapp-810e7.firebaseio.com/");

        /// <summary>
        /// The notes repository
        /// </summary>
        private NotesRepository notesRepository = new NotesRepository();

        /// <summary>
        /// Initializes a new instance of the <see cref="Homepage"/> class.
        /// </summary>
        public Homepage()
        {
            this.InitializeComponent();
            
        }

        /// <summary>
        /// Handles the Clicked event of the Save control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Save_Clicked(object sender, EventArgs e)
        {
            var response = DependencyService.Get<IFirebaseAuthenticator>().Sigout();
            if (response != null)
            {
                await this.Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await this.DisplayAlert("Log Out", "Successfully", "ok ");
                await this.Navigation.PushModalAsync(new Login());
            }
        }

        /// <summary>
        /// Handles the button event of the List control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void List_button(object sender, EventArgs e)
        {
            var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
            IList<Note> notesData = (await this.firebaseclint.Child("User").Child(uid).Child("Note").OnceAsync<Note>()).Select(item => new Note
            {
                Title = item.Object.Title,
                Notes = item.Object.Notes,
                noteType = item.Object.noteType
            }).ToList();


            IList<Note> listNote = new List<Note>();
            if (notesData == null)
            {
                foreach (var item in notesData)
                {
                    if (item.noteType == NoteType.isNote)
                    {
                        listNote.Add(item);
                    }
                }
                this.NoteGridView(listNote);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Notes());
        }

        /// <summary>
        /// Notes the grid view.
        /// </summary>
        /// <param name="list">The list.</param>
        public async void NoteGridView(IList<Note> list)
        {
            try
            {
                var alllabels = await this.firebase.GetAllLabels();
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout.Margin = 5;
                int rowCount = 0;
                for (int row = 0; row < list.Count; row++)
                {
                    if (row % 2 == 0)
                    {
                        GridLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    } 
                }

                var productIndex = 0;
                var indexe = -1;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        Note data = null;
                        indexe++;
                        if (indexe < list.Count)
                        {
                            data = list[indexe];
                        }

                        if (productIndex >= list.Count)
                        {
                            break;
                        }

                        productIndex += 1;
                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Title,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.Key,
                            IsVisible = false
                        };

                        var content = new Xamarin.Forms.Label
                        {
                            Text = data.Notes,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        StackLayout layout = new StackLayout()
                        {
                            Spacing = 2,
                            Margin = 2,
                           // BackgroundColor = Color.White
                        };

                       // var panGesture = new PanGestureRecognizer();
                      //  panGesture.PanUpdated += OnPanUpdated;
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                       // layout.GestureRecognizers.Add(panGesture);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        //layout.BackgroundColor = Color.White;

                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.CornerRadius = 25;
                        FrameColorSetter.GetColor(data, frame);
                        frame.Content = layout;

                        foreach (LabelNotes model in alllabels)
                        {
                            IList<string> lists = data.LabelsList;
                            foreach (var labelId in lists)
                            {
                                if (model.LabelKey.Equals(labelId))
                                {
                                    var labelName = new Label
                                    {
                                        Text = model.Label,
                                        HorizontalOptions = LayoutOptions.Center,
                                        VerticalOptions = LayoutOptions.Start,
                                        FontSize = 11
                                    };
                                    var labelFrame = new Frame();
                                    labelFrame.CornerRadius = 28;
                                    labelFrame.HeightRequest = 14;
                                    labelFrame.Content = labelName;
                                    labelFrame.BorderColor = Color.Gray;
                                    labelFrame.BackgroundColor = Color.FromHex(FrameColorSetter.GetHexColor(data));
                                    layout.Children.Add(labelFrame);
                                }
                            }
                        }

                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout layout123 = (StackLayout)sender;
                            IList<View> item = layout123.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                            Navigation.PushAsync(new UpdateNote(Keyval));
                        };

                        GridLayout.Children.Add(frame, columnIndex, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        double x, y;

        /// <summary>
        /// Called when [pan updated].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PanUpdatedEventArgs"/> instance containing the event data.</param>
        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {

                case GestureStatus.Running:
                    // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                    Content.TranslationX = Math.Max(Math.Min(0, x + e.TotalX), -Math.Abs(Content.Width - App.ScreenWidth));
                    Content.TranslationY = Math.Max(Math.Min(0, y + e.TotalY), -Math.Abs(Content.Height - App.ScreenHeight));
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    x = Content.TranslationX;
                    y = Content.TranslationY;
                    break;
            }
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected async override void OnAppearing()
        {
            try
            {
                var uid = DependencyService.Get<IFirebaseAuthenticator>().User();
                var notes = await this.notesRepository.GetNotesAsync(uid);               
                IList<Note> listNote = new List<Note>();
                IList<Note> listnote1 = new List<Note>();
                if (notes != null)
                {
                    foreach (var item in notes)
                    {
                        if (item.noteType == NoteType.isNote)
                        {
                            listNote.Add(item);
                        }
                    }
                    this.NoteGridView(listNote);
                    foreach (var item in notes)
                    {
                        if (item.noteType == NoteType.ispin)
                        {
                            listnote1.Add(item);
                        }
                    }
                    this.NoteGridPin(listnote1);
                }
                UserRepository userRepository = new UserRepository();
                User user = await userRepository.GetUserById();
                if (user.Imageurl != null)
                {
                    ImageSource profileImage = user.Imageurl;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Clicked event of the Cancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViewNote());
             this.Navigation.RemovePage(this);
        }

        /// <summary>
        /// Handles the Clicked event of the ImageButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(new SharePage());
            //await Navigation.PushAsync(new CameraPermition());
        }

        /// <summary>
        /// Handles the 1 event of the ImageButton_Clicked control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Drawingpage());
        }

        /// <summary>
        /// Handles the Clicked event of the Searchbar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Searchbar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FindNotes());
        }

        /// <summary>
        /// Notes the grid pin.
        /// </summary>
        /// <param name="listpin">The listpin.</param>
        public void NoteGridPin(IList<Note> listpin)
        {
            try
            {
                GridLayout1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(170) });
                GridLayout1.Margin = 5;
                int rowCount = 0;
                for (int row = 0; row < listpin.Count; row++)
                {
                    if (row % 2 == 0)
                    {
                        GridLayout1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) });
                        rowCount++;
                    }
                }

                var productIndex = 0;
                var indexe = -1;

                for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
                {
                    for (int columnIndex = 0; columnIndex < 2; columnIndex++)
                    {
                        Note data = null;
                        indexe++;
                        if (indexe < listpin.Count)
                        {
                            data = listpin[indexe];
                        }

                        if (productIndex >= listpin.Count)
                        {
                            break;
                        }

                        productIndex += 1;
                        var label = new Xamarin.Forms.Label
                        {
                            Text = data.Title,
                            TextColor = Color.Black,
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        var labelKey = new Xamarin.Forms.Label
                        {
                            Text = data.Key,
                            IsVisible = false
                        };

                        var content = new Xamarin.Forms.Label
                        {
                            Text = data.Notes,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.Start,
                        };

                        StackLayout layout = new StackLayout()
                        {
                            Spacing = 2,
                            Margin = 2,
                           // BackgroundColor = Color.White
                        };
                        var tapGestureRecognizer = new TapGestureRecognizer();
                        layout.Children.Add(labelKey);
                        layout.Children.Add(label);
                        layout.Children.Add(content);
                        layout.GestureRecognizers.Add(tapGestureRecognizer);
                        layout.Spacing = 2;
                        layout.Margin = 2;
                        //layout.BackgroundColor = Color.White;

                        var frame = new Frame();
                        frame.BorderColor = Color.Black;
                        frame.CornerRadius = 25;
                         FrameColorSetter.GetColor(data, frame);
                        frame.Content = layout;
                        tapGestureRecognizer.Tapped += (object sender, EventArgs args) =>
                        {
                            StackLayout layout123 = (StackLayout)sender;
                            IList<View> item = layout123.Children;
                            Xamarin.Forms.Label KeyValue = (Xamarin.Forms.Label)item[0];
                            var Keyval = KeyValue.Text;
                            Navigation.PushAsync(new UpdateNote(Keyval));
                        };

                        GridLayout1.Children.Add(frame, columnIndex, rowIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new gallarypermition());
        }

       

    }
}
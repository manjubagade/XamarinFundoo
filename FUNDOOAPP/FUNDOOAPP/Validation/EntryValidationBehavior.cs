//-----------------------------------------------------------------------
// <copyright file="EntryValidationBehavior.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FUNDOOAPP.Validation
{
    using Xamarin.Forms;

    /// <summary>
    /// the validation class
    /// </summary>
    /// <seealso cref="Xamarin.Forms.Behavior{Xamarin.Forms.Entry}" />
    public class EntryValidationBehavior : Behavior<Entry>
    {
        /// <summary>
        /// The is valid property
        /// </summary>
        public static readonly BindableProperty IsvalidProperty = BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EntryValidationBehavior), false, BindingMode.OneWayToSource);

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get { return (bool)GetValue(IsvalidProperty); }
            set { this.SetValue(IsvalidProperty, value); }
        }

        /// <summary>
        /// Attaches to the superclass and then calls the <see cref="M:Xamarin.Forms.Behavior`1.OnAttachedTo(`0)" /> method on this object.
        /// </summary>
        /// <param name="bindable">The bind object to which the behavior was attached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += this.HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        /// <summary>
        /// Calls the <see cref="M:Xamarin.Forms.Behavior`1.OnDetachingFrom(`0)" /> method and then detaches from the superclass.
        /// </summary>
        /// <param name="bindable">The bind object from which the behavior was detached.</param>
        /// <remarks>
        /// To be added.
        /// </remarks>
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged += this.HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }

        /// <summary>
        /// Handles the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            var isvalid = !string.IsNullOrWhiteSpace(e.NewTextValue);
            this.IsValid = isvalid;
        }
    }
}
//-----------------------------------------------------------------------
// <copyright file="Note.cs" company="BridgeLabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------


namespace FUNDOOAPP.Models
{
    using System.Collections.Generic;
    using static FUNDOOAPP.DataFile.Enum;

    /// <summary>
    /// this Note class
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the type of the note.
        /// </summary>
        /// <value>
        /// The type of the note.
        /// </value>
        public NoteType noteType { get; set; }

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>
        /// The date time.
        /// </value>
        public string DateTime { get; set; }

        public string ColorNote { get; set; }

        public IList<string> LabelsList { get; set; } = new List<string>();
    }
}

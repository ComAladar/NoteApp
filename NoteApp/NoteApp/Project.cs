using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс проекта. Содержит список всех записей.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Свойство Notes для создания списка заметок.
        /// </summary>
        public List<Note> Notes { get; set; } = new List<Note>();
    
    }
}

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
        private List<Note> _notes=new List<Note>();

        public List<Note> Notes
        {
            get
            {
               return _notes;
            }
            set
            {
                _notes = value;
            }
        }
    }
}

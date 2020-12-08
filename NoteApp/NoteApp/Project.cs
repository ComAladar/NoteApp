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
        public List<Note> CatNotes { get; set; } = new List<Note>();

        /// <summary>
        /// Метод сортирует список записей по последнему редактированию.
        /// </summary>
        public void SortList()
        {
            var SortList = Notes.OrderByDescending(item => item.Modified).ToList();
            Notes = SortList;
        }

        /// <summary>
        /// Метод сортирует список записей определенной категории по последнему редактированию.
        /// </summary>
        /// <param name="category"></param>
        public void SortList(NoteCategory category)
        { 
            var SortList = Notes.Where(item => item.Category==category).OrderByDescending(item=>item.Modified).ToList();
            CatNotes = SortList;
        }

        /// <summary>
        /// Свойство хранит выбранную заметку в последний раз работы с программой.
        /// </summary>
        public int CurrentNote { get; set; }

        /// <summary>
        /// Свойство хранит выбранную категорию отображаемых заметок в последний раз работы с программой.
        /// </summary>
        public int CurrentCategory { get; set; }

    }
}

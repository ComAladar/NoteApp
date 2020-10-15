using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс Заметки(Записи).Хранит основную информацию о заметке: имя, категорию заметки, текст заметки, Время создания заметки и время последнего изменения заметки.
    /// </summary>
    public class Note
    {
        private string _name; 
        private NoteCategories _categories; 
        private string _notetext;
        private readonly DateTime _timeofcreation=DateTime.Now;
        private DateTime _timeofedit=DateTime.Now; 

        /// <summary>
        /// Возвращает и задает названия заметки.
        /// <exception cref="_name">Не может быть больше 50 символов.</exception>
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value.Length >= 50)
                {
                    throw new ArgumentException("Максимальная длина названия записи должна быть не больше 50 символов.");
                }
                else
                {
                    TimeOfEdit = DateTime.Now;
                    if (value == String.Empty) 
                    {
                        _name = "Без Названия";
                    }
                    else _name = value;
                }
            }

        }
        /// <summary>
        /// Возвращает и задает значение категории.
        /// </summary>
        public NoteCategories Categories
        {
            get { return _categories; }
            set
            {
                TimeOfEdit = DateTime.Now;
                _categories = value;
            }
        }
        /// <summary>
        /// Возвращает и задает значение текста.
        /// </summary>
        public string NoteText
        {
            get { return _notetext; }
            set
            {
                if (value == String.Empty)
                {
                    _notetext = "Введите текст.";
                }
                else
                {
                    TimeOfEdit = DateTime.Now;
                    _notetext = value;
                }

                
            }

        }
        
        /// <summary>
        /// Задает и возвращает последнее время редактирования заметки.
        /// </summary>
        public DateTime TimeOfEdit
        {
            get { return _timeofedit;}
            set
            {
                _timeofedit=DateTime.Now;

            }

        }
        /// <summary>
        /// Возвращает время создания файла.
        /// </summary>
        public DateTime TimeOfCreation
        {
            get { return _timeofcreation; }
        }

    }
}

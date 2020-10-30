using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Класс Заметки.Хранит основную информацию о заметке.
    /// </summary>
    public class Note:ICloneable
    {
        /// <summary>
        /// Поле названия заметки.
        /// </summary>
        private string _name; 
        /// <summary>
        /// Поле категории заметки.
        /// </summary>
        private NoteCategory _category; 
        /// <summary>
        /// Поле текста заметки.
        /// </summary>
        private string _text;
        /// <summary>
        /// Поле время создания заметки.
        /// </summary>
        private readonly DateTime _created=DateTime.Now;
        /// <summary>
        /// Поле время изменения заметки.
        /// </summary>
        private DateTime _modified=DateTime.Now; 

        /// <summary>
        /// Возвращает и задает название заметки.
        /// <exception cref="ArgumentException">Не может быть больше 50 символов.</exception>
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length > 50)
                {
                    throw new ArgumentException("Максимальная длина названия записи должна быть не больше 50 символов.");
                }
                else
                {
                    if (value == String.Empty) 
                    {
                        _name = "Без Названия";
                    }
                    else _name = value;
                    Modified = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Возвращает и задает значение категории.
        /// </summary>
        public NoteCategory Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                Modified = DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает и задает значение текста.
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (value == String.Empty)
                {
                    _text = "Введите текст.";
                }
                else
                {
                    _text = value;
                }
                Modified = DateTime.Now;
            }
        }
        
        /// <summary>
        /// Возвращает и задает последнее время редактирования заметки.
        /// </summary>
        public DateTime Modified
        {
            get
            {
                return _modified;
            }
            set
            {
                _modified = DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает время создания заметки.
        /// </summary>
        public DateTime Created
        {
            get { return _created; }
        }

        public object Clone()
        {
            return new Note()
            {
                Name = this.Name,
                Text = this.Text,
                Category = this.Category
            };
        }

        /// <summary>
        /// Пустой конструктор класса Note.
        /// </summary>
        public Note() { }

        /// <summary>
        /// Конструктор класса Note с заданием названия, текста и категории.
        /// </summary>
        /// <param name="name">Название заметки.</param>
        /// <param name="text">Текст заметки.</param>
        /// <param name="category">Категория заметки.</param>
        public Note(string name,string text,NoteCategory category)
        {
            Name = name;
            Text = text;
            Category = category;
        }
    }
}

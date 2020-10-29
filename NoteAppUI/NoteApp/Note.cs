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
        private string _name; 
        private NoteCategory _category; 
        private string _text;
        private readonly DateTime _creationtime=DateTime.Now;
        private DateTime _modifiedtime=DateTime.Now; 

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
                    ModifiedTime = DateTime.Now;
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
                ModifiedTime = DateTime.Now;
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
                ModifiedTime = DateTime.Now;
            }
        }
        
        /// <summary>
        /// Возвращает и задает последнее время редактирования заметки.
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return _modifiedtime;
            }
            set
            {
                _modifiedtime = DateTime.Now;
            }
        }

        /// <summary>
        /// Возвращает время создания заметки.
        /// </summary>
        public DateTime CreationTime
        {
            get { return _creationtime; }
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

        public Note()
        {

        }

        public Note(string name,string text,NoteCategory category)
        {
            Name = name;
            Text = text;
            Category = category;
        }
    }
}

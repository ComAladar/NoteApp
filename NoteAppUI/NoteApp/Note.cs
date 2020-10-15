using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    public class Note
    {
        private string _name; //Доступны для измнения
        private NoteCategories _categories; //От 0 до ....
        private string _notetext;
        private readonly DateTime _timeofcreation=DateTime.Now;//Статичное, при создании заметки
        private DateTime _timeofedit=DateTime.Now; //Меняется при изменении первых трех

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
                    if (value == String.Empty) //IsEmpty? Проверка на длину символов(50)
                    {
                        _name = "Без Названия";
                    }
                    else _name = value;
                }
            }

        }

        public NoteCategories Categories
        {
            get { return _categories; }
            set
            {
                TimeOfEdit = DateTime.Now;
                _categories = value;
            }
        }

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
        
        public DateTime TimeOfEdit
        {
            get { return _timeofedit;}
            set
            {
                _timeofedit=DateTime.Now;

            }

        }
        public DateTime TimeOfCreation
        {
            get { return _timeofcreation; }
        }

    }
}

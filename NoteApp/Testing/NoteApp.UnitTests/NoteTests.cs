using System;
using System.Threading;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTests
    {
        private Note CreateNote()
        {
            var note = new Note();
            return note;
        }

        [Test]
        public void NameGetCorrectValueReturnCorrectValue()
        {
            //Setup
            var note=CreateNote();

            //Act
            note.Name = "Информация о корме для животного.";
            var actual = note.Name;

            //Assert
            Assert.AreEqual(note.Name, actual, "Геттер возвращает неправильное название заметки.");
        }

        [Test]
        public void NameSetCorrectValueSetCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = "Информация о корме для животного";
            note.Name = expected;

            //Assert
            Assert.AreEqual(expected, note.Name, "Сеттер присваивает неправильное название заметки.");
        }

        [Test]
        public void NameSetEmptyStringReturnDefaultString()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = "Без Названия";
            note.Name = "";

            //Assert
            Assert.AreEqual(expected, note.Name, "Сеттер не задает стандартное значение пустой строке названия заметки.");
        }

        [Test]
        public void NameSetThrowsArgumentExceptionLongName()
        {
            //Setup
            var note = CreateNote();

            //Act
            var WrongName = "12345678901234567890123456789012345678901234567890123";

            //Assert
            Assert.Throws<ArgumentException>((() => { note.Name = WrongName; }), "У сеттера не происходит срабатывание ArgumentException.");
        }

        [Test]
        public void CategoryGetCorrectValueReturnCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            note.Category = NoteCategory.Home;
            var actual = note.Category;

            //Assert
            Assert.AreEqual(note.Category, actual, "Геттер возвращает неправильную категорию.");
        }

        [Test]
        public void CategorySetCorrectValueSetCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = NoteCategory.Home;
            note.Category = expected;

            //Assert
            Assert.AreEqual(expected, note.Category, "Сеттер присваивает неправильную категорию.");
        }

        [Test]
        public void TextGetCorrectValueReturnCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            note.Text = "Текст";
            var actual = note.Text;

            //Assert
            Assert.AreEqual(note.Text, actual, "Геттер возвращает неправильную строку текста заметки.");
        }

        [Test]
        public void TextSetCorrectValueSetCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = note.Text;
            note.Text = expected;

            //Assert
            Assert.AreEqual(expected, note.Text, "Сеттер присваивает неправильную строку текста заметки.");
        }

        [Test]
        public void TextSetEmptyStringReturnDefaultString()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = "Введите текст.";
            note.Text = "";

            //Assert
            Assert.AreEqual(expected, note.Text, "Сеттер не задает стандартное значение строке текста.");
        }

        [Test]
        public void CreatedGetCorrectValueReturnCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = DateTime.Now;
            expected = new DateTime(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expected.Kind);
            var actual = note.Created;
            actual = new DateTime(actual.Year, actual.Month, actual.Day, actual.Hour, actual.Minute, actual.Second, actual.Kind);

            //Assert
            Assert.AreEqual(expected, actual, "Геттер возвращает не правильное время создания заметки.");
        }

        [Test]
        public void ModifiedGetCorrectValueReturnNotEqualValues()
        {
            //Setup
            var note = CreateNote();

            //Act
            note.Name = "Первое название.";
            var FirstEditTime = note.Modified;
            Thread.Sleep(1000);
            note.Name = "Второе название.";
            var SecondEditTime = note.Modified;

            //Assert
            Assert.AreNotEqual(FirstEditTime, SecondEditTime, "Геттер не возвращает верное время изменения заметки.");
        }

        [Test]
        public void ModifiedSetCorrectValueSetCorrectValue()
        {
            //Setup
            var note = CreateNote();

            //Act
            var expected = DateTime.Now;
            note.Modified = expected;

            //Assert
            Assert.AreEqual(expected,note.Modified,"Сеттер не присваивает верно время изменения заметки.");
        }

        [Test]
        public void CloneCorrectValueReturnCorrectNote()
        {
            //Setup
            var note = CreateNote();
            note.Name = "НазваниеКлона";
            note.Text = "ТекстКлона";
            note.Category = NoteCategory.Documents;

            //Act
            Note clone = (Note)note.Clone();

            //Assert
            Assert.Multiple((() =>
            {
                Assert.AreEqual(note.Name, clone.Name, "Имя было склонировано с ошибкой.");
                Assert.AreEqual(note.Text, clone.Text, "Текст был склонирован с ошибкой.");
                Assert.AreEqual(note.Category, clone.Category, "Категория была склонирована с ошибкой.");
            }));
        }

        [Test]
        public void ConstructorCorrectValueReturnCorrectNote()
        {
            //Setup
            var note = CreateNote();
            note.Name = "Название";
            note.Text = "Текст";
            note.Category = (NoteCategory) 0;

            //Act
            Note expectedNote = new Note("Название", "Текст", (NoteCategory)0,DateTime.Now);

            //Assert
            Assert.Multiple((() =>
            {
                Assert.AreEqual(note.Name, expectedNote.Name, "Конструктор передал поле 'имя' с ошибкой.");
                Assert.AreEqual(note.Text, expectedNote.Text, "Конструктор передал поле 'текст' с ошибкой.");
                Assert.AreEqual(note.Category, expectedNote.Category, "Конструктор передал поле 'категория' с ошибкой.");
            }));
        }
    }
}

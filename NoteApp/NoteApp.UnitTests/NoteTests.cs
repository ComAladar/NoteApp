using System;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class NoteTests
    {
        private Note _note;

        [SetUp]
        public void Setup()
        {
            _note=new Note();
        }

        [Test(Description = "Тест свойства Имя: Проверка возвращения заданного названия заметки.")]
        public void TestNameGet_CorrectValue()
        {
            _note.Name = "Информация о корме для животного.";
            var actual = _note.Name;
            Assert.AreEqual(_note.Name, actual ,"Геттер возвращает неправильное название заметки.");
        }

        [Test(Description = "Тест свойства Имя: Проверка присвоения правильного названия заметки.")]
        public void TestNameSet_CorrectValue()
        {
            var expected = "Информация о корме для животного";
            _note.Name = expected;
            Assert.AreEqual(expected, _note.Name,"Сеттер присваивает неправильное название заметки.");

        }

        [Test(Description = "Тест свойства Имя: Проверка присвоения пустого названия заметки и задачей стандартного значения 'Без Названия'.")]
        public void TestNameSet_EmptyString()
        {
            var expected = "Без Названия";
            _note.Name = "";
            Assert.AreEqual(expected, _note.Name, "Сеттер не задает стандартное значение пустой строке названия заметки.");
        }

        [Test(Description = "Тест свойства Имя: Выброс исключения при присвоении названия заметки длинной более 50 символов.")]
        public void TestNameSet_ArgumentException()
        {
            var WrongName = "12345678901234567890123456789012345678901234567890123";
            Assert.Throws<ArgumentException>((() => { _note.Name = WrongName;}),"У сеттера не происходит срабатывание ArgumentException.");
        }

        [Test(Description = "Тест свойства Категория: Проверка возвращения заданной категории заметки.")]
        public void TestCategoryGet_CorrectValue()
        {
            _note.Category = NoteCategory.Home;
            var actual = _note.Category;
            Assert.AreEqual(_note.Category, actual,"Геттер возвращает неправильную категорию.");
        }

        [Test(Description = "Тест свойства Категория: Проверка присвоения заданной категории заметки.")]
        public void TestCategorySet_CorrectValue()
        {
            var expected = NoteCategory.Home;
            _note.Category = expected;
            Assert.AreEqual(expected,_note.Category,"Сеттер присваивает неправильную категорию.");
        }

        [Test(Description = "Тест свойства Текст: Проверка возвращения заданной строки текста заметки.")]
        public void TestTextGet_CorrectValue()
        {
            _note.Text = "Текст";
            var actual = _note.Text;
            Assert.AreEqual(_note.Text,actual,"Геттер возвращает неправильную строку текста заметки.");
        }

        [Test(Description = "Тест свойства Текст: Проверка присвоения заданной строки текста заметки.")]
        public void TestTextSet_CorrectValue()
        {
            var expected = _note.Text;
            _note.Text = expected;
            Assert.AreEqual(expected,_note.Text,"Сеттер присваивает неправильную строку текста заметки.");
        }

        [Test(Description = "Тест свойства Текст: Проверка присвоения пустой строки текста заметки и задачей стандартного значения 'Введите текст.'.")]
        public void TestTextSet_EmptyString()
        {
            var expected = "Введите текст.";
            _note.Text = "";
            Assert.AreEqual(expected, _note.Text, "Сеттер не задает стандартное значение строке текста.");
        }

        [Test(Description = "Тест свойства Время создания заметки: Проверки правильности возвращения время создания.")]
        public void TestCreatedGet_CorrectValue()
        {
            var expected = DateTime.Now;
            expected=new DateTime(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expected.Kind);
            var actual = _note.Created;
            actual=new DateTime(actual.Year, actual.Month, actual.Day, actual.Hour, actual.Minute, actual.Second, actual.Kind);
            Assert.AreEqual(expected, actual,"Геттер возвращает не правильное время создания заметки.");
        }

        /*
        [Test(Description = "Тест свойства Время создания заметки: Проверки правильности ")]
        public void TestCreatedSet_CorrectValue()
        {
            Assert.Pass();
        }
        */

        [Test(Description = "Тест свойства Время редактирования заметки: Проверки правильности возвращения время редактирования.")]
        public void TestModifiedGet_CorrectValue()
        {
            _note.Name = "Первое название.";
            var FirstEditTime = _note.Modified;
            _note.Name = "Второе название.";
            var SecondEditTime = _note.Modified;
            Assert.AreNotEqual(FirstEditTime, SecondEditTime, "Сеттер не присваивает верное время изменения заметки.");
        }

        /*
        [Test(Description = "Тест свойства Время редактирования заметки: Проверка правильности присвоения время редактирования.")]
        public void TestModifiedSet_CorrectValue()
        {
            _note.Name = "Первое название.";
            var FirstEditTime = _note.Modified;
            _note.Name = "Второе название.";
            var SecondEditTime = _note.Modified;
            Assert.AreNotEqual(FirstEditTime,SecondEditTime,"");
        }
        */

    }
}
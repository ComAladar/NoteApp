using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectTests
    {
        private Project _project;
        private Note _note;

        [SetUp]
        public void Setup()
        {
            _project=new Project();
            _note = new Note();
        }

        [Test(Description = "")]
        public void TestNotesGet_CorrectValue()
        {
            _project.Notes.Add(_note);
            var actual = _project.Notes;
            Assert.AreEqual(_project.Notes,actual,"Возвращение списка заметок было совершено с ошибкой.");
        }

        [Test(Description = "")]
        public void TestNotesSet_CorrectValue()
        {
            var expected = _project;
            _project = expected;
            Assert.AreEqual(expected,_project,"Присваивание списка заметок было совершено с ошибкой.");
        }

    }
}

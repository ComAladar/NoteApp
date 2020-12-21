using System;
using System.Threading;
using NUnit.Framework;
using NoteApp;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;


namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectTests
    {
        private Project CreateProject()
        {
            var _project=new Project();
            return _project;
        }

        [Test]
        public void NotesGetCorrectValueGetCorrectList()
        {
            //Setup
            var project = CreateProject();

            //Act
            project.Notes.Add(new Note());
            var actual = project.Notes;

            //Assert
            Assert.AreEqual(project.Notes, actual, "Возвращение списка заметок было совершено с ошибкой.");
        }

        [Test]
        public void NotesSetCorrectValueReturnNotEmptyList()
        {
            //Setup
            var expectedProject = CreateProject();
            var actualProject = CreateProject();
            actualProject.Notes.Add(new Note()
            {
                Name = "Note",
                Text = "Text",
                Category = NoteCategory.Documents
            });

            //Act
            expectedProject.Notes = null;

            //Assert
            Assert.AreNotEqual(expectedProject,actualProject, "Присваивание списка заметок было совершено с ошибкой.");
        }

        
        [Test]
        public void SortListCorrectSortByCreatedTime()
        {
            //Setup
            var unsortedproject = CreateProject();
            var sortedproject = CreateProject();
            unsortedproject.Notes.Add(new Note());
            Thread.Sleep(2000);
            unsortedproject.Notes.Add(new Note());
            Thread.Sleep(2000);
            unsortedproject.Notes.Add(new Note());

            //Act
            sortedproject.Notes = unsortedproject.Notes;
            sortedproject.Notes=sortedproject.SortList(sortedproject.Notes);

            //Assert
            Assert.AreNotEqual(unsortedproject,sortedproject,"Список заметок был отсортирован с ошибкой.");
        }

        [Test]
        public void SortListCorrectSortByCategory()
        {
            //Setup
            var unsortedProject = CreateProject();
            var sortedProject = CreateProject();
            unsortedProject.Notes.Add(new Note()
            {
                Category =(NoteCategory)1
            });
            Thread.Sleep(2000);
            unsortedProject.Notes.Add(new Note()
            {
                Category = (NoteCategory)1
            });
            Thread.Sleep(2000);
            unsortedProject.Notes.Add(new Note()
            {
                Category =(NoteCategory)0
            });

            //Act
            sortedProject.Notes = unsortedProject.Notes;
            sortedProject.Notes=sortedProject.SortList((NoteCategory) 1, sortedProject.Notes);

            //Assert
            Assert.AreNotEqual(sortedProject,unsortedProject,"Сортировка списка по категориям была совершена с ошибкой.");
        }

        [Test]
        public void CurrentNoteCorrectGetSetCorrectValue()
        {
            //Setup
            var expectedProject = CreateProject();
            var actualProject = CreateProject();
            var GetNote = 5;

            //Act
            actualProject.CurrentNote = 5;
            var expectedNote = actualProject.CurrentNote;
            expectedProject.CurrentNote = GetNote;

            //Assert
            Assert.Multiple((() =>
            {
                Assert.AreEqual(expectedNote, actualProject.CurrentNote, "Геттер счетчика работает с ошибкой.");
                Assert.AreEqual(GetNote, expectedProject.CurrentNote, "Сеттер счетсчика работает с ошибкой.");
            }));
        }

        [Test]
        public void CurrentCategoryCorrectGetSetCorrectValue()
        {
            //Setup
            var expectedProject = CreateProject();
            var actualProject = CreateProject();
            var GetCategory = (int)NoteCategory.Work;

            //Act
            actualProject.CurrentCategory = (int)NoteCategory.Work;
            var expectedCategory = actualProject.CurrentCategory;
            expectedProject.CurrentNote = GetCategory;

            //Assert
            Assert.Multiple((() =>
            {
                Assert.AreEqual(expectedCategory, actualProject.CurrentCategory, "Геттер счетчика работает с ошибкой.");
                Assert.AreEqual(GetCategory, expectedProject.CurrentCategory, "Сеттер счетчика работате с ошибкой.");
            }));
        }

    }
}

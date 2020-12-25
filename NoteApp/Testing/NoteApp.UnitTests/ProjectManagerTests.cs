using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using NoteApp;

namespace NoteApp.UnitTests
{
    [TestFixture]
    public class ProjectManagerTests
    {
        private Project CreateProject()
        {
            var project=new Project();
            project.Notes.Add(new Note("Note", "Text", (NoteCategory)0, DateTime.MinValue)
                {
                    Modified = DateTime.MinValue
                }
            );
            project.Notes.Add(new Note("Note1", "Text1", (NoteCategory)1, DateTime.MinValue)
                {
                    Modified =DateTime.MinValue
                }
            );
            project.Notes.Add(new Note("Note2", "Text2", (NoteCategory)2, DateTime.MinValue)
                {
                    Modified = DateTime.MinValue
                }
            );
            return project;
        }

        /// <summary>
        /// Метод для создания новых эталонных проектов для тестирования при изменении бизнес-логики.
        /// </summary>
        /// <param name="project"></param>
        private void PrepareExpectedProject(Project project)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var Filename = testDataFolder + @"\expectedProject.json";
            ProjectManager.SaveToFile(project,Filename);
        }

        [Test]
        public void SaveToFile_CorrectProject_ReturnCorrectProject()
        {
            //Setup
            var project = CreateProject();
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var actualFilename = testDataFolder + @"\actualProject.json";
            var expectedFilename = testDataFolder + @"\expectedProject.json";
            if (File.Exists(actualFilename))
            {
                File.Delete(actualFilename);
            }

            //Act
            ProjectManager.SaveToFile(project,actualFilename);

            //Assert
            var actualFileContent = File.ReadAllText(actualFilename);
            var expectedFileContent = File.ReadAllText(expectedFilename);
            Assert.AreEqual(expectedFileContent,actualFileContent,"Сохранение записи случилось с ошибкой.");
        }

        [Test]
        public void LoadFromFile_CorrectProject_ReturnCorrectProject()
        {
            //Setup
            var actualProject = CreateProject();
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var expectedFilename = testDataFolder + @"\expectedProject.json";

            //Act
            var expectedProject = ProjectManager.LoadFromFile(expectedFilename);
            var expectedFileContent = File.ReadAllText(expectedFilename);
            //Assert
            Assert.AreEqual(actualProject.Notes.ToString(),expectedProject.Notes.ToString(),"Загрузка записи случилась с ошибкой.");
        }

        [Test]
        public void LoadFromFile_CorruptedProject_ReturnEmptyProject()
        {
            //Setup
            var expectedProject=new Project();
            var actualProject=new Project();
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var expectedFilename = testDataFolder + @"\expectedProjectCorrupted.json";
            //Act
            actualProject=ProjectManager.LoadFromFile(expectedFilename);
            //Assert
            Assert.AreEqual(expectedProject.Notes,actualProject.Notes,"Загрузка записи из испорченного файла не вернула пустой записи.");
        }

        [Test]
        public void DefaultFilePath_GetCorrectValue_ReturnFilePath()
        {
            //Setup
            var expectedFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NoteApp\NoteAppInfo.notes";

            //Act
            var actualFilePath = ProjectManager.DefaultFilePath;

            //Assert
            Assert.AreEqual(expectedFilePath,actualFilePath,"Стандартный путь сохранения был ошибочный.");
        }
    }
}

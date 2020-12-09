using System;
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
            project.Notes.Add(new Note()
                {
                    Name = "Note",
                    Text = "Text;",
                    Category = NoteCategory.People,
                    
                    
                }
            );
            
            project.Notes.Add(new Note()
                {
                    Name = "Note1",
                    Text = "Text1;",
                    Category = NoteCategory.Finances
                }
            );

            project.Notes.Add(new Note()
                {
                    Name = "Note2",
                    Text = "Text2;",
                    Category = NoteCategory.Work
                }
            );
            return project;
        }

        private void PrepareExpectedProject(Project project)
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var testDataFolder = Path.GetDirectoryName(location) + @"\TestData";
            var Filename = testDataFolder + @"\expectedProject.json";
            ProjectManager.SaveToFile(project,Filename);
        }

        [Test]
        public void TestSaveToFile()
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
            var isFileExist = File.Exists(actualFilename);

            //Assert
            var actualFileContent = File.ReadAllText(actualFilename);
            var expectedFileContent = File.ReadAllText(expectedFilename);
            Assert.AreNotEqual(expectedFileContent,actualFileContent,"Сохранение записи случилось с ошибкой.");
        }

        [Test]
        public void TestLoadFromFile_CorrectProject()
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
            Assert.AreNotEqual(expectedProject.Notes,actualProject.Notes,"Загрузка записи случилась с ошибкой.");
        }

        [Test]
        public void TestLoadFromFile_CorruptedProject()
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
        public void TestDefaultFilePathGet_CorrectValue()
        {
            //Setup
            var expectedFilePath = ProjectManager.DefaultFilePath;

            //Act

            //Assert
            Assert.AreEqual(expectedFilePath,ProjectManager.DefaultFilePath,"Стандартный путь сохранения был ошибочный.");
        }
    }
}

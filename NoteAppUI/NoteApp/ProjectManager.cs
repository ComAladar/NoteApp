using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NoteApp
{
    /// <summary>
    /// Класс Менеджер Проекта. Содержит методы для сохранения и загрузки данных о всех записях.
    /// </summary>
    public static class ProjectManager
    {
        /// <summary>
        /// Путь до файла с данными о всех записях.
        /// </summary>
        public static string DefaultFilePath { get; set; }
        /// <summary>
        /// Путь до директории приложения.
        /// </summary>
        public static string DefaultDirectoryPath { get; set; }

        /// <summary>
        /// Метод выполняющий сериализацию записей.
        /// </summary>
        /// <param name="project">Класс проекта, хранящий все записи.</param>
        /// <param name="path">Путь до файла, хранящего данные о записях.</param>
        public static void SaveToFile(Project project, string path)
        {
            Directory.CreateDirectory(DefaultDirectoryPath);
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            };
        }

        /// <summary>
        /// Метод выполняющий десериализацию записей.
        /// </summary>
        /// <param name="path">Путь до файла, хранящего данные о записях.</param>
        /// <returns></returns>
        public static Project LoadFromFile(string path)
        {
            Project project = new Project();
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                using (StreamReader sr = new StreamReader(path))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    project = (Project)serializer.Deserialize<Project>(reader);
                }
            }
            catch (DirectoryNotFoundException)
            {
                SaveToFile(project,DefaultFilePath);
            }
            return project;
        }

        /// <summary>
        /// Конструктор путей до директории и файлов приложения.
        /// </summary>
        static ProjectManager()
        {
            DefaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\NoteApp\NoteAppInfo.notes";
            DefaultDirectoryPath= Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\NoteApp";
            //DefaultPath += @"\NoteApp\Info.notes";
        }
    }
}

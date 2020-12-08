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
        /// Метод выполняющий сериализацию записей.
        /// </summary>
        /// <param name="project">Класс проекта, хранящий все записи.</param>
        /// <param name="fullFilename">Путь до файла, хранящего данные о записях.</param>
        public static void SaveToFile(Project project, string fullFilename)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullFilename));
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Include;
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Formatting = Formatting.Indented;
            using (StreamWriter sw = new StreamWriter(fullFilename))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, project);
            };
        }

        /// <summary>
        /// Метод выполняющий десериализацию записей.
        /// </summary>
        /// <param name="fullFilename">Путь до файла, хранящего данные о записях.</param>
        /// <returns></returns>
        public static Project LoadFromFile(string fullFilename)
        {
            Project project = new Project();
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.NullValueHandling = NullValueHandling.Include;
                serializer.TypeNameHandling = TypeNameHandling.All;
                serializer.Formatting = Formatting.Indented;
                using (StreamReader sr = new StreamReader(fullFilename))
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    project = (Project)serializer.Deserialize<Project>(reader);
                }
            }
            catch (Exception)
            {
                return project;
            }
            return project;
        }

        /// <summary>
        /// Конструктор путя до директории.
        /// </summary>
        static ProjectManager()
        {
            DefaultFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+ @"\NoteApp\NoteAppInfo.notes";
        }
    }
}

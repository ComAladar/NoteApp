using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp
{
    /// <summary>
    /// Тестовый класс, необходимый для проведения тестов работоспособности классов.
    /// </summary>
    /// <returns> Показывает старое и новое значениек переменной. </returns>
    public class Testing
    {
        /// <summary>
        /// Метод для смены ранее внесенного значения переменной.
        /// </summary>
        /// <param name="newVar"> Новое значение для переменной. </param>
        /// <returns> Показывает новое значение переменной. </returns>
        public int ChangeVariable(int newVar)
        {
            Console.Write(newVar);
            return newVar;
        }
    }
}
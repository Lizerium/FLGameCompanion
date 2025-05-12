/*
 * Author: Nikolay Dvurechensky
 * Site: https://www.dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 12 мая 2025 06:38:00
 * Version: 1.0.27
 */

using GraphX;
using GraphX.Common.Models;

namespace WindowsFormsProject
{
    /* DataVertex — это класс данных для вершин. Он содержит все пользовательские данные вершин, указанные пользователем.
    * Этот класс также должен быть производным от VertexBase, который предоставляет свойства и методы, обязательные для
    * правильные операции GraphX.
    * Некоторые из полезных членов VertexBase:
    * - Свойство ID, в котором хранится уникальный положительный идентификационный номер. Свойство должно быть заполнено пользователем.
    */
    public class DataVertex: VertexBase
    {
        /// <summary>
        /// Некоторое строковое свойство для примера
        /// </summary>
        public string Text { get; set; }
 
        #region Calculated or static props

        public override string ToString()
        {
            return Text;
        }


        #endregion

        /// <summary>
        /// Конструктор без параметров по умолчанию для этого класса
        /// (требуется для сериализации YAXlib)
        /// </summary>
        public DataVertex():this("")
        {
        }

        public DataVertex(string text = "")
        {
            Text = text;
        }
    }
}

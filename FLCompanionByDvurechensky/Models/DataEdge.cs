/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 10 октября 2025 06:52:11
 * Version: 1.0.73
 */

using GraphX.Common.Models;

namespace WindowsFormsProject
{
    /* DataEdge — это класс данных для ребер. Он содержит все данные о пользовательских краях, указанные пользователем.
    * Этот класс также должен быть производным от класса EdgeBase, который предоставляет свойства и методы, обязательные для
    * правильные операции GraphX.
    * Некоторые из полезных членов EdgeBase:
    * - Свойство ID, в котором хранится уникальный положительный идентификационный номер. Свойство должно быть заполнено пользователем.
    * - логическое свойство IsSelfLoop, указывающее, является ли это ребро самозацикленным (например, имеет идентичные вершины Target и Source)
    * — коллекция точек RoutingPoints, используемая для создания пути маршрутизации по краю. Если Null, то для рисования края будет использоваться прямая линия.
    * В большинстве случаев GraphX ​​обрабатывает это автоматически.
    * - Исходное свойство, которое содержит исходную вершину ребра.
    * - Целевое свойство, которое содержит целевую вершину ребра.
    * - Свойство Weight, которое содержит необязательное значение веса ребра, которое можно использовать в некоторых алгоритмах компоновки.
    */
    public class DataEdge : EdgeBase<DataVertex>
    {
        /// <summary>
        /// Конструктор по умолчанию. Нам нужно установить как минимум исходные и целевые свойства ребра.
        /// </summary>
        /// <param name="source">Исходные данные вершин</param>
        /// <param name="target">Данные целевой вершины</param>
        /// <param name="weight">Необязательный вес ребра</param>
        public DataEdge(DataVertex source, DataVertex target, double weight = 1)
    : base(source, target, weight)
        {
        }

        /// <summary>
        /// Конструктор без параметров по умолчанию (для совместимости с сериализацией)
        /// </summary>
        public DataEdge()
            : base(null, null, 1)
        {
        }

        /// <summary>
        /// Пользовательское строковое свойство, например
        /// </summary>
        public string Text { get; set; }

        #region GET members
        public override string ToString()
        {
            return Text;
        }
        #endregion
    }
}

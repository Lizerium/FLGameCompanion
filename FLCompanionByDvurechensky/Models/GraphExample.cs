/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 мая 2026 10:03:33
 * Version: 1.0.298
 */

using QuickGraph;

namespace WindowsFormsProject
{
    /// <summary>
    /// Это наш пользовательский график данных, полученный из класса BidirectionalGraph с использованием пользовательских типов данных.
    /// Граф данных хранит данные о вершинах и ребрах, которые используются GraphArea и конечным пользователем для различных операций.
    /// Содержимое графика данных обрабатывается пользователем вручную (добавление/удаление объектов). Основная идея заключается в том, что вы можете динамически
    /// удалять/добавлять объекты в макет GraphArea, а затем использовать график данных для восстановления исходного содержимого макета.
    /// </summary>
    public class GraphExample : BidirectionalGraph<DataVertex, DataEdge> { }
}

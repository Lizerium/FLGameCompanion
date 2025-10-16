/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 16 октября 2025 10:58:18
 * Version: 1.0.79
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

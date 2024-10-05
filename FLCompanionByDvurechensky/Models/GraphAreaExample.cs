using GraphX.Controls;
using QuickGraph;

namespace WindowsFormsProject
{
    /// <summary>
    /// Это пользовательское представление GraphArea с использованием пользовательских типов данных.
    /// GraphArea — компонент визуальной панели, отвечающий за отрисовку визуальных элементов (вершин и ребер).
    /// Он также предоставляет множество глобальных настроек и методов, что делает GraphX ​​таким настраиваемым и удобным для пользователя.
    /// </summary>
    public class GraphAreaExample : GraphArea<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>> { }
}

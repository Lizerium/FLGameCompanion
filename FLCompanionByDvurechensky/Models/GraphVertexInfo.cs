/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 марта 2026 13:11:04
 * Version: 1.0.237
 */

namespace FLCompanionByDvurechensky.Data
{
    /// <summary>
    /// Информация о вершине
    /// </summary>
    public class GraphVertexInfo
    {
        /// <summary>
        /// Вершина
        /// </summary>
        public GraphVertex Vertex { get; set; }

        /// <summary>
        /// Не посещенная вершина
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма весов ребер
        /// </summary>
        public int EdgesWeightSum { get; set; }

        /// <summary>
        /// Предыдущая вершина
        /// </summary>
        public GraphVertex PreviousVertex { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertex">Вершина</param>
        public GraphVertexInfo(GraphVertex vertex)
        {
            Vertex = vertex;
            IsUnvisited = true;
            EdgesWeightSum = int.MaxValue;
            PreviousVertex = null;
        }
    }
}

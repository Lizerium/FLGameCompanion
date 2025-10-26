/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 26 октября 2025 12:24:31
 * Version: 1.0.89
 */

/// <summary>
/// Ребро графа
/// </summary>
public class GraphEdge
{
    /// <summary>
    /// Связанная вершина
    /// </summary>
    public GraphVertex ConnectedVertex { get; }

    /// <summary>
    /// Вес ребра
    /// </summary>
    public int EdgeWeight { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="connectedVertex">Связанная вершина</param>
    /// <param name="weight">Вес ребра</param>
    public GraphEdge(GraphVertex connectedVertex, int weight)
    {
        ConnectedVertex = connectedVertex;
        EdgeWeight = weight;
    }
}
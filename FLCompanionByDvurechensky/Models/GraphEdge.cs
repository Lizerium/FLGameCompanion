/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 04 сентября 2026 07:38:41
 * Version: 1.0.406
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
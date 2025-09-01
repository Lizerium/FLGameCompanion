/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 01 сентября 2025 11:06:33
 * Version: 1.0.34
 */

using System.Collections.Generic;

/// <summary>
/// Вершина графа
/// </summary>
public class GraphVertex
{
    /// <summary>
    /// Название вершины
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Список ребер
    /// </summary>
    public List<GraphEdge> Edges { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    public GraphVertex(string vertexName)
    {
        Name = vertexName;
        Edges = new List<GraphEdge>();
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="newEdge">Ребро</param>
    public void AddEdge(GraphEdge newEdge)
    {
        Edges.Add(newEdge);
    }

    /// <summary>
    /// Добавить ребро
    /// </summary>
    /// <param name="vertex">Вершина</param>
    /// <param name="edgeWeight">Вес</param>
    public void AddEdge(GraphVertex vertex, int edgeWeight)
    {
        AddEdge(new GraphEdge(vertex, edgeWeight));
    }

    /// <summary>
    /// Преобразование в строку
    /// </summary>
    /// <returns>Имя вершины</returns>
    public override string ToString() => Name;
}
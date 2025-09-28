/*
 * Author: Nikolay Dvurechensky
 * Site: https://sites.google.com/view/dvurechensky
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 28 сентября 2025 21:14:32
 * Version: 1.0.61
 */

using System.Collections.Generic;

/// <summary>
/// Граф
/// </summary>
public class Graph
{
    /// <summary>
    /// Список вершин графа
    /// </summary>
    public List<GraphVertex> Vertices { get; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Graph()
    {
        Vertices = new List<GraphVertex>();
    }

    /// <summary>
    /// Добавление вершины
    /// </summary>
    /// <param name="vertexName">Имя вершины</param>
    public void AddVertex(string vertexName)
    {
        Vertices.Add(new GraphVertex(vertexName));
    }

    /// <summary>
    /// Поиск вершины
    /// </summary>
    /// <param name="vertexName">Название вершины</param>
    /// <returns>Найденная вершина</returns>
    public GraphVertex FindVertex(string vertexName)
    {
        foreach (var v in Vertices)
        {
            if (v.Name.Contains(vertexName))
            {
                return v;
            }
        }

        return null;
    }

    /// <summary>
    /// Добавление ребра
    /// </summary>
    /// <param name="firstName">Имя первой вершины</param>
    /// <param name="secondName">Имя второй вершины</param>
    /// <param name="weight">Вес ребра соединяющего вершины</param>
    public void AddEdge(string firstName, string secondName, int weight)
    {
        var v1 = FindVertex(firstName);
        var v2 = FindVertex(secondName);
        if (v2 != null && v1 != null)
        {
            v1.AddEdge(v2, weight);
            v2.AddEdge(v1, weight);
        }
    }
}
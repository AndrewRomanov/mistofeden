using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, определяющий игровую сетку
/// </summary>
public class GridScript : MonoBehaviour
{
    /// <summary>
    /// Поле для отладки привязки игрока к клетке
    /// </summary>
    public Transform Player;

    /// <summary>
    /// Массив клеток
    /// </summary>
    private Node[,] grid;

    /// <summary>
    /// Диаметр одной клетки
    /// </summary>
    float nodeDiameter;

    /// <summary>
    /// Радиус одной клетки
    /// </summary>
    public float NodeRadius;

    /// <summary>
    /// Размеры сетки
    /// </summary>
    public Vector2 GridWorldSize;

    /// <summary>
    /// Слой, к которому будут принадлежать объекты, по которым нельзя ходить
    /// </summary>
    public LayerMask UnwalkableLayerMask;

    /// <summary>
    /// Поля определяющие размер сетки по X и Y
    /// </summary>
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        // Определяем диаметр клетки
        nodeDiameter = NodeRadius * 2;
        // Определяем, какое количество клеток может поместиться на поле
        gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
        CreateGrid(gridSizeX, gridSizeY);
    }

    /// <summary>
    /// Метод возвращающий всех соседей для данной клетки
    /// </summary>
    /// <param name="node">Клетка, для которой ищутся соседи</param>
    /// <returns>Список соседних клеток</returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    /// <summary>
    /// Создание сетки
    /// </summary>
    /// <param name="gridSizeX">Размерность по x</param>
    /// <param name="gridSizeY">Размерность по y</param>
    private void CreateGrid(int gridSizeX, int gridSizeY)
    {
        grid = new Node[gridSizeX, gridSizeY];
        // Определяем левую границу сетки
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableLayerMask);
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    /// <summary>
    /// Метод, который находит клетку на которой стоит юнит
    /// </summary>
    /// <param name="worldPosition">Позиция юнита</param>
    /// <returns></returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        // Находим процентное соотношение к одному из нодов
        float percentX = (worldPosition.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldPosition.z + GridWorldSize.y / 2) / GridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // Находим нод по координатам
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    /// <summary>
    /// Список нодов, формирующих путь
    /// </summary>
    public List<Node> Path;

    /// <summary>
    /// Метод для отладки. Показывает гизмо различных объектов
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
        if (grid != null)
        {
            var playerNode = NodeFromWorldPoint(Player.position);
            foreach (var node in grid)
            {
                Gizmos.color = node.Walkable ? Color.white : Color.red;
                if (Path != null && Path.Contains(node))
                {
                    Gizmos.color = Color.black;
                }
                if (node == playerNode)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
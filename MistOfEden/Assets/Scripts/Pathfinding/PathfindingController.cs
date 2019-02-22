using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Контроллер, отвечающий за поиск пути
/// </summary>
public class PathfindingController : MonoBehaviour
{
    /// <summary>
    /// Поля определяющие начальную и конечную точки
    /// </summary>
    public Transform Seeker, Target;

    /// <summary>
    /// Игровая сетка
    /// </summary>
    private GridScript grid;

    void Awake()
    {
        grid = GetComponent<GridScript>();
    }

    void Update()
    {
        // Ищем путь
        FindPath(Seeker.position, Target.position);
    }

    /// <summary>
    /// Метод нахождения оптимального пути
    /// </summary>
    /// <param name="startPos">Стартовая точка</param>
    /// <param name="targetPos">Конечная точка</param>
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Получаем ноды стартовой и конечной точки
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>(); // Список нодов на рассмотрение
        HashSet<Node> closedSet = new HashSet<Node>(); // Список просмотренных нодов
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if ((openSet[i].FCost < node.FCost || openSet[i].FCost == node.FCost) && openSet[i].HCost < node.HCost)
                {
                    node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.GCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Метод, формирующий путь
    /// </summary>
    /// <param name="startNode">Стартовый нод</param>
    /// <param name="endNode">Конечный нод</param>
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.Path = path;
    }

    /// <summary>
    /// Метод, считающий расстояние между нодами
    /// </summary>
    /// <param name="nodeA">Первый нод</param>
    /// <param name="nodeB">Второй нод</param>
    /// <returns></returns>
    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
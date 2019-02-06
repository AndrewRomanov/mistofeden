using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform Player; // Поле для отладки привязки игрока к клетке
    private Node[,] _grid; // Массив клеток
    float nodeDiameter; // Диаметр одной клетки
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

    int gridSizeX, gridSizeY; // Поля определяющие размер сетки по X и Y
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
    /// Создание сетки
    /// </summary>
    /// <param name="gridSizeX">Размерность по x</param>
    /// <param name="gridSizeY">Размерность по y</param>
    private void CreateGrid(int gridSizeX, int gridSizeY)
    {
        _grid = new Node[gridSizeX, gridSizeY];
        // Определяем левую границу сетки
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 - Vector3.forward * GridWorldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + NodeRadius) + Vector3.forward * (y * nodeDiameter + NodeRadius);
                bool walkable = !Physics.CheckSphere(worldPoint, NodeRadius, UnwalkableLayerMask);
                _grid[x, y] = new Node(walkable, worldPoint);
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
        return _grid[x, y];
    }

    /// <summary>
    /// Метод для отладки. Показывает гизмо различных объектов
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
        if (_grid != null)
        {
            var playerNode = NodeFromWorldPoint(Player.position);
            foreach (var node in _grid)
            {
                Gizmos.color = node.Walkable ? Color.white : Color.red;
                if (node == playerNode)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
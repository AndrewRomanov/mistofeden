using UnityEngine;

public class Grid : MonoBehaviour
{
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

    private void Start()
    {
        // Определяем диаметр клетки
        nodeDiameter = NodeRadius * 2;
        // Определяем, какое количество клеток может поместиться на поле
        int gridSizeX = Mathf.RoundToInt(GridWorldSize.x / nodeDiameter);
        int gridSizeY = Mathf.RoundToInt(GridWorldSize.y / nodeDiameter);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(GridWorldSize.x, 1, GridWorldSize.y));
        if(_grid != null)
        {
            foreach (var node in _grid)
            {
                Gizmos.color = node.Walkable ? Color.white : Color.red;
                Gizmos.DrawCube(node.WorldPosition, Vector3.one * nodeDiameter);
            }
        }
    }
}
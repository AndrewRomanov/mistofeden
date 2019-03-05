using UnityEngine;

/// <summary>
/// Класс, определяющий один нод (клетку) сетки
/// </summary>
public class Node
{
    /// <summary>
    /// Можно ли ходить по данной клетке
    /// </summary>
    public bool Walkable;

    /// <summary>
    /// Позиция клетки на поле
    /// </summary>
    public Vector3 WorldPosition;

    /// <summary>
    /// Местоположение клетки в массиве
    /// </summary>
    public int GridX, GridY;

    /// <summary>
    /// "Вес" или "стоимость" клетки.
    /// GCost - расстояние до стартовой клетки
    /// HCost - расстояние до конечной клетки
    /// </summary>
    public int GCost, HCost;

    /// <summary>
    /// Предыдущая клетка, по которой был проложен путь
    /// </summary>
    public Node parent;

    /// <summary>
    /// Конструктор класса клетки
    /// </summary>
    /// <param name="walkable">Возможно ли ходить по данной клетке</param>
    /// <param name="worldPosition">Позиция клетки на поле</param>
    /// <param name="gridX">Позиция клетки в массиве по X</param>
    /// <param name="gridY">Позиция клетки в массиве по Y</param>
    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY)
    {
        Walkable = walkable;
        WorldPosition = worldPosition;
        GridX = gridX;
        GridY = gridY;
    }

    /// <summary>
    /// Общий "вес" или "стоимость" клетки. Равна сумме GCost и HCost
    /// </summary>
    public int FCost => GCost + HCost;
}
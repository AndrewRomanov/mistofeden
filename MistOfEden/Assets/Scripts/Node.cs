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
    /// Конструктор класса клетки
    /// </summary>
    /// <param name="walkable">Возможно ли ходить по данной клетке</param>
    /// <param name="worldPosition">Позиция клетки на поле</param>
    public Node(bool walkable, Vector3 worldPosition)
    {
        Walkable = walkable;
        WorldPosition = worldPosition;
    }
}
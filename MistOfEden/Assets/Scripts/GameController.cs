using UnityEngine;

/// <summary>
/// Игровой контроллер, отвечающий за построение сетки и пошаговость
/// </summary>
public class GameController : MonoBehaviour
{
    /// <summary>
    /// Префаб, которым будет заполнятся поле
    /// </summary>
    public Transform prefab;

    /// <summary>
    /// Метод заполнения сетки
    /// </summary>
    /// <param name="grid">Массив с нодами, на основании которых нужно построить сетку</param>
    /// <param name="nodeDiameter">Размер одной клетки</param>
    public void FillGrid(Node[,] grid, float nodeDiameter)
    {
        prefab.localScale = new Vector3(nodeDiameter, 1, nodeDiameter);
        foreach (var node in grid)
        {
            Instantiate(prefab, node.WorldPosition, Quaternion.identity);
        }
    }
}
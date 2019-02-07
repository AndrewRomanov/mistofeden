using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

/// <summary>
/// Класс, опеределяющий игрока
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Алгоритм поиска пути
    /// </summary>
    public GameObject aStar;

    /// <summary>
    /// Игровая сетка
    /// </summary>
    private GridScript grid;

    /// <summary>
    /// Скорость игрока
    /// </summary>
    public float Speed;

    private void Start()
    {
        grid = aStar.GetComponent<GridScript>();
    }

    bool isMove = false;
    private void Update()
    {
        if (Input.GetKeyDown("space") && !isMove)
        {
            isMove = true;
            List<Node> path = grid.Path;
            while (transform.position.x < grid.Path[0].WorldPosition.x || transform.position.z < grid.Path[0].WorldPosition.z)
            {
                var step = Speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, grid.Path[0].WorldPosition, step);
            }
            isMove = false;
        }
    }
}

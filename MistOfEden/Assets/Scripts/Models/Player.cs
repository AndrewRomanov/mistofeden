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

	private bool isMove = false;
	private void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			isMove = true;
		}
		if (isMove && grid.Path.Count > 0 && transform.position.x != grid.Path[0].WorldPosition.x && transform.position.z != grid.Path[0].WorldPosition.z)
		{
			transform.position = Vector3.MoveTowards(transform.position, grid.Path[0].WorldPosition, Speed * Time.deltaTime);
			Debug.Log(isMove);
		}
		else
		{
			isMove = false;
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    private GameManager gameManager;
    private Vector2Int currentCell;
    private Transform foodView;

    public void Setup(GameManager manager)
    {
        gameManager = manager;

        if (foodView == null)
        {
            foodView = CreateFoodView();
        }
    }

    public void SpawnFood()
    {
        List<Vector2Int> freeCells = new List<Vector2Int>();

        for (int x = 0; x < gameManager.BoardWidth; x++)
        {
            for (int y = 0; y < gameManager.BoardHeight; y++)
            {
                Vector2Int cell = new Vector2Int(x, y);
                if (!gameManager.IsSnakeOnCell(cell))
                {
                    freeCells.Add(cell);
                }
            }
        }

        if (freeCells.Count == 0)
        {
            gameManager.GameOver();
            return;
        }

        currentCell = freeCells[Random.Range(0, freeCells.Count)];
        foodView.position = gameManager.GridToWorld(currentCell);
    }

    public bool IsFoodOnCell(Vector2Int cell)
    {
        return currentCell == cell;
    }

    private Transform CreateFoodView()
    {
        GameObject food = new GameObject("Food");
        food.transform.SetParent(transform);

        SpriteRenderer renderer = food.AddComponent<SpriteRenderer>();
        renderer.sprite = gameManager.CellSprite;
        renderer.color = gameManager.FoodColor;

        return food.transform;
    }
}

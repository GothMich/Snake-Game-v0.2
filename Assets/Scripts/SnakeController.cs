using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private int startLength = 3;

    private readonly List<Vector2Int> cells = new List<Vector2Int>();
    private readonly List<Transform> segmentViews = new List<Transform>();

    private GameManager gameManager;
    private Vector2Int currentDirection;
    private Vector2Int nextDirection;
    private float moveTimer;
    private int pendingGrowth;

    public void Setup(GameManager manager)
    {
        gameManager = manager;
    }

    public void BeginGame(Vector2Int startCell)
    {
        ClearSnake();

        int safeLength = Mathf.Max(1, startLength);
        currentDirection = Vector2Int.right;
        nextDirection = currentDirection;
        moveTimer = 0f;
        pendingGrowth = 0;

        for (int i = 0; i < safeLength; i++)
        {
            cells.Add(new Vector2Int(startCell.x - i, startCell.y));
            Transform segmentView = CreateSegmentView(i == 0);
            segmentViews.Add(segmentView);
        }

        RefreshVisuals();
    }

    private void Update()
    {
        if (gameManager == null || gameManager.IsGameOver)
        {
            return;
        }

        ReadInput();

        moveTimer += Time.deltaTime;
        if (moveTimer >= gameManager.MoveDelay)
        {
            moveTimer -= gameManager.MoveDelay;
            MoveStep();
        }
    }

    public bool OccupiesCell(Vector2Int cell)
    {
        return cells.Contains(cell);
    }

    private void MoveStep()
    {
        currentDirection = nextDirection;
        Vector2Int nextHead = cells[0] + currentDirection;

        if (!gameManager.IsInsideBoard(nextHead) || HitsSnake(nextHead))
        {
            gameManager.GameOver();
            return;
        }

        bool ateFood = gameManager.HasFoodAt(nextHead);
        if (ateFood)
        {
            pendingGrowth++;
        }

        cells.Insert(0, nextHead);

        if (pendingGrowth > 0)
        {
            pendingGrowth--;
        }
        else
        {
            cells.RemoveAt(cells.Count - 1);
        }

        EnsureViewCount();
        RefreshVisuals();

        if (ateFood)
        {
            gameManager.HandleFoodEaten();
        }
    }

    private bool HitsSnake(Vector2Int nextHead)
    {
        for (int i = 0; i < cells.Count; i++)
        {
            bool isTail = i == cells.Count - 1;
            if (cells[i] != nextHead)
            {
                continue;
            }

            if (isTail && pendingGrowth == 0)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    private void ReadInput()
    {
        if (PressedUp() && currentDirection != Vector2Int.down)
        {
            nextDirection = Vector2Int.up;
        }
        else if (PressedDown() && currentDirection != Vector2Int.up)
        {
            nextDirection = Vector2Int.down;
        }
        else if (PressedLeft() && currentDirection != Vector2Int.right)
        {
            nextDirection = Vector2Int.left;
        }
        else if (PressedRight() && currentDirection != Vector2Int.left)
        {
            nextDirection = Vector2Int.right;
        }
    }

    private bool PressedUp()
    {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool PressedDown()
    {
        return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    }

    private bool PressedLeft()
    {
        return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    }

    private bool PressedRight()
    {
        return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    }

    private Transform CreateSegmentView(bool isHead)
    {
        GameObject segment = new GameObject(isHead ? "SnakeHead" : "SnakeBody");
        segment.transform.SetParent(transform);

        SpriteRenderer renderer = segment.AddComponent<SpriteRenderer>();
        renderer.sprite = gameManager.CellSprite;
        renderer.color = isHead ? gameManager.SnakeHeadColor : gameManager.SnakeBodyColor;

        return segment.transform;
    }

    private void EnsureViewCount()
    {
        while (segmentViews.Count < cells.Count)
        {
            segmentViews.Add(CreateSegmentView(false));
        }

        while (segmentViews.Count > cells.Count)
        {
            Transform lastView = segmentViews[segmentViews.Count - 1];
            segmentViews.RemoveAt(segmentViews.Count - 1);
            Destroy(lastView.gameObject);
        }
    }

    private void RefreshVisuals()
    {
        for (int i = 0; i < segmentViews.Count; i++)
        {
            segmentViews[i].position = gameManager.GridToWorld(cells[i]);

            SpriteRenderer renderer = segmentViews[i].GetComponent<SpriteRenderer>();
            renderer.color = i == 0 ? gameManager.SnakeHeadColor : gameManager.SnakeBodyColor;
            segmentViews[i].name = i == 0 ? "SnakeHead" : "SnakeBody_" + i;
        }
    }

    private void ClearSnake()
    {
        cells.Clear();

        for (int i = 0; i < segmentViews.Count; i++)
        {
            if (segmentViews[i] != null)
            {
                Destroy(segmentViews[i].gameObject);
            }
        }

        segmentViews.Clear();
    }
}

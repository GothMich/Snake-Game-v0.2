using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Board")]
    [SerializeField] private int boardWidth = 20;
    [SerializeField] private int boardHeight = 20;
    [SerializeField] private float moveDelay = 0.15f;

    [Header("References")]
    [SerializeField] private SnakeController snakeController;
    [SerializeField] private FoodSpawner foodSpawner;

    [Header("Colors")]
    [SerializeField] private Color snakeHeadColor = new Color(0.12f, 0.75f, 0.25f);
    [SerializeField] private Color snakeBodyColor = new Color(0.22f, 0.9f, 0.35f);
    [SerializeField] private Color foodColor = new Color(0.95f, 0.2f, 0.2f);

    private Sprite cellSprite;
    private int score;
    private bool isGameOver;

    public int BoardWidth => boardWidth;
    public int BoardHeight => boardHeight;
    public float MoveDelay => moveDelay;
    public bool IsGameOver => isGameOver;
    public Sprite CellSprite => cellSprite;
    public Color SnakeHeadColor => snakeHeadColor;
    public Color SnakeBodyColor => snakeBodyColor;
    public Color FoodColor => foodColor;

    private void Awake()
    {
        cellSprite = CreateCellSprite();
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        if (snakeController == null || foodSpawner == null)
        {
            Debug.LogError("Assign SnakeController and FoodSpawner in the GameManager inspector.");
            enabled = false;
            return;
        }

        score = 0;
        isGameOver = false;

        Vector2Int startCell = new Vector2Int(boardWidth / 2, boardHeight / 2);

        snakeController.Setup(this);
        foodSpawner.Setup(this);
        snakeController.BeginGame(startCell);
        foodSpawner.SpawnFood();
    }

    public bool IsInsideBoard(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < boardWidth && cell.y >= 0 && cell.y < boardHeight;
    }

    public bool IsSnakeOnCell(Vector2Int cell)
    {
        return snakeController != null && snakeController.OccupiesCell(cell);
    }

    public bool HasFoodAt(Vector2Int cell)
    {
        return foodSpawner != null && foodSpawner.IsFoodOnCell(cell);
    }

    public void HandleFoodEaten()
    {
        score++;
        foodSpawner.SpawnFood();
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public Vector3 GridToWorld(Vector2Int cell, float z = 0f)
    {
        float x = cell.x - boardWidth / 2f + 0.5f;
        float y = cell.y - boardHeight / 2f + 0.5f;
        return new Vector3(x, y, z);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10f, 10f, 220f, 25f), "Score: " + score);
        GUI.Label(new Rect(10f, 35f, 320f, 25f), "Move: W A S D or arrow keys");

        if (isGameOver)
        {
            GUI.Label(new Rect(10f, 65f, 320f, 25f), "Game Over - press R to restart");
            GUI.Label(new Rect(10f, 90f, 320f, 25f), "Final score: " + score);
        }
    }

    private Sprite CreateCellSprite()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.name = "CellSprite";
        texture.filterMode = FilterMode.Point;
        texture.SetPixel(0, 0, Color.white);
        texture.Apply();

        return Sprite.Create(texture, new Rect(0f, 0f, 1f, 1f), new Vector2(0.5f, 0.5f), 1f);
    }
}

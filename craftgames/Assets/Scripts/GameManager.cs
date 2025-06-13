using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager singleton;
    public bool GameStarted { get; private set; }
    public bool GameEnded { get; private set; }
    private float slowMotionFactor = 0.1f;
    [SerializeField] private Transform startTransform;
    [SerializeField] private Transform goalTransform;
    [SerializeField] private BallController ball;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject failLevelPanel;

    public float EntireDistance { get; private set; }
    public float DistanceLeft { get; private set; }


    private void Start()
    {
        EntireDistance = goalTransform.position.z - startTransform.position.z;
    }
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if (singleton != this)
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    
    public void StartGame()
    {
        GameStarted = true;
        Debug.Log("Game Started");
    }

    // Update is called once per frame
    public void EndGame(bool win)
    {
        GameEnded = true;
        Debug.Log("Game Ended");
        if (!win)
        {
            Debug.Log("You Lose!");
            // Invoke("RestartGame", 2 * slowMotionFactor);
            failLevelPanel.SetActive(true);
            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Debug.Log("You Win!");
            nextLevelPanel.SetActive(true);

            Time.timeScale = slowMotionFactor;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            // Invoke("RestartGame", 2f);
            // Time.timeScale = 1f; 
            // Time.fixedDeltaTime = 2f * Time.timeScale;
        }
    }

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    void Update()
    {
        DistanceLeft = Vector3.Distance(ball.transform.position, goalTransform.position);
        if (DistanceLeft > EntireDistance)
        {
            DistanceLeft = EntireDistance;
        }
        if (ball.transform.position.z > goalTransform.position.z)
        {
            DistanceLeft = 0;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerController player;
    
    [Header("Menu GameObjects")]
    public GameObject[] menus = new GameObject[3];

    [Header("Health")]
    public TextMeshProUGUI HealthMesh;

    [Header("Time")]
    public TextMeshProUGUI TimeMesh;
    public float lvlTimer;

    public bool isGameWin = false;

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        Time.timeScale = 1f;
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameWin)
        {
            return;
        }

        if (menus[0].activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        TimeUpdater();
    }

    public void HealthUpdater(int health)
    {
        if (health <= 0)
        {
            health = 0;
        }
        HealthMesh.text = "Health: " + health.ToString();
    }

    public void TimeUpdater()
    {
        if (lvlTimer <= 0)
        {
            lvlTimer = 0;
            GameOverStatus(false);

            TimeMesh.text = "Time: 0:00";

            return;
        }

        lvlTimer -= Time.deltaTime * 2;

        string minutes = Mathf.FloorToInt(lvlTimer / 60).ToString();
        string seconds = (lvlTimer % 60).ToString("F0");
        seconds = seconds.Length == 1 ? seconds = "0" + seconds : seconds;

        TimeMesh.text = "Time: " + "  " + minutes + ":" + seconds;
    }

    public void Jump()
    {
        player.PlayerJump();
    }

    public void Shoot()
    {
        player.PlayerShoot();
    }

    public void Crouch()
    {
        player.PlayerCrouch();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOverStatus(bool gameOver)
    {
        if(gameOver)
        {
            isGameWin = true;
            menus[1].SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            isGameWin = false;
            menus[2].SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
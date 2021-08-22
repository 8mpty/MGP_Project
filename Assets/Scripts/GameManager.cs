using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerController player;
    [HideInInspector] public bool isGameWin = false;

    [Header("Menu GameObjects")]
    public GameObject[] menus;

    [Header("Health")]
    public TextMeshProUGUI HealthMesh;

    [Header("Time")]
    public TextMeshProUGUI TimeMesh;
    public float lvlTimer;

    [Header("Score")]
    public TextMeshProUGUI ScoreMesh;
    public TextMeshProUGUI HighScoreMesh;
    public TextMeshProUGUI CurrentScoreMesh;
    public int gameScore;

    public bool main = false;

    public Toggle bgmToggle;

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
        if (main == false)
        {
            for (int i = 0; i < menus.Length; i++)
            {

                menus[i].SetActive(false);
            }
            menus[3].SetActive(true);
        }
        
        if(HighScoreMesh != null)
        {
            HighScoreMesh.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();
        }

        if (CurrentScoreMesh != null)
        {
            CurrentScoreMesh.text = "Score: 0";
        }

        if (PlayerPrefs.GetInt("Toggle") == 0)
        {
            bgmToggle.isOn = true;
        }
        else if (PlayerPrefs.GetInt("Toggle") == 1)
        {
            bgmToggle.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameWin)
        {
            return;
        }

        if(main == false)
        {
            if (menus[0].activeInHierarchy || menus[1].activeInHierarchy || menus[2].activeInHierarchy)
            {
                Time.timeScale = 0f;
                menus[3].SetActive(false);
            }
            else
            {
                menus[3].SetActive(true);
                Time.timeScale = 1f;
            }
        }
        TimeUpdater();
    }

    public void HealthUpdater(int health)
    {
        if (health <= 0)
        {
            health = 0;
        }

        if(health >= 100)
        {
            health = 100;
        }
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

        if(TimeMesh != null)
        {
            TimeMesh.text = "Time: " + "  " + minutes + ":" + seconds;
        }
    }

    public void ScoreUpdater(int score)
    {
        gameScore += score;

        ScoreMesh.text = "Score: " + gameScore;
        if (gameScore > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", gameScore);
            HighScoreMesh.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", gameScore).ToString();
        }
        else
        {
            HighScoreMesh.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", gameScore).ToString();
        }

        if(CurrentScoreMesh != null)
        {
            CurrentScoreMesh.text = "Score: " + gameScore.ToString();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
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
        PlayerPrefs.DeleteKey("Toggle");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOverStatus(bool gameOver)
    {
        PlayerPrefs.GetInt("Toggle");
        if(gameOver)
        {
            isGameWin = true;
            menus[1].SetActive(true);
            Time.timeScale = 0f;
            menus[3].SetActive(false);
            menus[4].SetActive(true);
        }
        else
        {
            isGameWin = false;
            menus[2].SetActive(true);
            Time.timeScale = 0f;
            menus[3].SetActive(false);
            menus[4].SetActive(true);
        }
    }

    public void StopBgm()
    {
        if(!bgmToggle.isOn)// Check is toggle is NOT turned on
        {
            PlayerPrefs.SetInt("Toggle", 1);
            AudioManager.instance.PauseBGM("BGM");
        }
        else // If it is turned on, ...
        {
            PlayerPrefs.SetInt("Toggle", 0);
            AudioManager.instance.UnPauseBGM("BGM");
        }
    }
}
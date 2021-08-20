using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player;

    public GameObject PauseMenu;

    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        player = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        Debug.Log("Pressed Start");
    }

    public void QuitGame()
    {
        Application.Quit();
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
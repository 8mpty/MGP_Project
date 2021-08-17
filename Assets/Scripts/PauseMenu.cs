using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject _pauseMenu;
    private bool isGamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pauseMenu != null)
        {
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }
}
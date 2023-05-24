using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public GameObject speedButton;
    public GameObject slowButton;
    private float gameSpeed = 1f;
    
    void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = gameSpeed;
        }
    }

    public void SpeedUp()
    {
        gameSpeed = 2f;
        Time.timeScale = gameSpeed;
        speedButton.SetActive(false);
        slowButton.SetActive(true);
    }
    
    public void SpeedDown()
    {
        gameSpeed = 1f;
        Time.timeScale = gameSpeed;
        slowButton.SetActive(false);
        speedButton.SetActive(true);
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

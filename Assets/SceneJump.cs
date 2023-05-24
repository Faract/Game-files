﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneJump : MonoBehaviour
{
    public int SceneNumber;
    public void Transition()
    {
        SceneManager.LoadScene(SceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
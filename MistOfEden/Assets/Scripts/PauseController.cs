﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public SceneAsset mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene.name);
    }
}
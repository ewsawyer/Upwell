using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public bool isGameOver { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            isGameOver = false;
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (CheckForRestart())
            SceneManager.LoadScene("GAME");

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private bool CheckForRestart()
    {
        // Pressing R always restarts
        if (Input.GetKeyDown(KeyCode.R))
            return true;

        // If game isn't over, can't restart
        if (!isGameOver)
            return false;

        // Restart via gamepad input
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            return true;

        // Restart via mouse input
        if (Input.GetMouseButtonDown(0))
            return true;

        return false;
    }

    public void EndGame()
    {
        isGameOver = true;
    }
}

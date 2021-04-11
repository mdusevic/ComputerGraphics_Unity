/*
 * File:	UIManager.cs
 *
 * Author: Mara Dusevic (s200494@students.aie.edu.au)
 * Date Created: Monday 12 April 2021
 * Date Last Modified: Monday 12 April 2021
 *
 * Used to manage all the UI elements in scene. 
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Takes in the text on screen to update it
    [SerializeField]
    public Text shardCountText;

    // HUD UI is used to enable and disable it
    [SerializeField]
    public GameObject topBarUI;

    // Used to grab the script from the player
    [SerializeField]
    public ShardCollection playerShards;

    // Used to enable and disable the end game screen
    [SerializeField]
    public GameObject endGameUI;

    // Stores shard collected
    private int shardCount;

    // Stores max amount of shards that can be collected
    private int maxShards;

    // Used to enable and disable cursor modes
    private bool switchCursorMode = false;
    private bool disableCursorMode = false;

    // Start Function
    void Start()
    {
        // Grabs the max amount of shards
        maxShards = playerShards.GetMaxShards();
        topBarUI.SetActive(true);
        endGameUI.SetActive(false);
    }

    // Update Function
    void Update()
    {
        // When TAB is pressed and cursor modes are enabled
        if (Input.GetKeyDown(KeyCode.Tab) && !disableCursorMode)
        {
            switchCursorMode = !switchCursorMode;

            if (switchCursorMode == true)
            {
                // Freezes screen and allows cursor to move freely on screen
                // Allows for UI in the world to be interacted with
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
            }
            else
            {
                // Unfreezes screen and locks cursor on screen to allow for raycasting
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1.0f;
            }
        }

        // Checks every update whether its the end of the game
        CheckForEndGame();

        // Updates the shards count
        UpdateShardCount();
    }

    // Changes the amount collected from the player's data 
    private void UpdateShardCount()
    {
        shardCount = playerShards.GetShards();

        // Updates the on screen text to display new values
        shardCountText.text = "Shards Collected:   " + shardCount + " / " + maxShards;
    }

    // Checks if game has ended
    private void CheckForEndGame()
    {
        // Uses the players check for the shard count
        bool gameWin = playerShards.IsEndGame();

        // If game has ended
        if (gameWin)
        {
            // Enables end game UI, freezes screen and disables cursor modes
            disableCursorMode = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            topBarUI.SetActive(false);
            endGameUI.SetActive(true);
        }
    }

    // Loads main game
    public void StartGame()
    {
        // Allows for cursor modes
        disableCursorMode = false;
        
        // Unfreezes screen
        Time.timeScale = 1.0f;

        // Loads main game scene
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    // Exits game
    public void ExitGame()
    {
        Application.Quit();
    }
}
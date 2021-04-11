using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Text shardCountText;

    [SerializeField]
    public GameObject topBarUI;

    [SerializeField]
    public ShardCollection playerShards;

    [SerializeField]
    public GameObject endGameUI;

    private int shardCount;
    private int maxShards;
    private bool switchCursorMode = false;
    private bool disableCursorMode = false;

    // Start is called before the first frame update
    void Start()
    {
        maxShards = playerShards.GetMaxShards();
        topBarUI.SetActive(true);
        endGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !disableCursorMode)
        {
            switchCursorMode = !switchCursorMode;

            if (switchCursorMode == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0.0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1.0f;
            }
        }

        CheckForEndGame();
        UpdateShardCount();
    }

    private void UpdateShardCount()
    {
        shardCount = playerShards.GetShards();
        shardCountText.text = "Shards Collected:   " + shardCount + " / " + maxShards;
    }

    private void CheckForEndGame()
    {
        bool gameWin = playerShards.IsEndGame();

        if (gameWin)
        {
            disableCursorMode = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            topBarUI.SetActive(false);
            endGameUI.SetActive(true);
        }
    }

    public void StartGame()
    {
        disableCursorMode = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
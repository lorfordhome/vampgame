using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }
    public GameState currentState;

    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject levelUpScreen;
    public GameObject gameHUD;

    public bool choosingUpgrade = false;
    AudioSource levelUpSound;
    public GameObject player;
    public float UIDelay = 0.5f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("gamemanager singleton error");
        }
        levelUpSound= GetComponent<AudioSource>();
        DisableScreens();
        Cursor.visible = false;
    }
    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                break;
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    gameHUD.SetActive(false);
                    levelUpScreen.SetActive(true);
                }
                break;
            default:
                Debug.LogWarning("invalid state");
                break;
        }
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            Cursor.visible = true;
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            gameHUD.SetActive(false);
            Debug.Log("game paused");
        }
    }
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            Cursor.visible = false;
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            gameHUD.SetActive(true);
            Debug.Log("game resumed");
        }
    }
    public void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState==GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartLevelUp()
    {
        Cursor.visible = true;
        StartCoroutine(misclickPrevention());
        levelUpSound.Play();
        ChangeState(GameState.LevelUp);
        player.SendMessage("RemoveAndApplyUpgrades");
    }
    public void EndLevelUp()
    {
        Cursor.visible = false;
        choosingUpgrade = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        gameHUD.SetActive(true) ;
        ChangeState(GameState.Gameplay);
    }

    private void DisableScreens()
    {
        pauseScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    IEnumerator misclickPrevention()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSecondsRealtime(UIDelay);
        Cursor.lockState = CursorLockMode.None;
    }
    
}

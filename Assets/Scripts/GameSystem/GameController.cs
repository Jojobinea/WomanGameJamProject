using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _settingsPanel;

    [SerializeField] private GameObject[] enemyPrefabs;

    private bool _gameIsPaused;

    private void Start()
    {
        EventManager.OnGamePauseEvent += PauseGame;
        EventManager.OnResumeGameEvent += UnpauseGame;

        _gameIsPaused = false;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        EventManager.OnGamePauseEvent -= PauseGame;
        EventManager.OnResumeGameEvent -= UnpauseGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_gameIsPaused)
                EventManager.OnGamePauseTrigger();
            else if (_gameIsPaused)
                EventManager.OnResumeGameTrigger();
        }

        if (AreAllEnemiesDefeated())
        {
            LoadVictoryScene();
        }
    }

    private bool AreAllEnemiesDefeated()
    {
        return GameObject.FindGameObjectsWithTag("Enemies").Length == 0;
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene("You Win");
    }

    private void PauseGame()
    {
        StopTime();
        _pausePanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _gameIsPaused = true;
    }

    private void UnpauseGame()
    {
        _pausePanel.SetActive(false);
        _settingsPanel.SetActive(false);
        RestartTime();
        _gameIsPaused = false;
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    private void RestartTime()
    {
        Time.timeScale = 1;
    }
}

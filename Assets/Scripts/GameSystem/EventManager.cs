using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    // EVENTS 

    #region Game Events

    public delegate void OnGamePause();
    public static event OnGamePause OnGamePauseEvent;

    public delegate void ResumeGame();
    public static event ResumeGame OnResumeGameEvent;

    #endregion


    
    #region Player Events

    public delegate void OnPlayerShoot();
    public static event OnPlayerShoot OnPlayerShootEvent;

    public delegate void OnPlayerChangeMagic(int identifier);
    public static event OnPlayerChangeMagic OnPlayerChangeMagicEvent;

    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath OnPlayerDeathEvent;

    #endregion



    // TRIGGERS

    #region GameEvents

    public static void OnGamePauseTrigger()
    {
        OnGamePauseEvent?.Invoke();
    }

    public static void OnResumeGameTrigger()
    {
        OnResumeGameEvent?.Invoke();
    }

    #endregion



    #region Player Triggers

    public static void OnPlayerShootTrigger()
    {
        OnPlayerShootEvent?.Invoke();
    }

    public static void OnPlayerChangeMagicTrigger(int identifier)
    {
        OnPlayerChangeMagicEvent?.Invoke(identifier);
    }

    public static void OnPlayerDeathTrigger()
    {
        OnPlayerDeathEvent?.Invoke();
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    // EVENTS 
    
    #region Player Events

    public delegate void OnPlayerShoot();
    public static event OnPlayerShoot OnPlayerShootEvent;

    public delegate void OnPlayerChangeMagic(int identifier);
    public static event OnPlayerChangeMagic OnPlayerChangeMagicEvent;

    #endregion

    // TRIGGERS

    #region Player Triggers

    public static void OnPlayerShootTrigger()
    {
        OnPlayerShootEvent?.Invoke();
    }

    public static void OnPlayerChangeMagicTrigger(int identifier)
    {
        OnPlayerChangeMagicEvent?.Invoke(identifier);
    }

    #endregion
}

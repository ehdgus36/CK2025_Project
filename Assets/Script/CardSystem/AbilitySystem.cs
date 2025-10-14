using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

public class AbilitySystem 
{
    Dictionary<string, Action<Unit>> Ability_TimingEvents;

    public static readonly string KEY_IS_PLAYER_HIT = "IsPlayerHit";
    public static readonly string KEY_IS_ENEMY_HIT = "IsEnemyHit";
    public static readonly string KEY_IS_CARD_PLAYED = "IsCardPlayed";
    public static readonly string KEY_IS_RHYTHMGAME_END = "IsRhythmGameEnd";

    public void Clear() => Ability_TimingEvents.Clear();

    public void PlayeEvent(string event_key, Unit unit)
    {
        //Ability_TimingEvents[event_key]?.Invoke(unit);
    }

    public void AddAvilityEvent(string event_key, Action<Unit> ability_event)
    {
        if (event_key == "0") return; 

        Ability_TimingEvents[event_key] += ability_event;
    }

}

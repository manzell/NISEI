using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public static class CombatManager
{
    public static ICE targetIce;
    public static bool inCombat;
    
    public static UnityEvent
        turnStartEvent = new UnityEvent(),
        turnEndEvent = new UnityEvent();
}

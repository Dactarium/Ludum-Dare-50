using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainEvent : GameEvent
{
    protected int LuckPoint;

    public static int CalculateTotalLuckPoint(params MainEvent[] mainEvents){
        int totalLP = 0;
        
        foreach(MainEvent mainEvent in mainEvents){
            totalLP += mainEvent.LuckPoint;
        }

        return totalLP;
    }

    public static MainEvent GetMainEventByLuck(float luck, params MainEvent[] mainEvents){
        float totalLP = CalculateTotalLuckPoint(mainEvents);
        float currentLP = 0f;

        foreach(MainEvent mainEvent in mainEvents){
            currentLP += mainEvent.LuckPoint;
            if(luck < currentLP/totalLP) return mainEvent;
        }

        return new NullEvent();
    }
}

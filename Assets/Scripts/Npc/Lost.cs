using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lost : Npc
{
    [SerializeField] private List<LostBuff> _possibleBuffs;

    public LostBuff Buff{get; private set;}
    
    protected override void Start(){
        base.Start();

        int totalLuckPoint = 0;

        foreach(LostBuff buff in _possibleBuffs){
            totalLuckPoint += buff.LuckPoint;
        }

        int selected = Random.Range(0, totalLuckPoint);

        foreach(LostBuff buff in _possibleBuffs){
            selected -= buff.LuckPoint;
            if(selected > 0) continue;
            ApplyBuff(buff);
            break;
        }
    }

    void ApplyBuff(LostBuff buff){
        gameObject.name += " (" + buff.name + ")";
        GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = buff.Material;
        GetComponentInChildren<Light>().color = buff.Material.GetColor("_Color_Top");

        Buff = buff;
    }
}

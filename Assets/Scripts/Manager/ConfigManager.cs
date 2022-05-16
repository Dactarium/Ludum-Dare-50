using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    public static ConfigManager Instance {get; private set;}
    
    public WaypointRoot WalkableWaypointRoot;
    public NpcSpawner HumanSpawner;
    public GameObject DeathMark;

    #region Main Events
        [Header("Get Soul From Target")]
        #region 
            [Rename("Luck Point")] [SerializeField] int _getSoulFromTarget_LuckPoint;
            public int GetSoulFromTarget_LuckPoint {get => _getSoulFromTarget_LuckPoint;}

            [Rename("Time Gain")] [SerializeField] float _getSoulFromTarget_TimeGain;
            public float GetSoulFromTarget_TimeGain {get => _getSoulFromTarget_TimeGain;}

            [Rename("Event Detail")] [SerializeField] string _getSoulFromTarget_EventDetail;
            public string GetSoulFromTarget_EventDetail {get => _getSoulFromTarget_EventDetail;}
        #endregion
        
        [Header("Delay Your Death")]
        #region 
            [Rename("Luck Point")] [SerializeField] int _delayYourDeath_LuckPoint;
            public int DelayYourDeath_LuckPoint {get => _delayYourDeath_LuckPoint;}

            [Rename("Time Gain")] [SerializeField] float _delayYourDeath_TimeGain;
            public float DelayYourDeath_TimeGain {get => _delayYourDeath_TimeGain;}

            [Rename("Event Detail")] [SerializeField] string _delayYourDeath_EventDetail;
            public string DelayYourDeath_EventDetail {get => _delayYourDeath_EventDetail;}

            [Rename("Target Soul Amount")] [SerializeField] int _delayYourDeath_TargetSoulAmount;
            public int DelayYourDeath_TargetSoulAmount {get => _delayYourDeath_TargetSoulAmount;}
        #endregion
    #endregion

    #region Side Events
        [Header("Collect Losts")]
        #region 
            [Rename("Time Gain")] [SerializeField] float _collectLosts_TimeGain;
            public float CollectLosts_TimeGain {get => _collectLosts_TimeGain;}
        #endregion

        [Header("Collecting Soul Too Soon")]
        #region 
            [Rename("Time Gain")] [SerializeField] float _collectingSoulTooSoon_TimeGain;
            public float CollectingSoulTooSoon_TimeGain {get => _collectingSoulTooSoon_TimeGain;}

            [Rename("Spawn Object")] [SerializeField] GameObject _collectingSoulTooSoon_Spawn_Object;
            public GameObject CollectingSoulTooSoon_Spawn_Object {get => _collectingSoulTooSoon_Spawn_Object;}
        #endregion
    #endregion

    #region Npc Profiles
        [Header("Human")]
        #region
            [Rename("Integrity")] [SerializeField] float _human_Integrity;
            public float Human_Integrity {get => _human_Integrity;}
        #endregion
        
        [Header("Lost")]
        #region
            [Rename("Integrity")] [SerializeField] float _lost_Integrity;
            public float Lost_Integrity {get => _lost_Integrity;}
        #endregion
    #endregion
    void Awake(){
        Instance = this;
    } 
}

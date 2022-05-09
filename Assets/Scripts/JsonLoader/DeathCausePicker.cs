using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCausePicker : MonoBehaviour
{   
    public static DeathCausePicker Instance {get; private set;}
    [SerializeField] private TextAsset json;
    private JsonData data;
    
    public string RandomCause(string name){
            string cause = data.causes[Random.Range(0, data.causes.Length)];
            return cause.Replace("{name}", name);
    }

    void Awake(){
        Instance = this;
        data = JsonUtility.FromJson<JsonData>(json.text);
    }

    [System.Serializable]
    public class JsonData{
        public string[] causes;
    }

}

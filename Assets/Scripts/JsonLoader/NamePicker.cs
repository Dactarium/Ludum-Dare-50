using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamePicker : MonoBehaviour
{   
    public static NamePicker Instance {get; private set;}

    [SerializeField] private TextAsset json;
    private JsonData data;
    public enum Type
    {
        SpecialMale,
        SpecialFemale,
        Male,
        Female
    }

    void Awake(){
        Instance = this;
        data = JsonUtility.FromJson<JsonData>(json.text);
    }
    

    public string RandomName(Type type){
        string name = "";
        
        switch(type){
            case Type.SpecialMale:
                name = data.special_male[Random.Range(0, data.special_male.Length)];
                break;
            case Type.SpecialFemale:
                name = data.special_female[Random.Range(0, data.special_female.Length)];
                break;
            case Type.Male:
                name = data.male[Random.Range(0, data.male.Length)];
                name += " " + RandomSurname();
                break;
            case Type.Female:
                name = data.female[Random.Range(0, data.female.Length)];
                name += " " + RandomSurname();
                break;
        }

        return name;
    }

    private string RandomSurname(){
        return data.surname[Random.Range(0, data.surname.Length)];
    }


    [System.Serializable]
    public class JsonData{
        public string[] special_male;
        public string[] special_female;
        public string[] male;
        public string[] female;
        public string[] surname;
    }

}




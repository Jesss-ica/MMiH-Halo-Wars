using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveDataJSON : MonoBehaviour
{
    private SaveData saveData;
    // Start is called before the first frame update
    private void Awake()
    {
        //EnsureData();
    }

    void Start()
    {
        saveData = SaveData.Instance;
    }

    public void StoreData()
    {
        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);

        using (StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json")) 
        { 
            writer.WriteLine(json);
        }
    }

    public void RetrieveData()
    {
        string json = string.Empty;
        Debug.Log(json);

        using(StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        { 
            json = reader.ReadToEnd();
        }

        SaveData data = JsonUtility.FromJson<SaveData>(json);
        saveData.SetSaveData(data.MariG, data.Pilot, data.CChic, data.CliSci, data.ZombieDef, data.BatDef, data.SpiderDef, data.ZTrophy, data.BTrophy, data.STrophy, data.DmndPlaque, data.AlexPlaque);
    }

    public void EnsureData()
    {
        if(!File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {

        }
    }
}

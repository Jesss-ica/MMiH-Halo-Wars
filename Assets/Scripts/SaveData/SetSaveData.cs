using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class SetSaveData : MonoBehaviour
{
    public TextMeshProUGUI SaveDebugMenu;
    [SerializeField] SaveDataJSON SDJSON;
    SaveData saveData = SaveData.Instance;

    public GameObject[] secrets;

    private void Start()
    {
        if (File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            SDJSON.RetrieveData();
        }
        if (saveData.DmndPlaque == false || saveData.AlexPlaque == false)
        {
            DmndPlaqueSet();
            AlexPlaqueSet();
            SDJSON.StoreData();
        }
        SetSecrets();
        SetDebugMenu();
    }

    private void DmndPlaqueSet()
    {
        if (saveData.MariG && saveData.Pilot && saveData.CChic && saveData.CliSci)
        {
            saveData.DmndPlaque = true;
        }
    }

    private void AlexPlaqueSet()
    {
        if (saveData.ZTrophy && saveData.BTrophy && saveData.STrophy)
        {
            saveData.AlexPlaque = true;
        }
    }

    void SetSecrets()
    {
        secrets[0].SetActive(saveData.MariG);
        secrets[1].SetActive(saveData.Pilot);
        secrets[2].SetActive(saveData.CChic);
        secrets[3].SetActive(saveData.CliSci);
        secrets[4].SetActive(saveData.ZTrophy);
        secrets[5].SetActive(saveData.BTrophy);
        secrets[6].SetActive(saveData.STrophy);
        secrets[7].SetActive(saveData.DmndPlaque);
        secrets[8].SetActive(saveData.AlexPlaque);
    }

    private void SetDebugMenu()
    {
        SaveDebugMenu.text = "MariG found //" + SaveData.Instance.MariG +
            "\r\nPilot found //" + SaveData.Instance.Pilot +
            "\r\nCChic found //" + SaveData.Instance.CChic +
            "\r\nCliSci found //" + SaveData.Instance.CliSci +
            "\r\n--" +
            "\r\nZombie Def //" + SaveData.Instance.ZombieDef +
            "\r\nBat Def //" + SaveData.Instance.BatDef +
            "\r\nSpider Def //" + SaveData.Instance.SpiderDef +
            "\r\nZ Trophy //" + SaveData.Instance.ZTrophy +
            "\r\nB Trophy //" + SaveData.Instance.BTrophy +
            "\r\nS Trophy //" + SaveData.Instance.STrophy +
            "\r\n--" +
            "\r\nJessD Plaq //" + SaveData.Instance.DmndPlaque +
            "\r\nAlex Plaq //" + SaveData.Instance.AlexPlaque;
    }
}

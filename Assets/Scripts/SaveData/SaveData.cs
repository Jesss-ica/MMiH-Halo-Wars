using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveData
{
    // Secret
    public bool MariG;
    public bool Pilot;
    public bool CChic;
    public bool CliSci;

    //Enemy Defeat
    public int ZombieDef;
    public int BatDef;
    public int SpiderDef;

    // Enemy Trophy
    public bool ZTrophy;
    public bool BTrophy;
    public bool STrophy;

    // 100% Completion
    public bool DmndPlaque;
    public bool AlexPlaque;

    private static SaveData _instance = null;

    public delegate void OnSaveDataChange(bool mariG, bool pilot, bool cChic, bool cliSci,int zombieDef, int batDef, int spiderDef, bool zTrophy, bool bTrophy, bool sTrophy, bool dmndPlaque, bool alexPlaque);
    public static event OnSaveDataChange OnDataChange;

    public static SaveData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new SaveData(false, false, false, false, 0, 0, 0, false, false, false, false, false);
            }

            return _instance;
        }
    }

    private SaveData(bool mariG, bool pilot, bool cChic, bool cliSci, int zombieDef, int batDef, int spiderDef, bool zTrophy, bool bTrophy, bool sTrophy, bool dmndPlaque, bool alexPlaque)
    {
        this.MariG = mariG;
        this.Pilot = pilot;
        this.CChic = cChic;
        this.CliSci = cliSci;

        this.ZombieDef = zombieDef;
        this.BatDef = batDef;
        this.SpiderDef = spiderDef;
        this.ZTrophy = zTrophy;
        this.BTrophy = bTrophy;
        this.STrophy = sTrophy;

        this.DmndPlaque = dmndPlaque;
        this.AlexPlaque = alexPlaque;
    }

    public void SetSaveData(bool mariG, bool pilot, bool cChic, bool cliSci, int zombieDef, int batDef, int spiderDef, bool zTrophy, bool bTrophy, bool sTrophy, bool dmndPlaque, bool alexPlaque)
    {
        this.MariG = mariG;
        this.Pilot = pilot;
        this.CChic = cChic;
        this.CliSci = cliSci;

        this.ZombieDef = zombieDef;
        this.BatDef = batDef;
        this.SpiderDef = spiderDef;
        this.ZTrophy = zTrophy;
        this.BTrophy = bTrophy;
        this.STrophy = sTrophy;

        this.DmndPlaque = dmndPlaque;
        this.AlexPlaque = alexPlaque;

        OnDataChange?.Invoke(mariG, pilot, cChic, cliSci, zombieDef, batDef, spiderDef, zTrophy, bTrophy, sTrophy, dmndPlaque, alexPlaque);
    }
}


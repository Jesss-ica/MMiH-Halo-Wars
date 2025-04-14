using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ArchiveManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] Image imageHolder;

    public SaveDataJSON save;

    public static event Action OnArciveStarted;
    public static event Action OnArciveEnded;
    
    bool cycleArciveTriggered;
    int cycleDirection;
    bool inArchive = false;

    public ArchiveSO ArchiveAsset;

    [Header("Display Position")]
    public GameObject positionMarkerPrefab;
    public GameObject positionBar;

    /*
    public static ArchiveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    */


    public void StartArchive()
    {
        inArchive = true;
        StopAllCoroutines();
        StartCoroutine(RunArchive());
    }

    IEnumerator RunArchive()
    {
        cycleArciveTriggered = false;
        OnArciveStarted?.Invoke();

        for (int i = 0; inArchive == true; i = i + cycleDirection)
        {
            i = LoopArchive(i);
            SetPosInArchive(i);
            titleText.text = ArchiveAsset.entry[i].title;
            descriptionText.text = ArchiveAsset.entry[i].description;
            imageHolder.sprite = ArchiveAsset.entry[i].image;
            while (cycleArciveTriggered == false)
            {
                // Wait for the current line to be skipped
                yield return null;
            } 
            cycleArciveTriggered = false;
        }

        inArchive = false;
        OnArciveEnded?.Invoke();
    }

    public void SetArchivePosMarkers()
    {
        for (int i = 0; i < ArchiveAsset.entry.Length; i++)
        {
            Instantiate(positionMarkerPrefab, positionBar.transform);
        }
    }

    void SetPosInArchive(int i)
    {
        for(int j = 0; j < ArchiveAsset.entry.Length; j++)
        {
            positionBar.transform.GetChild(j).transform.GetComponent<Image>().color = Color.white;
        }
        positionBar.transform.GetChild(i).transform.GetComponent<Image>().color = Color.blue;
    }

    public void NextEntry()
    {
        cycleDirection = 1;
        cycleArciveTriggered = true;
    }

    public void PreviousEntry()
    {
        cycleDirection = -1;
        cycleArciveTriggered = true;
    }

    int LoopArchive(int i)
    {
        if( i == -1)
        {
            return ArchiveAsset.entry.Length - 1;
        }
        if( i == ArchiveAsset.entry.Length)
        {
            return 0;
        }
        return i;
    }
}

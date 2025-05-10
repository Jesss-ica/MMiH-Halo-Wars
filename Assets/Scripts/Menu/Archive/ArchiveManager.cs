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

    int indexArchive = 0;

    public ArchiveSO ArchiveAsset;

    [Header("Display Position")]
    public GameObject positionMarkerPrefab;
    public GameObject positionBar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousEntry();
        }else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextEntry();
        }
    }

    public void UpdateArchivePos()
    {
        titleText.text = ArchiveAsset.entry[indexArchive].title;
        descriptionText.text = ArchiveAsset.entry[indexArchive].description;
        imageHolder.sprite = ArchiveAsset.entry[indexArchive].image;
        SetPosInArchive(indexArchive);
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

    private void Start()
    {
        UpdateArchivePos();
    }

    public void NextEntry()
    {
        indexArchive = (indexArchive + 1) % ArchiveAsset.entry.Length;
        UpdateArchivePos();
    }

    public void PreviousEntry()
    {
        indexArchive = (ArchiveAsset.entry.Length + indexArchive - 1) % ArchiveAsset.entry.Length;
        UpdateArchivePos();
    }
}

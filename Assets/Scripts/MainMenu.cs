using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject shade;
    public GameObject credits;
    public GameObject archive;
    public GameObject changeLog;
    public GameObject buttonPanel;
    public ArchiveManager archiveManager;

    public Camera mainCamera;

    private bool archivePanelState = false;
    private bool creditPanelState = false;
    private bool changeLogPanelState = false;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ReadAchivment();
        }
    }
    private void Start()
    {
        archiveManager.SetArchivePosMarkers();
        //changeLog.transform.GetComponent<ChangeLogHandler>().SetChangeLog();
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void ReadAchivment()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Achivment")
            {
                objectHit.gameObject.GetComponent<AudioSource>().Play();
            }
        }

    }

    public void SetArchive()
    {
        if (archivePanelState == false)
        {
            arcState();
            archiveManager.StartArchive();
            archivePanelState = true;
        }
        else
        {
            arcState();
            archivePanelState = false;
        }
    }

    public void SetChangeLog()
    {
        if (changeLogPanelState == false)
        {
            clogState();
            //changeLog.transform.GetComponent<ChangeLogHandler>().ActivateChangeLog();
            changeLogPanelState = true;
        }
        else
        {
            clogState();
            changeLogPanelState = false;
        }
    }

    private void clogState()
    {
        buttonPanel.SetActive(changeLogPanelState);
        changeLog.SetActive(!changeLogPanelState);
        shade.SetActive(!changeLogPanelState);
    }

    private void arcState()
    {
        buttonPanel.SetActive(archivePanelState);
        archive.SetActive(!archivePanelState);
        shade.SetActive(!archivePanelState);
    }

    public void SetCredit()
    {
        if (creditPanelState == false)
        {
            creditPanelState = true;
        }
        else
        {
            creditPanelState = false;
        }
        buttonPanel.SetActive(!creditPanelState);
        credits.SetActive(creditPanelState);
        shade.SetActive(creditPanelState);

    }
    public void ExitApplication()
    {
        Application.Quit();
    }

}

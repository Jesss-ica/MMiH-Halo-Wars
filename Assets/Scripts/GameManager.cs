using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject shade;
    public GameObject PauseMenu;
    public GameObject DeathScreen;
    public GameObject VictoryScreen;


    private bool gamePaused;
    private bool gameEnd = false;
    Door[] doors;
    [HideInInspector]
    public SaveDataJSON save;
    void Start()
    {
        save = FindAnyObjectByType<SaveDataJSON>();
        
        if (File.Exists(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            save.RetrieveData();
        }
        doors = FindObjectsOfType<Door>();
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].CloseDoor();
        }
    }

    void Update()
    {
        if(gameEnd == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gamePaused)
                {
                    CursorState();
                    ResumeGame();
                }
                else if (!gamePaused)
                {
                    CursorState();
                    PauseGame();
                }
            }
        }
    }


    public void VictoryState()
    {
        gameEnd = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        shade.SetActive(true);
        VictoryScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeathState()
    {
        gameEnd = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        shade.SetActive(true);
        DeathScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        shade.SetActive(false);
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        gamePaused = false;
    }

    public void PauseGame()
    {
        shade.SetActive(true);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void CursorState()
    {
        if(!gamePaused)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        } else if (gamePaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

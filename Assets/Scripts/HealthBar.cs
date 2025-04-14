using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image fill;
    public PlayerController player;
    void Start()
    {
        maximum = player.HealthPoints;
        current = player.HealthPoints;
    }

    public void GetCurrentFill(int hp)
    {
        current = hp;
        float fillAmount = (float)current / (float)maximum;
        fill.fillAmount = fillAmount;
    }
}

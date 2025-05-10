using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{
    private Camera mainCamera;
    public int Damage;
    public UnityEngine.UI.Image crosshair;
    public GameObject weaponVisual;
    public AudioClip weaponSound;

    private Sprite nonShotSprite;
    public Sprite shotSprite;

    [HideInInspector]
    public bool IsAvailable = true;
    public float CooldownDuration = 2f;

    void Start()
    {
        mainCamera = Camera.main;
        crosshair.color = Color.cyan;
        nonShotSprite = weaponVisual.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            UseAbility();
        }
    }

    void UseAbility()
    {
        // if not available to use (still cooling down) just exit
        if (IsAvailable == false)
        {
            return;
        }

        Shoot();

        // start the cooldown timer and flash gun
        
        StartCoroutine(StartCooldown());
        StartCoroutine(WeaponFlash());
    }
    public IEnumerator WeaponFlash()
    {
        weaponVisual.GetComponent<SpriteRenderer>().sprite = shotSprite;
        yield return new WaitForSeconds(0.2f);
        weaponVisual.GetComponent<SpriteRenderer>().sprite = nonShotSprite;
    }
    public IEnumerator StartCooldown()
    {
        IsAvailable = false;
        crosshair.color = Color.yellow;

        yield return new WaitForSeconds(CooldownDuration);

        crosshair.color = Color.cyan;
        IsAvailable = true;
    }
    private void Shoot()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        weaponVisual.transform.GetComponent<AudioSource>().Play();

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log(hit);
            if (objectHit.tag == "envInteractable")
            {
                objectHit.gameObject.GetComponent<AnotherScriptThisTimeForTheDoorAgain>().GetHit();
            }
            if (objectHit.tag == "Enemy")
            {
                objectHit.gameObject.GetComponent<Agent>().GetShot(Damage);
                Debug.Log(hit.transform.name);
            }
        }
    }
}

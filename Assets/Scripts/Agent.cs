using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Agent : MonoBehaviour
{
    #region Agent
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private float DetectionDistance;
    [SerializeField]
    private List<Transform> PatrolTargets;
    [SerializeField]
    public Color FlashColour; // The colour to flash when shot. Set in the inspector.

    private int destPoint = 0;
    private NavMeshAgent agent;

    private bool playerDetected;
    private bool patrolling;
    private MeshRenderer meshRenderer;
    
    private Color originalColour; // The colour the mesh STARTed with.

    private int healthPoints;
    public bool IsAvailable = true;
    private char Type;
    [HideInInspector]
    public bool isDead;

    public GameObject assignedDoor;

    [HideInInspector]
    public SaveDataJSON save;
    [HideInInspector]
    public SaveData saveData = SaveData.Instance;

    private GameObject temp;
    private ParticleSystem particleSys;

    [Header("Audio Clips")]
    public AudioClip[] HitSounds;
    private AudioSource audioSource;

    public void AgentStart()
    {
        playerTransform = FindAnyObjectByType<PlayerController>().transform;
        save = FindAnyObjectByType<SaveDataJSON>();
        AssignEnemyToDoor();

        if(TryGetComponent<NavMeshAgent>(out NavMeshAgent component2))
        {
            agent = GetComponent<NavMeshAgent>();
        }
        
        
        meshRenderer = transform.GetChild(0).gameObject.GetComponent<MeshRenderer>();
        particleSys = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        originalColour = meshRenderer.material.color;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void AssignEnemyToDoor()
    {
        if(assignedDoor != null)
        {
            if (assignedDoor.TryGetComponent(out MultiDoorHandler component))
            {
                component.AddEnemies();
            }
            else if (assignedDoor.TryGetComponent(out Door component1))
            {
                component1.AddEnemy();
            }
        }
    }

    public void SetStats(int HP, char type)
    {
        healthPoints = HP;
        IsAvailable = true;
        Type = type;
    }

    public void Temp(GameObject Temp)
    {
        temp = Temp;
    }

    public void AgentUpdate()
    {
        if (isPlayerClose())
        {
            agent.destination = playerTransform.position;
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                goToNextPoint();
            }
        }
    }

    public void GetShot(int dmg){
        HitNoise();
        PlayParticals();
        Damaged(dmg);
        //StopAllCoroutines();
        //StartCoroutine(Pause());
        StartCoroutine(Flash());
    }

    public void PlayParticals()
    {
        particleSys.Play();
    }

    void HitNoise()
    {
        //Stole this code from the Unity Docs :D https://docs.unity3d.com/2020.2/Documentation/Manual/class-Random.html#:~:text=Choosing%20a%20Random%20Item%20from%20an%20Array&text=var%20element%20%3D%20myArray%5BRandom.,Length)%5D%3B
        audioSource.clip = HitSounds[UnityEngine.Random.Range(0, HitSounds.Length)];
        // audio.pitch
        audioSource.Play();
    }

    public void Damaged(int dmg)
    {
        healthPoints = healthPoints - dmg;
        if (healthPoints <= 0)
        {
            EnemySaves();
            if (assignedDoor.TryGetComponent(out MultiDoorHandler component))
            {
                component.RemoveEnemies();
            }
            else if (assignedDoor.TryGetComponent(out Door component1))
            {
                component1.RemoveEnemy();
            }
            if (Type == 't')
            {
                Destroy(temp);
            }
            ElimEnemy();
        }
    }

    void ElimEnemy()
    {
        StopAllCoroutines();
        isDead = true;
        IsAvailable = false;
        if(gameObject.TryGetComponent<NavMeshAgent>(out NavMeshAgent component3))
        {
            component3.enabled = false;
        }
        meshRenderer.enabled = false;
        if (gameObject.TryGetComponent<SphereCollider>(out SphereCollider component))
        {
            component.enabled = false;
        }
        if (gameObject.TryGetComponent<CapsuleCollider>(out CapsuleCollider component1))
        {
            component1.enabled = false;
        }
        if (gameObject.TryGetComponent<MeshCollider>(out MeshCollider component2))
        {
            component2.enabled = false;
        }
        PlayParticals();
        StartCoroutine(TimeTillDeath(1f));
    }

    void EnemySaves()
    {
        switch (Type) {
            case 'z':
                saveData.ZombieDef++;
                if (saveData.ZombieDef >= 30)
                {
                    saveData.ZTrophy = true;
                }
                break;
            case 's':
                saveData.SpiderDef++;
                if (saveData.SpiderDef >= 20)
                {
                    saveData.STrophy = true;
                }
                break;
            case 'b':
                saveData.BatDef++;
                if (saveData.BatDef >= 20)
                {
                    saveData.BTrophy = true;
                }
                break;
        }
        save.StoreData();
    }

    public IEnumerator Pause(){
        agent.isStopped = true;
        yield return new WaitForSeconds(1.0f);
        agent.isStopped = false;
    }

    IEnumerator Flash(){
        meshRenderer.material.color = FlashColour;
        yield return new WaitForSeconds(0.2f);
        meshRenderer.material.color = originalColour;
    }

    void goToNextPoint(){
        // Returns if no points have been set up
        if (PatrolTargets.Count == 0){
            return;
        }

        // Set the agent to go to the currently selected destination
        agent.destination = PatrolTargets[destPoint].position;

        // Choose the next target in the list as the destination, cycling to the 
        // start if necessary.
        destPoint = (destPoint + 1) % PatrolTargets.Count;
    }

    public IEnumerator StartCooldown(float CooldownDuration)
    {
        IsAvailable = false;
        yield return new WaitForSeconds(CooldownDuration);
        IsAvailable = true;
    }

    public IEnumerator TimeTillDeath(float CooldownDuration)
    {
        yield return new WaitForSeconds(CooldownDuration);
        Destroy(gameObject);
    }

    public bool isPlayerClose(){
        if (Vector3.Distance(transform.position, playerTransform.position) <= DetectionDistance){
            return true;
        }
        return false;
    }

    public bool inRange(float variable)
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= agent.stoppingDistance + variable)
        {
            return true;
        }
        return false;
    }
    #endregion
}

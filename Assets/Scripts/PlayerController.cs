using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// https://sharpcoderblog.com/blog/unity-3d-fps-controller
public class PlayerController : MonoBehaviour
{
    #region player controller
    // Public variable declarations - these can be overridden in the Inspector
    public float WalkingSpeed = 7.5f;
    public float RunningSpeed = 11.5f;
    public float JumpSpeed = 8.0f;
    public float Gravity = 20.0f;
    public Camera PlayerCamera;
    public float LookSpeed = 2.0f;
    public float LookXLimit = 45.0f;

    // Private variable declarations
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;

    private bool canMove = true;

    void Start()
    {
        //HPTMP.text = "HP:" + HealthPoints;

        characterController = GetComponent<CharacterController>();
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HitEffect.SetActive(false);
    }

    void Update()
    {
        Facing();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? RunningSpeed : WalkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = JumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= Gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * LookSpeed;
            rotationX = Mathf.Clamp(rotationX, -LookXLimit, LookXLimit);
            PlayerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * LookSpeed, 0);
        }
    }
    #endregion

    #region Dialogue Interact

    [SerializeField] float talkDistance = 2;
    bool inConversation;
    public GameObject prompt;

    void Interact()
    {
        if (inConversation)
        {
            DialogueManager.instance.SkipLine();
        }
        else
        {
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out PassiveNPC npc))
                {
                    DialogueManager.instance.StartDialogue(npc.dialogueAsset.dialogue, npc.StartPosition, npc.npcName, npc.dialogueAsset.Index, npc.gameObject.GetComponent<AudioSource>());
                }
                if(hitInfo.collider.gameObject.TryGetComponent<Door>(out Door component))
                {
                    if(component.awaitinglocked == true)
                    {
                        component.UnlockDoor();
                    }
                }
            }
        }
    }

    void Facing()
    {
        if(Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, talkDistance))
        {
            if (hitInfo.collider.gameObject.transform.tag == "NPC")
            {
                prompt.SetActive(true) ;
            }
            if (hitInfo.collider.gameObject.TryGetComponent<Door>(out Door component))
            {
                if(component.awaitinglocked == true)
                {
                    prompt.SetActive(true);
                }
            }
        }
        else {
            prompt.SetActive(false) ;
        }
    }

    void JoinConversation()
    {
        inConversation = true;
    }

    void LeaveConversation()
    {
        inConversation = false;
    }

    private void OnEnable()
    {
        DialogueManager.OnDialogueStarted += JoinConversation;
        DialogueManager.OnDialogueEnded += LeaveConversation;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStarted -= JoinConversation;
        DialogueManager.OnDialogueEnded -= LeaveConversation;
    }
    #endregion

    #region General
    public int HealthPoints;
    public TextMeshProUGUI HPTMP;
    public GameManager GameManager;
    public HealthBar HealthBar;
    public GameObject HitEffect;
    public void TakeDamage(int damage)
    {
        HealthPoints = HealthPoints - damage;
        //HPTMP.text = "HP:" + HealthPoints;
        HealthBar.GetCurrentFill(HealthPoints);
        HitAnimation();
        if (HealthPoints <= 0)
        {
            GameManager.DeathState();
        }
    }

    void HitAnimation()
    {
        HitEffect.SetActive(true);
        HitEffect.GetComponent<Animation>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.VictoryState();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enviromental"))
        {
            TakeDamage(1);
        }
        if (other.CompareTag("InstaDeath"))
        {
            TakeDamage(10000);
        }
    }

    #endregion
}
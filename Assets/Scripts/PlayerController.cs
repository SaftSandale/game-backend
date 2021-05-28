using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Unity Variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject InteractIcon;
    public Vector2 movement;
    public float moveLimiter = 0.7f;
    public Transform trans;
    public PauseManager pauseManager;
    public InformationUIManager infoUIManager;
    public GameObject infoUI;
    private bool controlallowed;
    private Vector2 boxSize = new Vector2(0.1f, 0.1f);
    #endregion

    #region Unity Methods
    void Start()
    {
        controlallowed = true;
    }

    void Update()
    {
        if (controlallowed)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            Vector3 lookVec = new Vector3(movement.x, movement.y, 360);

            // Check for diagonal movement and limit it
            if (movement.x != 0 && movement.y != 0)
            {
                movement.x *= moveLimiter;
                movement.y *= moveLimiter;
            }

            // Check for player rotation after movement stopped never change anything
            if (movement.x != 0 || movement.y != 0)
            {
                trans.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(movement.x, -movement.y) * Mathf.Rad2Deg);
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("Rotation_Z", trans.rotation.z);

            if (Input.GetKeyDown(KeyCode.E))
            {
                checkInteraction();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseManager.WakePauseMenu();
            }

            
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetFloat("Rotation_Z", trans.rotation.z);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (infoUI.activeSelf)
            {
                infoUIManager.CloseInfoMenu();
            }
            else
            {
                infoUIManager.WakeInfoMenu();
            }
        }
    }
   
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Zeigt das Interact Icon an.
    /// </summary>
    public void openInteractableIcon()
    {
        InteractIcon.SetActive(true);
    }

    /// <summary>
    /// Blendet das Interact Icon aus.
    /// </summary>
    public void closeInteractableIcon()
    {
        InteractIcon.SetActive(false);
    }

    /// <summary>
    /// Prüft, ob aktuell interagiert werden kann.
    /// </summary>
    private void checkInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D ray in hits)
            {
                if (ray.transform.GetComponent<Interactable>())
                {
                    ray.transform.GetComponent<Interactable>().interact();
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Sperrt die Bewegung des Spielers.
    /// </summary>
    public void suspendMovement()
    {
        controlallowed = false;
    }

    /// <summary>
    /// Aktiviert die Bewegung des Spielers.
    /// </summary>
    public void resumeMovement()
    {
        controlallowed = true;
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject InteractIcon;
    public Vector2 movement;
    public float moveLimiter = 0.7f;
    public Transform trans;
    private bool controlallowed;

    private Vector2 boxSize = new Vector2(0.1f, 0.1f);

    // Start is called before the first frame update
    void Start()
    {
        controlallowed = true;
    }

    // Update is called once per frame
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
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void openInteractableIcon()
    {
        InteractIcon.SetActive(true);
    }

    public void closeInteractableIcon()
    {
        InteractIcon.SetActive(false);
    }

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

    public void suspendMovement()
    {
        controlallowed = false;
    }

    public void resumeMovement()
    {
        controlallowed = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{   
    [SerializeField] protected float movementSpeed = 100f;
    protected float verticalDirection = 1;
    protected float sprintValue = 0f;
    
    protected Rigidbody rb;
    protected Animator animator;

    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        verticalDirection = Input.GetAxis("Vertical");
        verticalDirection = Mathf.Clamp(verticalDirection, 0, 1);

        sprintValue = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", verticalDirection + sprintValue);
    }
        void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.velocity = Vector3.forward * verticalDirection * (sprintValue + 1) * movementSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}


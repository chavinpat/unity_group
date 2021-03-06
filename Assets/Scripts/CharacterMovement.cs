using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 100f;
    [SerializeField] private ParticleSystem bloodFX;
    [SerializeField] private Transform bloodSpawnPoint;
    [SerializeField] private AudioSource gunsound;

    protected float verticalDirection = 1;

    protected float sprintValue = 0f;

    public bool IsInvulnerable { get; private set; }

    private bool canMove = true;

    protected Rigidbody rb;
    protected Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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

    public bool IsMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }

    public virtual void Die()
    {   
        animator.SetTrigger("Death");

        var spawnedFX = Instantiate(bloodFX, bloodSpawnPoint.position, bloodFX.transform.rotation);
        gunsound.Play();
        Destroy(spawnedFX, 5f);
        
        canMove = false;
        Debug.Log(name + " has died!");
    }

    public virtual void Win()
    {
        IsInvulnerable = true;
        Debug.Log(name + " has won!");
    }
}

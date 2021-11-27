using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{   
    public Rigidbody obst;
    private bool direction = true;

    // Start is called before the first frame update
    void Start()
    {
        obst = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "wall_left") {
            direction = true;
            
        }
        
        if (other.gameObject.tag == "wall_right") {
            direction = false;
            
        }
    }


    void Update()
    {
        if (direction == true) {
            transform.position += Vector3.right * Time.deltaTime;
        }

        if (direction == false) {
            transform.position += Vector3.left * Time.deltaTime;
        }
    }
}

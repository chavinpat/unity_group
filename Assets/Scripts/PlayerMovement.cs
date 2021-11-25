using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;

// [RequireComponent(typeof(ARFace))]
public class PlayerMovement : CharacterMovement
{   
    public float player_jaw_coef = 0f;
    public float player_tongue_coef = 0f;
    // private ARKitBlendShapeLocation blendShapeToTrack = ARKitBlendShapeLocation.JawOpen;

    void Start() {
    
    verticalDirection = 0;
    }
    
    public void GetCoeff(float jaw_coef, float tongue_coef){
        Debug.Log("jaw_coef_player = "+ jaw_coef);
        Debug.Log("tongue_coef_player = "+ tongue_coef);
        player_jaw_coef = jaw_coef;
        player_tongue_coef = tongue_coef;
    }

    void Update() 
    {
        if (player_jaw_coef>0.2) {
            verticalDirection = player_jaw_coef;
        }
        else {
            verticalDirection = 0;
        }
        // verticalDirection = jaw_coef;
        verticalDirection = Mathf.Clamp(verticalDirection, 0, 1);

        // sprintValue = Input.GetAxis("Sprint");
        sprintValue = player_tongue_coef;

        animator.SetFloat("Speed", verticalDirection + sprintValue);
    }

    public override void Die()
    {
        base.Die();
        UIManager.Instance.TriggerLoseMenu();
    }

    public override void Win()
    {
        base.Win();
        UIManager.Instance.TriggerWinMenu();
    }
}

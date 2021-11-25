using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;

[RequireComponent(typeof(ARFace))]
public class PlayerMovement : CharacterMovement
{   
    private ARFace face;
    private ARKitFaceSubsystem faceSubsystem;

    [SerializeField]
    private Dictionary<ARKitBlendShapeLocation, float> cacheBlendShape;

    private ARKitBlendShapeLocation blendShapeToTrack = ARKitBlendShapeLocation.JawOpen;

    public float test_coef;

    void Start() {
    
        face = GetComponent<ARFace>();
        ARFaceManager faceManager = FindObjectOfType<ARFaceManager>();
        
        verticalDirection = 0;

        if (faceManager != null) {
            faceSubsystem = (ARKitFaceSubsystem)faceManager.subsystem;
        }
        face.updated += ArFace_Updated;


    }

    private void ArFace_Updated(ARFaceUpdatedEventArgs obj) {
        cacheBlendShape = new Dictionary<ARKitBlendShapeLocation, float>();
        using (var blendShapes = faceSubsystem.GetBlendShapeCoefficients(face.trackableId, Allocator.Temp)) {
            foreach (var featureCoefficient in blendShapes) {
                cacheBlendShape[featureCoefficient.blendShapeLocation] = featureCoefficient.coefficient;
            }
        test_coef = cacheBlendShape[blendShapeToTrack];
            }
    }

    void OnDisable() {
        face.updated -= ArFace_Updated;
    }
    
    private void Update() 
    {
        
        if (test_coef>0) {
            verticalDirection = 1;
        }
        
        verticalDirection = Mathf.Clamp(verticalDirection, 0, 1);

        sprintValue = Input.GetAxis("Sprint");

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

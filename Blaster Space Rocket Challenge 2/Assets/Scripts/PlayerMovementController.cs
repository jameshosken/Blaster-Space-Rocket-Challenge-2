﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {


    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField]float speed = 1f;

    [Header("Weapons")][SerializeField] float firingRate = 10f;
    [SerializeField] GameObject[] cannons;

    //FollowTargetPosition followTarget;
    //AlignToTrajectory alignTrajectory;                  //Rename to rotationScript

    //[SerializeField] Transform target;
    [Header("Screen Position")]
    [SerializeField] float xClamp = 1.6f;
    [SerializeField] float yClamp = 0.8f;

    [Header("Roll/Pitch/Yaw")]
    [SerializeField] float turnLag = 0.5f;
    [SerializeField] float positionPitchFactor = -10f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = 10f;
    [SerializeField] float controlYawFactor = 10f;

    [SerializeField] float controlRollFactor = -20f;

    [Header("Death Sequence")]
    Vector3 lostControlRotationVector = new Vector3(0, 0, 1);

    [Header("Sounds")] AudioSource laser;
    
    //[SerializeField] float positionPitchFactor = 1f;

    bool isControlEnabled = true;

    private void Start()
    {
        laser = GetComponent<AudioSource>();
        //followTarget = GetComponent<FollowTargetPosition>();
        //alignTrajectory = GetComponent<AlignToTrajectory>();
    }

    void Update () {

        float[] throwXY = GetInputThrow();
        float[] offset  = GetOffsetXY(throwXY[0], throwXY[1]);
        float[] clamped = GetClampedXY(offset[0], offset[1]);

        if (isControlEnabled)
        {
            ProcessPosition(clamped[0], clamped[1]);
            ProcessRotation(clamped, throwXY);
            ProcessFiring();
        }
        else
        {
            ApplyLostControlRotation();
        }
	}

    /// <summary>
    /// TRANSLATION
    /// </summary>
    
    float[] GetInputThrow()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float[] throwXY = new float[2];
        throwXY[0] = xThrow;
        throwXY[1] = yThrow;
        return throwXY;
    }

    float[] GetOffsetXY(float xThrow, float yThrow) { 

        float xOffset = xThrow * speed * Time.deltaTime;
        
        float yOffset = yThrow * speed * Time.deltaTime;

        float[] offset = new float[2];
        offset[0] = xOffset;
        offset[1] = yOffset;
        return offset;
    }

    float [] GetClampedXY(float xoff, float yoff)
    {
        float rawNewXPos = transform.localPosition.x + xoff;
        //float rawNewXPos = target.localPosition.x + xoff;
        float clampedXOffset = Mathf.Clamp(rawNewXPos, -xClamp, xClamp);

        float rawNewYPos = transform.localPosition.y + yoff;
        //float rawNewYPos = target.localPosition.y + yoff;
        float clampedYOffset = Mathf.Clamp(rawNewYPos, -yClamp, yClamp);

        float[] clamped = new float[2];
        clamped[0] = clampedXOffset;
        clamped[1] = clampedYOffset;
        return  clamped;
    }

    void ProcessPosition(float x, float y)
    {
        transform.localPosition = new Vector3(x, y, 0);
        //target.localPosition = new Vector3(x, y, 0);
        //followTarget.ApplyFollowTarget(target);

    }

    /// <summary>
    /// FIRING
    /// </summary>

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            ActivateCannons();

        }
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            if (!laser.isPlaying)
            {
                laser.Play();
            }

        }

        
        else if (CrossPlatformInputManager.GetButtonUp("Fire"))
        {
            DeactivateCannons();
            laser.Stop();
        }

    }

    void ActivateCannons()
    {
        foreach (GameObject cannon in cannons)
        {
            var emission = cannon.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = firingRate;
            //cannon.SetActive(true);
        }
    }

    void DeactivateCannons()
    {
        foreach (GameObject cannon in cannons)
        {
            var emission = cannon.GetComponent<ParticleSystem>().emission;
            emission.rateOverTime = 0f;
            //cannon.SetActive(false);
        }
    }



    /// <summary>
    /// ROTATION
    /// </summary>

    void ProcessRotation(float[] clamped, float[] throwXY)
    {
        //Vector3 orientation = alignTrajectory.GetOrientationFromTrajectory();
        //transform.LookAt(target);

        ApplyTurnRotationFromLocalMovement(clamped, throwXY);

    }

   
    void ApplyTurnRotationFromLocalMovement(float[] localXY, float[] throwXY)
    {
        float pitchDueToPosition = localXY[1] * positionPitchFactor;
        float pitchDueToControlThrow = throwXY[1] * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = localXY[0] * positionYawFactor;
        float yawDueToControlThrow = throwXY[0] * controlYawFactor;
        float yaw = yawDueToPosition + yawDueToControlThrow;

        float rollDueToControlThrow = throwXY[0] * controlRollFactor;
        float roll = rollDueToControlThrow;




        Vector3 turnAngles = new Vector3(pitch, yaw, roll);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(turnAngles), turnLag*Time.deltaTime) ;

    }

    void ApplyLostControlRotation()
    {
        transform.Rotate(lostControlRotationVector*Time.deltaTime);
    }

    /// <summary>
    /// MESSAGES
    /// </summary>

    void OnPlayerDeath()    //Called by String
    {
        isControlEnabled = false;

        //Player no longer follows target. Set parent
        //gameObject.transform.parent = target;


        // todo Make lostCOntrolRotation not magic numbers
        lostControlRotationVector.x = Random.Range(-200, 200);
        lostControlRotationVector.y = Random.Range(-400, 400);
        lostControlRotationVector.z = Random.Range(-600, 600);
    }

}



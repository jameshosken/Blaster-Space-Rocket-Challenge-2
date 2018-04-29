using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    [Tooltip("In ms^-1")][SerializeField]float speed = 1f;

    FollowTargetPosition followTarget;
    AlignToTrajectory alignTrajectory;                  //Rename to rotationScript

    [SerializeField] Transform target;

    [SerializeField] float xClamp = 5f;
    [SerializeField] float yClamp = 5f;

    [SerializeField] float positionPitchFactor = 20f;
    [SerializeField] float controlPitchFactor = -20f;

    [SerializeField] float positionYawFactor = 20f;
    [SerializeField] float controlYawFactor = -20f;
    //[SerializeField] float positionPitchFactor = 1f;



    private void Start()
    {
        followTarget = GetComponent<FollowTargetPosition>();
        alignTrajectory = GetComponent<AlignToTrajectory>();
    }

    void Update () {

        float[] throwXY = GetInputThrow();
        float[] offset  = GetOffsetXY(throwXY[0], throwXY[1]);
        float[] clamped = GetClampedXY(offset[0], offset[1]);

        ProcessPosition(clamped[0], clamped[1]);
        
        ProcessRotation(clamped, throwXY);

	}

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
        
        float rawNewXPos = target.localPosition.x + xoff;
        float clampedXOffset = Mathf.Clamp(rawNewXPos, -xClamp, xClamp);

        
        float rawNewYPos = target.localPosition.y + yoff;
        float clampedYOffset = Mathf.Clamp(rawNewYPos, -yClamp, yClamp);

        float[] clamped = new float[2];
        clamped[0] = clampedXOffset;
        clamped[1] = clampedYOffset;
        return  clamped;
    }

    void ProcessPosition(float x, float y)
    {

        target.localPosition = new Vector3(x, y, 0);
        followTarget.ApplyFollowTarget(target);

    }

    /// <summary>
    /// ROTATION
    /// </summary>
    
    void ProcessRotation(float[] clamped, float[] throwXY)
    {
        Vector3 orientation = alignTrajectory.GetOrientationFromTrajectory();
        transform.LookAt(target);

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

        Vector3 turnAngles = new Vector3(pitch, yaw, 0);

        transform.Rotate(turnAngles, Space.Self);

    }


}



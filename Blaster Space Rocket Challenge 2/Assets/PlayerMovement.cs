using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    [Tooltip("In ms^-1")][SerializeField]float speed = 1f;

    FollowTargetPosition followTarget;

    [SerializeField] Transform target;
    [SerializeField] float xClamp = 5f;
    [SerializeField] float yClamp = 5f;


    private void Start()
    {
        followTarget = GetComponent<FollowTargetPosition>();
    }

    void Update () {

        //don't overwrite original target
        //Transform newTarget = target;

        float horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = horizontalThrow * speed * Time.deltaTime;
        float rawNewXPos = target.localPosition.x + xOffset;
        float clampedXOffset = Mathf.Clamp(rawNewXPos, -xClamp, xClamp);

        float verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = verticalThrow * speed * Time.deltaTime;
        float rawNewYPos = target.localPosition.y + yOffset;
        float clampedYOffset = Mathf.Clamp(rawNewYPos, -yClamp, yClamp);
        

        target.localPosition = new Vector3(clampedXOffset, clampedYOffset, 0);


        //newTarget.Translate(translateVector, Space.Self);

        followTarget.ApplyFollowTarget(target);

	}
}

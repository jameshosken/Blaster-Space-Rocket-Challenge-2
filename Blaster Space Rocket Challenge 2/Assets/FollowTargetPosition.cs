using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetPosition : MonoBehaviour {


    [SerializeField] float lag = 0.5f;


    public void ApplyFollowTarget(Transform target)
    {
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, lag);
    }

}

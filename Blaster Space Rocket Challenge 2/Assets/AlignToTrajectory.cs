using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToTrajectory : MonoBehaviour
{

    [SerializeField] float trajectoryLag = 0.1f;
    [SerializeField] float turnLag = 0.5f;

    Vector3 previousPosition = new Vector3(0, 0, 0);


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public Vector3 GetOrientationFromTrajectory()
    {
        Vector3 orientation = this.transform.position - previousPosition;
        previousPosition = this.transform.position;
        return orientation;
    }

}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToTrajectory : MonoBehaviour {

    [SerializeField] float lag = 0.5f;
    Vector3 previousPosition = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 orientation = this.transform.position - previousPosition;

        

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(orientation), lag * Time.deltaTime);

        //Vector3 goal = this.transform.position + orientation;

        //this.transform.rotation = Quaternion.Slerp(this.transform.rotation, goal.rotation, lag);

        previousPosition = this.transform.position;
	}
}

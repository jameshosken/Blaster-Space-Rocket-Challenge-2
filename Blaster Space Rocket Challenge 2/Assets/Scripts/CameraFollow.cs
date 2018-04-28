using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float posfactor = 0.5f;
    [SerializeField] float rotfactor = 0.5f;

    // Use this for initialization
    void Start () {
		
	}

     void FixedUpdate()
    {
        
    }
    
    // Update is called once per frame
    void Update () {
        this.transform.position = Vector3.Lerp(this.transform.position, target.position, posfactor);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, target.rotation, rotfactor);
    }
}

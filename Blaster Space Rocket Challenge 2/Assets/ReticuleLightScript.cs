using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticuleLightScript : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float lag = 0.5f;


    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        transform.position = Vector3.Lerp(transform.position, target.position, lag);
        
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zDistance);
        //transform.localPosition.Set(transform.localPosition.x, transform.localPosition.y, zDistance);
	}
}

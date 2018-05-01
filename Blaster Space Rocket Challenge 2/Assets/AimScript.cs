using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour {

    LineRenderer line;
    [SerializeField] Transform target;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void LateUpdate () {

        

        line.SetPositions(new Vector3[] { transform.position,  target.position});
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour {


    [SerializeField] Vector3 axis;
    [SerializeField] bool randomStart = true;


    private void Start()
    {
        if (randomStart)
        {
            transform.Rotate( axis * Random.Range(0, 2* Mathf.PI),Space.Self);
        }
    }
    void Update () {
        transform.Rotate(axis * Time.deltaTime, Space.Self);
	}
}

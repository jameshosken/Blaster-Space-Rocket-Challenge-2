using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {

    float xOffset;
    float yOffset;
    //[SerializeField] float multiplier =  Mathf.PI;
    [SerializeField] float[] xRange;
    [SerializeField] float[] yRange;

    [SerializeField] bool lookAtPlayer = false;

    [SerializeField] float speed = 0.01f;

    Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        xOffset = Random.Range(0, 10);
        yOffset = Random.Range(0, 10);
    }
	
	// Update is called once per frame
	void Update () {

        
        if (lookAtPlayer)
        {
            transform.LookAt(target);
            transform.Rotate(Vector3.right*90, Space.Self);
        }
        else
        {
            float x = Mathf.PerlinNoise(xOffset, 0);
            float y = Mathf.PerlinNoise(0, yOffset);

            x = map(x, 0, 1, xRange[0], xRange[1]);

            y = map(y, 0, 1, yRange[0], yRange[1]);

            xOffset += speed;
            yOffset += speed;

            transform.localRotation = Quaternion.Euler(y, x, 0);
        }
        
	}

    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}

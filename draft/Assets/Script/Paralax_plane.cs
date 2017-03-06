using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax_plane : MonoBehaviour {
    public float speed = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate((Vector3.forward*speed) * Time.deltaTime);
    }
}

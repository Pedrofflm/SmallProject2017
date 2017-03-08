using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour {
    public GameObject arrow, arrowClone;
    public const float arrowVelocity=220;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void fireArrow(float force = arrowVelocity) {
        print("arrow force: " + force);
        arrowClone = Instantiate(arrow, transform.position, transform.rotation) as GameObject;
        arrowClone.GetComponent<Rigidbody>().AddForce( transform.right * force);
    }

}

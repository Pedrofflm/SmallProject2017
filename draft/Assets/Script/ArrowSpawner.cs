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
    public void fireArrow(int power = -1) {
        float force = (power == -1)? arrowVelocity:(power * 150);
        
        print("arrow force: " + force);
        arrowClone = Instantiate(arrow, transform.position, transform.rotation) as GameObject;
        arrowClone.tag = "Arrow";
        arrowClone.GetComponent<Arrow_Power>().power = (power == -1) ? 0:power;
        arrowClone.GetComponent<Rigidbody>().AddForce( transform.right * force);
       //Debug.Break();
    }

}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed = 1.0f;
    public Vector3 target;
    GameObject st;
    // Use this for initialization
    void Start () {
       st= GameObject.FindGameObjectsWithTag("MainCamera")[0];
        
    }
	
	// Update is called once per frame
	void Update () {
        target = st.transform.position;
        

        Vector3 dir = target - transform.position;
        dir.y = 0; // keep the direction strictly horizontal
        Quaternion rot = Quaternion.LookRotation(-dir);
        // slerp to the desired rotation over time
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
    }
}

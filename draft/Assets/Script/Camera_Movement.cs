using UnityEngine;
using System.Collections;

public class Camera_Movement : MonoBehaviour {

    public Vector3 center;
    
	// Use this for initialization
	void Start () {
        GameObject st = GameObject.FindGameObjectsWithTag("Stage")[0];
        center = st.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(center);
    }
}

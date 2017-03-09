using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestruct : MonoBehaviour {
    public float longevity=5.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        longevity -= Time.deltaTime;
        if (longevity < 0)
        {
            DestroyObject(gameObject);
        }
        
    }

}

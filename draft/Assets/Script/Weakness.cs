using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakness : MonoBehaviour {
    public int weakness = 1;
    public int weaknessType = 0;
    Enemy_State es;
	// Use this for initialization
	void Start () {
        es=GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Enemy_State>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow") {
            print("newHP!!! -> " + es.getHP());
            print("power collision :"+other.GetComponent<Arrow_Power>().power);
            if (other.GetComponent<Arrow_Power>().power == weaknessType) { 
                es.decHP(weakness);

            }
            else { print("not enough altitude (power)"); }
            Destroy(other.gameObject);
        }
    }
}

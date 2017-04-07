using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    public int weakness = 1;
   
   // ParticleSystem particle;
    Player_State ps;
    // Use this for initialization
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("GameEngine").GetComponent<Player_State>();
        //particle = GetComponentInChildren<ParticleSystem>();
        //particle.Stop(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Got Hit by The MONSTAHHH");
          
            if (other.GetComponent<Arrow_Power>().power == weaknessType)
            {
                ps.decHP(weakness);
                print("HIIITTT!!!! - " + ps.getHP());
               // particle.Stop(false);
               // particle.Play(false);
            }
            else { }
           // Destroy(other.gameObject);
        }
    }
}

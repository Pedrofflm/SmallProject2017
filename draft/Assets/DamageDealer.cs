using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour {
    public int damage = 1;
   
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
            if (ps.getState() != 0) { 
                print("Got Hit by The MONSTAHHH");
          
            
                ps.decHP(damage);
                ps.setState(0);

                print("HIIITTT!!!! - " + ps.getHP());
                // particle.Stop(false);
                // particle.Play(false);
            }


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour {
    public int HP = 10;
    public int currentState = 1;
    public float hurtTime = 3.0f;
    private int playerStates = 4;
    private Camera_Movement camState;
    private int version = 0;
    private float timer=0.0f;

    /*  
        1-idle
        2-moving left
        3-moving right
        0-damaged
    */


    void Start()
    {
        version = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<BuildVersion>().getVersion();
        camState = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera_Movement>();
     //   print("Player_State versions found: " + version);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == 0) {
            timer -= Time.deltaTime;
            print(timer);
            if (timer < 0) { currentState = 1; }
        }
       /* switch (currentState)
        { }*/
            /* switch (currentState)
             {
                 case 0:
                     if (HP < 6) currentState = 1;
                     if (HP < 3) currentState = 2;
                     break;
                 case 1:
                     if (HP > 5) currentState = 0;
                     if (HP < 3) currentState = 2;
                     break;
                 case 2:

                     if (HP > 2) currentState = 1;
                     if (HP > 5) currentState = 0;
                     break;

             }*/
        }

    public void incHP() { HP++; }// incrementHP
    public void decHP() { HP--; }// incrementHP
    public void decHP(int x) { HP -= x; }
    public int getHP() { return HP; }
    public int getState() { return currentState; }
    public void setState(int st) {
        print("player state is set to "+st);
        currentState = st;
        if (currentState == 0) {
            timer = hurtTime;
            print(0);
            camState.damaged();
        }
    }
    public float getHurtTime() { return hurtTime; }
}

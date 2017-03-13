using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_State : MonoBehaviour {

    public int HP = 10;
    private int enemeyStates = 3;
    public int currentState=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState) {
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

        }
	}

    public void incHP() { HP++; }// incrementHP
    public void decHP() { HP--; }// incrementHP
    public void decHP(int x) { HP-=x; }
    public int getHP() { return HP; }
}

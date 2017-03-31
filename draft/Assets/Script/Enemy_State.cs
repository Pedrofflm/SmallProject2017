using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_State : MonoBehaviour {

    public int HP = 10;
    private int enemeyStates = 3;
    public int currentState=0;
    public GameObject text;
    public float restartTimer = 5;
    private bool gameover = false;
	// Use this for initialization
	void Start () {
        gameover = false;
        text.GetComponent<GUIText>().text="";
    }

    // Update is called once per frame
    void Update() {

        if (gameover) {
            text.GetComponent<GUIText>().text=" Game Over!/ n Game Will Restart in 5 seconds.";
            restartTimer -= Time.deltaTime;
            if (restartTimer < 0) {
                string s=SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(s); }
        }else{
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
        if (HP <= 0)
        {

            gameover = true;
           

        }
    }
	}

    public void incHP() { HP++; }// incrementHP
    public void decHP() { HP--; }// incrementHP
    public void decHP(int x) { HP-=x; }
    public int getHP() { return HP; }
}

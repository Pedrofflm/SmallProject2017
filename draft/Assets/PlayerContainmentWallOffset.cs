using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContainmentWallOffset : MonoBehaviour {

    public int side = 0;//0-left, 1-right
    private Player_State player;
    private GameObject mainCamera;

    void Start(){
            player = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
            mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    void OnTriggerEnter(Collider other) {
       // print("TRIGGERED WALL " + side);
        if (other.gameObject.tag == "Player")
        {
          //  print("triggered by PLAYER " + side);
            int st=player.getState();
            switch (side)
            {
                case 0: if (st == 2) mainCamera.GetComponent<Camera_Movement>().triggeredLeftWall();
                    break;
                case 1: if (st == 3) mainCamera.GetComponent<Camera_Movement>().triggeredRightWall();
                    break;
            }
        }

    }

}

using UnityEngine;
using System.Collections;

public class Camera_Movement : MonoBehaviour {

    public Vector3 center;
    public GameObject player, leftPosition, rightPosition;
    public Player_State playerState;
    public float offset = 0.0f;
    // private Vector3 player_offset; //distance from player
    // private Vector3 scene_offset; //distance from player
    private float distPlayerCenter, rotOffset;
    private int state = 0;
    /*
        0-idle
        1-moving right
        2-moving left
        3-accompany
        */

    // Use this for initialization
    void Start() {
        float aux = Vector3.Distance(leftPosition.transform.position, rightPosition.transform.position)/2;
        float temp = Vector3.Distance(leftPosition.transform.position, center);
        rotOffset = Mathf.Rad2Deg*Mathf.Asin(aux / temp)*2;
        print("angle difference: " + rotOffset);
        GameObject st = GameObject.FindGameObjectsWithTag("Stage")[0];
        if (center == null) center = st.transform.position;
        playerState= GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
        // player_offset = transform.position - player.transform.position;
        //scene_offset = transform.position - center;
        //distPlayerCenter = Vector3.Distance(player.transform.position, center);

    }

    // Update is called once per frame
    void Update() {

        // transform.RotateAround(center, Vector3.up, player.transform.position);
        transform.LookAt(center);
        
       // player=  GameObject.FindGameObjectsWithTag("Player")[0];
        switch (state)
        {
            case 0:
                //camera is idle, does not move
                break;
            case 1:
                // = Mathf.Abs(Mathf.DeltaAngle(player.transform.rotation.eulerAngles.y, leftPosition.transform.rotation.eulerAngles.y));
                
                // print("rotating player to left|| player rotation: " + player.transform.rotation.eulerAngles.y + "|| place rotation :" + leftPosition.transform.rotation.eulerAngles.y);
               // print("difference angle: " + (rotOffset));
                transform.RotateAround(center, Vector3.up, -rotOffset);
                if (playerState.getState() == 3) state = 0;
                //camera moves so that player is at left
                break;
            case 2:
               // rotOffset = Mathf.Abs(Mathf.DeltaAngle(player.transform.rotation.eulerAngles.y, rightPosition.transform.rotation.eulerAngles.y));
              //  print("rotating player to left|| player rotation: " + player.transform.rotation.eulerAngles.y + "|| place rotation :" + leftPosition.transform.rotation.eulerAngles.y);
              //  print("difference angle: " + (rotOffset));
                transform.RotateAround(center, Vector3.up, rotOffset);
                print("rotating player to right");
                if (playerState.getState() == 2) state = 0;
                //camera moves so that player is at right
                break;
            case 3:
                //camera moves ahead and accompanies player movement
                break;
            case 4:
                break;
        }


    }

    public void triggeredRightWall() { print("right triggered"); if (playerState.getState() == 3) print("moved to left"); state = 1; }
    public void triggeredLeftWall() { print("left triggered"); if (playerState.getState() == 2) print("moved to right"); state = 2; }

}

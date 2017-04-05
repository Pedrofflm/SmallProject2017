using UnityEngine;
using System.Collections;

public class Camera_Movement : MonoBehaviour {

    public Vector3 center;
    public GameObject player, leftPosition, rightPosition;
    public Player_State playerState;
    public Player_Movement playerMov;
    public float offset = 0.0f, cameraAccelaration = 1.0f, precision = 0.5f;
    // private Vector3 player_offset; //distance from player
    // private Vector3 scene_offset; //distance from player
    private float distPlayerCenter, rotOffset, playerOffset;
    private int state = 0,previousState=0;
    private bool adjustingFlag = false;
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
        playerState = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
        playerMov = player.GetComponent<Player_Movement>();
        // player_offset = transform.position - player.transform.position;
        //scene_offset = transform.position - center;
        //distPlayerCenter = Vector3.Distance(player.transform.position, center);

    }

    // Update is called once per frame
    void Update() {

        // transform.RotateAround(center, Vector3.up, player.transform.position);
       
        
       // player=  GameObject.FindGameObjectsWithTag("Player")[0];
        switch (state)
        {
            case 0:
                adjustingFlag = false;
                //camera is idle, does not move
                break;
            case 1:
                // = Mathf.Abs(Mathf.DeltaAngle(player.transform.rotation.eulerAngles.y, leftPosition.transform.rotation.eulerAngles.y));
                
                // print("rotating player to left|| player rotation: " + player.transform.rotation.eulerAngles.y + "|| place rotation :" + leftPosition.transform.rotation.eulerAngles.y);
               // print("difference angle: " + (rotOffset));
               // transform.RotateAround(center, Vector3.up, -rotOffset);
                if (playerState.getState() == 3) { adjustingFlag = true; print("Camera STATE 3"); previousState = state; state = 3; }//go to 3
                //camera moves so that player is at left
                break;
            case 2:
               // rotOffset = Mathf.Abs(Mathf.DeltaAngle(player.transform.rotation.eulerAngles.y, rightPosition.transform.rotation.eulerAngles.y));
              //  print("rotating player to left|| player rotation: " + player.transform.rotation.eulerAngles.y + "|| place rotation :" + leftPosition.transform.rotation.eulerAngles.y);
              //  print("difference angle: " + (rotOffset));

               // transform.RotateAround(center, Vector3.up, rotOffset);
                print("rotating player to right");
                if (playerState.getState() == 2) { adjustingFlag=true; print("Camera STATE 3"); previousState = state; state = 3; }//go to 3
                //camera moves so that player is at right
                break;
            case 3:
                float translation = Input.GetAxis("Horizontal") * playerMov.getCurrentSpeed();
                if (adjustingFlag) { 
                    switch (previousState)
                    {
                        case 1:
                            playerOffset = Vector3.Distance(player.transform.position, leftPosition.transform.position);
                            if (playerOffset < precision)
                            {
                                adjustingFlag = false;
                            }
                            else {
                                translation -= cameraAccelaration;
                            }
                            break;
                        case 2:
                            playerOffset = Vector3.Distance(player.transform.position, rightPosition.transform.position);
                            if (playerOffset < precision)
                            {
                                adjustingFlag = false;
                            }
                            else {
                                translation += cameraAccelaration;
                            }
                            break;
                    }
                }
                
                transform.RotateAround(center, Vector3.up, translation * Time.deltaTime);
                print("STATE 3 ROTATION-previous state: "+previousState);
                
                if (previousState == 2) { if (playerState.getState() != 2) { print("idle"); previousState = state; state = 0; } }
                if (previousState == 1) { if (playerState.getState() != 3) { previousState = state; state = 0;} }
                //camera moves ahead and accompanies player movement
                break;
            case 4:
                break;
        }
        transform.LookAt(center);

    }

    public void triggeredRightWall() { print("right triggered"); if (playerState.getState() == 3) print("moved to left"); previousState = state; state = 1; }
    public void triggeredLeftWall() { print("left triggered"); if (playerState.getState() == 2) print("moved to right"); previousState = state; state = 2; }
    public int getState() { return state; }
}

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class Camera_Movement : MonoBehaviour {

    public Vector3 center;
    public GameObject player, leftPosition, rightPosition, centerPosition;
    public Player_State playerState;
    public Player_Movement playerMov;
    public float offset = 0.0f, cameraAccelaration = 1.0f, precision = 0.5f;
    // private Vector3 player_offset; //distance from player
    // private Vector3 scene_offset; //distance from player
    private float distPlayerCenter, rotOffset, playerOffset;
    private int state = 0,previousState=0,version;
    private bool adjustingFlag = false, change=false;
    private int direction = 0;
    private float angle = 0;
    //private Vector3 centerPosition;
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
       // centerPosition = player.transform;
        rotOffset = Mathf.Rad2Deg*Mathf.Asin(aux / temp)*2;
        print("angle difference: " + rotOffset);
        GameObject st = GameObject.FindGameObjectsWithTag("Stage")[0];
        if (center == null) center = st.transform.position;
        playerState = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
        playerMov = player.GetComponent<Player_Movement>();
        version = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<BuildVersion>().getVersion();
        // player_offset = transform.position - player.transform.position;
        //scene_offset = transform.position - center;
        //distPlayerCenter = Vector3.Distance(player.transform.position, center);

    }

    // Update is called once per frame
    void Update() {
        playerState = GameObject.FindGameObjectsWithTag("GameEngine")[0].GetComponent<Player_State>();
        if (playerState.getState() > 0 && state == 4) { state = 1;print("1"); } 
        // transform.RotateAround(center, Vector3.up, player.transform.position);


        // player=  GameObject.FindGameObjectsWithTag("Player")[0];
        switch (state)
        {
            case 0:
                adjustingFlag = false;
                //camera is idle, does not move
                break;
            case 1:
              // print("difference angle: " + (rotOffset));
                if (playerState.getState() == 3) { adjustingFlag = true; print("Camera STATE 3"); previousState = state; state = 3; }//go to 3
                //camera moves so that player is at left
                break;
            case 2:
               
               // print("rotating player to right");
                if (playerState.getState() == 2) { adjustingFlag=true; print("Camera STATE 3"); previousState = state; state = 3; }//go to 3
                //camera moves so that player is at right
                break;
            case 3:
                float translation;// = Input.GetAxis("Horizontal") * playerMov.getCurrentSpeed();
                if (version > 2) {
                    translation =-(CrossPlatformInputManager.GetAxis("Horizontal") * playerMov.getCurrentSpeed()); } else
                {
                    translation = Input.GetAxis("Horizontal") * playerMov.getCurrentSpeed();
                }
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
                //print("STATE 3 ROTATION-speed: "+translation);
                
                if (previousState == 2) { if (playerState.getState() != 2) { print("idle"); previousState = state; state = 0; } }
                if (previousState == 1) { if (playerState.getState() != 3) { previousState = state; state = 0;} }
                //camera moves ahead and accompanies player movement
                break;
            case 4:
                float translation1 = 0;// = Input.GetAxis("Horizontal") * playerMov.getCurrentSpeed();

                 
                
                if (adjustingFlag)
                {
                    change = true;
                    direction = 2;
                    /*float cp_angle = player.transform.eulerAngles.y;
                    float cc_angle = this.transform.eulerAngles.y;
                    angle = Mathf.DeltaAngle(cc_angle, cp_angle);*/
                   
                    adjustingFlag = false;
                    //print("Adjust");

                    Vector3 centerPlayer = player.transform.position - center;
                   
                    Vector3 centerCamera = centerPosition.transform.position - center;
                    //print("Cross : " + Vector3.Cross(centerCamera, centerPlayer).y);
                    if (Vector3.Cross(centerCamera, centerPlayer).y < 0)
                    {
                        centerCamera.z = 0;
                        centerPlayer.z = 0;
                        angle = -Vector3.Angle(centerCamera, centerPlayer);
                    }
                    else {
                        angle = -angle;
                        centerCamera.z = 0;
                        centerPlayer.z = 0;
                        angle = Vector3.Angle(centerCamera, centerPlayer);
                    }
                    //if (angle < 0) direction = 1;
                   // print("Cross : " + Vector3.Cross(centerCamera, centerPlayer).y);
                   // if (Vector3.Cross(centerCamera, centerPlayer).y < 0) angle = -angle;
                    //print("ANGLE "+angle);
                }


                /* if (adjustingFlag)
                 {*/
                //switch (direction)
                //{
                //    case 1:
                //        /*playerOffset = Vector3.Distance(player.transform.position, centerPosition.transform.position);
                //        if (playerOffset < precision)
                //        {
                //           // adjustingFlag = false;
                //            print("precision");
                //        }
                //        else {*/
                //            translation1 = angle;
                //        //}
                //        break;
                //    case 2:
                //        /*playerOffset = Vector3.Distance(player.transform.position, centerPosition.transform.position);
                //        if (playerOffset < precision)
                //        {
                //           // adjustingFlag = false; print("precision");
                //        }
                //        else {*/
                //            translation1 = angle;
                //        //}
                //        break;
                //}

                if (change) { 
                    transform.RotateAround(center, Vector3.up, (angle/(2*playerState.getHurtTime()))* Time.deltaTime);
                    //print("STATE 4 ROTATION-previous state: " + previousState);
                    print("REPOSITIONING CAMERA: " + (angle));
                }
                // }
                //camera moves ahead and accompanies player movement
                break;
        }
        /*if (playerState.getState() == 0)
        {

            float translation = cameraAccelaration * 3;
            // Transform aux= new Transform(leftPosition.transform);
            playerOffset = Vector3.Distance(player.transform.position, centerPosition.transform.position);
            if (playerOffset < precision * 2)
            {
                adjustingFlag = false;
            }
            else {
                //translation -= cameraAccelaration*3;
            }
            translation -= cameraAccelaration * 3;
            float cp_angle = player.transform.eulerAngles.y;
            float cc_angle = this.transform.eulerAngles.y;

            float angle = Mathf.DeltaAngle(cc_angle, cp_angle);
            if (angle < 0) {
                translation -= cameraAccelaration;
            }
            else {
                translation += cameraAccelaration;
            }
            transform.RotateAround(center, Vector3.up, translation * Time.deltaTime);
            print("REPOSITIONING CAMERA: " + (translation * Time.deltaTime));



           /* print("recente");
            Vector3 centerPlayer = player.transform.position - center;
            float cp_angle = player.transform.eulerAngles.y;
            float cc_angle = this.transform.eulerAngles.y;
            Vector3 centerCamera = this.transform.position-center;
            float angle = Mathf.DeltaAngle(cc_angle, cp_angle);//Vector3.Angle(centerPlayer, centerPlayer);*/
           // if (Mathf.DeltaAngle(cc_angle, cp_angle) < 0) angle = -angle;
            //if (Vector3.Cross(centerCamera, centerPlayer).y < 0) angle = -angle;
            //transform.RotateAround(center, Vector3.up, angle);
           // print("REPOSITIONING CAMERA: "+angle);
        /*}*/
        transform.LookAt(center);

    }

    public void triggeredRightWall() { print("right triggered"); if (playerState.getState() == 3) print("moved to left"); previousState = state; state = 1; }
    public void triggeredLeftWall() { print("left triggered"); if (playerState.getState() == 2) print("moved to right"); previousState = state; state = 2; }
    public void damaged() { state = 4;/*adjustingFlag= true; */}
    public void adjustFlag() { adjustingFlag = !adjustingFlag; }
    public int getState() { return state; }
}

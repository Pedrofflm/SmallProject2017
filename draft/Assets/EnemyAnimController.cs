using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour {

    public float p_speed = 5.0f;
    public float e_speed = 1.0f;
    GameObject player,d_enemy;
    public float e_angle,p_angle;
    Vector3 newRotation = new Vector3(0,0,0);

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        d_enemy = GameObject.Find("Dummy_anim");
        //newRotation = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y / 2, player.transform.eulerAngles.z);
        //d_enemy.transform.eulerAngles = newRotation;

    }

    // Update is called once per frame
    void Update () {
       
       //Angle in which the plane is 
        p_angle = player.transform.eulerAngles.y;
        e_angle = d_enemy.transform.eulerAngles.y;
        //print(e_angle);
        //Dummy
        // newRotation.z = player.transform.rotation.y;
        // d_enemy.transform.Rotate(newRotation);
        //d_enemy.transform.rotation = 
        //d_enemy.transform.Rotate(Vector3.up*rotAngle);

        //newRotation = new Vector3(player.transform.eulerAngles.x, player.transform.eulerAngles.y / 2, player.transform.eulerAngles.z);
        //d_enemy.transform.eulerAngles = newRotation;
        
        if (e_angle != p_angle)
        {
           // print("Looking for player");
            if (e_angle < p_angle)
            {
                d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y - 0.3f, player.transform.eulerAngles.z);
              //  print("-decrease");
            }
            if(e_angle > p_angle)
             {
                d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y +0.3f, player.transform.eulerAngles.z);
                print("+increase");
            }
            // d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y +0.3f , player.transform.eulerAngles.z);
            //print("DENTRO IF");
        }
        else
        {
            print("Player Found");
        }
        print("Player angle:"+p_angle);
        print("Enemy angle:"+ e_angle);


    }

    void FixedUpdate()
    {

    }
}

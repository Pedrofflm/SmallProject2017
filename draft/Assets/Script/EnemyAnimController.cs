using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    public Vector3 center;
    public float p_speed = 5.0f;
    public float e_speed = 0.1f;
    public float random_num, e_att_speed, dyn_att_speed,sp_att1, sp_att2;
    GameObject player, d_enemy, d_attack,enemy_ai;
    public float e_angle, p_angle;
    public int attackstate = 2;
    Vector3 newRotation = new Vector3(0, 0, 0);
    float init_pos_at;
    public float cooldownDef, cooldownCur;
    public int attperformance = 0;
    bool at_dir = false;
    int dir_change = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        d_enemy = GameObject.FindGameObjectWithTag("enemy_ai");
        d_attack = GameObject.FindGameObjectWithTag("dummy_attack");
       // enemy_ai = GameObject.FindGameObjectWithTag("enemy_ai");
        cooldownCur = 3;
        cooldownDef = 0;
        init_pos_at = d_attack.transform.position.y;
        center = new Vector3(0, init_pos_at, 0);

    }

    // Update is called once per frame
    void Update()
    {
        //TImer for attack cooldown
        if (cooldownCur > 1) { cooldownCur = cooldownCur - Time.deltaTime; }

        //Triggering attacks
        random_num = Random.value;

        //Is the enemy attacking?
        switch (attackstate)
        {
            case 1:
             //   if (cooldownCur > 1 && Mathf.DeltaAngle(p_angle, e_angle) >= -5 && Mathf.DeltaAngle(p_angle, e_angle) <= 5){

                //Which attack is being performed?
                switch (attperformance)
                {
                    case 1:
                        //Static Attack
                            sp_att1 = e_att_speed + 1;
                            if (at_dir == false)
                            {
                                if (d_attack.transform.position.y > 0.7f)
                                {
                                    // Moving the club downwards
                                    d_attack.transform.Translate(0f, 0f, sp_att1 * Time.deltaTime);
                                }
                                else
                                {
                                    at_dir = true;
                                }
                            } else {

                                if (d_attack.transform.position.y < init_pos_at)
                                {
                                    //Moving the club upwards
                                    d_attack.transform.Translate(0f, 0f, -e_att_speed * Time.deltaTime);
                                }
                                else
                                {
                                    attackstate = 2;
                                    print("Back to searching");
                                    at_dir = false;
                                }
                            }

                        break;

                    case 2:
                        //Dynamic Attack
                        sp_att2 = e_att_speed;

                        float distance = Vector3.Distance(d_attack.transform.position, player.transform.position);
                        float d_angle = d_attack.transform.eulerAngles.y;
                        float angle = Mathf.DeltaAngle(p_angle, d_angle);
                        if (at_dir == false)
                        {
                           
                            //// Moving the club downwards
                            if (d_attack.transform.position.y > 0.7f)
                            {
                                //Depending on the position of player it does attack dyagonally to right
                                if (Mathf.DeltaAngle(p_angle, e_angle) > 0)
                                {

                                    if (dir_change == 2)
                                    {                                      
                                        d_attack.transform.Translate(0f, 0f, sp_att2*3 * Time.deltaTime);

                                    } else {

                                        //   print("Following Right dyn");
                                        d_attack.transform.Translate(0f, 0f, sp_att2 * Time.deltaTime);
                                        d_attack.transform.RotateAround(center, Vector3.up, -dyn_att_speed * Time.deltaTime);
                                    
                                    }

                                    dir_change = 1;
                                    if (Mathf.DeltaAngle(p_angle, d_angle)< -7)
                                    {
                                        print("Brake!!!");
                                        dir_change = 2;
                                    }

                                }
                                //Depending on the position of player it does attack dyagonally to left
                                if (Mathf.DeltaAngle(p_angle, e_angle) < 0)
                                {
                                    print("Angle between dummy and ruler"+ Mathf.DeltaAngle(d_angle, e_angle));
                                    //print("Inside If");
                                    if (dir_change == 1)
                                    {
                                        //Change direction - BRAKE
                                        d_attack.transform.Translate(0f, 0f, sp_att2*3 * Time.deltaTime);
                                    }
                                    else
                                    {
                                  //      print("Following Left dyn");
                                        d_attack.transform.Translate(0f, 0f, sp_att2 * Time.deltaTime);
                                        d_attack.transform.RotateAround(center, Vector3.up, +dyn_att_speed * Time.deltaTime);
                                    }

                                    dir_change = 2;
                                    if (Mathf.DeltaAngle(p_angle, d_angle) > 7)
                                    {
                                        dir_change = 1;
                                    }
                                    
                                }
                            }
                            else
                            {
                                at_dir = true;
                            }
                        }
                        else {
                            if (d_attack.transform.position.y < init_pos_at)
                            {
            
                                //Moving the club upwards
                                if (Mathf.DeltaAngle(p_angle, e_angle) > 0)
                                {                                        
                                    d_attack.transform.Translate(0f, 0f, -sp_att2 * Time.deltaTime);
                                }
                                if (Mathf.DeltaAngle(p_angle, e_angle) < 0)
                                {                                       
                                    d_attack.transform.Translate(0f, 0f, -sp_att2 * Time.deltaTime);                                  
                                }

                            }
                            else
                            {
                                //Rotate to original position
                                d_attack.transform.RotateAround(center, Vector3.up, Mathf.DeltaAngle(d_angle, e_angle));

                                //Go back to searching
                                attackstate = 2;
                                at_dir = false;
                                dir_change = 0;
                            }
                        }

                        break;

                    case 3:
                        print("Ain't nobody got time fo dat!!!");
                        attackstate = 2;

                        break;
                }
               // }

                break;

            //Is enemy searching?
            case 2:
                //Player and enemy angles
                p_angle = player.transform.eulerAngles.y;
                e_angle = d_enemy.transform.eulerAngles.y;
               // print("Searchingg");
                //I cooldown is over
                if (cooldownCur < 1)
                {
                    //To follow the player through the scenario
                    // Use deltaAngle to now which enemy_angle_animation use and trigger attacks
                    if (e_angle != p_angle)
                    {
                        if (Mathf.DeltaAngle(p_angle, e_angle) > 0)
                        {
                            d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y - e_speed, player.transform.eulerAngles.z);
                        }
                        if (Mathf.DeltaAngle(p_angle, e_angle) < 0)
                        {
                            d_enemy.transform.eulerAngles = new Vector3(player.transform.eulerAngles.x, d_enemy.transform.eulerAngles.y + e_speed, player.transform.eulerAngles.z);
                        }
                    }


                    //Is player found?
                    if (Mathf.DeltaAngle(p_angle, e_angle) >= -5 && Mathf.DeltaAngle(p_angle, e_angle) <= 5)
                    {
                        print("Found");
                        //It is, select the attack. Random to decide IA
                        if (random_num > 0.2)
                        {
                            //1
                            attperformance = 1;
                            if (random_num > 0.7)
                            {
                                attperformance = 2;
                                if (random_num > 0.8)
                                {
                                    //3
                                    attperformance = 3;
                                }
                            }
                        }
                        else
                        {
                            attperformance = 1;
                        }

                        //Attack selection
                        switch (attperformance)
                        {
                            case 1:
                                print("Basic front attack");
                                // basicAnimAttack(d_attack);
                                cooldownCur = 7;
                                break;
                            case 2:
                                print("Dynamic diagonal attack");
                                // d_attack.transform.Translate(0f, 0f, 5f * Time.deltaTime);
                                cooldownCur = 7;
                                break;
                            case 3:
                                print("STILL STANCE");
                                //    d_attack.transform.Translate(0f, 0f, 5f * Time.deltaTime);
                                cooldownCur = 7;
                                break;
                        }
                        attackstate = 1;
                    }
                }
                break;
        }


    }


    // print("Diference between angles is:"+Mathf.DeltaAngle(p_angle, e_angle));


    void basicAnimAttack(GameObject dummy)
    {
        float i = 0.0f;
        d_attack.transform.Translate(0f, 0f, 5f * Time.deltaTime);
        //   dummy.transform.Translate(Vector3.down * 5.0f * Time.deltaTime, Space.World);   
    }

    void dynamicAnimAttack(GameObject dummy)
    {

    }

    void FixedUpdate()
    {

    }
}

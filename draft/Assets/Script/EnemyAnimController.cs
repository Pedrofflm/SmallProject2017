using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{

    public float p_speed = 5.0f;
    public float e_speed = 0.1f;
    public float random_num;
    GameObject player, d_enemy, d_attack;
    public float e_angle, p_angle;
    public int attackstate = 2;
    Vector3 newRotation = new Vector3(0, 0, 0);

    public float cooldownDef, cooldownCur;
    public int attperformance = 0;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        d_enemy = GameObject.Find("Dummy_anim");
        d_attack = GameObject.FindGameObjectWithTag("dummy_attack");
        cooldownCur = 3;
        cooldownDef = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //TImer for attack cooldown
        if (cooldownCur > 1) { cooldownCur = cooldownCur - Time.deltaTime; }

        //Triggering attacks
        random_num = Random.value;

        //Is the enemy attacking
        switch (attackstate)
        {
            case 1:
                if (cooldownCur < 1 && Mathf.DeltaAngle(p_angle, e_angle) >= -5 && Mathf.DeltaAngle(p_angle, e_angle) <= 5)
                {

                    //Which attack is being performed?
                    switch (attperformance)
                    {
                        case 1:
                            print("Kablammmm!!!!!!!!");
                            attackstate = 2;
                            break;

                        case 2:
                            print("KASHWWWIIISSHHHHH!!!!!");
                            attackstate = 2;
                            break;

                        case 3:
                            print("Ain't nobody got time fo dat!!!");
                            attackstate = 2;
                            break;
                    }
                }

                break;

            //Is enemy searching?
            case 2:
                //Player and enemy angles
                p_angle = player.transform.eulerAngles.y;
                e_angle = d_enemy.transform.eulerAngles.y;
                print("Searchingg");
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
                            attperformance = 1;
                            if (random_num > 0.7)
                            {
                                attperformance = 2;
                                if (random_num > 0.8)
                                {
                                    attperformance = 3;
                                }
                            }
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

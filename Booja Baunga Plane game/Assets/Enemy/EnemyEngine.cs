using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEngine : MonoBehaviour
{
    public Transform player;
    public float speed;
    #region Unity Function
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    int counter;
    int Uper = 1;


    public float ObstacleTime;


    private void Update()
    {
        if (player != null)
        {

            if (transform.position.x < player.position.x - 15)
            {
                Destroy(gameObject);
            }
            else if(transform.position.x > player.position.x)
            {
                transform.LookAt(player.position);
                if (ObstacleTime >= Time.time)
                {
                    transform.position += Vector3.forward * speed;

                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed);
                    transform.position += new Vector3(0, 0.35f * Uper, 0);
                }
            }

            if (counter > 20)
            {
                Uper = -1;
            }
            else if (counter < 0)
            {
                Uper = 1;
            }
            counter += Uper;
        }
       
    }
    int amountofTuch;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                if (other.GetComponent<PlainEngine2>().ObstacleTime <= Time.time + 1)
                {
                    amountofTuch += 1;
                    other.GetComponent<PlainEngine2>().ObstacleTime = Time.time + 1f;
                }
                if(amountofTuch > 1)
                {
                    Destroy(gameObject);
                }
                break;
            case "Tree":
                if (ObstacleTime <= Time.time + 1)
                {

                    ObstacleTime = Time.time + 1f;
                }
                break;
        }
    }



    #endregion

    #region MyFunction

    #endregion
}

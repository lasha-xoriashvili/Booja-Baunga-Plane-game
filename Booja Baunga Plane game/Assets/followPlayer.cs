using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform Player;

    void Update()
    {
        if (Player != null)
        {
            transform.position = Player.position;
        }
    }
}

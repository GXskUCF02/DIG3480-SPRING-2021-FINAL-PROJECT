using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagble_Zone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Ruby_PlayerCharacter_Script controller = other.GetComponent<Ruby_PlayerCharacter_Script >();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Collectible : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        Ruby_PlayerCharacter_Script controller = other.GetComponent<Ruby_PlayerCharacter_Script>();

        if (controller != null)
            {
                if(controller.health < controller.maxHealth)
                {
                    controller.ChangeHealth(1);
	                Destroy(gameObject);

                    controller.PlaySound(collectedClip);
                }
            }
    }
}

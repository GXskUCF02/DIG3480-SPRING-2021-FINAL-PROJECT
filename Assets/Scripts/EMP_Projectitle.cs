using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP_Projectitle : MonoBehaviour
{
    public ParticleSystem empEffect;
    Rigidbody2D rigidbody2d;
    Animator animator;
    
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        Robot_Script h = other.collider.GetComponent<Robot_Script>();
        if (h != null)
        {
            h.Stun();
        }
    
        HardRobot_Script o = other.collider.GetComponent<HardRobot_Script>();
        if (o != null)
        {
            o.Stun();
        }

        Destroy(gameObject);
        

    }
}

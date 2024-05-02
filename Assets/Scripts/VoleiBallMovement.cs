using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoleiBallMovement : MonoBehaviour
{
    
    public GameObject player;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private CircleCollider2D ballCatchCollider;
    [SerializeField] private Vector2 aimDirection;
    [SerializeField] float bumpPassForce;

    void Start()
    {
        
    }    

    // Update is called once per frame
    void Update()
    {
        aimDirection = player.GetComponent<PlayerStateMachine>().GetAimDirection();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other == ballCatchCollider)
        {
            body.velocity = aimDirection * bumpPassForce;
        }
    }
}

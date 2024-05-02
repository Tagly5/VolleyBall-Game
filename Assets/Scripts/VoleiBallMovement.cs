using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoleiBallMovement : MonoBehaviour
{
    
    public PlayerStateMachine stateMachine;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private CircleCollider2D ballCatchCollider;
    [SerializeField] private Vector2 aimDirection;
    private bool _hasTriggered;
    public float bumpPassForce;

    [Range(0f, 1f)] public float bumpDecay;
    

    void Start()
    {
        
    }    

    // Update is called once per frame
    void Update()
    {
        aimDirection = stateMachine.GetComponent<PlayerStateMachine>().GetAimDirection();
        if(_hasTriggered && stateMachine.GetComponent<PlayerStateMachine>().GetAtkInput() > 0)
        {
            body.velocity = aimDirection * bumpPassForce;
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other == ballCatchCollider)
        {
            _hasTriggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other == ballCatchCollider)
        {
            _hasTriggered = false;
        }
    }
}

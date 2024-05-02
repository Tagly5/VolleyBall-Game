using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VoleiBallMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D body;
    [SerializeField] CircleCollider2D ballCatchCollider;
    [SerializeField] float bumpPassForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other == ballCatchCollider)
        {
            body.velocity = Vector2.up * bumpPassForce;
        }
    }
}

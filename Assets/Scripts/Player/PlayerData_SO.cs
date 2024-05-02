using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData_SO : ScriptableObject
{
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    [Range(0,1f)] public float groundDecay;
    public float jumpForce;
    public float jumpTime;
}

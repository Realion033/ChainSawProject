using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private float AirRatateValue;
    [SerializeField] private float maxAngularSpeed = 10f;
    private Rigidbody2D rb;
    
    private void Update()
    {
        LimitRotate();
        AirRotate();
    }

    private void LimitRotate()
    {
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void AirRotate()
    {
        
    }

    public void ResetRotate()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}

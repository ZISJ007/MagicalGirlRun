using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObstacle : BaseObstacle
{
    [SerializeField] private float upMoveSpeed;
    [SerializeField] private float targetY;
    [SerializeField]private float xMoveSpeed;
    private GameSystem gameSystem;
    private bool isYMoving = false;

    public override void Spawn()
    {
    }

    protected override void Update()
    {
        base.Update();
        if (isYMoving)
        {
            YMove();
        }
    }
    
    

    protected override void YMove()
    {
        if (transform.position.y < targetY)
        {
            transform.position += Vector3.up * upMoveSpeed * Time.deltaTime;
            if (transform.position.y > targetY)
            {
                transform.position =new Vector3(transform.position.x, targetY, transform.position.z);;
                isYMoving = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("YMoveTrigger"))
        {
            isYMoving = true;

            Debug.Log("ºÎµóÈû");
        }
    }
}
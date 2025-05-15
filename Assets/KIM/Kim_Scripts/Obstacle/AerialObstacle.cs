using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerialObstacle : BaseObstacle
{
    [SerializeField] private float downMoveSpeed;
    [SerializeField] private float targetY;
    private bool isYMoving=false;

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
        if (transform.position.y > targetY)
        {
            transform.position += Vector3.down * downMoveSpeed * Time.deltaTime;
            if (transform.position.y < targetY)
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
        }
    }
}

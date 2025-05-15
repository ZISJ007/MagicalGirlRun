using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 0, 100);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}

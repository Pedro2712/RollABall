using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationDirection = new Vector3(15, 30, 45); // Direção padrão de rotação

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationDirection * Time.deltaTime);
    }
}

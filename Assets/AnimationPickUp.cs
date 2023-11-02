using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float turnSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        //Aixo el que fa es rota l'objecte
        transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
    }
}

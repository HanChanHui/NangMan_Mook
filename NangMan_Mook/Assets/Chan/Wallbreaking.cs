using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallbreaking : MonoBehaviour
{
    [SerializeField] private float DrawMass;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject)
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().mass >= DrawMass)
                Destroy(this.gameObject);
        }
    }
}

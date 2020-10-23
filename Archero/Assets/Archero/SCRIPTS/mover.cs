using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour
{
    public float powerAtak = 1;  // сила атаки при ударе об другой объект
    public float speed;
    public float destroyTime = 3;
    public Owner owner = Owner.Monster;

    public void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(this.gameObject, destroyTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "block")
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 0;
        }
        else if (other.GetComponent<Entity>() != null && other.GetComponent<Entity>().owner != owner)
        {
            other.GetComponent<Entity>().TakeDamage(powerAtak);
        }
    }

}

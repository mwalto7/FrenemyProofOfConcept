using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploding : Bullet
{

    public int speed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || (other.gameObject.CompareTag("Player") && other.gameObject != owner))
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}

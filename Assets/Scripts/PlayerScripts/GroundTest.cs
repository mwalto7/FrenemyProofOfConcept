using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTest : MonoBehaviour
{
    PlayerController parent;

    void Awake()
    {
       parent = this.transform.parent.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Weapon") { parent.touchingWeapons.Add(other.gameObject); }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) { parent.onGround = true; }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon")) { parent.touchingWeapons.Remove(other.gameObject); }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) { parent.onGround = false; }
    }

}

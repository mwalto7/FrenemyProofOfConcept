using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int firerate;
    private int ammo;
    public bool isHeld = false;
    [SerializeField]bool onGround = false;
    public Bullet bulletGeneric;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) { onGround = true; }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) {onGround = false; }
    }

    void Update()
    {
        if (!onGround && !isHeld)
        {
            transform.Translate(new Vector2(0, -.02f));
        }
    }

    public void Shoot()
    {
        Bullet bulletClone = Instantiate(bulletGeneric, transform.position, transform.rotation);
        bulletClone.owner = transform.parent.gameObject;
    }
}

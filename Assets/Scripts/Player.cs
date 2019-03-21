using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //PLAYERNUM needs to be updated in unity to reflect the actual player number


    [SerializeField] private HealthBar healthBar; 

    public GameObject player;
    public float health = 100f;

    bool facingRight;
    bool hasWeapon;
    bool flippedWeapon;
    bool repeated;
    public int playerNum;

    private void Start()
    {
        //p1Health = 100f;
        //p2Health = 100f;
    }
    void Update()
    {
        facingRight = player.transform.right.x > 0;//bool to tell whether player is facing right or not
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(Input.GetButtonDown("pickup weapon Player " + playerNum))
        {
        repeated = false;
            if(col.tag == "Weapon")
            {
                if (!repeated)
                {
                    repeated = true;
                    int children = player.transform.childCount;
                    col.transform.parent = player.transform;//the collider's (or gun's) parent is now the player
                    hasWeapon = true;
                    if (col.name == "SubMachineGun")
                    {
                        col.transform.localPosition = new Vector3(0.08f, -0.55f, 0);
                    }
                    else if (col.name == "RocketLauncher")
                    {
                        col.transform.localPosition = new Vector3(0.245f, -0.43f, 0);
                    }
                    else if (col.name == "Pistol")
                    {
                        col.transform.localPosition = new Vector3(0.368f, -0.46f, 0);
                    }
                    else if (col.name == "Grenade")
                    {
                        col.transform.localPosition = new Vector3(0.28f, -0.55f, 0);
                    }
                    else if (col.name == "Knife")
                    {
                        col.transform.localPosition = new Vector3(0.64f, -0.56f, 0);
                    }

                    // drop weapon if you have 2 guns
                    if (children > 4)//we don't want more than one weapon at a time for now
                    {
                        print("2 guns");
                        player.transform.GetChild(3).gameObject.transform.parent = null;
                    }
                    // print("weapon facing: " + player.transform.GetChild(3).transform.right.x + ", facing right: " + facingRight);
                    print(player.transform.GetChild(3).name);
                    if (player.transform.GetChild(3).transform.right.x > 0 && !facingRight)
                    {
                        if (!flippedWeapon)//for some reason the weapon was getting flipped twice so i had to use a bool to stop it
                        {
                            print("flipping weapon");
                            flippedWeapon = true;
                            player.transform.GetChild(3).Rotate(0f, 180f, 0f);//rotates weapon along with the firepoint
                        }
                    }
                }
            }
        }
    }

    public void Damage(float damage, GameObject target)
    {
        //need a check to make sure that the amount of damage taken is not greater
        //than the amount of health left, otherwise the health bar would be negative
        health -= damage;
        healthBar.SetSize(health);
        if (health <= 0)
        {
            Die(target);
        }
        print(target.name+" now has " + health + " health.");
    }
    void Die(GameObject deadGuy)
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deadGuy);
    }
}

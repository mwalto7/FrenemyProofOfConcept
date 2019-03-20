using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //PLAYERNUM needs to be updated in unity to reflect the actual player number

    //public GameObject player1;
    //public GameObject player2;
    //public static float p1Health;
    //public static float p2Health;

    //public GameManagr gm;

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
            if(col.tag == "Weapon")
            {
                if (!repeated)
                {
                    repeated = true;
                    int children = player.transform.childCount;
                    if (children == 3)
                    {
                        col.transform.parent = player.transform;//the collider's (or gun's) parent is now the player
                        hasWeapon = true;
                        if (col.name == "SubMachineGun")
                        {
                            col.transform.localPosition = new Vector3(0.195f, -0.161f, 0);
                        }
                        else if (col.name == "RocketLauncher")
                        {
                            col.transform.localPosition = new Vector3(-0.003f, -0.094f, 0);
                        }
                        else if (col.name == "Pistol")
                        {
                            col.transform.localPosition = new Vector3(0.368f, -0.14f, 0);
                        }
                        else if (col.name == "Grenade")
                        {
                            col.transform.localPosition = new Vector3(-0.2557f, -0.2155f, 0);
                        }
                        else if (col.name == "Knife")
                        {
                            col.transform.localPosition = new Vector3(0.52f, -0.25f, 0);
                        }
                    }
                    else if (children >= 4)//we don't want more than one weapon at a time for now
                    {
                        print("somehow we got here");
                        player.transform.GetChild(3).gameObject.transform.parent = null;
                        col.transform.parent = player.transform;
                        hasWeapon = true;
                        if (col.name == "SubMachineGun")
                        {
                            col.transform.localPosition = new Vector3(0.195f, -0.161f, 0);
                        }
                        else if (col.name == "RocketLauncher")
                        {
                            col.transform.localPosition = new Vector3(-0.003f, -0.094f, 0);
                        }
                        else if (col.name == "Pistol")
                        {
                            col.transform.localPosition = new Vector3(0.368f, -0.14f, 0);
                        }
                        else if (col.name == "Grenade")
                        {
                            col.transform.localPosition = new Vector3(-0.2557f, -0.2155f, 0);
                        }
                        else if (col.name == "Knife")
                        {
                            col.transform.localPosition = new Vector3(0.52f, -0.25f, 0);
                        }
                    }
                    print("weapon facing: " + player.transform.GetChild(3).transform.right.x + ", facing right: " + facingRight);
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

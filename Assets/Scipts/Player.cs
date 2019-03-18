using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public CharacterController2D controller;
    public GameObject Playerr;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool facingRight;
    public static float health;

    private void Start()
    {
        health = 100f;
    }
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (health <= 0)
        {
            Destroy(gameObject);//add death animation before destroy
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        facingRight = Playerr.transform.localScale.x > 0;
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Bullet"))
        {
            health -= 5f;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        //when a player collides make weapon a child of the player if they dont already have a weapon
        if (Playerr.transform.childCount == 2)
        {
            if (Input.GetKey(KeyCode.E))
            {
                col.transform.parent = Playerr.transform;
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
                Debug.Log(facingRight);
                if (!facingRight)
                {
                    Vector3 scale = col.transform.localScale;
                    scale.x *= -1;
                    col.transform.localScale = scale;
                }
            }

        }
        // else if (Player.transform.childCount == 3)
        // {
        //   if (Input.GetKey(KeyCode.E))
        //   {
        //     Player.transform.GetChild(2).gameObject.transform.parent = null;
        //     col.transform.parent = Player.transform;
        //     if (col.name == "SubMachineGun")
        //     {
        //       col.transform.localPosition = new Vector3(0.195f, -0.161f, 0);
        //     }
        //     else if (col.name == "RocketLauncher")
        //     {
        //       col.transform.localPosition = new Vector3(-0.003f, -0.094f, 0);
        //     }
        //     else if (col.name == "Pistol")
        //     {
        //       col.transform.localPosition = new Vector3(0.368f, -0.14f, 0);
        //     }
        //     else if (col.name == "Grenade")
        //     {
        //       col.transform.localPosition = new Vector3(-0.2557f, -0.2155f, 0);
        //     }
        //     else if (col.name == "Knife")
        //     {
        //       col.transform.localPosition = new Vector3(0.52f, -0.25f, 0);
        //     }
        //     Debug.Log(facingRight);
        //     if (!facingRight)
        //     {
        //       Vector3 scale = col.transform.localScale;
        //       scale.x *= -1;
        //       col.transform.localScale = scale;
        //     }
        //}
        //}
    }
}

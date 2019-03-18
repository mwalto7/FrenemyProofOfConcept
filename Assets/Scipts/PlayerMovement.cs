using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

  public CharacterController2D controller;
  public GameObject Player;
  public Animator animator;
  public float runSpeed = 40f;
  float horizontalMove = 0f;
  bool jump = false;
  bool crouch = false;


  void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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
    animator.SetBool("IsCrouching", isCrouching);
  }

  void OnTriggerStay2D(Collider2D col)
  {
    //when a player collides make weapon a child of the player if they dont already have a weapon
    if (Player.transform.childCount == 2)
    {
      if (Input.GetKey(KeyCode.E))
      {
        col.transform.parent = Player.transform;
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
    }
    else if (Player.transform.childCount == 3)
    {
      if (Input.GetKey(KeyCode.E))
      {
        Player.transform.GetChild(2).gameObject.transform.parent = null;
        col.transform.parent = Player.transform;
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
    }
  }
}

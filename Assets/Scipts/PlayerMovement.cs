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
  bool facingRight;
  public int playerNum;

  void Update()
  {
    horizontalMove = Input.GetAxisRaw("Horizontal p"+playerNum) * runSpeed;

    animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

    if (Input.GetButtonDown("Jump p"+playerNum))
    {
      jump = true;
      Debug.Log("Jumping");
      animator.SetBool("IsJumping", true);
    }

    if (Input.GetButtonDown("Crouch p"+playerNum))
    {
      Debug.Log("crounching");
      crouch = true;
    }
    else if (Input.GetButtonUp("Crouch p"+playerNum))
    {
      crouch = false;
    }

    facingRight = Player.transform.localScale.x > 0;
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
    Debug.Log(isCrouching);
    animator.SetBool("isCrouching", isCrouching);
  }

  void OnTriggerStay2D(Collider2D col)
  {
    //when a player collides make weapon a child of the player if they dont already have a weapon
    if (Player.transform.childCount == 2)
    {
      if (Input.GetButtonDown("pickup weapon p"+playerNum))
      {
        if (col.tag == "Weapon")
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
          if (Player.transform.GetChild(2).transform.localScale.x < 0 && !facingRight)
          {
            Debug.Log("flipping weapon");
            Vector3 scale = Player.transform.GetChild(2).transform.localScale;
            scale.x *= -1;
            Player.transform.GetChild(2).transform.localScale = scale;
          }
        }
      }
    }
    else if (Player.transform.childCount == 3)
    {
      if (Input.GetButtonDown("pickup weapon p"+playerNum))
      {
        if (col.tag == "Weapon")
        {
          col.transform.parent = Player.transform;
          if (Player.transform.childCount == 4)
          {
            Player.transform.GetChild(2).gameObject.transform.parent = null;
          }
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
          if (Player.transform.GetChild(2).transform.localScale.x < 0 && !facingRight)
          {
            Debug.Log("flipping weapon");
            Vector3 scale = Player.transform.GetChild(2).transform.localScale;
            scale.x *= -1;
            Player.transform.GetChild(2).transform.localScale = scale;
          }
        }
      }
    }
  }
}

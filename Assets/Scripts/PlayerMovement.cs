using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //PLAYERNUM needs to be updated in unity to reflect the actual player number

    public CharacterController2D controller;
    public GameObject Player;
    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public int playerNum;

    public AudioClip jumpsound;
    public AudioSource source;

    Vector3 start;

    void Awake(){
        source = GetComponent<AudioSource>();
        start = Player.transform.position;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal Player " + playerNum) * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump Player " + playerNum))
        {
            jump = true;
            source.PlayOneShot(jumpsound);
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonDown("Crouch Player " + playerNum))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch Player " + playerNum))
        {
            crouch = false;
        }

        if(Player.transform.position.y < -18f){
            Player.transform.position = start;
            if(Player.transform.childCount > 3){
                Player.transform.GetChild(3).transform.parent = null;
            }
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
        animator.SetBool("isCrouching", isCrouching);
    }
}

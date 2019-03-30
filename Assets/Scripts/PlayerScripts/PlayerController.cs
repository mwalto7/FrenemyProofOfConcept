using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Identifer 
    public int id;

    //Stats for player
    [SerializeField]
    private enum playerClass
    {
        Spy,
        Scientist,
        Soldier,
        Engineer
    };

    [SerializeField] private int speed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float slideSpeed;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxHp;

    //Player state variables 
    public bool onGround = false;
    public bool jump = false;
    private bool hasGun = false;
    private int isFacing = 1;
    private bool isSliding = false;

    //Variables you don't change
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private Vector3 m_Velocity = Vector3.zero;
    public List<GameObject> touchingWeapons;
    public GameObject currentWeapon;
    public Transform GunLocation;
    private float moveHorizontal;
    float slideCoolDown;
    public float slideCoolDownDuration;

    //Player Compoents needed for reference later
    Rigidbody2D MyRigidBody;
    Animator animator;
    GameObject bar;


    //Start method sets refrences
    void Start()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bar = transform.Find("Bar").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) {onGround = true; jump = false; }
        if (other.gameObject.CompareTag("Bullet")) {
            if(other.GetComponent<Bullet>().owner != gameObject)
            currentHp--;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Platform")) { onGround = false;}
    }


    //Movement Functions start here

    /*~~~Movement Function List~~~~~~~~~
     *Flip()
     *Jump()
     *Drop()
     *Slide()
     */


    /*Flip
     *Utility function used by Move()
     *Just rotates the sprite 180 degrees
     */
    private void Flip()
    {
        transform.Rotate(new Vector3(0, 180, 0));
        bar.transform.Rotate(new Vector3(0, 180, 0));
        isFacing *= -1;
    }

    /*Jump
   *Utility function used by Move()
   * Jumps!
   */
    private void Jump()
    {
        if (Input.GetButtonDown("Jump" + id) && jump == false)
        {
            MyRigidBody.AddForce(new Vector3(0f, jumpHeight * 100));
            jump = true;
        }
    }

    /*Drop
    *Utility function used by Move()
    * Drops!
    * Used for checking weather a player wants to drop down off a platform
    */
    private void Drop()
    {
        if (Input.GetButton("Down" + id))
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

   
    private void Slide()
    {
        if (Input.GetButtonDown("Slide" + id) && moveHorizontal != 0 && onGround && !isSliding)
        {
            MyRigidBody.AddForce(new Vector2(100 * slideSpeed * Mathf.Sign(moveHorizontal), 0f));
            animator.SetBool("isSliding", true);
            isSliding = true;
            slideCoolDown = Time.time + slideCoolDownDuration;
        }

        if (slideCoolDown <= Time.time)
        {
            isSliding = false;
            animator.SetBool("isSliding", false);
        }

    }


    /* Movement
     * -This handles all movement for this player object 
     * -Called in update (Meaning it is called every frame)
     * -Checks for relevent player input (Jump, HorizontalMovement, and Slide input)  
     * -ONLY DEALS WITH PLAYER MOVEMENT NOT ANY OTHER ACTIONS
     * -Uses the Unity 2D Physics engine
     */
    private void Move()
    {

        Vector3 targetVelocity;
        moveHorizontal = Input.GetAxis("Horizontal" + id);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        if (!isSliding)
        { 
        if (isFacing != Mathf.Sign(moveHorizontal) && moveHorizontal != 0) { Flip(); }
        targetVelocity = new Vector2(moveHorizontal * speed, MyRigidBody.velocity.y);
        MyRigidBody.velocity = Vector3.SmoothDamp(MyRigidBody.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        }
        if(moveHorizontal == 0 && onGround && !isSliding && jump)
        {
            MyRigidBody.velocity = new Vector3(0, 0, 0);
        }
        Jump();
        Drop();
        Slide();
    }


    //Action functions start here

    /*~~Action Function list~~
     * WeaponPickup()
     * WeaponDrop()
     * WeaponShoot()
     * Action()
     */

    /*WeaponPickup
     * -Should only be called if Player is 
     * * * 1. Colliding with weapon pickup collider (Check the weapon class) 
     * * * 2. Player has pressed Weapon pickup key this frame (Default 'E')
     * -Checks if Player has gun and if true drops it 
     * -Sets gun refrence to colliding gun (The gun they want to pick up)
     * -Sets gun parent to this player
     */
    private void WeaponPickup()
    {
        if (Input.GetButtonDown("Pickup" + id))
        {
           if (touchingWeapons.Count > 0)
            {
                touchingWeapons[0].transform.parent = transform;
                currentWeapon = touchingWeapons[0];
                currentWeapon.transform.rotation = transform.rotation;
                currentWeapon.transform.localPosition = new Vector2(0,-.5f);
                currentWeapon.GetComponent<Weapon>().isHeld = true;
            }
        }
    }
    /*WeaponDrop
     *   
    */
    private void WeaponDrop()
    {
        if (Input.GetButtonDown("DropWeapon" + id) && currentWeapon != null)
        {
            currentWeapon.GetComponent<Weapon>().isHeld = false;
            currentWeapon.transform.parent = null;
            currentWeapon = null;
        }
    }
    /*WeaponShoot 
     *   
     */
    private void WeaponShoot()
    {
        if (Input.GetButtonDown("Fire" + id) || Input.GetAxis("Fire" + id) > 0) { currentWeapon.GetComponent<Weapon>().Shoot(); }
    }

    /*Ability
     * Overide in respective class files
     *    
     */
    private void Ability()
    {

    }

    //End of action functions

    /* Action
     * -Action should be called every frame  
     * -Only one action should be performed per frame
     * -Checks for relevent player input (Drop, Pickup, Shoot)
     * -Calls relevent function(WeaponPickup, WeaponDrop)   
     */
    private void Action()
    {
        WeaponPickup();
        WeaponDrop();
        WeaponShoot();
    }

    /* FixedUpdate
     * -Called every frame
     * -Move() checks for player movemnt
     * -Action() checks for player action   
     * 
     * 
     */
    
    void FixedUpdate()
    {
        Move();
        Action();
        bar.GetComponent<HealthBar>().SetHp(currentHp / maxHp);
        if (currentWeapon != null) {
            currentWeapon.transform.position = GunLocation.position;
        }
    }
}

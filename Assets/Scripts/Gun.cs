using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform firePoint;//firepoint from player needs to be put on gun
    public GameObject bulletPreFab;
    public bool hasWeapon;

    public GameObject bullet;
    string player;
    public AudioClip shootsound;
    public AudioSource source;

    private float fireRate = 0.3f;//.5f = 2 bullets per second, 1f = 1 bullet per second
    float nextFire = 0f;
    
    void Awake(){
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if this has a parent, and parent is a player. thus if player is holding this gun
        if (this.transform.parent != null && this.transform.parent.tag == "Player")
        {
            player = this.transform.parent.name;// either Player 1 or Player 2


            if (Input.GetButtonDown("Fire " + player) && Time.time > nextFire)
            {

                string gunType = this.transform.name;
                if (gunType == "Grenade")
                {
                    fireRate = 1f;
                    //make grenade throw and blast 
                    //for this you can set the gravity on the object to (1) i guess
                    //and then set the angle at which it's thrown to (45 degrees)
                    //and then give it some kind of velocity
                    //the hard part will be to maske it blow up (instantiate bullets in all directions for .5 secs?)
                }
                else if (gunType == "SubMachineGun")
                {
                    fireRate = .3f;
                    print("fire rate is: " + fireRate);
                }
                else if (gunType == "RocketLauncher")
                {
                    fireRate = 1.5f;
                    print("fire rate is: " + fireRate);
                }
                else if (gunType == "Knife")
                {

                }
                else if (gunType == "Pistol")
                {
                    fireRate = .5f;
                    print("fire rate is: " + fireRate);
                }


                nextFire = Time.time + fireRate;
                Shoot();
            }





        }
    }
    public void Shoot()
    {
        Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        source.PlayOneShot(shootsound);
        print("bang");
    }

}

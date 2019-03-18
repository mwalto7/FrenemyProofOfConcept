using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject Bullet;

    public GameObject Weapon;

    // Update is called once per frame
    void Update()
    {
        //if parent is a player
        if (Weapon.transform.parent.tag == "Player"){

            //drop/destroy the gun when a player presses 'E'
            if (Input.GetKey(KeyCode.R)){
                Debug.Log("R was pressed");
                DestroyImmediate(Weapon);
            }
            
            //left click shoots the gun when a player is holding it
            if(Input.GetMouseButtonDown(0)){
             Instantiate(Bullet);
            }
        }
    }

}

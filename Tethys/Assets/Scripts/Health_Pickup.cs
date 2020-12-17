using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health_Pickup : MonoBehaviour
{
    public Temp_MC_Stats player;
    public HealthBar healthBar;

    public bool CanClickPickUp;
    public bool CanWalkPickUp;

    public bool FullHeart = true;
    public bool HalfHeart;
    public bool QuarterHeart;

    private bool WithinRange;
    private float HeartRegain;

    void Update()
    {
        //if statement that checks if object can be clicked and if mc is within object
        //Will pickup item when E is pressed
        if (CanClickPickUp && WithinRange && Input.GetKeyDown(KeyCode.E))
        {
            OnPickup();
        }

    }

    //Sets the health pickup value when game starts depending on what is checked in the inspector.
    //By default, it is set to Full Heart.
    private void Awake()
    {
        if (FullHeart)
        {
            HeartRegain = 25f;
        }
        if(HalfHeart)
        {
            HeartRegain = 15f;
        }
        if (QuarterHeart)
        {
            HeartRegain = 5f;
        }
    }

    //On Trigger checks if tag is player then checks if you can click to pick up or walk over to pick up
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            WithinRange = true;
            if(CanClickPickUp)
            {
                Debug.Log("Press E to pickup!");
            }
            if (CanWalkPickUp)
            {
                OnPickup();
            }
        }

    }

    //Lets player know if they are out of range of the pickup
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            WithinRange = false;
            if(gameObject)
            {
                Debug.Log("Out of Range of Pickup");
            } 
        }
    }
    
    //Pickups item and deactivates gameObject
    //Will add it to inventory system
    public void OnPickup()
    {
        //use item picked up immediately
        if (player.curHearts < player.MaxHearts)
        {
            FindObjectOfType<SoundManager>().PlaySound("HealthPickup");
            //update player health plus health regain
            player.curHearts += HeartRegain;

            // remove gameobject from scene 
            Destroy(gameObject);
            Debug.Log("Health Picked Up");
        }
        else
        {
            Debug.Log("Health is full!");
        }
    }
}

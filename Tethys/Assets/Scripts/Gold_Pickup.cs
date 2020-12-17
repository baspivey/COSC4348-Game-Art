using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold_Pickup : MonoBehaviour
{
    public int GoldValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Temp_MC_Stats player = FindObjectOfType<Temp_MC_Stats>();

            if (player != null)
            {
                FindObjectOfType<SoundManager>().PlaySound("CoinPickup");
                player.curGold += GoldValue;
                Destroy(gameObject);
            }
        }
    }
}

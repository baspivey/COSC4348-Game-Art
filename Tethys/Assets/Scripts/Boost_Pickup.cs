using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

public class Boost_Pickup : MonoBehaviour
{
    public IsoMovementController movement;
    public Temp_MC_Stats player;

    public GameObject speedicon, healthicon, attackicon;

    public HealthBar healthbar;

    //public Animator animator;

    public bool SpeedBoost;
    public bool AtkBoost;
    public bool HealthBoost;

    public float duration = 5f;
    public float boostMultipler = 2f;

    //Boost/PowerUp 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //animator.SetTrigger("StartTimer");
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(pickup(collision));
        }
    }

    IEnumerator pickup(Collider2D other)
    {
        if (SpeedBoost)
        {
            speedicon.SetActive(true);
            FindObjectOfType<SoundManager>().PlaySound("SpeedBoost");
            movement.movementSpeed *= boostMultipler;
        }
        if (AtkBoost)
        {
            attackicon.SetActive(true);
            player.attackPwr *= boostMultipler;
        }
        if (HealthBoost)
        {
            healthicon.SetActive(true);
            FindObjectOfType<SoundManager>().PlaySound("HealthBoost");
            player.MaxHearts *= boostMultipler;
            player.curHearts *= boostMultipler;
            healthbar.SetHealth(player.curHearts);
            healthbar.SetMaxHealth(player.MaxHearts);
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        if (SpeedBoost)
        {
            speedicon.SetActive(false);
            movement.movementSpeed /= boostMultipler;
        }
        if (AtkBoost)
        {
            attackicon.SetActive(false);
            player.attackPwr /= boostMultipler;
        }
        if (HealthBoost)
        {
            healthicon.SetActive(false);
            player.MaxHearts /= boostMultipler;
            player.curHearts /= boostMultipler;
            healthbar.SetHealth(player.curHearts);
            healthbar.SetMaxHealth(player.MaxHearts);
        }

        Destroy(gameObject);
    }
}

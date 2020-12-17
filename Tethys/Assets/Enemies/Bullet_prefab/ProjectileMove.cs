using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public float fadeDistance = 3.0f;
    public GameObject muzzlePrefab;
    public GameObject hitPrefab;
    public GameObject fadePrefab;

    private string characterTag = "Default";
    private Vector3 startPos;
    private GameObject muzzleVFX;
    // Start is called before the first frame update
    void Start()
    {
        if (muzzlePrefab != null) {
            muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (characterTag == "Player") {
            if (Vector3.Distance(startPos, transform.position) >= fadeDistance) {
                Destroy(gameObject);
                if (fadePrefab != null) {
                    var fadeVFX = Instantiate(fadePrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("onCollisionCollider");
        if (collision.collider != null)
        {
            if (collision.collider.tag == "Player")
            {
                collision.collider.GetComponent<Temp_MC_Stats>().DecreaseHealth();
            }
            else if (collision.collider.tag == "Projectile")
            {
                return;
            }
            else
            {
                var enemy = collision.collider.GetComponent<follow_MC>();

                if (enemy != null)
                {
                    enemy.TakeDamage(35);
                }
            }
        }

        if (hitPrefab != null) {
            var hitVFX = Instantiate(hitPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void setCharacterTag(string tag) {
        characterTag = tag;
    }

    public void setStartPos(Vector3 pos) {
        startPos = pos;
    }
}

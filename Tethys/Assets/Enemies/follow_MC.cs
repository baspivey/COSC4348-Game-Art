using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_MC : MonoBehaviour
{
    // Main character gameObject
    public Transform player;
    public bool followingPlayer;

    // parameters for main character distance detection
    private Rigidbody2D rb;
    public float minPlayerDist = 0.5f;
    public float playerInSightDist = 1f;
    public float MoveSpeed = 1f;

    // enemey shooting parameters
    public GameObject bullet;
    public float bulletSpeed = 1f;
    public float fireWaitTime = 1f;
    private float timer = 0;
    private Quaternion rotation;
    private bool shotFired = false;

    // ray cast parameters
    private Vector2 direction = new Vector2(0, 0);
    private float distanceToPlayer = 0f;

    enemy_renderer enemyRenderer;

    private float health = 100;
    public bool IsDead { get; protected set; }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponentInChildren<enemy_renderer>();
        health = 100;
        IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        if (!IsDead)
        {
            timer += Time.deltaTime;

            direction = player.position - transform.position;
            distanceToPlayer = Vector2.Distance(transform.position, player.position);
            RaycastHit2D hit = GetRayCastHit(direction.normalized, distanceToPlayer);

            if (distanceToPlayer < minPlayerDist)
            {
                rb.velocity = new Vector2(0, 0);
                if (timer > fireWaitTime && !player.GetComponent<Temp_MC_Stats>().IsDead)
                {
                    enemyRenderer.PlayAttack(direction);   
                    enemyRenderer.shotFired = false;
                    timer = 0; // reset the shoot timer
                    shotFired = true;
                } else {
                    // enemyRenderer.SetDirection(new Vector2(0, 0));
                }
            }
            else
            {

                if (hit.collider != null && hit.collider.tag == "Player" && distanceToPlayer < playerInSightDist)
                {
                    Vector2 newPos = rb.position + direction * MoveSpeed * Time.fixedDeltaTime;
                    enemyRenderer.SetDirection(direction);
                    rb.MovePosition(newPos);
                }
                else
                {
                    enemyRenderer.SetDirection(new Vector2(0, 0));
                }
            }
        }
    }

    private RaycastHit2D GetRayCastHit(Vector2 direction, float distance) {
        RaycastHit2D[] hits = new RaycastHit2D[2];
        int hitCount = Physics2D.RaycastNonAlloc(transform.position, direction, hits);
        for (int i = 0; i < hitCount; i++) {
            if (hits[i].collider.tag == "Player") {
                return hits[i];
            }
        }
        return hits[0];
    }

    public void TakeDamage(float damage)
    {
        FindObjectOfType<SoundManager>().PlaySound("EnemyGrunt");

        if (health > 0)
        {
            health -= damage;

            if (health <= 0)
            {
                IsDead = true;
                enemyRenderer.PlayDeath();

                var colliders = GetComponents<Collider2D>();

                foreach(var collider in colliders)
                {
                    collider.enabled = false;
                }
            }
        }
    }

    public void Attack() {
        GameObject clone;
        Vector3 direction = player.position - transform.position;
        //direction.z = 0;
        direction.Normalize();
        FindObjectOfType<SoundManager>().PlaySound("EnemyAttack1");
        clone = Instantiate(bullet, (transform.position + (direction * 0.35f)), Quaternion.LookRotation(direction));
        clone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}

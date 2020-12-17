using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_renderer : MonoBehaviour
{
    public static readonly string[] idleDirections = { "enemy_idle_n", "enemy_idle_nw", "enemy_idle_w", "enemy_idle_sw", "enemy_walk_s", "enemy_idle_se", "enemy_idle_e", "enemy_idle_ne" };
    public static readonly string[] walkDirections = { "enemy_walk_n", "enemy_walk_nw", "enemy_walk_w", "enemy_walk_sw", "enemy_walk_s", "enemy_walk_se", "enemy_walk_e", "enemy_walk_ne" };
    public static readonly string[] deathDirections = { "enemy_death_n", "enemy_death_nw", "enemy_death_w", "enemy_death_sw", "enemy_death_s", "enemy_death_se", "enemy_death_e", "enemy_death_ne" };
    public static readonly string[] attackDirections = { "enemy_attack_n", "enemy_attack_nw", "enemy_attack_w", "enemy_attack_sw", "enemy_attack_s", "enemy_attack_se", "enemy_attack_e", "enemy_attack_ne" };

    Animator animator;
    int lastDirection;
    private string attackState;
    public bool shotFired = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(attackState) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f >= 0.5f) {
            if (!shotFired) {
                transform.GetComponentInChildren<follow_MC>().Attack();
                shotFired = true;
            }
        }
    }

    public void PlayDeath()
    {
        string[] directionArray = deathDirections;
        string stateHash = directionArray[lastDirection];
        animator.Play(stateHash);
        animator.Play("gibs");
    }

    public void PlayAttack(Vector2 direction)
    {
        string[] directionArray = attackDirections;
        lastDirection = DirectionToIndex(direction, 8);
        attackState = attackDirections[lastDirection];
        animator.Play(attackState);
    }

public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;

        if (direction.magnitude < .01f)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(direction, 8);
        }

        string stateHash = directionArray[lastDirection];
        animator.Play(stateHash);
    }
    
    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;

        float step = 360f / sliceCount;
        float halfstep = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, normDir);

        angle += halfstep;

        if (angle < 0) {
            angle += 360;
        }

        float stepCount = angle / step;

        return Mathf.FloorToInt(stepCount);
    }
}

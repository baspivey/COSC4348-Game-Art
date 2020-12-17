using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCharacterRenderer : MonoBehaviour
{
    public static readonly string[] idleDirections = { "Kruger_idle_N", "Kruger_idle_NW", "Kruger_idle_W", "Kruger_idle_SW", "Kruger_idle_S", "Kruger_idle_SE", "Kruger_idle_E", "Kruger_idle_NE" };
    public static readonly string[] walkDirections = { "Kruger_walk_N", "Kruger_walk_NW", "Kruger_walk_W", "Kruger_walk_SW", "Kruger_walk_S", "Kruger_walk_SE", "Kruger_walk_E", "Kruger_walk_NE" };
    public static readonly string[] deathDirections = { "Kruger_death_N", "Kruger_death_NW", "Kruger_death_W", "Kruger_death_SW", "Kruger_death_S", "Kruger_death_SE", "Kruger_death_E", "Kruger_death_NE" };
    public static readonly string[] shootAnims = { "Kruger_shoot_N", "Kruger_shoot_NW", "Kruger_shoot_W", "Kruger_shoot_SW", "Kruger_shoot_S", "Kruger_shoot_SE", "Kruger_shoot_E", "Kruger_shoot_NE" };


    private bool stopRendering = false;
    Animator animator;
    int lastDirection;
    Temp_MC_Stats _stats;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _stats = GetComponent<Temp_MC_Stats>();
    }

    public void AttackDirection(Vector2 direction, bool isMelee=false)
    {
        string[] directionArray = null;
        if (isMelee)
        {
            // TODO: add melee attacks
        }
        else
        {
            FindObjectOfType<SoundManager>().PlaySound("LaserShot");
            directionArray = shootAnims;
        }

        int shootDirection = DirectionToIndex(direction, 8);
        string stateHash = directionArray[shootDirection];

        animator.Play(stateHash);
    }

    public void SetDirection(Vector2 direction)
    {
        string[] directionArray = null;
        if (_stats.IsDead)
        {
            directionArray = deathDirections;
        }
        else
        {
            if (direction.magnitude < .01f)
            {
                directionArray = idleDirections;
            }
            else
            {
                directionArray = walkDirections;
                lastDirection = DirectionToIndex(direction, 8);
            }
        }

        string stateHash = directionArray[lastDirection];

        if (!stopRendering) {
            animator.Play(stateHash);
        }
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

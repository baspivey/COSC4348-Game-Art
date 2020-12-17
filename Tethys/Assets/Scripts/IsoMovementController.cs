using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoMovementController : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 4;

    public float movementSpeed = 1f;
    IsoCharacterRenderer isoRenderer;
    Temp_MC_Stats _stats;

    private Vector3 _direction;
    private Vector3 _muzzle_offset;
    private float _nextAttack;
    private Quaternion rotation;

    Rigidbody2D rbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsoCharacterRenderer>();
        _stats = GetComponent<Temp_MC_Stats>();
        _direction = new Vector3(1, 1, 0);
        _direction.Normalize();
        _muzzle_offset = new Vector3(0f, 0.3f, 0f);
        _nextAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_stats.IsDead)
        {
            if (Time.time > _nextAttack)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 origin = transform.position + _muzzle_offset;
                    Vector3 shootDir = (clickPos - origin);
                    shootDir.z = 0;
                    shootDir.Normalize();


                    isoRenderer.AttackDirection(shootDir);
                    GameObject p;
                    p = Instantiate(projectile, origin + (shootDir * 0.4f), Quaternion.LookRotation(shootDir));
                    p.GetComponent<Rigidbody2D>().velocity = shootDir * speed;
                    p.GetComponent<ProjectileMove>().setCharacterTag("Player");
                    p.GetComponent<ProjectileMove>().setStartPos(origin);
                    _nextAttack = Time.time + 0.5f;
                }
                else
                {
                    Vector2 currentPos = rbody.position;
                    float horizontalInput = Input.GetAxis("Horizontal");
                    float verticalInput = Input.GetAxis("Vertical");
                    Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
                    inputVector = Vector2.ClampMagnitude(inputVector, 1);
                    Vector2 movement = inputVector * movementSpeed;

                    if (Mathf.Abs(movement.magnitude) > 0)
                    {
                        _direction.x = movement.x;
                        _direction.y = movement.y;
                        _direction.Normalize();
                    }

                    Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
                    isoRenderer.SetDirection(movement);
                    rbody.MovePosition(newPos);
                }
            }
        }
    }
}

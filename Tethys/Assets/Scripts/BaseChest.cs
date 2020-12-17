using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
    private Animator animator;

    public List<GameObject> LootList;
    public float MinRange = 0.35f;
    public float MaxRange = 0.5f;

    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened)
        {
            if (collision != null && collision.tag == "Player")
            {
                isOpened = true;

                FindObjectOfType<SoundManager>().PlaySound("ChestOpen");
                animator.SetTrigger("Open");

                //foreach (var item in LootList)
                for (int i = 0; i < LootList.Count; i++)
                {
                    var item = LootList[i];

                    if (item != null)
                    {
                        Vector3 spawnLocation = transform.position;

                        float angle = 0;
                        if (LootList.Count == 1)
                        {
                            angle = 3 * Mathf.PI / 2;
                        }
                        else
                        {
                            angle = (5*Mathf.PI/4) + (i * Mathf.PI / LootList.Count);
                        }
                        float radius = Random.Range(MinRange, MaxRange);

                        float x = Mathf.Cos(angle) * radius;
                        float y = Mathf.Sin(angle) * radius;

                        spawnLocation.x += x;
                        spawnLocation.y += y;
                        spawnLocation.z = 3;

                        var loot = Instantiate(item, spawnLocation, transform.rotation);
                    }
                }
            }
        }
    }
}

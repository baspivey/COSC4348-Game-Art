using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_destoryer : MonoBehaviour
{
    private float duration = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            var ps = transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null) {
                if (ps.main.duration > duration) {
                    duration = ps.main.duration;
                }

                ps.Play();
                Destroy(gameObject, duration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public SceneLoader Loader;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Player")
        {
            Loader.LoadNext();
        }
    }
}
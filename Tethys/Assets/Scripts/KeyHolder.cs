using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    private List<Key.KeyType> keyList;
    public GameObject RedKey, BlueKey, GreenKey;

    private void Awake()
    {
        keyList = new List<Key.KeyType>();
    }

    public void addKey(Key.KeyType keyType)
    {
        FindObjectOfType<SoundManager>().PlaySound("KeyPickup");
        if (keyType == Key.KeyType.Red)
            RedKey.SetActive(true);
        if (keyType == Key.KeyType.Blue)
            BlueKey.SetActive(true);
        if (keyType == Key.KeyType.Green)
            GreenKey.SetActive(true);
        //Debug.Log("Key Added: " + keyType);
        keyList.Add(keyType);
    }
    public void removeKey(Key.KeyType keyType)
    {
        if (RedKey)
            RedKey.SetActive(false);
        if (BlueKey)
            BlueKey.SetActive(false);
        if (GreenKey)
            GreenKey.SetActive(false);
        //Debug.Log("Key Removed: " + keyType);
        keyList.Remove(keyType);
    }
    public bool hasKey(Key.KeyType keyType)
    {
        return keyList.Contains(keyType);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Key key = collision.GetComponent<Key>();
        if(key!=null)
        {
            addKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        doors Doors = collision.GetComponent<doors>();
        if(Doors != null)
        {
            if (Doors.GetKeyType() == Key.KeyType.None)
            {
                Doors.OpenDoor();
            }
            else if (hasKey(Doors.GetKeyType()))
            {
                removeKey(Doors.GetKeyType());
                Doors.OpenDoor();
            }
        }
    }
}

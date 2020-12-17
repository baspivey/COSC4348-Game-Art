using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour
{
    [SerializeField]private Key.KeyType keyType;
    public Animator animator;
    bool IsOpen;
    public Collider2D DoorCollider;

    public Key.KeyType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor()
    {
        //gameObject.SetActive(false);
        FindObjectOfType<SoundManager>().PlaySound("Door");
        animator.SetTrigger("OpenDoor");
        DoorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        //gameObject.SetActive(false);
        animator.SetTrigger("CloseDoor");
        DoorCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //OpenDoor();
    }
}

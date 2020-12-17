using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    public Texture2D cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

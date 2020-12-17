using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDb : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>() {
            new Item(0, "Weak Gun", "A basic gun made from scrap",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                
            }),

            new Item(1, "Weak Potion", "A small potion made from berries",
            new Dictionary<string, int>
            {
                {"Restores", 10 }

            }),

            new Item(2, "BFG", "An obscenely large gun (╯°□°）╯︵ ┻━┻",
            new Dictionary<string, int>
            {
                {"Restores", 10 }

            })// end of list

         };
    }
}

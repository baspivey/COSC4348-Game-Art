using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiInventory : MonoBehaviour
{
    public List<UiItem> uIItems = new List<UiItem>();
    public GameObject slotPrefab;
    public Transform PanelSlot;
    public int numberOfSlots = 5;

    private void Awake()
    {
        for(int i =0; i < numberOfSlots; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(PanelSlot);
            uIItems.Add(instance.GetComponentInChildren<UiItem>());
        }
    }

    public void UpdateSlot(int slot, Item item)
    {
        uIItems[slot].UpdateItem(item);
    }

    public void AddNewItem(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItems(Item item)
    {
        UpdateSlot(uIItems.FindIndex(i => i.item == item), null);
    }

}

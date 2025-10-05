using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer healthSr;
    [SerializeField] private SpriteRenderer manaSr;

    [SerializeField] private int health = MaxValue;
    [SerializeField] private int mana = MaxValue;
    
    private const int MaxValue = 100;
    private const int MinValue = 0;
    private const float InterfaceScale = 0.01f;
    
    [SerializeField] private ItemSlot[] itemSlots;
    
    [SerializeField] private DeckController deckController;

    public void ChangeHealth(int value)
    {
        health += value;
        if (health <= MinValue)
        {
            health = MinValue;
            deckController.ResetHealth();
        }

        if (health > MaxValue)
        {
            health = MaxValue;
        }
        
        DrawHealth();
    }
    
    public void ChangeMana(int value)
    {
        mana += value;
        if (mana <= MinValue)
        {
            mana = MinValue;
            deckController.ResetMana();
        }

        if (mana > MaxValue)
        {
            mana = MaxValue;
        }
        
        DrawMana();
    }

    public void TryPutItemInInventory(Item item)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.Item is null)
            {
                itemSlot.Item = Instantiate(item, itemSlot.transform);
                return;
            }
        }
    }

    private void DrawHealth()
    {
        var sizeY = health * InterfaceScale;
        var currentSize = healthSr.size;
        healthSr.size = new Vector2(currentSize.x, sizeY);
    }

    private void DrawMana()
    {
        var sizeY = mana * InterfaceScale;
        var currentSize = manaSr.size;
        manaSr.size = new Vector2(currentSize.x, sizeY);
    }
}

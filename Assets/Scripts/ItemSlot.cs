using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : SpriteButton
{
    public Item Item { get; set; }

    private PlayerController playerController;
        
    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
    }
    
    public override void OnPointerClick(PointerEventData eventData)
    {
        if (Item is null) return;
        
        playerController.ChangeHealth(Item.hpEffect);
        playerController.ChangeMana(Item.manaEffect);
        Destroy(Item.gameObject);
        Item = null;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (Item is not null)
        {
            Item.transform.localScale *= 1.2f;
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (Item is not null)
        {
            Item.transform.localScale /= 1.2f;
        }
    }
}
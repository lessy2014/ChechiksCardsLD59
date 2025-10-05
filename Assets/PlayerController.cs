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

    public void ChangeHealth(int value)
    {
        health += value;
        if (health <= MinValue)
        {
            // TODO: reset
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
            // TODO: reset
        }

        if (mana > MaxValue)
        {
            mana = MaxValue;
        }
        
        DrawMana();
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

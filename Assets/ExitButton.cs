using UnityEngine;
using UnityEngine.EventSystems;

public class ExitButton : SpriteButton
{
    public override void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.Events;

public class Card
{
    private DeckController DeckController { get; set; }
    
    public string Text { get; set; } 
    public Sprite Sprite { get; set; }
    public string Requirement { get; set; }
    public string ProgressionUnlock { get; set; } 
        
    public string LeftOptionText { get; set; }
    public string RightOptionText { get; set; }
    
    public CardResult LeftOptionResult { get; set; }
    public CardResult RightOptionResult { get; set; }
    
    public Card[] LeftOptionPossibleContinuations { get; set; } // Могут быть одинаковыми, если выбор не решает
    public Card[] RightOptionPossibleContinuations { get; set; }
}

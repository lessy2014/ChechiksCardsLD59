using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DeckController : MonoBehaviour
{
    private Card CurrentCard { get; set; }
    private int CurrentCardNumber { get; set; }
    private Card[] RandomStartCards { get; set; }
    private Card[] StoryStartCards { get; set; }
    
    private Card HealthResetCard { get; set; }
    private Card ManaResetCard { get; set; }
    private HashSet<string> Unlockables { get; set; } = new();
    private string lastUnlockable = global::Unlockables.Jaba;
    
    [SerializeField] private TextMeshPro CardText;
    [SerializeField] private SpriteRenderer CardPicture;
    [SerializeField] private SpriteRenderer CardAnswerBackground;
    [SerializeField] private TextMeshPro CardAnswerText;
    
    private const int RandomCardsNumber = 5;
    
    [SerializeField] private PlayerController playerController;
    
    void Start()
    {
        CardPicture.color = Color.white;

        var resetCards = DeckSetup.ResetCardsSetup();
        HealthResetCard = resetCards.Item1;
        ManaResetCard = resetCards.Item2;

        // var debugCards = DeckSetup.DebugSetup();
        // RandomStartCards = debugCards.Item1;
        // StoryStartCards = debugCards.Item2;

        var normalCards = DeckSetup.Setup();
        RandomStartCards = normalCards.Item1;
        StoryStartCards = normalCards.Item2;
        
        var prologueCard = DeckSetup.PrologueSetup();
        CurrentCard = prologueCard;
        DrawCurrentCard();
    }

    public void ResetHealth()
    {
        CurrentCard = HealthResetCard;
        Reset();
    }

    public void ResetMana()
    {
        CurrentCard = ManaResetCard;
        Reset();
    }

    private void Reset()
    {
        DrawCurrentCard();
        CurrentCardNumber = -1;
    }

    public void HideAnswer()
    {
        CardAnswerBackground.gameObject.SetActive(false);
    }

    public void ShowLeftAnswer()
    {
        CardAnswerBackground.gameObject.SetActive(true);
        CardAnswerText.text = CurrentCard.LeftOptionText;
    }
    
    public void ChooseLeftOption()
    {
        ProcessCardResult(CurrentCard.LeftOptionResult);
        if (CurrentCardNumber == -1)
        {
            CurrentCardNumber++;
            return;
        }
        if (CurrentCard.LeftOptionPossibleContinuations is null || CurrentCard.LeftOptionPossibleContinuations.Length == 0)
        {
            ChooseNextRandomCard();
            return;
        }
        var nextCard = CurrentCard.LeftOptionPossibleContinuations[Random.Range(0, CurrentCard.LeftOptionPossibleContinuations.Length)];
        PlaceNextStoryCard(nextCard);
    }

    public void ShowRightAnswer()
    {
        CardAnswerBackground.gameObject.SetActive(true);
        CardAnswerText.text = CurrentCard.RightOptionText;
    }

    public void ChooseRightOption()
    {
        ProcessCardResult(CurrentCard.RightOptionResult);
        if (CurrentCardNumber == -1)
        {
            CurrentCardNumber++;
            return;
        }
        if (CurrentCard.RightOptionPossibleContinuations is null || CurrentCard.RightOptionPossibleContinuations.Length == 0)
        {
            ChooseNextRandomCard();
            return;
        }
        var nextCard = CurrentCard.RightOptionPossibleContinuations[Random.Range(0, CurrentCard.RightOptionPossibleContinuations.Length)];
        PlaceNextStoryCard(nextCard);
    }

    void ProcessCardResult(CardResult result)
    { 
        if (result is null)
            return;
        
        // TODO: предмет
        if (result.Unlockable is not null)
            Unlockables.Add(result.Unlockable);
        
        if (result.Item is not null)
            playerController.TryPutItemInInventory(result.Item);
        
        if (result.Hp != 0)
            playerController.ChangeHealth(result.Hp);
        
        if (result.Mana != 0)
            playerController.ChangeMana(result.Mana);
    }

    void ChooseNextRandomCard()
    {
        // TODO: в одном круге рандомных карт не должно идти одинаковых 
        
        if (CurrentCardNumber != 0 && CurrentCardNumber % RandomCardsNumber == 0 && !Unlockables.Contains(lastUnlockable))
        {
            CurrentCard = StoryStartCards[Random.Range(0, StoryStartCards.Length)];
            while (CurrentCard.Requirement is not null && !Unlockables.Contains(CurrentCard.Requirement) || Unlockables.Contains(CurrentCard.ProgressionUnlock))
            {
                CurrentCard = StoryStartCards[Random.Range(0, StoryStartCards.Length)];
            }
        }
        else
        {
            CurrentCard = RandomStartCards[Random.Range(0, RandomStartCards.Length)];
            while (CurrentCard.Requirement is not null && !Unlockables.Contains(CurrentCard.Requirement))
            {
                CurrentCard = RandomStartCards[Random.Range(0, RandomStartCards.Length)];
            }
        }

        CurrentCardNumber++;
        DrawCurrentCard();
    }
    
    void PlaceNextStoryCard(Card card)
    {
        CurrentCard = card;
        DrawCurrentCard();
    }

    void DrawCurrentCard()
    {
        CardText.text = CurrentCard.Text;
        CardPicture.sprite = CurrentCard.Sprite;
    }
}

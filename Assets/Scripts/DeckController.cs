using System;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
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
    private HashSet<string> Unlockables { get; set; } = new();
    
    [SerializeField] private TextMeshPro CardText;
    [SerializeField] private SpriteRenderer CardPicture;
    
    private const int RandomCardsNumber = 5;
    
    [SerializeField] private PlayerController playerController;
    
    void Start()
    {
        CardPicture.color = Color.white;
        
        DebugSetup();
    }
    
    public void ChooseLeftOption()
    {
        ProcessCardResult(CurrentCard.LeftOptionResult);
        if (CurrentCard.LeftOptionPossibleContinuations is null || CurrentCard.LeftOptionPossibleContinuations.Length == 0)
        {
            ChooseNextRandomCard();
            return;
        }
        var nextCard = CurrentCard.LeftOptionPossibleContinuations[Random.Range(0, CurrentCard.LeftOptionPossibleContinuations.Length)];
        PlaceNextStoryCard(nextCard);
    }

    public void ChooseRightOption()
    {
        ProcessCardResult(CurrentCard.RightOptionResult);
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
        
        if (result.Hp != 0)
            playerController.ChangeHealth(result.Hp);
        
        if (result.Mana != 0)
            playerController.ChangeMana(result.Mana);
    }

    void ChooseNextRandomCard()
    {
        // TODO: в одном круге рандомных карт не должно идти одинаковых 
        
        if (CurrentCardNumber != 0 && CurrentCardNumber % RandomCardsNumber == 0)
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

    void DebugSetup()
    {
        var randomCardEnd = new Card
        {
            Text = "Вот и сказочке конец, что бы ни выбрал - молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Canava")
        };
        var randomCard = new Card()
        {
            Text = "Налево нажмешь - конец истории получишь. Направо нажмешь - следующую карту получишь",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/PathStoneMeme"),
            LeftOptionPossibleContinuations = Array.Empty<Card>(),
            RightOptionPossibleContinuations = new Card[] { randomCardEnd }
        };

        var storyResult = new CardResult()
        {
            Unlockable = DefaultNamespace.Unlockables.Bober,
            Hp = -20,
            Mana = -50,
        };
        
        var storyCardEnd = new Card()
        {
            Text = "ЭТО СУПЕРРОФЛЗ. ОЧКО",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = DefaultNamespace.Unlockables.Bober,
            RightOptionResult = storyResult,
            LeftOptionResult = storyResult
        };
        
        var storyCard = new Card()
        {
            Text = "Это рофлз. Справа еще больше. Слева боюсь рофлзов",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = DefaultNamespace.Unlockables.Bober,
            RightOptionPossibleContinuations = new[] { storyCardEnd }
        };

        var story2Result = new CardResult()
        {
            Unlockable = DefaultNamespace.Unlockables.Orel,
            Hp = 10,
            Mana = 25,
        };
        
        var storyCard2 = new Card()
        {
            Requirement = DefaultNamespace.Unlockables.Bober,
            Text = "Если ты здесь, значит прошел рофлз вправо и получил очко. Молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MgeMachinegunner"),
            ProgressionUnlock = DefaultNamespace.Unlockables.Orel,
            LeftOptionResult = story2Result,
            RightOptionResult = story2Result
        };
        
        
        RandomStartCards = new[] { randomCard };
        StoryStartCards = new[] { storyCard, storyCard2 };

        CurrentCard = RandomStartCards[Random.Range(0, RandomStartCards.Length)];
        while (CurrentCard.Requirement is not null && !Unlockables.Contains(CurrentCard.Requirement))
        {
            CurrentCard = RandomStartCards[Random.Range(0, RandomStartCards.Length)];
        }
        DrawCurrentCard();
    }
}

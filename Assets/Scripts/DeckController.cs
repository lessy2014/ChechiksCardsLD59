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
    private string lastUnlockable = global::Unlockables.Orel;
    
    [SerializeField] private TextMeshPro CardText;
    [SerializeField] private SpriteRenderer CardPicture;
    [SerializeField] private SpriteRenderer CardAnswerBackground;
    [SerializeField] private TextMeshPro CardAnswerText;
    
    private const int RandomCardsNumber = 5;
    
    [SerializeField] private PlayerController playerController;
    public Dictionary<string, Item> allItems = new();
    
    void Start()
    {
        CardPicture.color = Color.white;
        
        ItemsSetup();
        ResetCardsSetup();
        DebugSetup();
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
        CurrentCardNumber = 0;
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

    void ItemsSetup()
    {
        var assets = Resources.LoadAll<Item>("Prefabs/Items");
        foreach (var asset in assets)
        {
            allItems.Add(asset.name.Split(".prefab")[0], asset);
        }
    }

    void ResetCardsSetup()
    {
        var result = new CardResult()
        {
            Hp = 70,
            Mana = 70
        };

        HealthResetCard = new Card()
        {
            Text = "Это был тяжелый день. Маленько устал",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"),
            RightOptionResult = result,
            LeftOptionResult = result,
        };

        ManaResetCard = new Card()
        {
            Text = "Это был долгий день. Маленько приуныл",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/sad gnome"),
            RightOptionResult = result,
            LeftOptionResult = result,
        };
    }
    
    void DebugSetup()
    {
        var randomResultRight = new CardResult()
        {
            Item = allItems["Mushroom0"]
        };
        var randomResultLeft = new CardResult()
        {
            Item = allItems["Mushroom1"]
        };
        var randomCardEnd = new Card
        {
            Text = "Вот и сказочке конец, что бы ни выбрал - молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Canava"),
            RightOptionResult = randomResultRight,
            LeftOptionResult = randomResultLeft,
            LeftOptionText = "Конец",
            RightOptionText = "Молодец"
        };
        var randomCard = new Card()
        {
            Text = "Налево нажмешь - конец истории получишь. Направо нажмешь - следующую карту получишь",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/PathStoneMeme"),
            LeftOptionPossibleContinuations = Array.Empty<Card>(),
            RightOptionPossibleContinuations = new[] { randomCardEnd },
            LeftOptionText = "Налево",
            RightOptionText = "Направо"
        };

        var storyResult = new CardResult()
        {
            Unlockable = global::Unlockables.Bober,
            Hp = -20,
            Mana = -50,
        };
        var storyCardEnd = new Card()
        {
            Text = "ЭТО СУПЕРРОФЛЗ. ОЧКО",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = global::Unlockables.Bober,
            RightOptionResult = storyResult,
            LeftOptionResult = storyResult
        };
        var storyCard = new Card()
        {
            Text = "Это рофлз. Справа еще больше. Слева боюсь рофлзов",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = global::Unlockables.Bober,
            RightOptionPossibleContinuations = new[] { storyCardEnd },
            LeftOptionText = "СТРАШНО",
            RightOptionText = "БОЛЬШЕ",
        };

        var story2Result = new CardResult()
        {
            Unlockable = global::Unlockables.Orel,
            Hp = 10,
            Mana = 25,
        };
        var storyCard2 = new Card()
        {
            Requirement = global::Unlockables.Bober,
            Text = "Если ты здесь, значит прошел рофлз вправо и получил очко. Молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MgeMachinegunner"),
            ProgressionUnlock = global::Unlockables.Orel,
            LeftOptionResult = story2Result,
            RightOptionResult = story2Result,
            LeftOptionText = "ЕЕЕЕЕЕЕЕЕЕЕЕЕЕ",
            RightOptionText = "РООООООООООООК"
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

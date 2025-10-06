using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckSetup
{ 
    public static Dictionary<string, Item> AllItems { get; private set; }

    static DeckSetup()
    {
        var assets = Resources.LoadAll<Item>("Prefabs/Items");
        AllItems = assets.ToDictionary(asset => asset.name.Split(".prefab")[0]);
    }

    public static Tuple<Card[], Card[]> Setup()
    {
        return new Tuple<Card[], Card[]>(RandomCardsSetup(), StoryCardsSetup());
    }

    public static Card[] RandomCardsSetup()
    {
        throw new Exception();
    }

    public static Card[] StoryCardsSetup()
    {
        throw new Exception();
    }
    /*
     
     
        var cardResult = new CardResult()
        {
            Hp = 50,
            Mana = -50,
            Item = AllItems["Mushroom0"],
            Unlockable = Unlockables.Orel
        };
        var nextCard = new Card();
        var card = new Card()
        {
            Text = "",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"),
            Requirement = Unlockables.Bober,
            ProgressionUnlock = Unlockables.Orel,
            LeftOptionText = "",
            RightOptionText = "",
            LeftOptionResult = cardResult,
            RightOptionResult = cardResult,
            LeftOptionPossibleContinuations = new[] { nextCard },
            RightOptionPossibleContinuations = Array.Empty<Card>()
        };
        
        
     */

    public static Card PrologueSetup()
    {
        var prologue1 = new Card()
        {
            Text = "Grand Wizard left at dawn, with comforting parting words \n\n- I believe in you. Trust yourself and the Forest. \n",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/start"),
            LeftOptionText = "I guess it is time to venture in to the Forest",
            RightOptionText = "It could not be hard to protect a forest, right?",
        };
        var prologue0 = new Card()
        {
            Text = "The starry night shines bright. You are sitting near a crackling fire, while your Master, Grand Wizard is waltzing around, picking random, at first sight, amulets and sticks. \n\n- I will be gone for a fortnight only, my young Apprentice. In the meantime, you should go out and learn the ins and outs of the Forest, as one day you’ll be the only one to protect it.\n",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/start"),
            LeftOptionText = "I understand, Master ",
            RightOptionText = "Why is it whenever I am given a task, you are always away?",
            LeftOptionPossibleContinuations = new[] { prologue1 },
            RightOptionPossibleContinuations = new[] { prologue1 }
        };
        return prologue0;
    }

    public static Tuple<Card, Card> ResetCardsSetup()
    {
        var result = new CardResult()
        {
            Hp = 70,
            Mana = 70
        };

        var healthResetCard = new Card()
        {
            Text = "Это был тяжелый день. Маленько устал",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"),
            RightOptionResult = result,
            LeftOptionResult = result,
        };

        var manaResetCard = new Card()
        {
            Text = "Это был долгий день. Маленько приуныл",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/sad gnome"),
            RightOptionResult = result,
            LeftOptionResult = result,
        };
        
        return new Tuple<Card, Card>(healthResetCard, manaResetCard);
    }
    
    public static Tuple<Card[], Card[]> DebugSetup()
    {
        var randomResultRight = new CardResult()
        {
            Item = AllItems["Mushroom0"]
        };
        var randomResultLeft = new CardResult()
        {
            Item = AllItems["Mushroom1"]
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
            Unlockable = Unlockables.Bober,
            Hp = -20,
            Mana = -50,
        };
        var storyCardEnd = new Card()
        {
            Text = "ЭТО СУПЕРРОФЛЗ. ОЧКО",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = Unlockables.Bober,
            RightOptionResult = storyResult,
            LeftOptionResult = storyResult
        };
        var storyCard = new Card()
        {
            Text = "Это рофлз. Справа еще больше. Слева боюсь рофлзов",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = Unlockables.Bober,
            RightOptionPossibleContinuations = new[] { storyCardEnd },
            LeftOptionText = "СТРАШНО",
            RightOptionText = "БОЛЬШЕ",
        };

        var story2Result = new CardResult()
        {
            Unlockable = Unlockables.Orel,
            Hp = 10,
            Mana = 25,
        };
        var storyCard2 = new Card()
        {
            Requirement = Unlockables.Bober,
            Text = "Если ты здесь, значит прошел рофлз вправо и получил очко. Молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MgeMachinegunner"),
            ProgressionUnlock = Unlockables.Orel,
            LeftOptionResult = story2Result,
            RightOptionResult = story2Result,
            LeftOptionText = "ЕЕЕЕЕЕЕЕЕЕЕЕЕЕ",
            RightOptionText = "РООООООООООООК"
        };

        return new Tuple<Card[], Card[]>(new[] { randomCard }, new[] { storyCard, storyCard2 });
    }
}
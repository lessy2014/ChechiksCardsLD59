using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeckSetup
{ 
    public static Dictionary<string, Item> AllItems { get; }

    static DeckSetup()
    {
        var assets = Resources.LoadAll<Item>("Prefabs/Items");
        AllItems = assets.ToDictionary(asset => asset.name.Split(".prefab")[0]);
    }

    public static Tuple<Card[], Card[]> Setup()
    {
        return new Tuple<Card[], Card[]>(RandomCardsSetup(), StoryCardsSetup());
    }

    private static Card[] RandomCardsSetup()
    {
        var randomCards = new List<Card>();
        
        // ГРИБЫ
        for (var i = 1; i < 4; i++)
        {
            var name = $"гриб {i}";
            var mushroomResult = new CardResult()
            {
                Item = GetItemDebug(name),
            };
            var mushroomCard = new Card()
            {
                Text = "The Forest is massive. There’s so many sounds and it feels truly magical. While walking you stumble upon a patch of mushrooms, growing in a shadow of a tree nearby.",
                Sprite = Resources.Load<Sprite>($"Arts/CardPictures/{name}"),
                LeftOptionText = "Pick some mushroom",
                RightOptionText = "Do nothing",
                LeftOptionResult = mushroomResult
            };
            randomCards.Add(mushroomCard);
        }
        
        // ОЗЕРО
        var lakeResult = new CardResult()
        {
            Hp = 10,
            Mana = 10
        };
        var lakeCard = new Card()
        {
            Text = "You stumble upon a lake, you see how beautiful the sun reflects on the water. Seeing this makes you wish to take a break and admire the majestic view.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/Lake"),
            LeftOptionText = "Take a break",
            RightOptionText = "Continue the journey ",
            LeftOptionResult = lakeResult
        };
        randomCards.Add(lakeCard);
        
        // КАМЕНЬ
        var rockResult = new CardResult()
        {
            Hp = 10
        };
        var rock1 = new Card()
        {
            Text = "You decided to name the rock Dwayne. It’s a cool name, right?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ROCK"),
            LeftOptionText = "It’s about drive",
            RightOptionText = "It’s about power",
            LeftOptionResult = rockResult,
            RightOptionResult = rockResult
        };
        var rock0 = new Card()
        {
            Text = "You find a rock. It’s a shiny rock. You don’t there’s anything magical in it but it looks cool. Do you want to take it with you?",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/ROCK"),
            LeftOptionText = "Take the rock",
            RightOptionText = "Do not take the rock",
            LeftOptionPossibleContinuations = new[] { rock1 }
        };
        randomCards.Add(rock0);
        
        // ПЕРЕКРЕСТОК И ПЕЩЕРА
        var caveFlowerResult = new CardResult()
        {
            Mana = 50
        };
        var caveSneakResult = new CardResult()
        {
            Item = GetItemDebug("HealthPotion")
        };
        var caveTeleportResult = new CardResult()
        {
            Hp = -15,
            Mana = -10,
        };
        var cave11Sneak = new Card()
        {
            Text =
                "You were close to waking up the beast a few times, but in the end made to the other side without any problems. There you could see trolls stash of food, from where you took a familiar looking vial of a health potion you lost a few weeks prior.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/CAVE POTION"),
            LeftOptionResult = caveSneakResult,
            RightOptionResult = caveSneakResult
        };
        var cave11Teleport = new Card()
        {
            Text =
                "Expecting to teleport past the troll, instead you find yourself standing right on his back. Suffice to say, the Troll did not enjoy his impromptu wake-up call.  Since he was having a good dream, he decided to just throw you out of the cave. Except for a few scratches and a beaten ego you were fine and continued your journey elsewhere",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/CAVE TELEPORT"),
            LeftOptionResult = caveTeleportResult,
            RightOptionResult = caveTeleportResult
        };
        var cave10 = new Card()
        {
            Text = "You are walking for a bit in the dark, only your staff emitting a small  light, when suddenly, there’s a flash of blue-ish light and you see that the whole floor is covered in flowers. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE FLOWERS"),
            LeftOptionText = "Pick a flower",
            RightOptionText = "Leave the cave",
            LeftOptionResult = caveFlowerResult
        };
        var cave11 = new Card()
        {
            Text =
                "As soon as you enter the cave you hear a loud snore vibrating through the cave. There’s a troll sleeping right in the middle of the cave. You think you see something past the giant sleeping figure and something tells you to check it out.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/TROLL SLEEPING "),
            LeftOptionText = "Sneak past",
            RightOptionText = "Teleport through",
            LeftOptionPossibleContinuations = new []{ cave11Sneak },
            RightOptionPossibleContinuations = new [] { cave11Teleport }
        };
        var cave0 = new Card()
        {
            Text = "You stand right before the cave entrance. You can’t see anything farther than your hand. Do you venture forth?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "Walk into the cave",
            RightOptionText = "Continue walking past cave",
            LeftOptionPossibleContinuations = new[] { cave10, cave11 }
        };
        var crossroad0 = new Card()
        {
            Text = "Your path leads you to a crossroads. Turning left you see a cave not that far from you. Turning right you see only hilly terrain and nothing else. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/CROSSROADS"),
            LeftOptionText = "Turn left",
            RightOptionText = "Turn right",
            LeftOptionPossibleContinuations = new[] { cave0 }
        };
        randomCards.Add(crossroad0);
        
        // ЛИСА
        var walkPastResult = new CardResult()
        {
            Hp = -50,
            Mana = -50,
        };
        var helpFoxSpellResult = new CardResult()
        {
            Mana = -20,
        };
        var podoroznikResult = new CardResult()
        {
            Item = GetItemDebug("podoroznik"),
        };
        var spellCard = new Card()
        {
            Text = "The healing spell worked. You lost some of your mana, but the fox is now healthy and definitely grateful to you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"), //TODO: ИМЯ СПРАЙТА,
        };
        var podoroznikCard = new Card()
        {
            Text =
                "You walk around searching for the healing herbs that grow around in the forest.  After sometime you find some and bring them to the fox. You attend to its wounds. The fox will have to rest for a bit, but now it is alright.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"), //TODO: ИМЯ СПРАЙТА
            LeftOptionText = "Take some of the herbs with you",
            RightOptionText = "Continue the journey",
            LeftOptionResult = podoroznikResult
        };
        var helpFox = new Card()
        {
            Text =
                "You know a few healing spells that may help the fox, but you are not sure. Maybe you can find something in the forest that will help? ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/tired gnome"), //TODO: ИМЯ СПРАЙТА
            LeftOptionText = "Use the spell",
            RightOptionText = "Find something to help",
            LeftOptionResult = helpFoxSpellResult,
            LeftOptionPossibleContinuations = new[] { spellCard },
            RightOptionPossibleContinuations = new[] { podoroznikCard }
        };
        var injuredFoxCard = new Card()
        {
            Text = "On your walk through the forest you find an injured fox. There’s no one around you and the fox definitely needs help. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"), //TODO: ИМЯ СПРАЙТА
            LeftOptionText = "Help the fox",
            RightOptionText = "Walk past it",
            RightOptionResult = walkPastResult,
            LeftOptionPossibleContinuations = new[] { helpFox },
        };
        randomCards.Add(injuredFoxCard);
        
        return randomCards.ToArray();
    }

    private static Card[] StoryCardsSetup()
    {
        var stories = new List<Card>();
        
        stories.Add(SetupAntEvent());
        
        return stories.ToArray();
    }

    private static Card SetupAntEvent()
    {
        var eventEndResult = new CardResult()
        {
            Unlockable = Unlockables.Ants
        };
        var eventEnd = new Card()
        {
            Text = "You receive the ability to summon ants",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Ant-tastic!",
            RightOptionText = "Splendid",
            LeftOptionResult = eventEndResult,
            RightOptionResult = eventEndResult,
        };
        var ant10Result = new CardResult()
        {
            Hp = -20,
        };
        Card redAnts = null;
        var blackAnotherSolution = new Card()
        {
            Text = "You do not want the fighting to continue. You remain silent, thinking of a way everything can be fixed. While you were in deep thoughts most of the Black Ants left, thinking you were not going to help. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Maybe speaking with the other side will help?",
            RightOptionText = "Maybe speaking with the other side will help?",
            LeftOptionPossibleContinuations = new[] { redAnts },
            RightOptionPossibleContinuations = new[] { redAnts },
        };
        var blackEnd = new Card()
        {
            Text =
                "The battle begins. Nobody is listening to you and they will not listen to you anytime soon. You pray nobody loses this fight. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "There’s nothing I can do for now ",
            RightOptionText = "There’s nothing I can do for now "
        };
        var acceptBlackResult = new CardResult()
        {
            Mana = 10, 
            Item = GetItemDebug("crystal")
        };
        var refuseBlackResult = new CardResult()
        {
            Hp = 10,
        };
        var blackGoodEnding = new Card()
        {
            Text =
                "Both Anton and Antei follow you on your journey through the field. One step at a time, the life around you blossoms with life, instead of dirt there is now fresh grass, wilted flowers are no longer wilted, flourishing in the sun. The era of peace is here. As a token of gratitude the Black Ants gift you a small crystal that they found sometime ago. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Accept the gift",
            RightOptionText = "Refuse the gift",
            LeftOptionResult = acceptBlackResult,
            RightOptionResult = refuseBlackResult,
            LeftOptionPossibleContinuations = new []{ eventEnd },
            RightOptionPossibleContinuations = new[] { eventEnd },
        };
        var blackStop = new Card()
        {
            Text =
                "You make a line in the dirt with your staff, grass starts to grow out on its edges.  For Ants it’s like a huge ravine and that makes them stop. You see as the leader of the Red Ants, Anton, makes it closer to the line, Antei doing the same from the other side. Anton speaks up. \n\n- Wizard! We have been fighting for so long that our field, our home, has not felt alive for many moons. We fight because there is nothing else. Will you help us?\n",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "- Certainly",
            RightOptionText = "Silently cast a spell",
            LeftOptionPossibleContinuations = new[] { blackGoodEnding },
            RightOptionPossibleContinuations = new[] { blackGoodEnding }
        };
        var blackReason = new Card()
        {
            Text =
                "There’s one last chance to try to talk the Black Ants out of the fighting, but you don’t get that chance because they have already climbed down and are in the middle of an intense battle. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Stop them",
            RightOptionText = "Do not stop them",
            LeftOptionPossibleContinuations = new[] { blackStop },
            RightOptionPossibleContinuations = new[] { blackEnd },
        };
        var helpBlack = new Card()
        {
            Text =
                "You help the Black Ants cross the field. On the way you see how much nature suffered in this conflict. It must be stopped. When you almost reach Red Ants anthill you see that they are already there and waiting for you. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Set Black Ants down",
            RightOptionText = "Try to reason with both sides",
            LeftOptionPossibleContinuations = new[] { blackEnd },
            RightOptionPossibleContinuations = new[] { blackReason },
        };
        var blackAnts = new Card()
        {
            Text = "You go to the Black Ants anthill. You are met with the presence of Antei - proud King and General of the Black Ants. He’s wearing armor out of some grass and flowers. \n\n- Greetings, o wise one! We are in a conflict with Red Ants. We are not at fault, and our defences are crumbling. Please, help us! Let me and my proud ants use your wisdom.\n",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Help the ants with negotiations",
            RightOptionText = "Think of another solution",
            LeftOptionPossibleContinuations = new[] { helpBlack },
            RightOptionPossibleContinuations = new[] { blackAnotherSolution }
        };

        var redAnotherSolutionResult = new CardResult()
        {
            Hp = -10,
        };
        var redAnotherSolution = new Card()
        {
            Text = "You want to stop fighting, not encourage it. The Red Ants did not appreciate your silence and in the end you got bit a few times. Time to speak to the other side of the conflict.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "Go speak with the Black Ants",
            RightOptionText = "Go speak with the Black Ants",
            LeftOptionResult = redAnotherSolutionResult,
            RightOptionResult = redAnotherSolutionResult,
            LeftOptionPossibleContinuations = new[] { blackAnts },
            RightOptionPossibleContinuations = new[] { blackAnts }
        };
        var redEnd = new Card()
        {
            Text = "The fighting begins anew. You can’t tell who’s winning or who’s losing. The only thing you know is that you won’t be able to reconcile the ants at this moment",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "I’ll come back later",
            RightOptionText = "There’s nothing I can do for now"
        };
        var tryToReasonResult = new CardResult()
        {
            Hp = -10,
        };
        var acceptRedResult = new CardResult()
        {
            Mana = 10,
            Item = GetItemDebug("bead")
        };
        var refuseRedResult = new CardResult()
        {
            Hp = 10,
        };
        var redGoodEnding = new Card()
        {
            Text =
                "Both Antei and Anton follow you on your journey through the field. One step at a time, the life around you blossoms with life, instead of dirt there is now fresh grass, wilted flowers are no longer wilted, flourishing in the sun. The era of peace is here. As a token of gratitude the Red Ants gift you a small magical bead that they found sometime ago. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "Accept the gift ",
            RightOptionText = "Refuse the gift ",
            LeftOptionResult = acceptRedResult,
            RightOptionResult = refuseRedResult,
            LeftOptionPossibleContinuations = new[] { eventEnd },
            RightOptionPossibleContinuations = new[] { eventEnd }
        };
        var redStopBattle = new Card()
        {
            Text =
                "You make a line in the dirt with your staff, grass starts to grow out on the edges.  For Ants it’s like a huge ravine and that makes them stop. You see as the leader of the Black Ants, Antei, makes it closer to the line, Anton doing the same from the other side. Antei speaks up. \n\n- O wise one, you possess a wonderful gift. Can you make our field bustle with life again? Our flowers wilted, animals left and there is nothing left for us but to fight each other.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "- I can and I will",
            RightOptionText = "Silently cast a spell",
            LeftOptionPossibleContinuations = new[] { redGoodEnding },
            RightOptionPossibleContinuations = new[] { redGoodEnding }
        };
        var tryToReason = new Card()
        {
            Text = "The moment you try to reason and bring both sides to peace, red ants start to bite you, making you drop them. As soon as they reach the ground they charge straight for black ants anthill",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "Stop them",
            RightOptionText = "Do not stop them",
            LeftOptionPossibleContinuations = new[] { redStopBattle },
            RightOptionPossibleContinuations = new[] { redEnd }
        };
        var helpRed = new Card()
        {
            Text =
                "You decided to help Red Ants. You help a small army of red ants cross the field in the matter of minutes, instead of days it would usually take them. When Black Ants see you, they start to scatter, trying to find anything to protect themselves and their home. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/AntEvent"),
            LeftOptionText = "Set Red Ants down",
            RightOptionText = "Try to reason with both sides",
            RightOptionResult = tryToReasonResult,
            LeftOptionPossibleContinuations = new[] { redEnd },
            RightOptionPossibleContinuations = new[] { tryToReason },
        };
        redAnts = new Card()
        {
            Text = "You go to the Red Ants anthill. You are met with the presence of Anton - King and General of the Red Ants. \n\n- I Greet you, Wizard of the Forest. We, Red Ants, are in a war with Black Ants. Those cowards don’t want to make it fair, hiding in their small holes, while my ants are suffering trying to breach their defences. If you would lead us we would end this once and for all!\n",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/AntEvent"),
            LeftOptionText = "Help Red Ants",
            RightOptionText = "Think of another solution",
            LeftOptionPossibleContinuations = new[] { helpRed },
            RightOptionPossibleContinuations = new[] { redAnotherSolution }
        };
        var ant2 = new Card()
        {
            Text =
                "On a closer look you see that there are definitely two sides to this battle. You see ants with red and black markings. Red ones seem a bit more aggressive, while black ones are more defensive. Being so “big” you can’t really understand who is winning. But you know the fighting must be stopped. Who do you talk to first?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/AntEvent"),
            LeftOptionText = "Red Ants",
            RightOptionText = "Black Ants",
            LeftOptionPossibleContinuations = new[] { redAnts },
            RightOptionPossibleContinuations = new[] { blackAnts }
        };
        var ant10 = new Card()
        {
            Text = "The Ants do not appreciate a wizard interrupting their feud. You won’t ever tell anyone about (especially your teacher) but ant bites, especially when there’s a LOT of them HURTS and no healing magic can help you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE"),
            LeftOptionText = "Ouch…",
            RightOptionText = "Try to speak to the ants, gently",
            LeftOptionResult = ant10Result,
            RightOptionResult = ant10Result,
            LeftOptionPossibleContinuations = new [] { ant2 },
            RightOptionPossibleContinuations = new [] { ant2 }
        };
        var ant1 = new Card()
        {
            Text = "You see that there is something moving on the field. Except that it is something small, hundreds and thousands of small dots, running around the field. You move closer. You see..ants? Ants that are fighting each other. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"),
            LeftOptionText = "Stop the fight",
            RightOptionText = "Try to speak to the ants ",
            LeftOptionPossibleContinuations = new Card[] { ant10 },
            RightOptionPossibleContinuations = new Card[] { ant2 }
        };
        var ant0 = new Card()
        {
            Text = "You walk onto a field. You know this field, it's one of your favourite places to rest between studying and studying. Different flowers grow here. Or rather, they used too. What you see now is a barren field, remaining flowers wilted. Whatever happened to this place? ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"), //TODO: НАЗВАНИЕ ВСЕЙ ИСТОРИИ!!!!!!!!!
            ProgressionUnlock = Unlockables.Ants,
            LeftOptionText = "Inspect the field ",
            RightOptionText = "Continue walking past the field",
            LeftOptionPossibleContinuations = new[] { ant1 },
            RightOptionPossibleContinuations = Array.Empty<Card>()
        };
        return ant0;
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
    
    private static Item GetItemDebug(string itemName) => AllItems.ContainsKey(itemName) ? AllItems[itemName] : AllItems["Mushroom0"]; // TODO: ФУНКЦИИ БЫТЬ НЕ ДОЛЖНО
}
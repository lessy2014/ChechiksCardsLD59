using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var name = $"ГРИБ {i}";
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/LAKE"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FOX HEALED SPELL"), //TODO: ИМЯ СПРАЙТА,
        };
        var podoroznikCard = new Card()
        {
            Text =
                "You walk around searching for the healing herbs that grow around in the forest.  After sometime you find some and bring them to the fox. You attend to its wounds. The fox will have to rest for a bit, but now it is alright.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FOX HEALED PODOROZNIK"), //TODO: ИМЯ СПРАЙТА
            LeftOptionText = "Take some of the herbs with you",
            RightOptionText = "Continue the journey",
            LeftOptionResult = podoroznikResult
        };
        var helpFox = new Card()
        {
            Text =
                "You know a few healing spells that may help the fox, but you are not sure. Maybe you can find something in the forest that will help? ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FOX INJURED"), //TODO: ИМЯ СПРАЙТА
            LeftOptionText = "Use the spell",
            RightOptionText = "Find something to help",
            LeftOptionResult = helpFoxSpellResult,
            LeftOptionPossibleContinuations = new[] { spellCard },
            RightOptionPossibleContinuations = new[] { podoroznikCard }
        };
        var injuredFoxCard = new Card()
        {
            Text = "On your walk through the forest you find an injured fox. There’s no one around you and the fox definitely needs help. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/FOX INJURED"),
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
        stories.Add(SetupBatStory());
        stories.Add(SetupDruidStory());
        stories.Add(SetupJabaStory());
        
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTS END"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTEI"),
            LeftOptionText = "Maybe speaking with the other side will help?",
            RightOptionText = "Maybe speaking with the other side will help?",
            LeftOptionPossibleContinuations = new[] { redAnts },
            RightOptionPossibleContinuations = new[] { redAnts },
        };
        var blackEnd = new Card()
        {
            Text =
                "The battle begins. Nobody is listening to you and they will not listen to you anytime soon. You pray nobody loses this fight. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP BLACK"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTS GOOD ENDING"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/BLACK STOP BATTLE"),
            LeftOptionText = "- Certainly",
            RightOptionText = "Silently cast a spell",
            LeftOptionPossibleContinuations = new[] { blackGoodEnding },
            RightOptionPossibleContinuations = new[] { blackGoodEnding }
        };
        var blackReason = new Card()
        {
            Text =
                "There’s one last chance to try to talk the Black Ants out of the fighting, but you don’t get that chance because they have already climbed down and are in the middle of an intense battle. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP BLACK"),
            LeftOptionText = "Stop them",
            RightOptionText = "Do not stop them",
            LeftOptionPossibleContinuations = new[] { blackStop },
            RightOptionPossibleContinuations = new[] { blackEnd },
        };
        var helpBlack = new Card()
        {
            Text =
                "You help the Black Ants cross the field. On the way you see how much nature suffered in this conflict. It must be stopped. When you almost reach Red Ants anthill you see that they are already there and waiting for you. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP BLACK"),
            LeftOptionText = "Set Black Ants down",
            RightOptionText = "Try to reason with both sides",
            LeftOptionPossibleContinuations = new[] { blackEnd },
            RightOptionPossibleContinuations = new[] { blackReason },
        };
        var blackAnts = new Card()
        {
            Text = "You go to the Black Ants anthill. You are met with the presence of Antei - proud King and General of the Black Ants. He’s wearing armor out of some grass and flowers. \n\n- Greetings, o wise one! We are in a conflict with Red Ants. We are not at fault, and our defences are crumbling. Please, help us! Let me and my proud ants use your wisdom.\n",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTEI"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTON"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP RED"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTS GOOD ENDING"),
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
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP RED"),
            LeftOptionText = "- I can and I will",
            RightOptionText = "Silently cast a spell",
            LeftOptionPossibleContinuations = new[] { redGoodEnding },
            RightOptionPossibleContinuations = new[] { redGoodEnding }
        };
        var tryToReason = new Card()
        {
            Text = "The moment you try to reason and bring both sides to peace, red ants start to bite you, making you drop them. As soon as they reach the ground they charge straight for black ants anthill",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/HELP RED"),
            LeftOptionText = "Stop them",
            RightOptionText = "Do not stop them",
            LeftOptionPossibleContinuations = new[] { redStopBattle },
            RightOptionPossibleContinuations = new[] { redEnd }
        };
        var helpRed = new Card()
        {
            Text =
                "You decided to help Red Ants. You help a small army of red ants cross the field in the matter of minutes, instead of days it would usually take them. When Black Ants see you, they start to scatter, trying to find anything to protect themselves and their home. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/HELP RED"),
            LeftOptionText = "Set Red Ants down",
            RightOptionText = "Try to reason with both sides",
            RightOptionResult = tryToReasonResult,
            LeftOptionPossibleContinuations = new[] { redEnd },
            RightOptionPossibleContinuations = new[] { tryToReason },
        };
        redAnts = new Card()
        {
            Text = "You go to the Red Ants anthill. You are met with the presence of Anton - King and General of the Red Ants. \n\n- I Greet you, Wizard of the Forest. We, Red Ants, are in a war with Black Ants. Those cowards don’t want to make it fair, hiding in their small holes, while my ants are suffering trying to breach their defences. If you would lead us we would end this once and for all!\n",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/ANTON"),
            LeftOptionText = "Help Red Ants",
            RightOptionText = "Think of another solution",
            LeftOptionPossibleContinuations = new[] { helpRed },
            RightOptionPossibleContinuations = new[] { redAnotherSolution }
        };
        var ant2 = new Card()
        {
            Text =
                "On a closer look you see that there are definitely two sides to this battle. You see ants with red and black markings. Red ones seem a bit more aggressive, while black ones are more defensive. Being so “big” you can’t really understand who is winning. But you know the fighting must be stopped. Who do you talk to first?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTS COLORS"),
            LeftOptionText = "Red Ants",
            RightOptionText = "Black Ants",
            LeftOptionPossibleContinuations = new[] { redAnts },
            RightOptionPossibleContinuations = new[] { blackAnts }
        };
        var ant10 = new Card()
        {
            Text = "The Ants do not appreciate a wizard interrupting their feud. You won’t ever tell anyone about (especially your teacher) but ant bites, especially when there’s a LOT of them HURTS and no healing magic can help you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/ANTS"),
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
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/ANTS"),
            LeftOptionText = "Stop the fight",
            RightOptionText = "Try to speak to the ants ",
            LeftOptionPossibleContinuations = new Card[] { ant10 },
            RightOptionPossibleContinuations = new Card[] { ant2 }
        };
        var ant0 = new Card()
        {
            Text = "You walk onto a field. You know this field, it's one of your favourite places to rest between studying and studying. Different flowers grow here. Or rather, they used too. What you see now is a barren field, remaining flowers wilted. Whatever happened to this place? ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/FIELD"), //TODO: НАЗВАНИЕ ВСЕЙ ИСТОРИИ!!!!!!!!!
            ProgressionUnlock = Unlockables.Ants,
            LeftOptionText = "Inspect the field ",
            RightOptionText = "Continue walking past the field",
            LeftOptionPossibleContinuations = new[] { ant1 },
            RightOptionPossibleContinuations = Array.Empty<Card>()
        };
        return ant0;
    }

    private static Card SetupBatStory()
    {
        Card mainHallFromGuards = null;
        Card kitchenFromGuards = null;
        Card kitchenFromMainHall = null;
        Card servantsRoom = null; 
        Card kingsQuarters = null;
        var bat0result = new CardResult()
        {
            Mana = -20,
        };
        var bat1SummonResult = new CardResult()
        {
            Mana = -30,
        };
        var bat11AnotherWayResult = new CardResult()
        {
            Mana = -10,
        };
        var walkingPenaltyResult = new CardResult()
        {
            Hp = -5,
        };
        var guardsRoomReStart = new Card()
        {
            Text =
                "You enter a room. It's guardsmen room again. The door behind you closes. You feel there’s something evil somewhere in the castle, but at this moment it’s very faint. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/guards"),
            LeftOptionText = "Turn left",
            RightOptionText = "Go straight",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { kitchenFromGuards },
            RightOptionPossibleContinuations = new[] { mainHallFromGuards }
        };
        var archives = new Card()
        {
            Text = "You see a room with dozens of scrolls laying around both floor and different tables. Something urges you to open one of the scrolls. The scroll says \n\n“Devs left a map hidden in the summary if you are struggling to find the end point. You are closer than you think! And because we said so we send you back to the guards room (start). Best of luck! <3”\n",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/archives"),
            LeftOptionText = "Can’t say no to the dev",
            RightOptionText = "Can’t say no to the dev",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { guardsRoomReStart },
            RightOptionPossibleContinuations = new[] { guardsRoomReStart },
        };
        var servantsTower = new Card()
        {
            Text =
                "You see a room with a staircase, leading very high up. You believe it is one of the guarding towers of the castle. The evil energy is almost nonexistent here. The door behind you suddenly closes.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/kitchen"),
            LeftOptionText = "Summon ants",
            RightOptionText = "Find a way out",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { servantsRoom },
            RightOptionPossibleContinuations = new[] { guardsRoomReStart }
        };
        var lightTheRoomResult = new CardResult()
        {
            Mana = -20,
        };
        var smallLightResult = new CardResult()
        {
            Mana = -5
        };
        var fightTheDarknessResult = new CardResult()
        {
            Hp = -15,
        };
        var summonAntsResult = new CardResult()
        {
            Mana = -10,
        };
        var run = new Card()
        {
            Text = "Deciding that your life is more important than some evil energy inside the castle you decide to make a run for it. Even if you got lost in the rooms for another hour or so. When you walk past the main gate it closes behind you. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MAIN GATE"),
            LeftOptionText = "Better luck next time",
            RightOptionText = "Better luck next time"
        };
        var batsResult = new CardResult()
        {
            Unlockable = Unlockables.Ants
        };
        var batsEnding = new Card()
        {
            Text = "You’ve gained the ability to summon a bat!",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/BAT END"),
            LeftOptionText = "Battastic!",
            RightOptionText = "Battacular",
            LeftOptionResult = batsResult,
            RightOptionResult = batsResult,
        };
        var antsNext = new Card()
        {
            Text =
                "- We really liked watching you from the shadows. You seem like a funny wizard to follow. We will leave the castle alone, if you allow us to help you on your journey.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/LIGHT ROOM BATS"),
            LeftOptionText = "I appreciate your help",
            RightOptionText = "I guess?",
            LeftOptionPossibleContinuations = new[] { batsEnding },
            RightOptionPossibleContinuations = new[] { batsEnding }
        };
        var ants = new Card()
        {
            Text = "Everyone underestimates ants. When there’s lots of them they are unstoppable. You see how the closest armor suit falls to the ground and you see…bats flying out of it? Bats???? And suddenly all of the armor suits fall to the ground and dozens of bats surround you.\n\n - Choose mercy, Wizard! We are proud bats, led by King Bathoven! We live by laughter, not slaughter. We use cursed armor to lure heroes here and scare them! The caste was like that already when we came. No one suffered ill fate from us.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/LIGHT ROOM BATS"),
            LeftOptionText = "",
            RightOptionText = "",
            LeftOptionPossibleContinuations = new []{ antsNext },
            RightOptionPossibleContinuations = new []{ antsNext }
        };
        var lightRoom = new Card()
        {
            Text = "Lighting up the room you can see that it looks like a typical throne room would: fancy, even if shabby carpet, suits of armor standing tall. And the dark figure, sitting on the throne. As soon as light reaches the dark figure it screeches. The next second you see how suits of armor around you come to life. And it is coming for YOUR life.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/LIGHT ROOM ATTACK"),
            LeftOptionText = "Run",
            RightOptionText = "Summon ants",
            RightOptionResult = summonAntsResult,
            LeftOptionPossibleContinuations = new[] { run },
            RightOptionPossibleContinuations = new[] { ants }
        };
        var fight = new Card()
        {
            Text = "You try to fight the darkness with your staff, swinging it around, hoping it would disperse. It does not.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DARK ROOM"),
            LeftOptionText = "Light the whole room",
            RightOptionText = "Light the whole room",
            LeftOptionResult = lightTheRoomResult,
            RightOptionResult = lightTheRoomResult,
            LeftOptionPossibleContinuations = new []{ lightRoom },
            RightOptionPossibleContinuations = new []{ lightRoom }
        };
        var smallLight = new Card()
        {
            Text = "Your staff shines a bit brighter, but it can’t handle the darkness. You see how with each second the light begins to fade more and more.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DARK ROOM"),
            LeftOptionText = "Try to fight the darkness",
            RightOptionText = "Try to cast a better spell",
            LeftOptionResult = fightTheDarknessResult,
            RightOptionResult = lightTheRoomResult,
            LeftOptionPossibleContinuations = new[] { fight },
            RightOptionPossibleContinuations = new[] { lightRoom }
        };
        var throneRoom = new Card()
        {
            Text = "This is it. Once you take a step into the throne room the evil energy surrounds you. It is dark. The darkest you’ve ever felt. You see some dark figure sitting on the throne, but you can’t even make a step without fearing that the dark will consume you.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DARK ROOM"),
            LeftOptionText = "Cast a small light",
            RightOptionText = "Light the whole room",
            LeftOptionResult = smallLightResult,
            RightOptionResult = lightTheRoomResult,
            LeftOptionPossibleContinuations = new Card[] { smallLight },
            RightOptionPossibleContinuations = new Card[] { lightRoom }
        };
        
        var towerFromKingsQuarters = new Card()
        {
            Text =
                "You see a room with a staircase, leading very high up. You believe it is one of the guarding towers of the castle. The evil energy is almost nonexistent here. The door behind you suddenly closes.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Summon ants",
            RightOptionText = "Find a way out",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { kingsQuarters },
            RightOptionPossibleContinuations = new[] { guardsRoomReStart }
        };
        kingsQuarters = new Card()
        {
            Text =
                "It feels like the king was close to his servants as it is his room you are entering next. There’s a huge bed, though it lays barren. You can make out some fancy clothes lying around but nothing else. You’ve never felt this much evil energy. As if its source is in the next room",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Go left",
            RightOptionText = "Go straight",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { throneRoom },
            RightOptionPossibleContinuations = new[] { towerFromKingsQuarters },
        };
        servantsRoom = new Card()
        {
            Text = "You enter a room with lots of cots lying around. There’s some commonfolk clothes thrown in the mix. It seems like you are in the servants quarters. You feel as if you are almost there. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Turn left",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new []{ kingsQuarters },
            RightOptionPossibleContinuations = new []{ servantsTower }
        };
        var library = new Card()
        {
            Text = "You enter a library. You see huge shelves left with books for no one to appreciate. The evil energy feels even closer.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Turn left",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { archives },
            RightOptionPossibleContinuations = new [] { servantsRoom }
        };
        var kitchenFromGuardsTower = new Card()
        {
            Text = "You see a room with a staircase, leading very high up. You believe it is one of the guarding towers of the castle. The evil energy is almost nonexistent here. The door behind you suddenly closes.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"), 
            LeftOptionText = "Summon ants",
            RightOptionText = "Find a way out",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { kitchenFromGuards },
            RightOptionPossibleContinuations = new []{ guardsRoomReStart }
        };
        var kitchenFromMainHallTower = new Card()
        {
            Text = "You see a room with a staircase, leading very high up. You believe it is one of the guarding towers of the castle. The evil energy is almost nonexistent here. The door behind you suddenly closes.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"), 
            LeftOptionText = "Summon ants",
            RightOptionText = "Find a way out",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { kitchenFromMainHall },
            RightOptionPossibleContinuations = new [] { guardsRoomReStart }
        };
        var guardsRoomFromMainHall = new Card()
        {
            Text =
                "You enter a room. It's guardsmen room again. The door behind you closes. You feel there’s something evil somewhere in the castle, but at this moment it’s very faint.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Go straight",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            RightOptionPossibleContinuations = new[] { kitchenFromGuards },
        };
        var mainHallFromKitchen = new Card()
        {
            Text = "You enter a large room, with a long dining table sitting right in the middle. You guess it once was a place of joy and good. The evil energy feels closer than before. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/DOOR"),
            LeftOptionText = "Go straight",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new Card[] { library },
            RightOptionPossibleContinuations = new Card[] { guardsRoomFromMainHall }
        };
        kitchenFromGuards = new Card()
        {
            Text = "You enter what seems to be the castle's kitchen. There’s no food here, so you can’t really hope to find a snack here. The evil energy is very faint here. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/DOOR"),
            LeftOptionText = "Go straight",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new Card[] { kitchenFromGuardsTower },
            RightOptionPossibleContinuations = new Card[] { mainHallFromKitchen }
        };
        var guardsRoomFromKitchen = new Card()
        {
            Text =
                "You enter a room. It's guardsmen room again. The door behind you closes. You feel there’s something evil somewhere in the castle, but at this moment it’s very faint.",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/DOOR"),
            LeftOptionText = "Turn left",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { mainHallFromGuards },
        };
        kitchenFromMainHall = new Card()
        {
            Text = "You enter what seems to be the castle's kitchen. There’s no food here, so you can’t really hope to find a snack here. The evil energy is very faint here. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/DOOR"),
            LeftOptionText = "Turn left",
            RightOptionText = "Turn right",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new Card[] { guardsRoomFromKitchen },
            RightOptionPossibleContinuations = new Card[] { kitchenFromMainHallTower }
        };
        mainHallFromGuards = new Card()
        {
            Text = "You enter a large room, with a long dining table sitting right in the middle. You guess it once was a place of joy and good. The evil energy feels closer than before. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/DOOR"),
            LeftOptionText = "Turn left",
            RightOptionText = "Go straight",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new Card[] { kitchenFromMainHall },
            RightOptionPossibleContinuations = new Card[] { library  }
        };
        var guardsRoomStart = new Card()
        {
            Text =
                "You enter a room. By the look of it, it looks like a guardsmen room. The door behind you closes. You feel there’s something evil somewhere in the castle, but at this moment it’s very faint. ",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MAIN GATE OPEN"),
            LeftOptionText = "Turn left",
            RightOptionText = "Go straight",
            LeftOptionResult = walkingPenaltyResult,
            RightOptionResult = walkingPenaltyResult,
            LeftOptionPossibleContinuations = new[] { kitchenFromGuards },
            RightOptionPossibleContinuations = new[] { mainHallFromGuards }
        };
        var bat11 = new Card()
        {
            Text =
                "Walking around the castle you find a ditch. It is mostly dried up, but there’s a certain smell in the air. You use the ditch to get in the castle but your ego (and your boots) suffered some damage. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/DITCH"),
            LeftOptionText = "You are in the castle",
            RightOptionText = "You are in the castle",
            LeftOptionResult = bat11AnotherWayResult,
            RightOptionResult = bat11AnotherWayResult,
            LeftOptionPossibleContinuations = new[] { guardsRoomStart },
            RightOptionPossibleContinuations = new[] { guardsRoomStart }
        };
        var bat1 = new Card()
        {
            Text =
                "The main gate is closed, but you can see the lever controlling the gates. You can’t reach it with your hands.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/MAIN GATE"),
            LeftOptionText = "Summon ants",
            RightOptionText = "Find another way",
            LeftOptionResult = bat1SummonResult,
            LeftOptionPossibleContinuations = new[] { guardsRoomStart },
            RightOptionPossibleContinuations = new[] { bat11 }
        };
        var bat0 = new Card()
        {
            Text =
                "The portal brings you straight to the main gate of a grand old castle, forgotten in the Forest for millennia, or even more. Although, right now it does not feel empty or forgotten. There’s something evil inside and it is your job to check it out. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/MAIN GATE"), //TODO: ДОБАВИТЬ НАЗВАНИЯ ВСЕЙ ИСТОРИИ
            Requirement = Unlockables.Ants,
            ProgressionUnlock = Unlockables.Bats,
            LeftOptionText = "Walk towards main gate",
            RightOptionText = "Turn around",
            RightOptionResult = bat0result,
            LeftOptionPossibleContinuations = new []{ bat1 },
        };
        return bat0;
    }

    private static Card SetupDruidStory()
    {
        var druid3Result = new CardResult()
        {
            Hp = 5,
            Mana = -30
        };
        var humResult = new CardResult()
        {
            Mana = 5,
        };
        var bearResult = new CardResult()
        {
            Unlockable = Unlockables.Mebeb
        };
        var beadEnding = new Card()
        {
            Text = "You’ve gained the ability to summon a bear!",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/BEAR END"),
            LeftOptionText = "Bearilliant",
            RightOptionText = "Bearrific",
        };
        var wizard = new Card()
        {
            Text =
                "The Druid is ecstatic to see its staff. You can see it in his eyes. \n\n- Thank you for your help! As a fellow druid, I can’t just let you go without a reward! Let me teach you the spell of how to summon a bear! \n",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE DRUID 3"),
            LeftOptionText = "Thank you for your wisdom",
            RightOptionText = "I guess if your companions don’t mind",
            LeftOptionPossibleContinuations = new[] { beadEnding },
            RightOptionPossibleContinuations = new[] { beadEnding }
        };
        var bat2 = new Card()
        {
            Text =
                "It does not take long for the bat to return, holding the lost staff in its claws. Even if it almost drops it a few times, you don’t say anything. In the end, you have the staff in your hands.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE BAT DRUID STAFF"),
            LeftOptionText = "Thank you, Bat",
            RightOptionText = "Let’s get it back to its owner",
            LeftOptionPossibleContinuations = new Card[] { wizard },
            RightOptionPossibleContinuations = new Card[] { wizard }
        };
        var bat0 = new Card()
        {
            Text = "The Bat appears right beside you, chirping happily. It knows what it needs to do. As it flies away, you sit on a tree stamp nearby and wait.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE BAT DRUID"),
            LeftOptionText = "Wait patiently",
            RightOptionText = "Hum a song",
            RightOptionResult = humResult,
            LeftOptionPossibleContinuations = new[] { bat2 },
            RightOptionPossibleContinuations = new[] { bat2 }
        };
        var staffToBatResult = new CardResult()
        {
            Hp = -10,
            Mana = -30
        };
        var goOutResult = new CardResult()
        {
            Hp = -30,
            Mana = -50
        };
        var staff = new Card()
        {
            Text = "It’s been almost a day since you started searching for the staff. You are tired, angry and honestly just hungry. Becoming a part of faes prank was not in your daily schedule. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE BAT NIGHT"),
            LeftOptionText = "I guess I have no choice but to summon the bat.",
            RightOptionText = "The wizard will have to find his staff himself, I am leaving.",
            LeftOptionResult = staffToBatResult,
            RightOptionResult = goOutResult,
            LeftOptionPossibleContinuations = new[] { bat0 },
        };
        var druid3 = new Card()
        {
            Text =
                "Leaving a cave you know that searching for a lost staff is not that easy. It may take quite some time before the task is finished.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE BAT"),
            LeftOptionText = "I choose you, Bat!",
            RightOptionText = "I will search for the staff myself",
            LeftOptionResult = druid3Result,
            LeftOptionPossibleContinuations = new[] { bat0 },
            RightOptionPossibleContinuations = new[] { staff }
        };
        var druid2 = new Card()
        {
            Text = "- There I was, just wandering about this majestic forest when suddenly, there was a weird flash and I woke up here! I may have been struck by misbehaving faes around here. Who knows! The bears were kind enough to let me rest and catch my breath, but I can’t seem to find my staff! Would you be so kind as to help me?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE DRUID 2"),
            LeftOptionText = "It is my job to help my fellow wizard!",
            RightOptionText = "Why is it always me that must help?...",
            LeftOptionPossibleContinuations = new[] { druid3 },
            RightOptionPossibleContinuations = new[] { druid3 }
        };
        var druid1 = new Card()
        {
            Text =
                "And there the Lone Druid was, surrounded by at least two large brown bears, sitting in the farthest corner of the cave. You can’t see him clearly but feel that he knows you are here. \n\n- Another wizard? How splendid! I was wondering when someone would notice me. Don’t mind my friends, they are just a tad bit protective!",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE DRUID 2"),
            LeftOptionText = "Greetings!",
            RightOptionText = "Why do wizards always end up in weird situations?",
            LeftOptionPossibleContinuations = new[] { druid2 },
            RightOptionPossibleContinuations = new[] { druid2 }
        };
        var druid0 = new Card()
        {
            Text =
                "Once you are in a cave you suddenly understand why there’s magic here. Another wizard, a druid, is here. You don’t know who or why, but you feel the need to be cautious.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/CAVE DRUID 1"),
            Requirement = Unlockables.Bats,
            ProgressionUnlock = Unlockables.Mebeb,
            LeftOptionText = "Proceed forth",
            RightOptionText = "Proceed forth",
            LeftOptionPossibleContinuations = new[] { druid1 },
            RightOptionPossibleContinuations = new[] { druid1 }
        };
        return druid0;
    }

    private static Card SetupJabaStory()
    { 
        var summonBearResult = new CardResult()
        {
            Mana = -30
        };
        var walkThroughResult = new CardResult()
        {
            Hp = -15
        };
        var askFrogResult = new CardResult()
        {
            Hp = -5
        };
        var sitFrogResult = new CardResult()
        {
            Mana = -5,
        };
        var bearSummonEnding = new CardResult()
        {
            Mana = -20,
            Hp = -20
        };
        var sleepResult = new CardResult()
        {
            Mana = 20,
            Hp = 20
        };
        var jabaEndingResult = new CardResult()
        {
            Unlockable = Unlockables.Jaba,
        };
        var jabaEnding = new Card()
        {
            Text = "You’ve gained the ability to summon a frog!",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG END"),
            LeftOptionText = "Kwa-kwa",
            RightOptionText = "Ribbit-ribbit",
            LeftOptionResult = jabaEndingResult,
            RightOptionResult = jabaEndingResult
        };
        var sleep = new Card()
        {
            Text = "You wake up feeling well rested. The unease that brought you here is no longer there. You open your eyes and the giant frog is also no longer there. You feel as if it was a lesson in patience.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/NO FROG"),
            LeftOptionText = "I guess it is time to get out of the black forest",
            RightOptionText = "Never question wisdom of a frog, huh",
            LeftOptionPossibleContinuations = new[] { jabaEnding },
            RightOptionPossibleContinuations = new[] { jabaEnding }
        };
        var jabaSit3 = new Card()
        {
            Text = "It’s dark now. You feel tired. Your eyes are slowly closing. You need to decide what to do. The frog still has not moved even an inch.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG5"),
            LeftOptionText = "Whatever, I am leaving",
            RightOptionText = "I’ll sleep here",
            RightOptionResult = sleepResult,
            RightOptionPossibleContinuations = new[] { sleep },
        };
        var bearSummon = new Card()
        {
            Text = "Bear truly tries to move the frog. He fails.The frog gets angry. One moment you are standing next to the frog, the next moment you are literally flying out of the dark forest borders. The frog did not like you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG LEAVE"),
            LeftOptionText = "Better luck next time",
            RightOptionText = "I guess we were bugging it too much",
            LeftOptionResult = bearSummonEnding,
            RightOptionResult = bearSummonEnding,
        };
        var jabaSit2 = new Card()
        {
            Text = "It’s been hours since you first got here. You’ve been sitting. Just sitting. Nothing is happening around you and you still can’t get past that frog.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG4"),
            LeftOptionText = "Summon a bear",
            RightOptionText = "Continue sitting",
            LeftOptionResult = summonBearResult,
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new [] { bearSummon },
            RightOptionPossibleContinuations = new[] { jabaSit3 }
            
        };
        var spell = new Card()
        {
            Text = "You try to use a simple spell. It literally bounces back from the frog, moving a nearby log. The frog continues to sit.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG3"),
            LeftOptionText = "Summon a bear",
            RightOptionText = "Continue sitting",
            LeftOptionResult = summonBearResult,
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new[] { bearSummon },
            RightOptionPossibleContinuations = new[] { jabaSit2 }
        };
        var jabaSit = new Card()
        {
            Text =
                "You sit next to the frog. You do not feel anything change. The frog continues to sit in your way, motionless.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG3"),
            LeftOptionText = "Try to move the frog with a spell",
            RightOptionText = "Sit next to the frog",
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new[] { spell },
            RightOptionPossibleContinuations = new[] { jabaSit2 }
        };
        Card jabaAsk = null;
        var jabaAskAgain = new Card()
        {
            Text = "The frog still sits. Unmoved. You are not sure even if it heard you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG2"),
            LeftOptionText = "Ask the frog to move ",
            RightOptionText = "Sit next to the frog",
            LeftOptionResult = askFrogResult,
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new[] { jabaAsk },
            RightOptionPossibleContinuations = new[] { jabaSit }
        };
        jabaAsk = new Card()
        {
            Text = "The frog continues to sit. Unmoved. You are not sure even if it heard you.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG2"),
            LeftOptionText = "Ask the frog to move ",
            RightOptionText = "Sit next to the frog",
            LeftOptionResult = askFrogResult,
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new []{ jabaAskAgain },
            RightOptionPossibleContinuations = new[] { jabaSit }
        };
        var jaba3 = new Card()
        {
            Text = "Each step you take makes you feel closer to whatever it is you are looking for. And now, there’s a giant frog sitting in your way. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/FROG"),
            LeftOptionText = "Ask the frog to move ",
            RightOptionText = "Sit next to the frog",
            LeftOptionResult = askFrogResult,
            RightOptionResult = sitFrogResult,
            LeftOptionPossibleContinuations = new []{ jabaAsk },
            RightOptionPossibleContinuations = new[] { jabaSit }
        };
        var jaba2 = new Card()
        {
            Text = "You stumble upon another thorny path. It’s not as bad as the previous one, you think you can pass through with no troubles.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/THORNS 2 "),
            LeftOptionText = "Summon bear",
            RightOptionText = "Walk through",
            LeftOptionResult = summonBearResult,
            RightOptionResult = walkThroughResult,
            LeftOptionPossibleContinuations = new[] { jaba3 },
            RightOptionPossibleContinuations = new[] { jaba3 }
        };
        var walkForwardResult = new CardResult()
        {
            Hp = 5,
            Mana = -10
        };

        var walk = new Card()
        {
            Text =
                "You feel as if you’ve been walking for hours and not even a step closer to that weird sensation of imbalance you felt earlier. As if the forest does not want you here. Suddenly you walk out on a sunny field. You hear birds, see flowers, as if you were not just in the middle of a dark forest. The path continues forward. Do you follow it?",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/SUS FIELD"),
            LeftOptionText = "Walk forward",
            RightOptionText = "Go back into the forest",
            LeftOptionResult = walkForwardResult,
            RightOptionPossibleContinuations = new[] { jaba2 },
        };
        var jaba1 = new Card()
        {
            Text = "Bear manages to strike through all of the thorns, leaving you a nice little path to follow. The search continues.",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/THORNS 2"),
            LeftOptionText = "Walk forward",
            RightOptionText = "Walk forward",
            LeftOptionPossibleContinuations = new[] { jaba2 },
            RightOptionPossibleContinuations = new[] { jaba2 }
        };
        var walkResult = new CardResult()
        {
            Hp = -20, Mana = -10
        };
        var jaba0 = new Card()
        {
            Text =
                "The more you traverse through dark forest, the more it seems impossible to take another step. Your path is blocked by a literal wall of thorns, you don’t believe you can just walk through it. ",
            Sprite = Resources.Load<Sprite>($"Arts/CardPictures/THORNS"), //TODO: ПОМЕНЯТЬ НАЗВАНИЕ ВО ВСЕЙ ИСТОРИИ
            Requirement = Unlockables.Mebeb,
            ProgressionUnlock = Unlockables.Jaba,
            LeftOptionText = "Try to walk around the wall",
            RightOptionText = "Summon bear",
            LeftOptionResult = walkResult,
            RightOptionResult = summonBearResult,
            LeftOptionPossibleContinuations = new[] { jaba1 },
            RightOptionPossibleContinuations = new[] { walk }
        };
        return jaba0;
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
            Text = "It was a tough day. I need some rest",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/tired gnome"),
            RightOptionResult = result,
            LeftOptionResult = result,
        };

        var manaResetCard = new Card()
        {
            Text = "It was a long day. I am not ready to push forward",
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
            Unlockable = Unlockables.Mebeb,
            Hp = -20,
            Mana = -50,
        };
        var storyCardEnd = new Card()
        {
            Text = "ЭТО СУПЕРРОФЛЗ. ОЧКО",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = Unlockables.Mebeb,
            RightOptionResult = storyResult,
            LeftOptionResult = storyResult
        };
        var storyCard = new Card()
        {
            Text = "Это рофлз. Справа еще больше. Слева боюсь рофлзов",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/Rofls"),
            ProgressionUnlock = Unlockables.Mebeb,
            RightOptionPossibleContinuations = new[] { storyCardEnd },
            LeftOptionText = "СТРАШНО",
            RightOptionText = "БОЛЬШЕ",
        };

        var story2Result = new CardResult()
        {
            Unlockable = Unlockables.Bats,
            Hp = 10,
            Mana = 25,
        };
        var storyCard2 = new Card()
        {
            Requirement = Unlockables.Mebeb,
            Text = "Если ты здесь, значит прошел рофлз вправо и получил очко. Молодец",
            Sprite = Resources.Load<Sprite>("Arts/CardPictures/MgeMachinegunner"),
            ProgressionUnlock = Unlockables.Bats,
            LeftOptionResult = story2Result,
            RightOptionResult = story2Result,
            LeftOptionText = "ЕЕЕЕЕЕЕЕЕЕЕЕЕЕ",
            RightOptionText = "РООООООООООООК"
        };

        return new Tuple<Card[], Card[]>(new[] { randomCard }, new[] { storyCard, storyCard2 });
    }
    
    private static Item GetItemDebug(string itemName) => AllItems.ContainsKey(itemName) ? AllItems[itemName] : AllItems["Mushroom0"]; // TODO: ФУНКЦИИ БЫТЬ НЕ ДОЛЖНО
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class commandExecute : MonoBehaviour {

	public Player player;
	public Story story;
	public BattleHandler battleHandler;
	public bool equipMenu;
	public bool battle;

	public Text displayedText;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		story = new Story(0,0);
		battleHandler = new BattleHandler();
		equipMenu = false;
		displayedText = GetComponent<Text>();
		displayedText.text = story.getStoryText();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void dispCommRes(string command)
	{
		command = command.ToLower();

		//Debug.Log(command);


		if (battle)
		{
			if(!battleHandler.doBattle(player,command))
			{
				battle = false;
				battleHandler.advanceBattleIdent();
				displayedText.text = story.getNextStoryStep();
			}
			if (battle)
			{
				displayedText.text = "Doing battle!!! \n\nEnemy: " + battleHandler.enemies[battleHandler.battleIdent].name;
				displayedText.text += "\nEnemy Health:  " + + battleHandler.enemies[battleHandler.battleIdent].health;
				displayedText.text += "\nEnemy Attack:  " + + battleHandler.enemies[battleHandler.battleIdent].attack;
				displayedText.text += "\nEnemy Defense: " + + battleHandler.enemies[battleHandler.battleIdent].defense;
				displayedText.text += "\n\n>attack >defend";
			}
		}



		if (!battle)
		{
			//Debug.Log (story.storyStep + " " + story.stepPart);
			if (command == "test")
			{
				displayedText.text = "Your test succeeded, Well Done";
			}
			else if (command == "equip")
			{
				displayedText.text = "equip what? \n";
				foreach (Item I in player.inventory.InventoryItems)
				{
					if (I != null)
						displayedText.text += I.Name + "\n";
				}
				equipMenu = true;
			}
			else if (equipMenu)
			{
				equipMenu = false;
				if (player.inventory.InventoryItems != null)
				{

					if (player.findItemWithName(command,player.inventory.InventoryItems) != null)
					{
						player.equipItem(player.findItemWithName(command,player.inventory.InventoryItems));
						displayedText.text += "\n\nequipped " + command + "!";
						if (command == "crown")
							displayedText.text += "\n\nYou see your surroundings light up, and feel a radiating wamrth from the crown!";
					}
					else 
					{
						displayedText.text += "\nitem not recognized!";
					}
				}
				else
				{
					displayedText.text += "\ninventory empty!";
				}

			}
			else if (command == "inventory")
			{
				displayedText.text = "Inventory: \n";
				foreach (Item I in player.inventory.InventoryItems)
				{
					if (I != null)
						displayedText.text += I.Name + "\n";
				}
				if (player.inventory.InventoryItems.Count == 0)
					displayedText.text += "empty!";
			}
			else if (command == "equipment" || command == "equipped")
			{
				displayedText.text = "Equpped Items: \n";
				if (player.equipment.EquipSlots.Count != 0)
				{
					foreach (equipSlot E in player.equipment.EquipSlots)
					{
						if (E.EquippedHere != null)
							displayedText.text += E.EquippedHere.Name + "\n";
					}
				}
				else 
				{
					displayedText.text += "nothing equipped!";
				}
			}
			else if (command == "stats")
			{
				displayedText.text = "player stats:\n";
				displayedText.text += "health  : " + player.stats.health;
				displayedText.text += "\nattack  : " + player.stats.attack;
				displayedText.text += "\ndefense : " + player.stats.defense;
			}
			//Begin Story Commands
			//continue if allowed by story and battles
			else if (command == "continue" && !(story.storyStep == 1 && story.stepPart == 4) && !(story.storyStep == 9 && (story.stepPart == 1 || story.stepPart == 2)) && !(story.storyStep == 10))
			{
				displayedText.text = story.getNextStoryStep();
			}
			else if (command == "story" || command == "look")
			{
				displayedText.text = story.getStoryText();
			}
			else if (story.storyStep == 1 && story.stepPart == 0)
			{
				if (command == "investigate")
				{
					displayedText.text = story.getNextStepPart();
				}
				else
					displayedText.text += "\nnot understood, try again";
			}
			else if (story.storyStep == 1 && (story.stepPart == 1 || story.stepPart == 2))
			{
				if (command == "check")
				{
					displayedText.text = story.getNextStepPart();
				}
				else
					displayedText.text += "\nnot understood, try again";
			}
			else if (story.storyStep == 1 && story.stepPart == 3)
			{
				if (command == "take")
				{
					displayedText.text = story.getNextStepPart();
				}
				else
					displayedText.text += "\nnot understood, try again";
			}
			else if (story.storyStep == 1 && story.stepPart == 4)
			{
				if (command == "take")
				{
					displayedText.text = story.getNextStepPart();
					player.addItemtoInventory(player.Loot[0]);
					player.stats.health -= 5;
				}
				else if (command == "leave")
				{
					displayedText.text = story.getStorySpecific(1,6);
				}
				else
					displayedText.text += "\nnot understood, try again";
			}
			if (story.storyStep == 2 && story.stepPart == 0)
			{
				if (player.stats.hasLight)
					displayedText.text = story.getNextStoryStep(); // go to light description
				else 
					displayedText.text = story.getStorySpecific(4,0); //go to Step after light description 
			}
			if (story.storyStep == 4 && story.stepPart == 0)
			{
				if (player.stats.hasLight)
					displayedText.text = story.getNextStoryStep();
				else 
					displayedText.text = story.getStorySpecific(6,0);
			}
			if (story.storyStep == 5 && story.stepPart == 0)
			{
				if (command == "loot")
				{
					displayedText.text = story.getNextStepPart();
					player.stats.attackBonus = 3;
					player.stats.attackBonusReset = 3;
				}
			}
			if (story.storyStep == 6 && story.stepPart == 0)
			{
				if (command == "search")
				{
					displayedText.text = story.getNextStepPart();
					player.addItemtoInventory(player.Loot[1]);
				}
			}
			if (story.storyStep == 7 && story.stepPart == 0)
			{
				
				displayedText.text += "\nName: " + battleHandler.enemies[0].name;
				displayedText.text += "\nHealth: " + battleHandler.enemies[0].health;
				displayedText.text += "\nAttack: " + battleHandler.enemies[0].attack;
				
				displayedText.text += "\nDefense: " + battleHandler.enemies[0].defense;

				
				displayedText.text += "\n>Attack >defend";
				battle = true;
			}
			if (story.storyStep == 8 && story.stepPart == 0)
			{
				player.addItemtoInventory(player.Loot[2]); //gained shortsword!
			}

			if (story.storyStep == 9 && story.stepPart == 0)
			{
				if (!player.stats.hasLight)
				{
					displayedText.text = story.getStorySpecific(10,0);
				}

//				if (command == "continue")
//				{
//					displayedText.text = story.getStorySpecific(10,0);
//				}
				if (command == "disarm" && player.stats.hasLight)
				{
					displayedText.text = story.getNextStepPart();
				}
			}
			if (story.storyStep == 9 && story.stepPart == 1)
			{
				if (command == "continue")
					displayedText.text = story.getStorySpecific(11,0);
				if (command == "loot")
				{
					player.addItemtoInventory(player.Loot[3]); //got armor!
					displayedText.text = story.getNextStepPart();
				}
			}
			if (story.storyStep == 9 && story.stepPart == 2)
			{
				if (command == "continue")
					displayedText.text = story.getStorySpecific(11,0);
			}

			if (story.storyStep == 10 && story.stepPart == 0)
			{

				story.getNextStepPart();

			
			}
			if (story.storyStep == 10 && story.stepPart == 1)
			{
				if (command == "restart")
				{
					player.stats.isAlive = false;
				}
			}

		}
		if (!player.stats.isAlive) //if player is dead then reset the game
		{
			battle = false;
			displayedText.text = "You died!\n" + story.getStorySpecific(0,0);
			player.inventory.InventoryItems.Clear();
			battleHandler.battleIdent = 0;
			foreach (Enemy E in battleHandler.enemies)
			{
				E.health = E.initialHealth;
			}
			foreach(equipSlot x in player.equipment.EquipSlots)
			{
				x.EquippedHere = null;
			}
			player.stats.reset();
		}
	}
}

public class BattleHandler
{
	public List<Enemy> enemies = new List<Enemy>();
	public int battleIdent = 0;

	public BattleHandler()
	{
		enemies.Add(new Enemy("goblin",10,11,4)); //health should be 50
	}

	public bool doBattle(Player player, string command)
	{
		if (command == "attack")
		{
			player.stats.attackBonus = player.stats.attackBonusReset;
			enemies[battleIdent].health -= player.stats.attack;
			player.stats.health -= enemies[battleIdent].attack;
		}
		if (command == "defend")
		{
			if (player.stats.attackBonus < 10)
				player.stats.attackBonus += (int)Random.Range(1,4);
			if (enemies[battleIdent].attack-player.stats.defense  > 0)
				player.stats.health -= (enemies[battleIdent].attack-player.stats.defense);
		}
		if (player.stats.health <= 0)
		{
			player.stats.isAlive = false;
		}

		if (enemies[battleIdent].health <= 0)
		{
			return false;
		}
		else 
		{
			return true;
		}


	}

	public void advanceBattleIdent()
	{
		battleIdent+=1;
	}
	
}

public class Enemy
{
	public string name;
	public int health;
	public int attack;
	public int defense;
	public int initialHealth;


	public Enemy(string Name, int Health, int Attack, int Defense)
	{
		this.name = Name;
		this.health = Health;
		this.initialHealth = Health;
		this.attack = Attack;
		this.defense = Defense;
	}
}

public class Story
{
	public int storyStep;
	public int stepPart;
	public string[,] storyText = new string[30,30];

	public Story(int step, int steppart)
	{
		this.storyStep = step;
		this.stepPart = steppart;
		//Beginning Step
		storyText[0,0] = "You find yourself in a dark hallway. The cold stone presses itself against your knees as you struggle to stand. You see a distant light... \n\n >continue";

		//Bug Panel Step
		storyText[1,0] = "You manage to gain your feet, and continue down the hallway towards the faint light. You hear small scratching noises all around you. \n\n >investigate >continue";
		storyText[1,1] = "Moving slowly closer to the wall on your right to investigate the sound, you realize what you hear is the muffled sound of an insect hive within the wall. \n\n >check >continue";
		storyText[1,2] = "You slide your hands along the edges of the smooth and slimy stone the wall is made of. The stone is cold, and as you move your hands across it, you come upon a crack. \n\n >check >continue";
		storyText[1,3] = "You slowly slide your fingers into the crack... \nYou find a switch!\n*CLICK*\n--WHOOSH--\nA panel slides open, revealing a cavity in the wall. The noise of the insects grows considerably.\n You cannot see well, but sense that the cavity is rather small. A small glint of gold near the floor catches your eye.\n\n >take >continue";
		storyText[1,4] = "You bend down to grasp the golden object, holding the panel frame with your other hand to steady yourself. You reach down and wrap your fingers slowly around a crown.\nBefore you can pick it up, you feel hundreds of tiny legs begin to cling to you and swarm up your arm. \n\n!!!>take !!!>leave";

		storyText[1,5] = "Instinctively you jerk your hand back fast, still gripping the crown. You frantically shake off the insects that are still clinging to your arm. \nYou feel a sharp pain in your right arm as the last insect tries to latch on to you with its fangs. You snatch it off of your arm, and throw it away. \n>continue";
		storyText[1,6] = "Instinctively, you jerk your hand back fast, dropping the crown. As the crown hits the ground, it is consumed by the rush of insects flowing away from you. \n>continue";


		//Step Bug Panel default
		storyText[2,0] = "You stumble backwards from the panel, falling down after hitting your head on the opposite wall. \n\n>continue";


		//Step Bug Panel Light
		storyText[3,0] = "As the crown illuminates the panel, you see thousands of beady eyed insects retreat into the crevices in the wall, revealing a half decayed skeleton of an adventurer. The remains are sitting directly across from you. The adventurer's eyes are pits, and is wearing a smile too wide to be pleasant. \n\n>continue  ";

		//Step filler 
		storyText[4,0] = "The dim light at the end of the hallway grows nearer. \n\n>continue";

		//Step finds body
		storyText[5,0] = "You find a body lying on the ground off to the side of the hallway. \n\n>loot >continue";
		storyText[5,1] = "You run your hands along the pockets of the corpe's armor to search for anything useful. You find a small green gem that seems to electrify your arm as you roll it around in your palm. \nThe gem dissolves in your hand and the remaining dust melds into your skin. The emerald hue of the crystal travels up your arm until you feel a burning sensation surge to your heart.\n\n>continue";

		//step crates
		storyText[6,0] = "You continue down the hallway. The dim light at the end of the hallway grows nearer still. You come across a stack of crates.\n\n>search >continue";
		storyText[6,1] = "You find a lantern and the means to light it!\n\n>continue";

		//step first battle!
		storyText[7,0] = "As you continue along the passageway you come upon a goblin blocking your path!\n\n";

		//step battle conclusion
		storyText[8,0] = "the battle is over! You won and gained shortsword!\n\n>continue";

		//step trap if light
		storyText[9,0] = "You spot a trap! The trap has already killed a previous adventurer, and if disarmed, you may be able to loot their armor.\n\n>disarm >continue";
		storyText[9,1] = "You disarmed the trap! \n\n>loot >continue";
		storyText[9,2] = "You looted the body and got armor! \n\n>continue";

		//step trap if no light
		storyText[10,0] = "You Died in a trap you twat.\n\n>restart";
		storyText[10,1] = "BLERGH";

		//step got past trap
		storyText[11,0] = "Continued past the trap\n\n>continue";

	}

	public string getStoryText()
	{
		return storyText[storyStep,stepPart];
	}

	public string getNextStoryStep()
	{
		storyStep += 1;
		stepPart = 0;
		return storyText[storyStep,0];
	}

	public string getNextStepPart()
	{
		stepPart += 1;
		return storyText[storyStep,stepPart];
	}

	public string getStorySpecific(int step, int part)
	{
		storyStep = step;
		stepPart = part;
		return storyText[step,part];
	}

}


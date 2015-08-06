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
				displayedText.text = "Doing battle!!! \n\n Enemy: " + battleHandler.enemies[battleHandler.battleIdent].name;
				displayedText.text += "\nEnemy Health:  " + + battleHandler.enemies[battleHandler.battleIdent].health;
				displayedText.text += "\nEnemy Attack:  " + + battleHandler.enemies[battleHandler.battleIdent].attack;
				displayedText.text += "\nEnemy Defense: " + + battleHandler.enemies[battleHandler.battleIdent].defense;
				displayedText.text += "\n\n>attack >defend";
			}
		}



		if (!battle)
		{
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
							displayedText.text += "\n\nYou see your surroundings light up!";
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
			else if (command == "continue" && !(story.storyStep == 1 && story.stepPart == 4))
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
			if (story.storyStep == 3 && story.stepPart == 0)
			{
				Debug.Log("story Step 3, part 0");
				battle = true;
			}

		}
	}
}

public class BattleHandler
{
	public List<Enemy> enemies = new List<Enemy>();
	public int battleIdent = 0;

	public BattleHandler()
	{
		enemies.Add(new Enemy("goblin",80,2,4));
	}

	public bool doBattle(Player player, string command)
	{
		if (command == "attack")
		{
			player.stats.attackBonus = 0;
			enemies[battleIdent].health -= player.stats.attack;
			player.stats.health -= enemies[battleIdent].attack;
		}
		if (command == "defend")
		{
			player.stats.attackBonus += (int)Random.Range(1,4);
			if (enemies[battleIdent].attack-player.stats.defense  > 0)
				player.stats.health -= (enemies[battleIdent].attack-player.stats.defense);
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


	public Enemy(string Name, int Health, int Attack, int Defense)
	{
		this.name = Name;
		this.health = Health;
		this.attack = Attack;
		this.defense = Defense;
	}
}

public class Story
{
	public int storyStep;
	public int stepPart;
	public string[,] storyText = new string[10,10];

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


		//Step
		storyText[2,0] = "This is a test battle\n\n>continue";


		//Step BATTLE TEST
		storyText[3,0] = "battle description \n\n >attack >defend";

		//Step
		storyText[4,0] = "battle is over!";

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
		return storyText[step,part];
	}

}


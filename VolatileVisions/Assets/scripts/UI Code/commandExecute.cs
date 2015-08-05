using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class commandExecute : MonoBehaviour {

	public Player player;
	public bool equipMenu;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		equipMenu = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void dispCommRes(string command)
	{
		command = command.ToLower();
		Text displayedText = GetComponent<Text>();
		//Debug.Log(command);


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
		else 
		{
			displayedText.text = "not understood, try again";
		}
	}
}

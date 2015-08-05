/*
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class playerSheet : MonoBehaviour 
{

	//Declare an instance of playerStats
	private playerStats stats =  new playerStats();
	public playerEquipment equipment = new playerEquipment();
	public playerInventory inventory = new playerInventory();


	public void logStats ()
	{
		//Debug.Log()
	}

	public void equipItem(Item item)
	{
		for (int i=0;i<5;i++)
		{
			if (item.Name == inventory.InventoryItems[i].Name)
			{
				for (int j = 0; j<4;j++)
				{
					if (item.SlotName == equipment.EquipSlots[j].SlotName)
					{
						if (equipment.EquipSlots[j].EquippedHere == null)
						{
							equipment.EquipSlots[j].EquippedHere = item;
							//inventory.InventoryItems[2];
						}
					}
				}
			}
		}
	}

	public void Update()
	{

		//Debug.Log(inventory.InventoryItems[1].Name);
	}
}

public class playerStats
{

}

public class playerInventory
{

	public Item[] InventoryItems =  new Item[5];

	public playerInventory() //hard coded constructor
	{
		InventoryItems[0] = new Item("",0,0,"head");
		InventoryItems[1] = new Item("testWeapon",3,0,"sword");
		InventoryItems[2] = new Item("",0,0,"dagger");
		InventoryItems[3] = new Item("",0,0,"armor");
	}
	

}

public class playerEquipment
{
	public equipSlot[] EquipSlots = new equipSlot[4];
	public playerEquipment()
	{
		EquipSlots[0] = new equipSlot(null, "head");
		EquipSlots[1] = new equipSlot(null, "sword"); //horrible need to use a list
		EquipSlots[2] = new equipSlot(null, "dagger");
		EquipSlots[3] = new equipSlot(null, "armor");
	}

	
}

public class equipSlot 
{
	public Item EquippedHere;
	public string SlotName;
	public equipSlot(Item equippedHere, string slotName)
	{
		EquippedHere = equippedHere;
		SlotName = slotName;
	}
}

public class Item 
{

	public string Name;
	public int Attack;
	public int Defense;
	public string SlotName;

	public Item(string name, int attack, int defense, string slotName)
	{
		this.Name = name; 
		this.Attack = attack;
		this.Defense = defense;
		this.SlotName = slotName;
	}

}

*/
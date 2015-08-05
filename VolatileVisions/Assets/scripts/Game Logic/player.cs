﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This file will hand the player's inventory and equipment, using Lists to contain them.
/// </summary>


public class player : MonoBehaviour {

	//public playerStats stats;
	public playerEquipment equipment = new playerEquipment();
	public playerInventory inventory = new playerInventory();
	public List<Item> Loot = new List<Item>();

	void Start () 
	{
		Loot.Add(new Item("Megasword", 2, 2, SlotType.sword));
	}
	

	void Update () 
	{
		foreach (equipSlot F in equipment.EquipSlots)
		{
			if (F.EquippedHere != null)
				Debug.Log(F.EquippedHere.Name);
		}

		if (Input.GetKeyUp(KeyCode.Z))
		{
			addItemtoInventory(Loot[0]);
		}
		if (Input.GetKeyUp(KeyCode.Space))    
		{
			equipItem(findItemWithName("Megasword",inventory.InventoryItems));
		}

	}

	void addItemtoInventory(Item item)
	{
		inventory.InventoryItems.Add(item);
	}
	void removeItemfromInventory(Item item)
	{
		inventory.InventoryItems.Remove(item);
	}

	
	Item findItemWithName(string name, List<Item> list)
	{
		foreach (Item J in list)
		{
			if (name == J.Name)
				return J;
		}
		return null;
	}

	void equipItem(Item item)
	{

		//if inventory contains this item
		if (inventory.InventoryItems.Contains(item))
		{
			//remove the item of that name from the inventory
			inventory.InventoryItems.RemoveAll(delegate(Item x) {return x.Name == item.Name;});

			//check which slot it matches
			foreach (equipSlot i in equipment.EquipSlots)
			{
				if (item.itemSlotType == i.equipSlotType) //for the slot it matches
				{
					i.EquippedHere = item;				//equip the item
				}
			}
		}

	}

	void unEquipItem(Item item)
	{

	}


}

public class playerStats
{
	//public int calculateStat()
	//{
	//
	//}
}




public class playerInventory
{
	public List<Item> InventoryItems = new List<Item>();

	public playerInventory()
	{
		InventoryItems.Add( new Item("",0,0,SlotType.head));
		InventoryItems.Add( new Item("",0,0,SlotType.armor));
		InventoryItems.Add( new Item("",0,0,SlotType.sword));
		InventoryItems.Add( new Item("",0,0,SlotType.dagger));
	}
}





public class playerEquipment
{
	public List<equipSlot> EquipSlots = new List<equipSlot>();

	public playerEquipment()
	{
		EquipSlots.Add (new equipSlot(null,SlotType.head));
		EquipSlots.Add (new equipSlot(null,SlotType.armor));
		EquipSlots.Add (new equipSlot(null,SlotType.sword));
		EquipSlots.Add (new equipSlot(null,SlotType.dagger));
	}
	
	
}




//This class defines an Item, and gives it the appropriate stats
public class Item 
{
	
	public string Name;
	public int Attack;
	public int Defense;
	//public string SlotName; //now attempting to use enum rather than string
	public SlotType itemSlotType;

	public Item(string name, int attack, int defense, SlotType slotType)
	{
		this.Name = name; 
		this.Attack = attack;
		this.Defense = defense;
		this.itemSlotType = slotType;
	}
}


//This class defines an equipment slot 
public class equipSlot 
{
	public Item EquippedHere;
	//public string SlotName;
	public SlotType equipSlotType;
	

	public equipSlot(Item equippedHere, SlotType slotType)
	{
		this.EquippedHere = equippedHere;
		this.equipSlotType = slotType;
	}
}

//This defines all of the slot types that an item or slot can have
public enum SlotType
{
	head,
	sword,
	dagger,
	armor
}
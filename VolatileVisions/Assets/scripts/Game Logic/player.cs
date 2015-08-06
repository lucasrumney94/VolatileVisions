using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This file will hand the player's inventory and equipment, using Lists to contain them.
/// </summary>


public class Player : MonoBehaviour {

	//public playerStats stats;
	public playerEquipment equipment = new playerEquipment();
	public playerInventory inventory = new playerInventory();
	public playerStats stats;

	public List<Item> Loot = new List<Item>();

	void Start () 
	{
		stats = new playerStats(equipment);
		Loot.Add(new Item("crown", 0, 3, SlotType.head, true));
		Loot.Add(new Item("megasword", 2, 3, SlotType.sword, false));
		Loot.Add(new Item("weaksword", 1, 0, SlotType.sword, false));
		Loot.Add(new Item("crappyhat", 0, 1, SlotType.head, false));
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
			addItemtoInventory(Loot[1]);//needs to be setup for plaintext
			addItemtoInventory(Loot[2]);

		}
		if (Input.GetKeyUp(KeyCode.Space))    
		{
			equipItem(findItemWithName("megasword",inventory.InventoryItems));
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			stats.calculateStats();
			Debug.Log("attack is " + stats.attack);
			Debug.Log("defense is " + stats.defense);
		}
		if (Input.GetKeyUp (KeyCode.I))
		{
			foreach (Item L in inventory.InventoryItems)
			{	
				if (L != null)
					Debug.Log(L.Name);
			}
		}


	}

	public void addItemtoInventory(Item item)
	{
		inventory.InventoryItems.Add(item);
	}
	public void removeItemfromInventory(Item item)
	{
		inventory.InventoryItems.Remove(item);
	}


	public Item findItemWithName(string name, List<Item> list)
	{
		foreach (Item J in list)
		{
			if (J != null)
			{
				if (name == J.Name)
					return J;
			}
		}
		return null;
	}

	public void equipItem(Item item)
	{
		if (item != null)
		{
			//if inventory contains this item
			if (inventory.InventoryItems.Contains(item))
			{
				//remove the item of that name from the inventory
				inventory.InventoryItems.RemoveAll(s => s != null && s.Name == item.Name);

				//check which slot it matches
				foreach (equipSlot i in equipment.EquipSlots)
				{
					if (item.itemSlotType == i.equipSlotType) //for the slot it matches
					{
						addItemtoInventory(i.EquippedHere);	//return the equipped item to inventory
						i.EquippedHere = item;				//equip the item
					}
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
	public playerEquipment equipment;
	private int attackBase = 10;
	private int defenseBase = 10;
	public int attack;
	public int defense;
	public int health = 100;


	public playerStats(playerEquipment Equipment)
	{
		this.equipment = Equipment;
	}

	public void calculateStats()
	{
		attack = attackBase;
		defense = defenseBase;

		foreach (equipSlot K in equipment.EquipSlots)
		{
			if (K.EquippedHere != null)
			{
				attack += K.EquippedHere.Attack;
				defense += K.EquippedHere.Defense;
			}
		}
	}
}




public class playerInventory
{
	public List<Item> InventoryItems = new List<Item>();

	public playerInventory()
	{
		/*
		InventoryItems.Add( new Item("",0,0,SlotType.head)); //dont think these are necessary?
		InventoryItems.Add( new Item("",0,0,SlotType.armor));
		InventoryItems.Add( new Item("",0,0,SlotType.sword));
		InventoryItems.Add( new Item("",0,0,SlotType.dagger));
		*/
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
	public SlotType itemSlotType;
	public bool lightSource;

	public Item(string name, int attack, int defense, SlotType slotType, bool lightsource)
	{
		this.Name = name; 
		this.Attack = attack;
		this.Defense = defense;
		this.itemSlotType = slotType;
		this.lightSource = lightsource;
	}
}


//This class defines an equipment slot 
public class equipSlot 
{
	public Item EquippedHere;
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
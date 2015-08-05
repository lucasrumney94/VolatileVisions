using UnityEngine;
using System.Collections;

public class UIHandler : MonoBehaviour {

	GameObject[] menuItems;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}



	public void navigateTo(string nextMenu) //Displays all of the Canvas' associated with the nextMenu tag
	{
		menuItems = GameObject.FindGameObjectsWithTag(nextMenu);
		for (int i = 0;i<menuItems.Length;i++) {menuItems[i].GetComponent<Canvas>().enabled = true; }
	}

	public void closeMenu(string menu)//hides all canvas' with the menu tag. 
	{
		menuItems =  GameObject.FindGameObjectsWithTag(menu);
		for (int i = 0;i<menuItems.Length;i++) {menuItems[i].GetComponent<Canvas>().enabled = false; }
	}



}

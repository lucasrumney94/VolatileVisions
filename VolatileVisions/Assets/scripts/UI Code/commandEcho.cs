using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class commandEcho : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void displayResults(string command)
	{
//		Debug.Log("The display function executed");
		GetComponent<Text>().text += "> " + command + "\n";
	}
}

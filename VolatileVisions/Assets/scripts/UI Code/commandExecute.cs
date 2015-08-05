using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class commandExecute : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
		else 
		{
			displayedText.text = "not understood";
		}
	}
}

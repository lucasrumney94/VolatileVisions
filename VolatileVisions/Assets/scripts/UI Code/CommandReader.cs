using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CommandReader : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

		//Debug.Log (GameObject.FindGameObjectWithTag("commandText").GetComponent<Text>().text);
	}


	void clearInput()
	{
//		Debug.Log("The clearInput Function Executed");
		GameObject.FindGameObjectWithTag("commandText").GetComponent<Text>().text = "";
		GameObject.FindGameObjectWithTag("commandPlaceholder").GetComponent<Text>().text = "";
		GameObject.Find("commandLine").GetComponent<InputField>().text = "";
	}





	public void OnGUI()
	{
		if (GameObject.FindGameObjectWithTag("play").GetComponent<Canvas>().enabled == true)
		{
			GameObject.Find("commandLine").GetComponent<InputField>().ActivateInputField();


			string command = GameObject.FindGameObjectWithTag("commandText").GetComponent<Text>().text;
			Event e = Event.current;
			if (e.type == EventType.keyDown && e.keyCode == KeyCode.Return)
			{
				
				clearInput();
				executeCommand(command);


			}
		}
	}




	void executeCommand(string command)
	{
		//check command legit?

		GameObject cmdEchoObj = GameObject.FindGameObjectWithTag("cmdEcho");
		commandEcho a = (commandEcho) cmdEchoObj.GetComponent(typeof(commandEcho));
		a.displayResults(command);

		GameObject cmdExeObj = GameObject.FindGameObjectWithTag("output");
		commandExecute b = (commandExecute) cmdExeObj.GetComponent(typeof(commandExecute));
		b.dispCommRes(command);
	}
}















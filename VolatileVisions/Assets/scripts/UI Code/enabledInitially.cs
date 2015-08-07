using UnityEngine;
using System.Collections;

public class enabledInitially : MonoBehaviour {

	void Start () 
	{
		gameObject.GetComponent<Canvas>().enabled = true;
	}

}

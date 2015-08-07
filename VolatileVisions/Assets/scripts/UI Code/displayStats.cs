using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class displayStats : MonoBehaviour 
{
	public Player player;
	public Text statText;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		statText = gameObject.GetComponent<Text>();
	}

	void Update()
	{
		if (player.stats != null)
		{
			player.stats.calculateStats();
			statText.text = "Stats\n\n" + "Health:  " + player.stats.health + "\nAttack:  " + player.stats.attack + "\ndefense: " + player.stats.defense + "\nAttack Bonus: " + player.stats.attackBonus;
			statText.text += "\nlight: " + player.stats.hasLight;
		}
	}

}

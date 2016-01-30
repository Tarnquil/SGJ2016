using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Text testLabel;

	List<int> currentSpell = new List<int> ();
	int x = 0;

	[SerializeField]
	int health = 100;
	[SerializeField]
	int mana = 100;

	public int Health
	{
		get
		{
			return this.health;
		}
		set
		{
			UpdateHealth(value);
		}
	}

	public int Mana
	{
		get
		{
			return this.mana;
		}
		set
		{
			this.mana = value;
		}
	}

	void ClearSpell ()
	{
		currentSpell = new List<int> ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		Debug.Log("FRIED");
//		x++;
//		testLabel.text = x.ToString();
		if(!currentSpell.Contains (_nodeNumber))
		{
			currentSpell.Add (_nodeNumber);
			testLabel.text = testLabel.text + _nodeNumber.ToString ();
		}
	}

	void UpdateHealth(int newHealth)
	{
		health = newHealth;
		if(health == 0)
		{
			//DEAD
		}

		// PUT ANIMATION OF UI HERE
	}

	void UpdateMana(int newMana)
	{
		mana = newMana;
		// PUT UI ANIMATION CODE HERE 
	}

	void Update()
	{
		if(Input.GetMouseButtonDown (0))
		{
			
		}
	}
	
}

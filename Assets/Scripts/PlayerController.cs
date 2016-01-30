using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Text testLabel;
	[SerializeField]
	TextAsset spellXmlFile;

	XmlDocument xmlDoc;
	XmlNodeList spellList;
	List<int> currentSpell = new List<int> ();
	int x = 0;


	[SerializeField]
	int health = 100;
	[SerializeField]
	int mana = 100;

	public MyNetManager test;

	void Start ()
	{
		xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (spellXmlFile.ToString ());
		spellList = xmlDoc.SelectNodes ("spells/spell");
	
	}

	void Update ()
	{
		Debug.Log (test.IsClientConnected ().ToString ());
		if(Input.GetMouseButtonUp (0))
		{
			CheckIfValidSpell(testLabel.text);
		}
	}


	public int Health {
		get {
			return this.health;
		}
		set {
			UpdateHealth (value);
		}
	}

	public int Mana {
		get {
			return this.mana;
		}
		set {
			this.mana = value;
		}
	}

	void ClearSpell ()
	{
		currentSpell.Clear ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		Debug.Log ("FRIED");
		if (!currentSpell.Contains (_nodeNumber) && Input.GetMouseButtonDown(0)) 
		{
			currentSpell.Add (_nodeNumber);
			testLabel.text = testLabel.text + _nodeNumber.ToString ();
		}
	}

	void CheckIfValidSpell ()
	{
		//Check against XML Spells
	}

	void UpdateHealth (int newHealth)
	{
		health = newHealth;
		if (health <= 0) {
			//DEAD
		}

		// PUT ANIMATION OF UI HERE
	}

	void UpdateMana (int newMana)
	{
		mana = newMana;
		// PUT UI ANIMATION CODE HERE 
	}
	
}

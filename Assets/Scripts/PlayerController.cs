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
			UpdateHealth (value);
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
			UpdateMana (value);
		}
	}

	XmlDocument xmlDoc;
	XmlNodeList spellList;
	List<int> currentSpell = new List<int> ();
	public List<GameObject> spellPrefabs = new List<GameObject>();



	public MyNetManager test;

	void Start ()
	{
		xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (spellXmlFile.ToString ());
		spellList = xmlDoc.SelectNodes ("spells/spell");
	
	}

	void Update ()
	{
		if(Input.GetMouseButtonUp(0))
		{
			CheckIfValidSpell(testLabel.text);
		}

		if(Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			CheckIfValidSpell("1234");
		}
	}




	void ClearSpell ()
	{
		currentSpell.Clear ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		Debug.Log ("FRIED");
		if (!currentSpell.Contains (_nodeNumber)) 
		{
			currentSpell.Add (_nodeNumber);
			testLabel.text = testLabel.text + _nodeNumber.ToString ();
		}
	}

	void CheckIfValidSpell (string spellcode)
	{
		//Check against XML Spells

		if (spellcode == "1234") 
		{
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) 
			{

				if (player.GetComponent<NetworkIdentity> ().isClient && Network.isClient) 
				{
					(player.GetComponent<NetPlayerTest>()).CmdScream(spellcode);
				}
				else if(player.GetComponent<NetworkIdentity> ().isServer && player.GetComponent<NetworkIdentity> ().isClient)
				{
					(player.GetComponent<NetPlayerTest>()).RpcScream(spellcode);
				}
			}
		}
	}

	void UpdateHealth (int newHealth)
	{
		health = newHealth;
		if (health <= 0) 
		{
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

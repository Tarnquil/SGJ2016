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
	Transform lineParent;

	int health = 100;
	[SerializeField]
	int mana = 100;

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
			UpdateMana (value);
		}
	}



	XmlDocument xmlDoc;
	XmlNodeList spellList;
	List<int> currentSpell = new List<int> ();
	public List<GameObject> spellPrefabs = new List<GameObject> ();

	public LineRenderer currentLine;


	bool dragging = false;
	public MyNetManager test;

	void Start ()
	{
		xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (spellXmlFile.ToString ());
		spellList = xmlDoc.SelectNodes ("spells/spell");

	}

	void Update ()
	{
		//Debug.Log (test.IsClientConnected ().ToString ());
		if (currentLine != null) {
			Debug.Log ("working");
			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;
			currentLine.SetPosition (1, Camera.main.ScreenToWorldPoint (linePos));
		}

		if (Input.GetMouseButtonUp (0)) {
			List<GameObject> children = new List<GameObject> ();
			foreach (Transform child in lineParent) {
				children.Add (child.gameObject);
			}

			children.ForEach (child => Destroy (child));

			//	dragging = false;

			/* if (Input.GetMouseButtonUp(0))
            {
                CheckIfValidSpell(testLabel.text);

            }*/
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("UPDATING");
			CheckIfValidSpell ("1234");
		}
	}




	void ClearSpell ()
	{
		currentSpell.Clear ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		Debug.Log ("FRIED");

		if (!currentSpell.Contains (_nodeNumber) && currentSpell.Count < 8) {


			currentSpell.Add (_nodeNumber);
			testLabel.text = testLabel.text + _nodeNumber.ToString ();


			GameObject newLine = new GameObject ();
			newLine.transform.parent = lineParent;
			currentLine = newLine.AddComponent<LineRenderer> ();
			currentLine.SetWidth (0.5f, 0.5f);

			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;

			currentLine.SetPosition (0, Camera.main.ScreenToWorldPoint (linePos));
			dragging = true;
		}
	}


	void CheckIfValidSpell (string spellcode)
	{
		//Check against XML Spells

		if (spellcode == "1234") {
			NetPlayerTest localPlayer = null;
			foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
				if (player.GetComponent <NetworkIdentity> ().isLocalPlayer) {

					localPlayer = player.GetComponent<NetPlayerTest> ();
				}
			}

			if (localPlayer != null) {
				if (localPlayer.isServer) {
					Debug.Log ("Server");
					localPlayer.RpcSpell ((spellcode));
				} else {
					Debug.Log ("Client");
					localPlayer.CmdSpell ((spellcode));
				}
			}
		}

		string nodeSequence;
		string spellCast = "No match";
		bool spellFound = false;
		foreach (XmlNode spell in spellList) {
			nodeSequence = spell.Attributes ["nodesequence"].Value;
			if (spellcode.Equals (nodeSequence)) {
				spellCast = spell.Attributes ["name"].Value;
				spellFound = true;
			}
			if (spellFound)
				break;
		}
		Debug.Log (spellCast);
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

	public void InstantiateSpell (string _spell)
	{
		switch (_spell) {
		case "Fireball":
			{
				GameObject spellPrefab = spellPrefabs.Find (item => item.name == _spell);
				Instantiate (spellPrefab, Vector3.zero, Quaternion.identity);
				break;
			}


		}
	}

}

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
	Transform
		lineParent;

	XmlDocument xmlDoc;
	XmlNodeList spellList;
	List<int> currentSpell = new List<int> ();
	int x = 0;

	public LineRenderer currentLine;

	[SerializeField]
	int health = 100;
	[SerializeField]
	int mana = 100;

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



	void CheckIfValidSpell ()
	{
		
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

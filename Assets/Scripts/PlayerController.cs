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
	Text playerOneReadyText;
	[SerializeField]
	Text playerTwoReadyText;
	[SerializeField]
	TextAsset spellXmlFile;
	[SerializeField]
	Transform lineParent;
	[SerializeField]
	EnergyBar manaBar;
	[SerializeField]
	EnergyBar healthBar;

	[SerializeField]
	int health = 100;
	[SerializeField]
	int mana = 100;

	[SerializeField]
	Material
		lineMat;


	bool loadingGame = false;

	bool castSpell = false;


	public bool playerOneReady;
	public bool playerTwoReady;

	public enum State
	{
		START,
		ROLECHOICE,
		LOBBY,
		IN_GAME,
		SEARCHING,
		SETTINGUP,
		WINNER,
		LOSER,
		NULL
	}


	public State currentState, prevState;

	[SerializeField]
	GameObject[] uiGroups;

	public int Shield = 0;

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

	public XmlDocument xmlDoc;
	public XmlNodeList spellList;
	List<int> currentSpell = new List<int> ();
	public List<GameObject> spellPrefabs = new List<GameObject> ();

	public LineRenderer currentLine;

	bool dragging = false;
	public MyNetManager netMan;

	public void ChangeState (string _toChangeTo)
	{
		currentState = (State)System.Enum.Parse (typeof(State), _toChangeTo);
	}

	void SwitchUIGroup (string _toSwitchTo)
	{
		foreach (GameObject canvas in uiGroups) {
			if (canvas.name == _toSwitchTo) {
				canvas.SetActive (true);
			} else {
				canvas.SetActive (false);
			}
		}
	}

	void Start ()
	{
		currentState = State.START;
		prevState = State.NULL;
		xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (spellXmlFile.ToString ());
		spellList = xmlDoc.SelectNodes ("spells/spell");
	}

	void Update ()
	{
		if (prevState != currentState) {
			prevState = currentState;
			SwitchUIGroup (currentState.ToString ());
		} 

		switch (currentState) {
		case State.LOBBY:
			LobbyUpdate ();
			;
			break;
		case State.IN_GAME:
			GameUpdate ();
			break;
		}
			

	}

	void LobbyUpdate ()
	{

		if (playerOneReady) {
			playerOneReadyText.text = "Player One: Ready";

		}

		if (playerTwoReady) {
			playerTwoReadyText.text = "Player Two: Ready";
		}

		if (playerOneReady && playerTwoReady && !loadingGame) {
			Invoke ("StartGame", 1.0f);
		}
	}

	void GameUpdate ()
	{
		//Debug.Log (test.IsClientConnected ().ToString ());
		if (currentLine != null) {
//			Debug.Log ("working");
			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;
			currentLine.SetPosition (1, this.gameObject.GetComponent<Camera> ().ScreenToWorldPoint (linePos));
		}

		if (Input.GetMouseButtonUp (0)) {
			EndSpell ();
			castSpell = false;
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

		Mana++;
	}


	void EndSpell ()
	{
		List<GameObject> children = new List<GameObject> ();
		foreach (Transform child in lineParent) {
			children.Add (child.gameObject);
		}

		children.ForEach (child => Destroy (child));
		testLabel.text = "";
		ClearSpell ();
	}

	void ClearSpell ()
	{
		currentSpell.Clear ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		Debug.Log ("FRIED");

		if (!currentSpell.Contains (_nodeNumber) && currentSpell.Count < 8 && !castSpell) {


			currentSpell.Add (_nodeNumber);

			testLabel.text = testLabel.text + _nodeNumber.ToString ();
			if (CheckIfValidSpell ((testLabel.text))) {
				castSpell = true;
				EndSpell ();
			}

			GameObject newLine = new GameObject ();
			newLine.transform.parent = lineParent;
			currentLine = newLine.AddComponent<LineRenderer> ();
			currentLine.SetWidth (1.0f, 1.0f);
			currentLine.material = lineMat;

			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;

			currentLine.SetPosition (0, this.gameObject.GetComponent<Camera> ().ScreenToWorldPoint (linePos));
			dragging = true;
		}
	}


	bool CheckIfValidSpell (string spellcode)
	{
		//Check against XML Spells
		string nodeSequence;
		string spellCast = "No match";
		bool spellFound = false;
		foreach (XmlNode spell in spellList) {
			nodeSequence = spell.Attributes ["nodesequence"].Value;
			if (spellcode.Equals (nodeSequence)) {
				spellCast = spell.Attributes ["name"].Value;
				spellFound = true;
				Mana -= System.Convert.ToInt32 (spell.Attributes ["manacost"].Value);
			}
			if (spellFound) {
				if (spellCast == "Heal" || spellCast == "Shield") {
					InstantiateSpell (spellCast);
				} else {
					NetPlayerTest localPlayer = null;
					foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
						if (player.GetComponent <NetworkIdentity> ().isLocalPlayer) {
							localPlayer = player.GetComponent<NetPlayerTest> ();
						}
					}

					if (localPlayer != null) {
						if (localPlayer.isServer) {
							Debug.Log ("Server");
							localPlayer.RpcSpell ((spellCast));
						} else {
							Debug.Log ("Client");
							localPlayer.CmdSpell ((spellCast));
						}
					}
				}
			}
		}
		Debug.Log (spellCast);
		return spellFound;
	}

	void UpdateHealth (int newHealth)
	{
		health = newHealth;
		if (health <= 0) {
			//DEAD
			Lose ();
		}


		health = Mathf.Clamp (health, 0, 100);
		// PUT ANIMATION OF UI HERE
		healthBar.SetEnergyBar ((float)health / (float)100);
	}

	void UpdateMana (int newMana)
	{
		mana = newMana;
		mana = Mathf.Clamp (mana, 0, 100);
		// PUT UI ANIMATION CODE HERE 
		manaBar.SetEnergyBar ((float)mana / (float)100);
	}

	public void InstantiateSpell (string _spell)
	{
		switch (_spell) {
		case "Fireball":
			{
				Debug.Log ("INSTANT");
				GameObject spellPrefab = spellPrefabs [0];//spellPrefabs.Find (item => item.name == _spell);
				Instantiate (spellPrefab, Vector3.zero, Quaternion.identity);
				break;
			}
		case "Heal":
			{
				Debug.Log ("INSTANT");
				GameObject spellPrefab = spellPrefabs [1];//spellPrefabs.Find (item => item.name == _spell);
				Instantiate (spellPrefab, Vector3.zero, Quaternion.identity);
				break;
			}
		case "Shield":
			{
				Debug.Log ("INSTANT");
				GameObject spellPrefab = spellPrefabs [2];//spellPrefabs.Find (item => item.name == _spell);
				Instantiate (spellPrefab, Vector3.zero, Quaternion.identity);
				break;
			}
		case "Sacrifice":
			{
				Debug.Log ("INSTANT");
				GameObject spellPrefab = spellPrefabs [3];//spellPrefabs.Find (item => item.name == _spell);
				Instantiate (spellPrefab, Vector3.zero, Quaternion.identity);
				break;
			}
		}


	}

	public void ReadyLobby ()
	{
		NetPlayerTest localPlayer = null;
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			if (player.GetComponent <NetworkIdentity> ().isLocalPlayer) {
				localPlayer = player.GetComponent<NetPlayerTest> ();
			}
		}

		if (localPlayer != null) {
			if (localPlayer.isServer) {
				Debug.Log ("Server");
				playerOneReady = true;
				localPlayer.RpcReady ();
			} else {
				Debug.Log ("Client");
				playerTwoReady = true;
				localPlayer.CmdReady ();
			}
		}
	}

	void StartGame ()
	{
		ChangeState ("IN_GAME");
	}

	void Lose ()
	{
		NetPlayerTest localPlayer = null;
		foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
			if (player.GetComponent <NetworkIdentity> ().isLocalPlayer) {
				localPlayer = player.GetComponent<NetPlayerTest> ();
			}
		}

		if (localPlayer != null) {
			if (localPlayer.isServer) {
				localPlayer.RpcWinner ();
			} else {
				Debug.Log ("Client");
				playerTwoReady = true;
				localPlayer.CmdWinner ();
			}
		}

		localPlayer.RpcLoser ();
	}
}

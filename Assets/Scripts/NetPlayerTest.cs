using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerTest : NetworkBehaviour
{
	PlayerController player;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ();
	}

	[ServerCallback]
	void Update ()
	{
		
	}

	public void Testing ()
	{
		foreach (GameObject cubes in GameObject.FindGameObjectsWithTag("Player")) {
			Debug.Log ("Testing");
		}
	}

	[ClientRpc]
	public void RpcSpell (string _spell)
	{
		player.InstantiateSpell (_spell);
	}

	[Command]
	public void CmdSpell (string _spell)
	{
		player.InstantiateSpell (_spell);
	}
}

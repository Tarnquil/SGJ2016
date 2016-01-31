using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerTest : NetworkBehaviour
{
	public PlayerController player;
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
		if (!(GetComponent <NetworkIdentity> ().isServer)) {
			player.InstantiateSpell (_spell);
		}
	}

	[Command]
	public void CmdSpell (string _spell)
	{
		player.InstantiateSpell (_spell);
	}

	[ClientRpc]
	public void RpcReady ()
	{
		if (!(GetComponent <NetworkIdentity> ().isServer)) {
			player.playerOneReady = true;
		}
	}

	[Command]
	public void CmdReady ()
	{
		player.playerTwoReady = true;
	}

	[ClientRpc]
	public void RpcWinner ()
	{
		if (!(GetComponent <NetworkIdentity> ().isServer)) {
			GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ().ChangeState ("WINNER");
		}
	}

	//[ClientRpc]
	public void RpcLoser ()
	{
//		if (!(GetComponent <NetworkIdentity> ().isServer)) {
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ().ChangeState ("LOSER");
//		}
	}

	[Command]
	public void CmdWinner ()
	{
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ().ChangeState ("WINNER");
	}

	//[Command]
	public void CmdLoser ()
	{
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ().ChangeState ("LOSER");
	}
}

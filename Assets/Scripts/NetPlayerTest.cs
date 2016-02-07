using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerTest : NetworkBehaviour
{
	public PlayerController player;
	NetworkIdentity networkID;
	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ();
		networkID = GetComponent <NetworkIdentity> ();
	}
		
	[ClientRpc]
	public void RpcSpell (string _spell)
	{
		if (!(networkID.isServer)) 
		{
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
		if (!(networkID.isServer)) {
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
		if (!(networkID.isServer)) {
			player.ChangeState ("WINNER");
		}
	}

	//[ClientRpc]
	public void RpcLoser ()
	{
		//		if (!(networkID.isServer)) {
			player.ChangeState ("LOSER");
//		}
	}

	[Command]
	public void CmdWinner ()
	{
		player.ChangeState ("WINNER");
	}

	//[Command]
	public void CmdLoser ()
	{
		player.ChangeState ("LOSER");
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class MyNetManager : NetworkManager
{
	public OverriddenNetworkDiscovery discovery;

	public override void OnStartHost ()
	{
		discovery.Initialize ();
		discovery.StartAsServer ();
	
	}

	public override void OnStartServer ()
	{
		base.OnStartServer ();
		Debug.Log ("Weeee");
	}

	public override void OnStartClient (NetworkClient client)
	{
		discovery.StartAsClient ();
		//Debug.Log ("Mother Fuckr");
		discovery.showGUI = false;
		//Debug.Log (discovery.running);

	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);
		if (!discovery.isServer) {
			discovery.StopBroadcast ();
		}
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ().ChangeState ("LOBBY");
		Debug.Log ("One or Two");
	}

	public override void OnStopClient ()
	{
		discovery.StopBroadcast ();
		discovery.showGUI = true;
	}

	void Update ()
	{
		//Debug.Log (discovery.running);
	}

}



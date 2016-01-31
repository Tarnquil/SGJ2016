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

	public override void OnStartClient (NetworkClient client)
	{
		//discovery.StartAsClient ();
		discovery.showGUI = false;

	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		base.OnClientConnect (conn);
		discovery.StopBroadcast ();
	}

	public override void OnStopClient ()
	{
		discovery.StopBroadcast ();
		discovery.showGUI = true;
	}
}



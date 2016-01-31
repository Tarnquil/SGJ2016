using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class OverriddenNetworkDiscovery : NetworkDiscovery
{
	public MyNetManager netMan;
	// Use this for initialization
	void Start ()
	{
		netMan = GetComponent <MyNetManager> ();
	}

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		Debug.Log (fromAddress);
		Debug.Log (data);
		Debug.Log (hostId);
		netMan.networkAddress = fromAddress;
		//netMan.discovery.StopBroadcast ();

		netMan.StartClient ();
	}

}
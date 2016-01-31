using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class MyNetManagerUI : MonoBehaviour
{
	public MyNetManager netMan;
	// Use this for initialization
	void Start ()
	{
		netMan = GetComponent <MyNetManager> ();
	}

	public void StartNetworkHost ()
	{
		netMan.StartHost ();
	}

	public void Listen ()
	{
		netMan.discovery.Initialize ();
		netMan.discovery.StartAsClient ();
		//Debug.Log (netMan.discovery.isClient);

	}
}

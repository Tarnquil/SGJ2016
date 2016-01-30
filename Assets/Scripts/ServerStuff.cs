using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class ServerStuff : MonoBehaviour
{
	
	public string connectionIP = "127.0.0.1";
	public int connectionPort = 25001;
	public NetworkManager test;
	NetworkDiscovery disc;

	void Start ()
	{
		disc = this.gameObject.GetComponent<NetworkDiscovery> ();
	}

	public void Connect ()
	{
		disc.StartAsClient ();
	}

	public void StartServer ()
	{
		disc.StartAsServer ();
	}

	void OnMatchCreate ()
	{
		Debug.Log ("Match Made");
	}
}
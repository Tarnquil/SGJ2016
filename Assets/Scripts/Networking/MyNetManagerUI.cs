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
	
	// Update is called once per frame
	void Update () 
	{
	}

	public void StartNetworkHost()
	{
	}

	public void Listen()
	{
		//netMan();
	}
}

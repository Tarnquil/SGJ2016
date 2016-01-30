﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetPlayerTest : NetworkBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}

	[ServerCallback]
	void Update ()
	{
		
	}

	public void Testing ()
	{
		foreach (GameObject cubes in GameObject.FindGameObjectsWithTag("Player")) {
			Debug.Log ("Testing");
			cubes.GetComponent<NetPlayerTest> ().RpcScream ();
		}
	}

	[ClientRpc]
	public void RpcScream ()
	{
		this.gameObject.transform.Translate (Vector3.one * Random.Range (-1.0f, 1.0f));
	}

	[Command]
	public void CmdScream ()
	{
		this.gameObject.transform.Translate (Vector3.one * Random.Range (-1.0f, 1.0f));
	}
}
using UnityEngine;
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
			cubes.GetComponent<NetPlayerTest> ().CmdScream ();
		}
	}

	[Command]
	public void CmdScream ()
	{
		Debug.Log ("Kyle is a dick");
	}
}

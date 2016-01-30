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
		foreach (GameObject cubes in GameObject.FindGameObjectsWithTag("Player"))
		{
			Debug.Log ("Testing");
		}
	}

	[ClientRpc]
	public void RpcScream (string _sound)
	{
		this.gameObject.transform.Translate (Vector3.one * Random.Range (-1.0f, 1.0f));
		Debug.Log (_sound);
	}

	[Command]
	public void CmdScream (string _sound)
	{
		this.gameObject.transform.Translate (Vector3.one * Random.Range (-1.0f, 1.0f));
		Debug.Log (_sound);
	}
}

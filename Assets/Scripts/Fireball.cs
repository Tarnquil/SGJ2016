using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class Fireball : Spell
{

	// Use this for initialization
	void Start () 
	{
		base.Start();
	}

	override protected void Cast()
	{
		base.Cast();
		player.Health -= spellStrength;
	}
}

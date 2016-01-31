using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class Fireball : Spell
{
	override protected void Cast ()
	{
		base.Cast ();
		player.Health -= spellStrength;
	}
}

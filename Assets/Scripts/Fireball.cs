using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking;

public class Fireball : Spell
{
	override protected void Cast ()
	{
		base.Cast ();
		if(player.Shield > 0)
		{
			player.Shield -= spellStrength;
			if(player.Shield < 0)
			{
				player.Health += player.Shield;
				player.Shield = 0;
			}
		}
		else
		{
			player.Health -= spellStrength;
		}


	}
}

using UnityEngine;
using System.Collections;

public class Heal : Spell
{
	override protected void Cast ()
	{
		base.Cast ();
		player.Health += spellStrength;
	}
}

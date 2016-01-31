using UnityEngine;
using System.Collections;

public class Sacrifice : Spell
{
	override protected void Cast ()
	{
		base.Cast ();
		player.Health -= spellStrength;
	}
}

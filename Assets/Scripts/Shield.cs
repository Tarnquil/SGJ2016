using UnityEngine;
using System.Collections;

public class Shield : Spell
{
	override protected void Cast ()
	{
		base.Cast ();
		player.Shield += spellStrength;
		player.ShieldOn();
	}
}

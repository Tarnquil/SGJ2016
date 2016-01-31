using UnityEngine;
using System.Collections;
using System.Xml;

public class Spell : MonoBehaviour
{
	[SerializeField]
	int manaCost;
	[SerializeField]
	protected int spellStrength;
    [SerializeField]
    protected string name;

	protected ParticleSystem particles;
	protected PlayerController player;

	// Use this for initialization
	protected void Start ()
	{
		particles = GetComponent<ParticleSystem> ();
		player = GameObject.FindGameObjectWithTag ("GameController").GetComponent<PlayerController> ();
        XmlNode spell = player.xmlDoc.SelectSingleNode("spells/spell[@name='"+name+"']");
        manaCost = int.Parse(spell.Attributes["manacost"].Value);
        spellStrength = int.Parse(spell.Attributes["strength"].Value);
		Cast ();
	}

	virtual protected void Cast ()
	{
		Debug.Log ("Mother fuckers are fucking mothers");
		particles.Play ();
		Invoke ("KillMe", 5.0f);
	}



	void KillMe ()
	{
		Destroy (this.gameObject);
	}
}

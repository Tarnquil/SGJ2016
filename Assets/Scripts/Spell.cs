using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
	[SerializeField]
	int manaCost;
	[SerializeField]
	protected int spellStrength;

	protected ParticleSystem particles;
	protected PlayerController player;

	// Use this for initialization
	protected void Start () 
	{
		particles = GetComponent<ParticleSystem>();
		player = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerController>();
	}
		
	virtual protected void Cast()
	{
		if (player.Mana > manaCost)
		{
			particles.Play();
		}
	}
}

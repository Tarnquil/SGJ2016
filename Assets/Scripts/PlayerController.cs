using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	Text
		testLabel;



	List<int> currentSpell = new List<int> ();
	int x = 0;

	void ClearSpell ()
	{
		currentSpell = new List<int> ();
	}

	public void AddNodeToSpell (int _nodeNumber)
	{
		x++;
		testLabel.text = x.ToString ();
	}
	
}

using UnityEngine;
using System.Collections;

public class LineDrawing : MonoBehaviour
{
	
	LineRenderer currentLine;
	bool dragging = false;

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			GameObject newLine = new GameObject ();
			currentLine = newLine.AddComponent<LineRenderer> ();
			currentLine.SetWidth (0.5f, 0.5f);
		
			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;

			currentLine.SetPosition (0, Camera.main.ScreenToWorldPoint (linePos));
			dragging = true;
		}

		if (dragging) {
			Vector3 linePos = Input.mousePosition;
			linePos.z += 15;
			currentLine.SetPosition (1, Camera.main.ScreenToWorldPoint (linePos));
		}

		if (Input.GetMouseButtonUp (0)) {
			dragging = false;
		}
	}
}

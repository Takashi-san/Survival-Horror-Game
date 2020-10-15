using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour {
	[SerializeField] Texture2D cursorTexture = null;
	[SerializeField] Vector2 hotSpot = Vector2.zero;
	CursorMode cursorMode = CursorMode.Auto;

	void OnEnable() {
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
	}

	void OnDisable() {
		Cursor.SetCursor(null, Vector2.zero, cursorMode);
	}
}

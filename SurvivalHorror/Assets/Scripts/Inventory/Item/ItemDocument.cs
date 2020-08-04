using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDocument : MonoBehaviour {
	[SerializeField] ItemType _document = null;
	[SerializeField] GameObject _canvas = null;
	[SerializeField] Image _image = null;
	[SerializeField] TextMeshProUGUI _text = null;

	void Awake() {
		GetComponent<SpriteRenderer>().sprite = _document.SpriteGame;
		_image.sprite = _document.SpriteIcon;
		_text.text = _document.Description;

		_canvas.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other) {
		_canvas.SetActive(true);
	}

	void OnTriggerExit2D(Collider2D other) {
		_canvas.SetActive(false);
	}
}

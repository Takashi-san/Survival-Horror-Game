using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
	[SerializeField] GameObject _defaultTransition = null;
	Animator _animator = null;

	void Awake() {
		if (_defaultTransition) {
			GameObject transition = Instantiate(_defaultTransition);
			_animator = transition.GetComponent<Animator>();
		}
	}

	public void LoadScene(string scene) {
		StartCoroutine(DoSceneLoad(scene));
	}

	IEnumerator DoSceneLoad(string scene) {
		if (_animator) {
			_animator.SetBool("in", true);
			yield return new WaitForEndOfFrame();
			yield return new WaitForSecondsRealtime(_animator.GetCurrentAnimatorStateInfo(0).length);
		}
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		yield break;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {
	//public Animator transition;

	public void LoadScene(string scene) {
		StartCoroutine(DoSceneLoad(scene));
	}

	IEnumerator DoSceneLoad(string scene) {
		//transition.SetTrigger("Start");
		//yield return new WaitForEndOfFrame();
		//yield return new WaitForSecondsRealtime(transition.GetCurrentAnimatorStateInfo(0).length);
		AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
		yield break;
	}
}

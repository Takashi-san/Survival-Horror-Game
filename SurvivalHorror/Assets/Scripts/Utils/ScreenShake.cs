using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {
	[SerializeField] [Min(0)] float _maxRotation = 0;
	[SerializeField] [Min(0)] float _maxTranslation = 0;
	[SerializeField] [Min(0)] float _scaleTime = 1;
	[SerializeField] [Min(0)] float _traumaReductionRate = 1;

	float _trauma = 0;
	float _screenShake = 0;

	public void AddTrauma(float p_trauma) {
		_trauma += p_trauma;
		_trauma = Mathf.Clamp01(_trauma);
	}

	void Update() {
		_screenShake = _trauma * _trauma;

		float __perlin = Mathf.PerlinNoise(Time.time * _scaleTime, (Time.time + 3) * _scaleTime);
		__perlin = (Mathf.Clamp01(__perlin) - 0.5f) / 0.5f;
		float __rotation = _maxRotation * _screenShake * __perlin;
		transform.rotation = Quaternion.AngleAxis(__rotation, Vector3.forward);

		__perlin = Mathf.PerlinNoise((Time.time + 1) * _scaleTime, Time.time * _scaleTime);
		__perlin = (Mathf.Clamp01(__perlin) - 0.5f) / 0.5f;
		float __translationX = _maxTranslation * _screenShake * __perlin;
		__perlin = Mathf.PerlinNoise((Time.time + 2) * _scaleTime, Time.time * _scaleTime);
		__perlin = (Mathf.Clamp01(__perlin) - 0.5f) / 0.5f;
		float __translationY = _maxTranslation * _screenShake * __perlin;
		transform.localPosition = new Vector3(__translationX, __translationY, 0);

		_trauma -= _traumaReductionRate * Time.deltaTime;
		_trauma = Mathf.Clamp01(_trauma);
	}
}

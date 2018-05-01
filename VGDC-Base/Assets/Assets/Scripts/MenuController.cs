using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

	private bool sceneChange = false;

	public Material diffuser;
	public Material transparent;
	public Material transparentText;
	public float timer = 0.0f;
	public float transitionTime = 0.0f;
	public float sceneChangeTime = 0.0f;
	public Camera cam;

	public float logoFadeTime = 60.0f;
	public float textAppearTime = 120.0f;
	public float textFadeTime = 30.0f;

	// Use this for initialization
	void Start () {
		Color tmp = transparent.color;
		tmp.a = 0.8f;
		transparent.color = tmp;

		tmp = transparentText.color;
		tmp.a = 0.0f;
		transparentText.color = tmp;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (transitionTime == 0.0f) {
				transitionTime++;
			}
		}
		if (transitionTime == 0.0f) {
			Flicker ();
		} else
			FlickerLight ();
		if (transitionTime > 0.0f && !sceneChange) {
			Transition ();
		}
		if (sceneChange) {
			ChangeScene ();
		}

	}

	//controls flickering
	void Flicker() {
		if (timer <= 0.0f) {
			diffuser.color = Random.ColorHSV (0.23f, 0.24f, 0.08f, 0.1f, 0.1f, 0.2f);
			timer = 10.0f;
		}
		timer--;
	}

	//controls flickering
	void FlickerLight() {
		if (timer <= 0.0f) {
			diffuser.color = Random.ColorHSV (0.23f, 0.24f, 0.2f, 0.21f, 0.3f, 0.32f);
			timer = 7.0f;
		}
		timer--;
	}

	//controls setting up menu after pressing space
	void Transition() {
		Color tmp = transparent.color;
		if (transitionTime < logoFadeTime) {
			tmp.a = (logoFadeTime - transitionTime) / logoFadeTime * 0.8f;
		} else
			tmp.a = 0.0f;
		transparent.color = tmp;

		if (cam.transform.position.z < 0.0f) {
			Vector3 camMove = new Vector3 (0.15f, 0.0f, 0.15f);
			cam.transform.Translate (camMove);
		}

		if (transitionTime > logoFadeTime) {
			tmp = transparentText.color;
			if (transitionTime < textAppearTime) {
				tmp.a = (transitionTime - logoFadeTime) / (textAppearTime - logoFadeTime) * 0.7f;
								
			} else {
				tmp.a = 0.7f;
			}
			transparentText.color = tmp;

			if (Input.GetKeyDown ("space")) {
				if (!sceneChange) {
					
					sceneChange = true;
				}
			}
		}

		if (transitionTime < 1000.0f)
			transitionTime++;
	}

	//controls changing scene
	void ChangeScene(){

		Color tmp = transparentText.color;
		if (sceneChangeTime < textFadeTime) {
			tmp.a = (textFadeTime - sceneChangeTime) / (textFadeTime) * 0.7f;

		} else {
			tmp.a = 0.0f;
		}
		transparentText.color = tmp;

		Vector3 camMove = new Vector3 (0.0f, 0.0f, 0.5f);
		cam.transform.Translate (camMove);

		if (sceneChangeTime > 60.0f)
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Test");

		sceneChangeTime++;
	}
}

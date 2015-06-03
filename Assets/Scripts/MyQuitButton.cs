using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyQuitButton : MonoBehaviour {

	private Button button;

	void Awake()
	{
		button = GetComponent<Button> ();
		button.onClick.AddListener (() => Application.Quit());
	}

}

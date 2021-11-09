using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
  	public static MenuController instance;


      private void Awake()
	{
		//this method is called BEFORE Start()
		//enforce our singleton pattern by making sure there are no other instances online
		if (instance == null) {
			instance = this;
		} else {
			//destroy the gameobject this instance is attached to
			Destroy (gameObject);
		}
    }
    public void ChangeToScene (int sceneToChangeTo) {
		SceneManager.LoadScene (sceneToChangeTo);
	}

	public void ChangeToScene (string sceneToChangeTo) {
		SceneManager.LoadScene (sceneToChangeTo);
	}




}

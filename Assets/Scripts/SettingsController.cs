using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class SettingsController : MonoBehaviour
{
    public static bool inputMode = false;
    public Button voiceInputButton, touchInputButton;
    public Text inputModeLabel;

    public void ChangeToScene (string sceneToChangeTo) {
		SceneManager.LoadScene (sceneToChangeTo);
	}

    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }

    public void ToggleInput()
    {
        inputMode = !inputMode;
        if (!inputMode)
        {
            voiceInputButton.gameObject.SetActive(true);
            touchInputButton.gameObject.SetActive(false);
            inputModeLabel.text = "Scream to Jump";
            PlayerPrefs.SetInt("inputMode", 0);
        }
        else
        {
            voiceInputButton.gameObject.SetActive(false);
            touchInputButton.gameObject.SetActive(true);
            inputModeLabel.text = "Touch to Jump";
            PlayerPrefs.SetInt("inputMode", 1);
        }
    }
    private void LoadPrefs()
    {
        //loading input mode
        int inputModeInt = PlayerPrefs.GetInt("inputMode", 0);
        if (inputModeInt == 0) inputMode = false;
        else if (inputModeInt == 1) inputMode = true;
        if (!inputMode)
        {
            voiceInputButton.gameObject.SetActive(true);
            touchInputButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("inputMode", 0);
            inputModeLabel.text = "Scream to Jump";
        }
        else
        {
            voiceInputButton.gameObject.SetActive(false);
            touchInputButton.gameObject.SetActive(true);
            PlayerPrefs.SetInt("inputMode", 1);
            inputModeLabel.text = "Touch to Jump";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Bird : MonoBehaviour
{


    public float speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    public GameObject ReplayBtn;
    public GameObject MenuButton;
    private bool isDead;

    public Score scoreText;
    private AudioSource[] birdSounds;
	private AudioSource flapSound;
	private AudioSource deadSound;
    private bool flapped;
    private AudioClip microphoneInput;

    private bool debugMode = true;
	public static Bird instance;

    private float voiceSensitivity;
 // Use this for initialization
    void Awake () {
		//this method is called BEFORE Start()
		//enforce our singleton pattern by making sure there are no other instances online
		if (instance == null) {
			instance = this;
		} else {
			//destroy the gameobject this instance is attached to
			Destroy (gameObject);
		}
	}
    void Start()
    {
                LoadInputMode();

        Time.timeScale=1;

        rb= GetComponent<Rigidbody2D>();

        voiceSensitivity = 0.2f;
        if(Microphone.devices.Length > 0)
        {
            //initializing scripting
            microphoneInput = Microphone.Start(Microphone.devices[0], true, 999, 44100);
        }
        birdSounds = GetComponents<AudioSource>();
		flapSound = birdSounds [1];
		deadSound = birdSounds [0];
    }

    // Update is called once per frame
    void Update()
    {

        if (!SettingsController.inputMode && !isDead ) {
        int dec = 128;
		float[] waveData = new float[dec];
		int micPosition = Microphone.GetPosition(null) - (dec + 1); // null means the first microphone
		microphoneInput.GetData(waveData, micPosition);

		// Getting a peak on the last 128 samples
		float levelMax = 0;

        for (int i = 0; i < dec; i++)
		{
			float wavePeak = waveData[i] * waveData[i];
			if (levelMax < wavePeak) levelMax = wavePeak;
		}

        float level = Mathf.Sqrt(Mathf.Sqrt(levelMax));


        if (level > voiceSensitivity && !flapped)
        {
            rb.velocity = Vector2.up * speed;
            flapSound.Play ();
            flapped = true;
        }

		if (level < voiceSensitivity && flapped) flapped = false;}
        else {
            if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * speed;
            flapSound.Play ();
        }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Column"))
        {
            scoreText.ScoreUp();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Ground") ||
        collision.gameObject.CompareTag("Pipe")
        )
        {           isDead = true;

            Time.timeScale = 0;
            deadSound.Play();

            ReplayBtn.SetActive(true);
            MenuButton.SetActive(true);
        }
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
     private void LoadInputMode()
    {
        int inputModeInt = PlayerPrefs.GetInt("inputMode", 0);
        if (inputModeInt == 0) SettingsController.inputMode = false;
        else if (inputModeInt == 1) SettingsController.inputMode = true;
    }
}

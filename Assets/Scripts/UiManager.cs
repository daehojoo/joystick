using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public GameObject player;
    public RectTransform mainImage;
    public RectTransform pauseImage;
    public RectTransform screenImage;
    public RectTransform soundImage;
    bool isPause = false;


    public Slider playerSoundSlider;
    public Slider BGMSlider;
    public AudioSource BGMAudioSource;
    public AudioSource playerAudioSource;

    public Toggle bgmMute;
    public Toggle playerSoundMute;


    public Slider fovSlider;
    public Dropdown shadowDropDown;



    // Start is called before the first frame update
    void Start()
    {
        mainImage = transform.GetChild(5).GetComponent<RectTransform>();
        pauseImage = mainImage.GetChild(0).GetComponent<RectTransform>();
        soundImage = mainImage.GetChild(1).GetComponent<RectTransform>();
        screenImage = mainImage.GetChild(2).GetComponent<RectTransform>();

        bgmMute =soundImage.GetChild(7).GetComponent<Toggle>();
        playerSoundMute = soundImage.GetChild(6).GetComponent<Toggle>();



        player = GameObject.FindWithTag("Player");
        isPause = false;


        BGMAudioSource = Camera.main.GetComponent<AudioSource>();
        playerAudioSource = player.GetComponent<AudioSource>();
        

       
        
        BGMAudioSource.volume = 1f;
        BGMSlider.value = BGMAudioSource.volume;
        playerAudioSource.volume = 1f;
        playerSoundSlider.value = playerAudioSource.volume;

        bgmMute.isOn = false;
        playerSoundMute.isOn = false;

         
}

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                    Resume();               
                else
                    Pause();
            }
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                    Resume();
                else
                    Pause();
            }
        }
    }
    public void Pause()
    { 
        isPause = true;
        if (mainImage.gameObject.activeInHierarchy==false)
        {
            if (pauseImage.gameObject.activeInHierarchy == false)
            {
                mainImage.gameObject.SetActive(true);
                soundImage.gameObject.SetActive(false);
                screenImage.gameObject.SetActive(false);
            }
            pauseImage.gameObject.SetActive(true);
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
            player.SetActive(false);
        }
    }
    public void Resume()
    {
        soundImage.gameObject.SetActive(false);
        screenImage.gameObject.SetActive(false);
        mainImage.gameObject.SetActive(false);
        pauseImage.gameObject.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
        player.SetActive(true);
        Debug.Log(Time.timeScale);



    }
    public void OnClickedSound(bool isOpenSound)
    {
        if (isOpenSound)
        {
            soundImage.gameObject.SetActive(isOpenSound);
            pauseImage.gameObject.SetActive(false);
            screenImage.gameObject.SetActive(false);
        }
        else
        {
            soundImage.gameObject.SetActive(false);
            pauseImage.gameObject.SetActive(true);
        }
    }
    public void OnClickedScreen(bool isOpenScreen)
    {
        if (isOpenScreen)
        {
            screenImage.gameObject.SetActive(isOpenScreen);
            pauseImage.gameObject.SetActive(false);
            soundImage.gameObject.SetActive(false);
        }
        else
        {
            screenImage.gameObject.SetActive(false);
            pauseImage.gameObject.SetActive(true);
        }
    }
    


    public void OnClickedBack()
    {
        soundImage.gameObject.SetActive(false);
        screenImage.gameObject.SetActive(false);
        pauseImage.gameObject.SetActive(true);
    }
}

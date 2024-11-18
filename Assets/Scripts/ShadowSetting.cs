using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowSetting : MonoBehaviour
{
    public Light mainLight;
    public Dropdown dropDown;


    void Start()
    {
        mainLight = GameObject.FindWithTag("Light").GetComponent<Light>();

        dropDown.onValueChanged.AddListener(OnShadowDropValueChange);
    }
    public void OnShadowDropValueChange(int value)
    {
        if (dropDown != null && mainLight != null)
        {
            switch (value)
            {
                case 0:
                    mainLight.shadows = LightShadows.Soft;
                    break;
                case 1:
                    mainLight.shadows = LightShadows.Hard;
                    break;
                case 2:
                    mainLight.shadows = LightShadows.None;
                    break;
                default:
                    Debug.LogWarning("Out of Range");
                    break;
            }
        }
        else
        {
            Debug.LogError("Off");
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (PlayerPrefs.HasKey("Volume"))
        {
            slider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            slider.value = 1f;
        }
    }

    void Update()
    {
        AudioListener.volume = slider.value;
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", slider.value);
    }
}

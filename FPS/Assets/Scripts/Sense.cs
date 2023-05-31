using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sense : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (PlayerPrefs.HasKey("Sense"))
        {
            slider.value = PlayerPrefs.GetFloat("Sense");
        }
        else
        {
            slider.value = 250f;
        }
    }

    public void SaveSense()
    {
        PlayerPrefs.SetFloat("Sense", slider.value);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}

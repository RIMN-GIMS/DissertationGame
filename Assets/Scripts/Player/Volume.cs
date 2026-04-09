using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private Slider slider;

    private float vol;

   



    void Start()
    {
        music.ignoreListenerPause = true;
       // music.volume = vol;
      
     

    }
    public void SetVol()
    {
        vol = slider.value;
        music.volume = vol;
          

    }
}

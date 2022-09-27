using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_Ses : MonoBehaviour
{
    private static GameObject instance;
    AudioSource ses;
    void Start()
    {
        ses = GetComponent<AudioSource>();
        ses.volume = PlayerPrefs.GetFloat("Menuses");
        DontDestroyOnLoad(gameObject);
            
            if(instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        ses.volume = PlayerPrefs.GetFloat("Menuses");
    }
}

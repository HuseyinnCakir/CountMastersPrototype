using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pasiflestir : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}

  

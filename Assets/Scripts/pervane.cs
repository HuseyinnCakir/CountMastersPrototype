using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pervane : MonoBehaviour
{
    public Animator anim;
    public float bekleme_Suresi;
    public BoxCollider ruzgr;
    public void animasyondurum(string durum)
    {
        if (durum == "true")
        {
            anim.SetBool("calistir", true);
            ruzgr.enabled = true;
        }
        else
        {
            anim.SetBool("calistir", false);
            ruzgr.enabled = false;
            StartCoroutine(tetikle());
        }
        
    }
    IEnumerator tetikle()
    {
        yield return new WaitForSeconds(bekleme_Suresi);
        animasyondurum("true");
    }
}

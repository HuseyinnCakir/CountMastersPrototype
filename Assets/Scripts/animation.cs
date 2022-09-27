using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    public Animator anim;
   public void pasiflestir()
    {
        anim.SetBool("goster", false);
    }
}

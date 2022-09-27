using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tarafsiz_karakter : MonoBehaviour
{
    public SkinnedMeshRenderer render;
    public Material renk;
    public NavMeshAgent nevmesh;
    public Animator anim;
    public GameObject target;
    bool temas_Varmi;
    public GameManager game;

    private void LateUpdate()
    {
        if(temas_Varmi)
        nevmesh.SetDestination(target.transform.position);
    }

    // Update is called once per frame
   
    void orduyaKatil()
    {
        GameManager.karakter_sayisi++;
        Debug.Log(GameManager.karakter_sayisi);
        gameObject.tag = "uretilen_karakter";
        anim.SetBool("attack", true);
        Material[] materyal = render.materials;
        materyal[0] = renk;
        render.materials = materyal;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")|| other.CompareTag("uretilen_karakter"))
        {
            if (gameObject.CompareTag("tarafsiz_karakter")){
                orduyaKatil();
                temas_Varmi = true;
                GetComponent<AudioSource>().Play();
            }
            
        }
        else if (other.CompareTag("dusman"))
        {
            game.YokOlmaEfektiOlustur(pozisyon(), false, false);
            gameObject.SetActive(false);
            
           

        }
        else if (other.CompareTag("ignelikutu"))
        {
            Debug.Log("dedi");
            game.YokOlmaEfektiOlustur(pozisyon());
            gameObject.SetActive(false);
            
        }
        else if (other.CompareTag("testere"))
        {
            game.YokOlmaEfektiOlustur(pozisyon());
            gameObject.SetActive(false);

          

        }
        else if (other.CompareTag("pervaneigne"))
        {
            game.YokOlmaEfektiOlustur(pozisyon());
            gameObject.SetActive(false);

           

        }
        else if (other.CompareTag("balyoz"))
        {
            game.YokOlmaEfektiOlustur(pozisyon(), true);
            gameObject.SetActive(false);

          

        }
    }
    Vector3 pozisyon()
    {

        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
}

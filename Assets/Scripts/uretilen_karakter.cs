using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class uretilen_karakter : MonoBehaviour
{
    public GameObject target;
    NavMeshAgent nevmesh;
    public GameManager game;
    void Start()
    {

        nevmesh = GetComponent<NavMeshAgent>();
        
        
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        nevmesh.SetDestination(target.transform.position);
    }
    Vector3 pozisyon()
    {
        
        return new Vector3(transform.position.x, .23f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ignelikutu"))
        {
            Debug.Log("deðdi");
            gameObject.SetActive(false);
            game.YokOlmaEfektiOlustur(pozisyon());
        }
       else if (other.CompareTag("testere"))
        {
            gameObject.SetActive(false);
            
            game.YokOlmaEfektiOlustur(pozisyon());

        }
        else if (other.CompareTag("pervaneigne"))
        {
            gameObject.SetActive(false);
           
            game.YokOlmaEfektiOlustur(pozisyon());

        }
       else if (other.CompareTag("balyoz"))
        {
            gameObject.SetActive(false);
            
            game.YokOlmaEfektiOlustur(pozisyon(), true);

        }
        else if (other.CompareTag("dusman"))
        {
            gameObject.SetActive(false);

            game.YokOlmaEfektiOlustur(pozisyon(),false,false);

        }
        else if (other.CompareTag("tarafsiz_karakter"))
        {
            game.karakterler.Add(other.gameObject);
           
        }
    }
}

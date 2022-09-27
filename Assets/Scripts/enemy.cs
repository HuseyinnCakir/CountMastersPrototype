using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public GameObject hedef;
    public NavMeshAgent agent;
    bool kontrol;
    
    void Start()
    {
        
    }
    public void animasyon_start()
    {
        GetComponent<Animator>().SetBool("attack", true);
        kontrol = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (kontrol)
        {
            agent.SetDestination(hedef.transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("uretilen_karakter"))
        {
            gameObject.SetActive(false);
            Vector3 pozisyon = new Vector3(transform.position.x, .23f, transform.position.z);
            GameObject.FindWithTag("gamemanager").GetComponent<GameManager>().YokOlmaEfektiOlustur(pozisyon,false,true);
        }
    }
}

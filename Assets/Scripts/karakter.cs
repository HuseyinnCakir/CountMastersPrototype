using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class karakter : MonoBehaviour
{
    public GameManager gamemanager;
    public Kamera_Control kamera;
    public bool sona_geldikmi;
    public GameObject gidilecek_yer;
    public Slider slider;
    public GameObject gecis_noktasi;
    float bolum_sonuna_uzaklik;
    // Start is called before the first frame update
    void Start()
    {
        bolum_sonuna_uzaklik = Vector3.Distance(transform.position, gecis_noktasi.transform.position);
        slider.maxValue = bolum_sonuna_uzaklik;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!sona_geldikmi)
        transform.Translate(Vector3.forward * .5f * Time.deltaTime);
    }
   
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (sona_geldikmi)
            {
                transform.position = Vector3.Lerp(transform.position, gidilecek_yer.transform.position, .125f);
                GetComponent<Animator>().SetBool("attack", false);
                if (slider.value != 0)
                {
                    slider.value -= 0.005f;
                }
            }
            else
            {
                bolum_sonuna_uzaklik = Vector3.Distance(transform.position, gecis_noktasi.transform.position);
                slider.value = bolum_sonuna_uzaklik;



                if (Input.GetKey(KeyCode.Mouse0))
                {
                    if (Input.GetAxis("Mouse X") < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y,
                            transform.position.z), .3f);
                    }
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y,
                            transform.position.z), .3f);
                    }
                }
               if (Input.touchCount > 0)
                {
                    Touch parmak = Input.GetTouch(0);
                    if (parmak.deltaPosition.x < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x - .1f, transform.position.y,
                            transform.position.z), .3f);
                    }
                    if (parmak.deltaPosition.x > 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + .1f, transform.position.y,
                            transform.position.z), .3f);
                    }

                }

            }
            
        }
        
        }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("carpma") || other.CompareTag("toplama") || other.CompareTag("cikarma") || other.CompareTag("bolme"))
        {
            int sayi = int.Parse(other.name);
            gamemanager.KarakterYonetim(other.tag,sayi, other.transform);
        }
        else if (other.CompareTag("dusmanlar_saldir"))
        {
            kamera.sona_geldikmi = true;
            gamemanager.dusmanlar_saldir();
            sona_geldikmi = true;
        }
        else if (other.CompareTag("tarafsiz_karakter"))
        {
            gamemanager.karakterler.Add(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("direk") || collision.gameObject.CompareTag("ignelikutu") 
            || collision.gameObject.CompareTag("pervaneigne"))
        {
            if(collision.transform.position.x> 0)
            transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y, transform.position.z);
        }
    }
}

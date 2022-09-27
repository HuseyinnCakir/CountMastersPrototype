using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    
    public GameObject varis_noktasi;
    public int karakter_sayisi = 1;
    public List<GameObject> karakterler;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var item in karakterler)
            {
                if (!item.activeInHierarchy)
                {
                    item.transform.position = dogma_noktasi.transform.position;
                    item.SetActive(true);
                    karakter_sayisi++;
                    break;

                }

            }
        }*/
    }
    public void KarakterYonetim(string veri,Transform pozisyon)
    {
        switch (veri)
        {
            case "x2":
                int sayi = 0;
                foreach (var item in karakterler)
                {
                    if (sayi < karakter_sayisi)
                    {
                        if (!item.activeInHierarchy)
                        {
                            item.transform.position = pozisyon.transform.position;
                            item.SetActive(true);
                            sayi++;

                        }

                    }
                    else { sayi = 0; break; }
                }
                karakter_sayisi *= 2;
                break;

            case "+3":
                int sayi2 = 0;
                foreach (var item in karakterler)
                {
                    if (sayi2 < 3)
                    {
                        if (!item.activeInHierarchy)
                        {
                            item.transform.position = pozisyon.transform.position;
                            item.SetActive(true);
                            sayi2++;

                        }

                    }
                    else { sayi2 = 0; break; }
                }
                karakter_sayisi += 3;
                break;
           
            case "bolu2":
                Debug.Log("girdi");
                if (karakter_sayisi <=2)
                {
                    foreach (var item in karakterler)
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    karakter_sayisi = 1;
                }
                else
                {
                    int bolen = karakter_sayisi / 2;
                    int sayi3 = 0;
                    foreach (var item in karakterler)
                    {
                        if (sayi3 !=bolen)
                        {
                            if (item.activeInHierarchy)
                            {
                                item.transform.position = Vector3.zero;
                                item.SetActive(false);
                                sayi3++;

                            }

                        }
                        else { sayi3 = 0; break; }
                    }
                    if (karakter_sayisi % 2 == 0)
                        karakter_sayisi /= 2;
                    else
                    {
                        karakter_sayisi /= 2;
                        karakter_sayisi++;
                    }
             
                }
                break;
        

            case "-4":
                if (karakter_sayisi < 4)
                {
                    foreach (var item in karakterler)
                    {
                        item.transform.position = Vector3.zero;
                        item.SetActive(false);
                    }
                    karakter_sayisi = 1;
                }
                else
                {
                    int sayi3 = 0;
                    foreach (var item in karakterler)
                    {
                        if (sayi3 < 3)
                        {
                            if (item.activeInHierarchy)
                            {
                                item.transform.position = Vector3.zero;
                                item.SetActive(false);
                                sayi3++;

                            }

                        }
                        else { sayi3 = 0; break; }
                    }
                    karakter_sayisi -= 4;
                   

                }
                break;
        }
    }
}

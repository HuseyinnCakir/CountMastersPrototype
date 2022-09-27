using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Huseyin_Cakir;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{


    public AudioSource[] oyunsesi;
    public static int karakter_sayisi = 1;
    public List<GameObject> karakterler;
    public List<GameObject> olusma_efekt;
    public List<GameObject> yok_olma_efekt;
    public List<GameObject> ezilme_efekt;
    public bool oyun_bittimi;
    public bool sona_Geldikmi;
    Library matematiksel_islemler = new Library();
    bellekyonetim BellekYonetim = new bellekyonetim();
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
    Reklam reklam_yonetim = new Reklam();
    [Header("LEVEL VERÝLERÝ")]
    public List<GameObject> dusmanlar;
    public int dusman_Sayisi;
    public GameObject[] sapkalar;
    public GameObject[] sopalar;
    public Material[] materyaller;
    public SkinnedMeshRenderer render;
    public Material default_materyal;

    public GameObject[] islem_paneller;
    public Slider slider;
    Scene scene;
    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    public TextMeshProUGUI[] texler;
  
    public GameObject loading;
    public Slider slider2;
    private void Awake()
    {
        BellekYonetim.veriKaydet_string("dil", "tr");
        oyunsesi[0].volume = BellekYonetim.veriOku_float("oyunses");
        slider.value= BellekYonetim.veriOku_float("oyunses");
        oyunsesi[1].volume = BellekYonetim.veriOku_float("Menufx");
        Destroy(GameObject.FindWithTag("Menuses"));
        karakter_itemleri_ayarla();
    }
    void Start()
    {
        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[6]);
        dil_tercihi();
        dusman_create();
        reklam_yonetim.RequestInterstitial();

        reklam_yonetim.request_rewardAD();
        scene = SceneManager.GetActiveScene();
    }
    
    void dil_tercihi()
    {
        if (PlayerPrefs.GetString("dil") == "tr")
        {
            for (int i = 0; i < texler.Length; i++)
            {
                texler[i].text = dil_verileri_ana_obje[0].Dil_tr[i].metin;
            }
        }
        else
        {
            for (int i = 0; i < texler.Length; i++)
            {
                texler[i].text = dil_verileri_ana_obje[0].Dil_en[i].metin;
            }
        }
    }
    public void dusman_create()
    {
        for (int i = 0; i < dusman_Sayisi; i++)
        {
            dusmanlar[i].SetActive(true);
        }
    }
    public void dusmanlar_saldir()
    {
        foreach (var item in dusmanlar)
        {
            if (item.activeInHierarchy)
            {
                item.GetComponent<enemy>().animasyon_start();
            }
        }
        sona_Geldikmi = true;
        kimkazanýyor();
    }

    // Update is called once per frame

    public void KarakterYonetim(string islemturu, int gelensayi, Transform pozisyon)
    {
        switch (islemturu)
        {
            case "carpma":
                matematiksel_islemler.carpma(gelensayi, karakterler, pozisyon, olusma_efekt);
                break;

            case "toplama":
                matematiksel_islemler.toplama(gelensayi, karakterler, pozisyon, olusma_efekt);
                break;

            case "bolme":
                matematiksel_islemler.bolme(gelensayi, karakterler, yok_olma_efekt);
                break;

            case "cikarma":
                matematiksel_islemler.cikarma(gelensayi, karakterler, yok_olma_efekt);
                break;
        }
    }


    public void YokOlmaEfektiOlustur(Vector3 pozisyon, bool balyoz = false, bool hangi_ninja_oldu = false)
    {

        foreach (var item in yok_olma_efekt)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = pozisyon;
                item.GetComponent<ParticleSystem>().Play();
                item.GetComponent<AudioSource>().Play();
                if (!hangi_ninja_oldu)
                {
                    karakter_sayisi--;

                }
                else
                {
                    dusman_Sayisi--;
                }
                break;
            }

        }

        if (balyoz)
        {
            Vector3 NEW_pozisyon = new Vector3(pozisyon.x, .002f, pozisyon.z);
            foreach (var item in ezilme_efekt)
            {
                if (!item.activeInHierarchy)
                {
                    item.SetActive(true);
                    item.transform.position = NEW_pozisyon;
                    break;
                }
            }
        }
        if (!oyun_bittimi)
        {
            kimkazanýyor();
        }
    }
    void kimkazanýyor()
    {
        if (sona_Geldikmi)
        {
            
            if (karakter_sayisi <=1 || dusman_Sayisi == 0)
            {
                oyun_bittimi = true;
                foreach (var item in dusmanlar)
                {
                    if (item.activeInHierarchy)
                        item.GetComponent<Animator>().SetBool("attack", false);
                }
                foreach (var item in karakterler)
                {
                    if (item.activeInHierarchy)
                        item.GetComponent<Animator>().SetBool("attack", false);
                }
                
                reklam_yonetim.reklam_goster();
                if (karakter_sayisi < dusman_Sayisi || karakter_sayisi == dusman_Sayisi)
                {
                    islem_paneller[3].SetActive(true);
                   
                }
                else
                {
                    islem_paneller[2].SetActive(true);
                   
                    if (karakter_sayisi > 5)
                    {
                        if (scene.buildIndex == BellekYonetim.veriOku_int("SonLevel"))
                        {
                            BellekYonetim.veriKaydet_int("Puan", BellekYonetim.veriOku_int("Puan") + 500);
                            BellekYonetim.veriKaydet_int("SonLevel", BellekYonetim.veriOku_int("SonLevel") + 1);
                        }

                    }

                    else if (karakter_sayisi < 5 && karakter_sayisi > 3)
                    {
                        if (scene.buildIndex == BellekYonetim.veriOku_int("SonLevel"))
                        {
                            BellekYonetim.veriKaydet_int("Puan", BellekYonetim.veriOku_int("Puan") + 250);
                            BellekYonetim.veriKaydet_int("SonLevel", BellekYonetim.veriOku_int("SonLevel") + 1);
                        }
                    }
                    else
                    {
                        if (scene.buildIndex == BellekYonetim.veriOku_int("SonLevel"))
                        {
                            BellekYonetim.veriKaydet_int("Puan", BellekYonetim.veriOku_int("Puan") + 100);
                            BellekYonetim.veriKaydet_int("SonLevel", BellekYonetim.veriOku_int("SonLevel") + 1);
                        }
                    }

                }
            }
        }

    }
    public void karakter_itemleri_ayarla()
    {
        if(BellekYonetim.veriOku_int("sapka")!=-1)
            sapkalar[BellekYonetim.veriOku_int("sapka")].SetActive(true);
        if (BellekYonetim.veriOku_int("sopa") != -1)
            sopalar[BellekYonetim.veriOku_int("sopa")].SetActive(true);



        if (BellekYonetim.veriOku_int("tema") != -1)
        {
            Material[] materyal = render.materials;
            materyal[0] = materyaller[BellekYonetim.veriOku_int("tema")];
            render.materials = materyal;
        }
            
        else
        {
            Material[] materyal = render.materials;
            materyal[0] = default_materyal;
            render.materials = materyal;
        }
    }

    public void cikis_yapilacakmi(string a)
    {
        oyunsesi[1].Play();
        Time.timeScale = 0;
        if (a=="durdur")
        {
            islem_paneller[0].SetActive(true);
        }
        else if (a == "devam_et")
        {
            islem_paneller[0].SetActive(false);
            Time.timeScale = 1;
        }
        else if (a == "tekrar")
        {
            SceneManager.LoadScene(scene.buildIndex);
            Time.timeScale = 1;
        }
        else if (a == "anasayfa")
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1;
        }
    }

    public void ayarlar(string a)
    {
        if (a == "ayarla")
        {
            islem_paneller[1].SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            islem_paneller[1].SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void ses_ayarla()
    {
        BellekYonetim.veriKaydet_float("Oyunses", slider.value);
        oyunsesi[0].volume = slider.value;
            
    }
    public void sonraki_level()
    {
        
        StartCoroutine(LoadAsync(scene.buildIndex + 1));
    }
    IEnumerator LoadAsync(int Sceneindex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Sceneindex);
        loading.SetActive(true);
        while (!operation.isDone)
        {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
        loading.SetActive(false);

    }
    public void puankazan()
    {
        reklam_yonetim.odullu_Reklam_goster();
    }
}

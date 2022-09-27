using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Huseyin_Cakir;

public class LEVEL_MANAGER : MonoBehaviour
{
    public Button[] butonlar;
    
    bellekyonetim bellek = new bellekyonetim();
    public Sprite lock_image;
    public AudioSource ses;
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
   
    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    public TextMeshProUGUI[] texler;
    public GameObject loading;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
       // bellek.veriKaydet_string("dil", "tr");
        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[3]);
        dil_tercihi();


        ses.volume=bellek.veriOku_float("Menufx");

        int mevcut_level = bellek.veriOku_int("SonLevel")-4;
        int index =1;
        for (int i = 0; i < butonlar.Length; i++)
        {
            if(index <= mevcut_level)
            {
                butonlar[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1).ToString();
               int Sahne_index = index + 4;
                butonlar[i].onClick.AddListener(delegate { sahneyukle(Sahne_index); });
            }
            else
            {
                butonlar[i].GetComponent<Image>().sprite=lock_image;
                butonlar[i].enabled = false;
            }
            index++;
        }
      

    }
    void dil_tercihi()
    {
        if (bellek.veriOku_String("dil") == "tr")
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
    public void geri_don(int index)
    {
        ses.Play();
        SceneManager.LoadScene(0);
    }
    public void sahneyukle(int index)
    {
        ses.Play();
        StartCoroutine(LoadAsync(index));
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
       

    }
}

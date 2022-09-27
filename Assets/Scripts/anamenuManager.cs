using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Huseyin_Cakir;
using TMPro;
using UnityEngine.UI;

public class anamenuManager : MonoBehaviour
{
    bellekyonetim bellekYonetim = new bellekyonetim();
    public GameObject cýkýs_panel;
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
    Reklam reklam_yonetim = new Reklam();
    public List<ItemBilgi> varsayýlan_item_bilgileri = new List<ItemBilgi>();
    public List<DilVerileri_Ana_obje> varsayýlan_dil_bilgileri = new List<DilVerileri_Ana_obje>();


    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    public AudioSource button_ses;
    public TextMeshProUGUI[] texler;
    public GameObject loading;
    public Slider slider;
    void Start()
    {
        bellekYonetim.kontrol_et_Ve_Tanimla();
        veri_yonetim.first_save_dosya_create(varsayýlan_item_bilgileri, varsayýlan_dil_bilgileri);
        button_ses.volume = bellekYonetim.veriOku_float("Menufx");
        Debug.Log(Application.persistentDataPath);
      
        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[1]);
        dil_tercihi();

        

    }
    void dil_tercihi()
    {
        if (bellekYonetim.veriOku_String("dil") == "tr")
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

    // Update is called once per frame
 
    public void SahneYukle(int sahneIndex)
    {
        button_ses.Play();
        SceneManager.LoadScene(sahneIndex);
    }
    public void oyna()
    {
        button_ses.Play();
        
        StartCoroutine(LoadAsync(bellekYonetim.veriOku_int("SonLevel")));
    }
    public void cikisyap()
    {
        button_ses.Play();
        cýkýs_panel.SetActive(true);
    }
    public void cikis_yapilacakmi(bool a)
    {
        button_ses.Play();
        if (a)
        {
            Application.Quit();
        }
        else
        {
            cýkýs_panel.SetActive(false);
        }
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Huseyin_Cakir;

using UnityEngine.SceneManagement;
using TMPro;

public class ayarlar_manager : MonoBehaviour
{
    /*
     * 
     *   PlayerPrefs.SetFloat("Menuses", 1);
                PlayerPrefs.SetFloat("Menufx", 1);
                PlayerPrefs.SetFloat("oyunses", 1);
      */
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
    bellekyonetim bellekyon = new bellekyonetim();
    public TextMeshProUGUI[] texler;
    public TextMeshProUGUI diltex;
    public Button[] button;
#pragma warning disable IDE0052 // Okunmamýþ özel üyeleri kaldýr
    int index=0;
#pragma warning restore IDE0052 // Okunmamýþ özel üyeleri kaldýr
    public AudioSource ses;
    public Slider menuses;
    public Slider menufx;
    public Slider oyunses;
    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    void Start()
    {
        //bellekyon.veriKaydet_string("dil", "en");
        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[5]);
        dil_tercihi();
        dil_durumu_Ne();

        ses.volume= bellekyon.veriOku_float("Menufx");

        menuses.value = bellekyon.veriOku_float("Menuses");
        menufx.value = bellekyon.veriOku_float("Menufx");
        oyunses.value = bellekyon.veriOku_float("oyunses");
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
    void dil_durumu_Ne()
    {
        if (bellekyon.veriOku_String("dil") == "tr")
        {
            index = 0;
            diltex.text="TÜRKÇE";
            button[0].interactable = false;
            
        }
        else
        {
            index = 1;
            diltex.text = "ENGLISH";
            button[1].interactable = false;
            
        }
    }
    public void dil_degistir(string yon)
    {
        dil_change();
        if (yon == "ileri")
        {
            index = 1;
            diltex.text = "ENGLISH";
            button[1].interactable = false;
            button[0].interactable = true;
            bellekyon.veriKaydet_string("dil", "en");
            dil_tercihi();
        }
        else
        {
            index = 0;
            diltex.text = "TÜRKÇE";
            button[0].interactable = false;
            button[1].interactable = true;
            bellekyon.veriKaydet_string("dil", "tr");
            dil_tercihi();
        }
    }
    public void geridon()
    {
        ses.Play();
        SceneManager.LoadScene(0);
    }
    public void dil_change()
    {
        ses.Play();
    }
    public void ses_Ayarla(string hangi_ayar)
    {
        switch (hangi_ayar)
        {
            case "menuses":
                bellekyon.veriKaydet_float("Menuses", menuses.value);
                break;
            case "menufx":
                bellekyon.veriKaydet_float("Menufx", menufx.value);
                break;
            case "oyunsesi":
                bellekyon.veriKaydet_float("oyunses", oyunses.value);
                break;
        }
    }
  
    
}

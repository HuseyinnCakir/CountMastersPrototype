using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Huseyin_Cakir;
using TMPro;

public class market_manager : MonoBehaviour
{
    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    public TextMeshProUGUI[] texler;
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
    bellekyonetim bellek = new bellekyonetim();
    void Start()
    {
        //bellek.veriKaydet_string("dil", "en");
        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[4]);
        dil_tercihi();

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
    // Update is called once per frame
    void Update()
    {
        
    }
}

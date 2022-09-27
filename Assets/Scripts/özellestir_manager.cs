using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Huseyin_Cakir;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class özellestir_manager : MonoBehaviour
{
    public GameObject[] islempanelleri;
    public GameObject islem_canvas;
    public GameObject[] genel_paneller;
   // public TextMeshProUGUI satin_alma_text;
    public Button[] islem_butonlar;
    public int aktif_canvas;
    public TextMeshProUGUI puantext;
    public TextMeshProUGUI sapkatext;
    public TextMeshProUGUI sopatext;
    public TextMeshProUGUI materyaltext;
    [Header("                  Sapkalar")]
    public GameObject[] sapkalar;
    public Button[] sapka_butonlari;
    [Header("                  Sopalar")]
    public GameObject[] sopalar;
    public Button[] sopa_butonlari;
    [Header("                  Materyal")]
    public Material[] materyaller;
    public Material default_materyal;
    public Button[] materyal_butonlari;
    public SkinnedMeshRenderer render;
    public AudioSource[] button_sesleri;

    bellekyonetim bellekYonetim = new bellekyonetim();
    Veri_Yonetim veri_yonetim = new Veri_Yonetim();
    int sapka_index = -1;
    int sopa_index = -1;
    int materyal_index = -1;

    [Header("                  GENEL")]
    public List<ItemBilgi> item_bilgileri = new List<ItemBilgi>();
    public List<DilVerileri_Ana_obje> dil_verileri_ana_obje = new List<DilVerileri_Ana_obje>();
    List<DilVerileri_Ana_obje> dil_okunan_veriler = new List<DilVerileri_Ana_obje>();
    public TextMeshProUGUI[] texler;
    string satin_alma_text;
    string item_text;
    public Animator kaydetme_animasyon;
    void Start()
    { 
        puantext.text= bellekYonetim.veriOku_int("Puan").ToString();

        veri_yonetim.load();
        item_bilgileri = veri_yonetim.listeyi_aktar();
        durum_kontrol(0,true);
        durum_kontrol(1,true);
        durum_kontrol(2,true);
        //bellekYonetim.veriKaydet_string("dil", "tr");
        button_sesleri[0].volume = bellekYonetim.veriOku_float("Menuses");
        button_sesleri[1].volume = bellekYonetim.veriOku_float("Menufx");
        button_sesleri[2].volume = bellekYonetim.veriOku_float("oyunses");

        veri_yonetim.dil_load();
        dil_okunan_veriler = veri_yonetim.dil_listeyi_aktar();
        dil_verileri_ana_obje.Add(dil_okunan_veriler[2]);
        dil_tercihi();
        //Debug.Log(Application.persistentDataPath);
    }
    void dil_tercihi()
    {
        if (bellekYonetim.veriOku_String("dil") == "tr")
        {
            for (int i = 0; i < texler.Length; i++)
            {
                texler[i].text = dil_verileri_ana_obje[0].Dil_tr[i].metin;
            }
            satin_alma_text = dil_verileri_ana_obje[0].Dil_tr[5].metin;
            item_text= dil_verileri_ana_obje[0].Dil_tr[4].metin;


        }
        else
        {
            for (int i = 0; i < texler.Length; i++)
            {
                texler[i].text = dil_verileri_ana_obje[0].Dil_en[i].metin;
            }
            satin_alma_text = dil_verileri_ana_obje[0].Dil_en[5].metin;
            item_text = dil_verileri_ana_obje[0].Dil_en[4].metin;
        }
    }
    void durum_kontrol( int durum,bool islem=false)
    {
        if(durum == 0)
        {
            if (bellekYonetim.veriOku_int("sapka") == -1)
            {
                foreach (var item in sapkalar)
                {
                    item.SetActive(false);
                }
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = false;
                if (!islem)
                {
                    sapka_index = -1;
                    sapkatext.text = item_text;
                }
                
            }
            else
            {

                foreach (var item in sapkalar)
                {
                    item.SetActive(false);
                }
                sapka_index = bellekYonetim.veriOku_int("sapka");
                sapkalar[sapka_index].SetActive(true);
                sapkatext.text = item_bilgileri[sapka_index].item_name;
                texler[5].text =satin_alma_text;
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = true;
            }

        }
        else if(durum == 1)
        {
            if (bellekYonetim.veriOku_int("sopa") == -1)
            {
                foreach (var item in sopalar)
                {
                    item.SetActive(false);
                }
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = false;
                texler[5].text = satin_alma_text;
                if (!islem)
                {
                    sopa_index = -1;
                    sopatext.text = item_text;
                }
            }
            else
            {
                foreach (var item in sopalar)
                {
                    item.SetActive(false);
                }
                sopa_index = bellekYonetim.veriOku_int("sopa");
                sopalar[sopa_index].SetActive(true);

                sopatext.text = item_bilgileri[sopa_index+3].item_name;
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = true;
            }

        }
        else
        {
            if (bellekYonetim.veriOku_int("tema") == -1)
            {
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = false;
                texler[5].text = satin_alma_text;
                if (!islem)
                {
                    texler[5].text = satin_alma_text;
                    materyal_index = -1;
                    materyaltext.text = item_text;
                }
                else
                {
                    Material[] materyal = render.materials;
                    materyal[0] = default_materyal;
                    render.materials = materyal;
                    texler[5].text = satin_alma_text;
                }
              
            }
            else
            {
                materyal_index = bellekYonetim.veriOku_int("tema");
                Material[] materyal = render.materials;
                materyal[0] = materyaller[materyal_index];
                render.materials = materyal;
                materyaltext.text = item_bilgileri[materyal_index+6].item_name;
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = true;
            }
        }



      
    }
 
    public void sapka_switch(string islem)
    {
        button_sesleri[0].Play();
        if (islem == "ileri")
        {

            if (sapka_index == -1)
            {
                sapka_index = 0;
                sapkalar[sapka_index].SetActive(true);
                sapkatext.text = item_bilgileri[sapka_index].item_name;
            
            if (!item_bilgileri[sapka_index].is_buying)
            {
                    texler[5].text = item_bilgileri[sapka_index].puan + "-" + satin_alma_text; ;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sapka_index].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                   
                
            }
            else
            {
                    texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
                islem_butonlar[1].interactable = true;
            }
        }
        else
        {
            sapkalar[sapka_index].SetActive(false);
            sapka_index++;
            sapkalar[sapka_index].SetActive(true);
            sapkatext.text = item_bilgileri[sapka_index].item_name;
                if (!item_bilgileri[sapka_index].is_buying)
                {
                    texler[5].text = item_bilgileri[sapka_index].puan + "-" + satin_alma_text; ;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sapka_index].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                }
                else
                {
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                    islem_butonlar[1].interactable = true;
                }
            }
            if(sapka_index== sapkalar.Length - 1)
            {
                sapka_butonlari[1].interactable = false;
            }
            else
            {
                sapka_butonlari[1].interactable = true;
            }
            if (sapka_index != -1)
            {
                sapka_butonlari[0].interactable = true;
            }


        }
        else 
        {
            
            if (sapka_index != -1)
            {
                sapkalar[sapka_index].SetActive(false);
                sapka_index--;
                if(sapka_index!= -1)
                {
                    sapkalar[sapka_index].SetActive(true);
                    sapka_butonlari[0].interactable = true;
                    sapkatext.text = item_bilgileri[sapka_index].item_name;

                    if (!item_bilgileri[sapka_index].is_buying)
                    {
                        texler[5].text = item_bilgileri[sapka_index].puan + "-" + satin_alma_text; ;
                        islem_butonlar[1].interactable = false;
                        if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sapka_index].puan)
                        {
                            islem_butonlar[0].interactable = false;
                        }
                        else
                        {
                            islem_butonlar[0].interactable = true;

                        }
                    }
                    else
                    {
                        texler[5].text = satin_alma_text;
                        islem_butonlar[0].interactable = false;
                        islem_butonlar[1].interactable = true;
                    }
                }
                else
                        {
              
                    sapka_butonlari[0].interactable = false;
                    sapkatext.text = item_text;
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                }
                
            }
            else
            {
                sapka_butonlari[0].interactable =false;
                sapkatext.text = item_text;
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;

            }
            if (sapka_index != sapkalar.Length - 1)
            {
                sapka_butonlari[1].interactable = true;
            }
            

        }

    }


    public void sopa_switch(string islem)
    {
        button_sesleri[0].Play();
        
        if (islem == "ileri")
        {
            
            if (sopa_index == -1)
            {
                sopa_index = 0;
                sopalar[sopa_index].SetActive(true);
                sopatext.text = item_bilgileri[sopa_index+3].item_name;

                if (!item_bilgileri[sopa_index+3].is_buying)
                {
                    texler[5].text = item_bilgileri[sopa_index+3].puan + "-" + satin_alma_text;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sopa_index + 3].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                }
                else
                {
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                    islem_butonlar[1].interactable = true;
                }
            }


            else
            {
                sopalar[sopa_index].SetActive(false);
                sopa_index++;
                sopalar[sopa_index].SetActive(true);
                sopatext.text = item_bilgileri[sopa_index+3].item_name;
                if (!item_bilgileri[sopa_index].is_buying)
                {
                    texler[5].text = item_bilgileri[sopa_index+3].puan + "-" + satin_alma_text;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sopa_index + 3].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                }
                else
                {
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                    islem_butonlar[1].interactable = true;
                }
            }
            if (sopa_index == sopalar.Length - 1)
            {
                sopa_butonlari[1].interactable = false;
            }
            else
            {
                sopa_butonlari[1].interactable = true;
            }
            if (sopa_index != -1)
            {
                sopa_butonlari[0].interactable = true;
            }


        }
        else
        {
            
            if (sopa_index != -1)
            {
                sopalar[sopa_index].SetActive(false);
                sopa_index--;
                if (sopa_index != -1)
                {
                    sopalar[sopa_index].SetActive(true);
                    sopa_butonlari[0].interactable = true;
                    sopatext.text = item_bilgileri[sopa_index+3].item_name;

                    if (!item_bilgileri[sopa_index + 3].is_buying)
                    {
                        texler[5].text = item_bilgileri[sopa_index + 3].puan + "-" + satin_alma_text;
                        islem_butonlar[1].interactable = false;
                        if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[sopa_index + 3].puan)
                        {
                            islem_butonlar[0].interactable = false;
                        }
                        else
                        {
                            islem_butonlar[0].interactable = true;

                        }
                    }
                    else
                    {
                        texler[5].text = satin_alma_text;
                        islem_butonlar[0].interactable = false;
                        islem_butonlar[1].interactable = true;
                    }
                }
                else
                {

                    sopa_butonlari[0].interactable = false;
                    sopatext.text = item_text;
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                }

            }
            else
            {
                sopa_butonlari[0].interactable = false;
                sopatext.text = item_text;
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
            }
            if (sopa_index != sopalar.Length - 1)
            {
                sopa_butonlari[1].interactable = true;
            }


        }

    }

    public void materyal_switch(string islem)
    {
        button_sesleri[0].Play();
        if (islem == "ileri")
        {

            if (materyal_index == -1)
            {
                materyal_index = 0;
                Material[] materyal = render.materials;
                materyal[0] = materyaller[materyal_index];
                render.materials = materyal;
               
                materyaltext.text = item_bilgileri[materyal_index + 6].item_name;

                if (!item_bilgileri[materyal_index + 6].is_buying)
                {
                    texler[5].text = item_bilgileri[materyal_index + 6].puan + "-" + satin_alma_text; ;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[materyal_index + 6].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                }
                else
                {
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                    islem_butonlar[1].interactable = true;
                }
            }
            else
            {
                materyal_index++;
                Material[] materyal = render.materials;
                materyal[0] = materyaller[materyal_index];
                render.materials = materyal;
                materyaltext.text = item_bilgileri[materyal_index + 6].item_name;


                    if (!item_bilgileri[materyal_index + 6].is_buying)
                {
                    texler[5].text = item_bilgileri[materyal_index + 6].puan + "-" + satin_alma_text; ;
                    islem_butonlar[1].interactable = false;
                    if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[materyal_index + 6].puan)
                    {
                        islem_butonlar[0].interactable = false;
                    }
                    else
                    {
                        islem_butonlar[0].interactable = true;

                    }
                }
                else
                {
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                    islem_butonlar[1].interactable = true;
                }

            }
            if (materyal_index == materyaller.Length - 1)
            {
                materyal_butonlari[1].interactable = false;
            }
            else
            {
                materyal_butonlari[1].interactable = true;
            }
            if (materyal_index != -1)
            {
                materyal_butonlari[0].interactable = true;
            }


        }
        else
        {

            if (materyal_index != -1)
            {
                
                materyal_index--;
                if (materyal_index != -1)
                {
                    Material[] materyal = render.materials;
                    materyal[0] = materyaller[materyal_index];
                    render.materials = materyal;

                    materyal_butonlari[0].interactable = true;
                    materyaltext.text = item_bilgileri[materyal_index + 6].item_name;
                    if (!item_bilgileri[materyal_index + 6].is_buying)
                    {
                        texler[5].text = item_bilgileri[materyal_index + 6].puan + "-" + satin_alma_text; ;
                        islem_butonlar[1].interactable = false;
                        if (bellekYonetim.veriOku_int("Puan") < item_bilgileri[materyal_index + 6].puan)
                        {
                            islem_butonlar[0].interactable = false;
                        }
                        else
                        {
                            islem_butonlar[0].interactable = true;

                        }
                    }
                    else
                    {
                        texler[5].text = satin_alma_text;
                        islem_butonlar[0].interactable = false;
                        islem_butonlar[1].interactable = true;
                    };
                }
                else
                {
                    Material[] materyal = render.materials;
                    materyal[0] = default_materyal;
                    render.materials = materyal;
                    materyal_butonlari[0].interactable = false;
                    materyaltext.text = item_text;
                    texler[5].text = satin_alma_text;
                    islem_butonlar[0].interactable = false;
                }

            }
            else
            {
                Material[] materyal = render.materials;
                materyal[0] = default_materyal;
                render.materials = materyal;
                materyal_butonlari[0].interactable = false;
                materyaltext.text = item_text;
                texler[5].text = satin_alma_text;
                islem_butonlar[0].interactable = false;
            }
            if (materyal_index != materyaller.Length - 1)
            {
                materyal_butonlari[1].interactable = true;
            }


        }

    }
    public void islem_paneli_Ac(int index)
    {
        button_sesleri[0].Play();
        durum_kontrol(index);
        genel_paneller[0].SetActive(true);
        aktif_canvas = index;
        islempanelleri[index].SetActive(true);
        genel_paneller[1].SetActive(true);
        islem_canvas.SetActive(false);
        
        
    }
    void satin_al_sonuc(int index)
    {
        item_bilgileri[index].is_buying = true;
        bellekYonetim.veriKaydet_int("puan", bellekYonetim.veriOku_int("Puan") - item_bilgileri[index].puan);
        texler[5].text = satin_alma_text;
        islem_butonlar[0].interactable = false;
        islem_butonlar[1].interactable = true;
        puantext.text = bellekYonetim.veriOku_int("Puan").ToString();
    }
    void kaydet_sonuc(string key,int index)
    {
        bellekYonetim.veriKaydet_int(key, index);
        islem_butonlar[1].interactable = false;
        if (!kaydetme_animasyon.GetBool("goster"))
        {
            kaydetme_animasyon.SetBool("goster", true);
        }
    }
    public void satinal()
    {
        button_sesleri[1].Play();
        if (aktif_canvas != -1)
        {
            switch (aktif_canvas)
            {
                case 0:
                    satin_al_sonuc(sapka_index);
                
                    break;
                case 1:
                    int index = sopa_index + 3;
                    satin_al_sonuc(index);
                    break;
                case 2:
                    int index2 = materyal_index + 6;
                    satin_al_sonuc(index2);
                    break;
            }

        }
    }
    public void kaydet()
    {
        button_sesleri[2].Play();
        if (aktif_canvas != -1)
        {
            switch (aktif_canvas)
            {
                case 0:
                    kaydet_sonuc("sapka", sapka_index);
                   
               
                    break;
                case 1:
                    kaydet_sonuc("sopa", sopa_index);
                   
                    break;
                case 2:
                    kaydet_sonuc("tema", materyal_index);
                    
                    break;
            }

        }
    }
    public void geriDon()
    {
        button_sesleri[0].Play();
        genel_paneller[0].SetActive(false);
        islem_canvas.SetActive(true);
        genel_paneller[1].SetActive(false);
        islempanelleri[aktif_canvas].SetActive(false);
        durum_kontrol(aktif_canvas,true);
        aktif_canvas--;
    }
    public void anamenuye_don()
    {
        button_sesleri[0].Play();
        veri_yonetim.save(item_bilgileri);
        SceneManager.LoadScene(0);
       
    }

}

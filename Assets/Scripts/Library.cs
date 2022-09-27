using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GoogleMobileAds.Api;

namespace Huseyin_Cakir
{
    public class Library
    {
        public void carpma(int gelensayi, List<GameObject> karakterler, Transform pozisyon, List<GameObject> olusturma_efektleri)
        {
            int dongusayisi = (GameManager.karakter_sayisi * gelensayi) - GameManager.karakter_sayisi;
            int sayi = 0;
            foreach (var item in karakterler)
            {
                if (sayi < dongusayisi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in olusturma_efektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {

                                item2.SetActive(true);
                                item2.transform.position = pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = pozisyon.transform.position;
                        item.SetActive(true);
                        sayi++;

                    }

                }
                else { sayi = 0; break; }
            }
            GameManager.karakter_sayisi *= gelensayi;

        }
        public void toplama(int gelensayi, List<GameObject> karakterler, Transform pozisyon, List<GameObject> olusturma_efektleri)
        {
            int sayi2 = 0;
            foreach (var item in karakterler)
            {
                if (sayi2 < gelensayi)
                {
                    if (!item.activeInHierarchy)
                    {
                        foreach (var item2 in olusturma_efektleri)
                        {
                            if (!item2.activeInHierarchy)
                            {

                                item2.SetActive(true);
                                item2.transform.position = pozisyon.position;
                                item2.GetComponent<ParticleSystem>().Play();
                                item2.GetComponent<AudioSource>().Play();
                                break;
                            }
                        }
                        item.transform.position = pozisyon.transform.position;
                        item.SetActive(true);
                        sayi2++;

                    }

                }
                else { sayi2 = 0; break; }
            }
            GameManager.karakter_sayisi += gelensayi;
        }
        public void cikarma(int gelensayi, List<GameObject> karakterler, List<GameObject> yok_olma_efektleri)
        {
            if (GameManager.karakter_sayisi < gelensayi)
            {
                foreach (var item in karakterler)
                {
                    foreach (var item2 in yok_olma_efektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {
                            Vector3 pozisyon = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = pozisyon;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.karakter_sayisi = 1;
            }
            else
            {
                int sayi3 = 0;
                foreach (var item in karakterler)
                {

                    if (sayi3 != gelensayi)
                    {


                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in yok_olma_efektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {

                                    Vector3 pozisyon = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = pozisyon;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }


                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;

                        }

                    }
                    else { sayi3 = 0; break; }
                }
                GameManager.karakter_sayisi -= gelensayi;


            }
        }
        public void bolme(int gelensayi, List<GameObject> karakterler, List<GameObject> yok_olma_efektleri)
        {
            if (GameManager.karakter_sayisi <= 2)
            {
                foreach (var item in karakterler)
                {
                    foreach (var item2 in yok_olma_efektleri)
                    {
                        if (!item2.activeInHierarchy)
                        {

                            Vector3 pozisyon = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                            item2.SetActive(true);
                            item2.transform.position = pozisyon;
                            item2.GetComponent<ParticleSystem>().Play();
                            item2.GetComponent<AudioSource>().Play();
                            break;
                        }
                    }
                    item.transform.position = Vector3.zero;
                    item.SetActive(false);
                }
                GameManager.karakter_sayisi = 1;
            }
            else
            {
                int bolen = GameManager.karakter_sayisi / gelensayi;
                int sayi3 = 0;
                foreach (var item in karakterler)
                {
                    if (sayi3 != bolen)
                    {
                        if (item.activeInHierarchy)
                        {
                            foreach (var item2 in yok_olma_efektleri)
                            {
                                if (!item2.activeInHierarchy)
                                {

                                    Vector3 pozisyon = new Vector3(item.transform.position.x, .23f, item.transform.position.z);
                                    item2.SetActive(true);
                                    item2.transform.position = pozisyon;
                                    item2.GetComponent<ParticleSystem>().Play();
                                    item2.GetComponent<AudioSource>().Play();
                                    break;
                                }
                            }
                            item.transform.position = Vector3.zero;
                            item.SetActive(false);
                            sayi3++;

                        }

                    }
                    else { sayi3 = 0; break; }
                }
                if (GameManager.karakter_sayisi % gelensayi == 0)
                    GameManager.karakter_sayisi /= gelensayi;
                else if (GameManager.karakter_sayisi % gelensayi == 1)
                {
                    GameManager.karakter_sayisi /= gelensayi;
                    GameManager.karakter_sayisi++;
                }
                else if (GameManager.karakter_sayisi % gelensayi == 2)
                {
                    GameManager.karakter_sayisi /= gelensayi;
                    GameManager.karakter_sayisi += 2;
                }
                else if (GameManager.karakter_sayisi % gelensayi == 3)
                {
                    GameManager.karakter_sayisi /= gelensayi;
                    GameManager.karakter_sayisi += 3;
                }

            }

        }
    }


    public class bellekyonetim
    {
        public void veriKaydet_string(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }
        public void veriKaydet_float(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        public void veriKaydet_int(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        public string veriOku_String(string key)
        {
            return PlayerPrefs.GetString(key);
        }
        public int veriOku_int(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        public float veriOku_float(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void kontrol_et_Ve_Tanimla()
        {
            if (!PlayerPrefs.HasKey("SonLevel"))
            {
                PlayerPrefs.SetInt("SonLevel", 5);
                PlayerPrefs.SetInt("Puan", 10000);
                PlayerPrefs.SetInt("sapka", -1);
                PlayerPrefs.SetInt("sopa", -1);
                PlayerPrefs.SetInt("tema", -1);
                PlayerPrefs.SetFloat("Menuses", 1);
                PlayerPrefs.SetFloat("Menufx", 1);
                PlayerPrefs.SetFloat("oyunses", 1);
                PlayerPrefs.SetString("dil", "tr");
            }
        }
    }




    
    [Serializable]
    public class ItemBilgi
    {
        public int grup_index;
        public int item_index;
        public string item_name;
        public int puan;
        public bool is_buying;

    }
    public class Veri_Yonetim
    {
        public void save(List<ItemBilgi> item_bilgileri)
        {
            //item_bilgileri[1].is_buying = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemVerileri.gd");
            bf.Serialize(file, item_bilgileri);
            file.Close();
        }
        public void first_save_dosya_create(List<ItemBilgi> item_bilgileri, List<DilVerileri_Ana_obje> dil_bilgileri)
        {
            
            //item_bilgileri[1].is_buying = true;
            if (!File.Exists(Application.persistentDataPath + "/ItemVerileri.gd")) //dosya yoksa oluþtur!
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemVerileri.gd");
                bf.Serialize(file, item_bilgileri);
                file.Close();
            }
            if (!File.Exists(Application.persistentDataPath + "/DilVerileri.gd")) //dosya yoksa oluþtur!
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/DilVerileri.gd");
                bf.Serialize(file, dil_bilgileri);
                file.Close();
            }
           




        }
        List<ItemBilgi> item_liste;
        List<DilVerileri_Ana_obje> dilverileri_liste;
        public void load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemVerileri.gd", FileMode.Open);
                item_liste = (List<ItemBilgi>)bf.Deserialize(file);

                file.Close();

            }
        }
        public void dil_load()
        {
            if (File.Exists(Application.persistentDataPath + "/DilVerileri.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/DilVerileri.gd", FileMode.Open);
                dilverileri_liste = (List<DilVerileri_Ana_obje>)bf.Deserialize(file);

                file.Close();

            }
        }
        public List<ItemBilgi> listeyi_aktar()
        {
            return item_liste;
        }
        public List<DilVerileri_Ana_obje> dil_listeyi_aktar()
        {
            return dilverileri_liste;
        }
    }


    [Serializable]
    public class DilVerileri_Ana_obje // dil yönetim
    {
        
        public List<Dilverileri_tr> Dil_tr = new List<Dilverileri_tr>();
        public List<Dilverileri_tr> Dil_en = new List<Dilverileri_tr>();
       

    }


    [Serializable]
    public class Dilverileri_tr
    {
        public string metin;
    }


    //reklam yönetimi


    public class Reklam
    {
        private InterstitialAd interstitial;
        private RewardedAd rewardAD;

        public void RequestInterstitial()
        {
            string id;
#if UNITY_ANDROID
            id = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
            id = "ca-app-pub-3940256099942544/4411468910";
#else       
id= "unexpected_platform";
#endif



            interstitial = new InterstitialAd(id);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += gecis_reklami_kapatildi;
        }
        void gecis_reklami_kapatildi(object sender,EventArgs args)
        {
            interstitial.Destroy();
            RequestInterstitial();
        }
        public void reklam_goster()
        {
           
            
                if (interstitial.IsLoaded())
                {
                    
                    interstitial.Show();
                }
                else
                {
                    interstitial.Destroy();
                    RequestInterstitial();
                }
              
            }
          
        public void request_rewardAD()
        {
            string id;
#if UNITY_ANDROID
            id = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            id = "ca-app-pub-3940256099942544/1712485313";
#else       
id= "unexpected_platform";
#endif



            rewardAD = new RewardedAd(id);
            AdRequest request = new AdRequest.Builder().Build();
            rewardAD.LoadAd(request);
            rewardAD.OnUserEarnedReward += odullu_reklam_tamamlandi;
            rewardAD.OnAdClosed += odul_reklami_kapatildi;
            rewardAD.OnAdLoaded += odullu_reklam_yuklendi;
            
        }
        public void odul_reklami_kapatildi(object sender, EventArgs args)
        {
            Debug.Log("reklam kapatildi");
            request_rewardAD();
        }
        public void odullu_reklam_tamamlandi(object sender, Reward e)
        {
            string type = e.Type;
            Debug.Log("ODUL ALINSIN" + type + "--" + e.Amount);
        }
        public void odullu_reklam_yuklendi(object sender, EventArgs args)
        {
            Debug.Log("reklam yuklendi");
        }
        public void odullu_Reklam_goster()
        {
            if (rewardAD.IsLoaded())
            {
                rewardAD.Show();
            }
        }
    }

    }


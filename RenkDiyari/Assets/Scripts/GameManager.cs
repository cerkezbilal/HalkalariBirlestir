using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    GameObject SeciliObje;//Secilen cember
    GameObject SeciliPlatform;//Cemberi taşımak istediğimiz stand
    Cember _cember;//Secilen cemberin script dosyasına ulaşacağımız değişken
    public bool HareketVar;//Birden fazla çember seçilmesini önlemek için

    //bunlara daha var
    public int HedefStandSayisi;//Hedeflenen standın sıra numarası
    int TamamlananStandSayisi;//4 çemberin tamamlandığı stand sayısı
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Objeyi seçebilmek için ışın oluşturuyoruz
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,100f))
            {
                if(hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    //Çemberi seçme ve Eğer çember seçili ise başka stand a tıklandığında çemberin oraya gitmesi

                    if(SeciliObje != null && SeciliPlatform != hit.collider.gameObject)
                    {
                        //Seçili bir çember var ve çemberin gitmesini istediğimiz stand da seçili
                        //Çember Hareket edecek


                    }
                    else
                    {
                        //Stand seçme işlemi ve çember seçme işlemi
                        Stand _Stand = hit.collider.GetComponent<Stand>();//Stand scriptine ulaşmak için

                        SeciliObje = _Stand.EnUsttekiCemberiVer();//Stand ın en üstteki çemberini seciliobje değişkenine atadık
                        _cember = SeciliObje.GetComponent<Cember>();//Seçtiğimiz cemberin Cember adlı scriptine ulaştık.
                        HareketVar = true;//Hareket başladı

                        //Seçilen çemberin hareket etme durumu var mı?

                    }
                }
            }
        }

    }
}
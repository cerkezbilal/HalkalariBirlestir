using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cember : MonoBehaviour
{
    //Baska dosyalardan cagıracaklarımız

    public GameObject _AitOlduguStand;//Cemberi aldığımız stand
    public GameObject _AitOlduguCemberSkoeti;//Cemberi ait olduğu soket
    public bool HareketEdebilirMi;//Cember hareket için uygun mu yani en üstte mi
    public string Renk;//Cemberin rengi
    public GameManager _GameManager;

    //Bu scriptte kullanılacaklar
    GameObject HareketPozisyonu;//Cemberin Pozisyonu
    GameObject AitOlduguStand;//Cemberin ait oldugu stand

    bool Secildi, PosDegistir, SoketOtur, SoketeGeriGit; //Cember secildi mi, Pozisyonu değişecek mi, Sokete yerleş, Sokete geri git


    public void HareketEt(string islem, GameObject Stand = null,GameObject Soket = null, GameObject GidilecekObje = null)
    {
        //islem: pozisyon mu değişti? seçildi mi? sokete mi oturdu gibi işlemler

        switch (islem)
        {
            case "Secim":
                HareketPozisyonu = GidilecekObje;//Cember gidilecek objeye gitsin
                Secildi = true;
                break;
            case "PozisyonDegistir":
                break;
            case "SoketeOtur":
                break;
            case "SoketeGeriGit":
                break;

        }
    }

    void Update()
    {
        if (Secildi)
        {
            //Eğer seçildiyse cemberin pozisyonunu hareket pozisyonuna eşitle lerp: yavaş bi şekilde
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if(Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10f)
            {
                //iki pozisyon arasındaki mesafe 0.1 den küçük ise
                Secildi = false;//pozisyonlar eşitlendiyse seçilme işlemi bitti
            }

        }
    }
}

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
    GameObject GidecegiStand;//Cemberin ait oldugu stand

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
                GidecegiStand = Stand;//Gideceği standı fonksiyondan gelecek standa eşitliyorum
                _AitOlduguCemberSkoeti = Soket;//ait olduğu soketi yeni gittiği soket yapıyoruz
                HareketPozisyonu = GidilecekObje; //Hareket pozisyonu artık yeni gittiği standdaki hareket pozisyonu
                PosDegistir = true;//Pozisyon değiştirdiği için true yapıyoruz.
                break; 
            case "SoketeGeriGit":
                //Cember aynı sokete geri dönüyor o yüzden değişkeni true yapıyoruz. Update de bunu kullanıcaz
                SoketeGeriGit = true;

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

        if (PosDegistir)
        {
            //Eğer pozisyon seçildiyse cemberin pozisyonunu hareket pozisyonuna eşitle lerp: yavaş bi şekilde
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .2f);
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .10f)
            {
                //iki pozisyon arasındaki mesafe 0.1 den küçük ise
                SoketOtur = true;//sokete oturma işlemi gerçekleşsin diye
                PosDegistir = false; //Hareket etme işlemi dursun.
            }

        }
        if (SoketOtur)
        {
            //Eğer sokete oturduysa cemberin pozisyonunu hareket pozisyonuna eşitle lerp: yavaş bi şekilde
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSkoeti.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSkoeti.transform.position) < .10f)
            {
                //iki pozisyon arasındaki mesafe 0.1 den küçük ise
                transform.position = _AitOlduguCemberSkoeti.transform.position; //Artık çemberin yeni konumu yeni ait olduğu çember soketi
                SoketOtur = false;//sokete oturma işlemi bitti

                _AitOlduguStand = GidecegiStand;//Çemberin ait olduğu standı yeni gittiği stand yapıyoruz

                if(_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count > 1)
                {
                    //Eğer stand da 2 ya da daha fazla çember varsa. Eğer biz ekledikten sonra 1 olduysa zaten biz koymadna önce hiç çember yoktu demektir. Biz koymadna önce bir ya da daha fazla varsa diye bu kodu yazıyoruz
                    _AitOlduguStand.GetComponent<Stand>()._Cemberler[_AitOlduguStand.GetComponent<Stand>()._Cemberler.Count - 2].GetComponent<Cember>().HareketEdebilirMi = false;//Yeni çeber geldiği için en üstteki çember değişmiş oldu o yüzden altta kalan çemberin hareket edilebilirliğini false yaptık.
                    
                }
                _GameManager.HareketVar = false;//Hareket işlemini sonlandırdık sokete oturduğu için

            }

        }
        if (SoketeGeriGit)
        {
            //Eğer sokete oturduysa cemberin pozisyonunu hareket pozisyonuna eşitle lerp: yavaş bi şekilde
            transform.position = Vector3.Lerp(transform.position, _AitOlduguCemberSkoeti.transform.position, .2f);
            if (Vector3.Distance(transform.position, _AitOlduguCemberSkoeti.transform.position) < .10f)
            {
                //iki pozisyon arasındaki mesafe 0.1 den küçük ise
                transform.position = _AitOlduguCemberSkoeti.transform.position; //Artık çemberin yeni konumu yeni ait olduğu çember soketi
                SoketeGeriGit = false;//sokete geri gitme işlemi bitti

               
                _GameManager.HareketVar = false;//Hareket işlemini sonlandırdık sokete oturduğu için

            }

        }

    }
}

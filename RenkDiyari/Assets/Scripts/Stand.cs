using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{

    public GameObject HareketPozisyonu;//Hareket pozisyonuna ulaşmak için
    public GameObject[] soketler; //Standta olacak olan soket sistemi
    public int bosOlanSoket;//Standta bulunan soketin indexi
    public List<GameObject> _Cemberler;//Cember limit listemi için 4 den fazla çember gelemez

    [SerializeField]
    private GameManager _GameManager;//Bununla çember hareketleri kontrol edilecek

    int CemberTamamlanmaSayisi;//Renkleri aynı olan çember sayısını tutacak

   public GameObject EnUsttekiCemberiVer()
    {
        //Standtaki en son yani en üstteki çember yakalamak için listenin son elemanını almak ile olur

        return _Cemberler[_Cemberler.Count - 1];
    }

    public GameObject MusaitSoketiVer()
    {
        //Standtaki en son yani en üstteki çember yakalamak için listenin son elemanını almak ile olur

        return soketler[bosOlanSoket];
    }

    public void SoketDegistirmeIslemleri(GameObject SilinecekObje)
    {
        //Çember başka bir sokete gidecekse olacaklar bu methodta
        //SlinecekObje: Çember ait olduğu standı terkedeceği için o standtaki çemberler listesinden silinmeli

        _Cemberler.Remove(SilinecekObje);//Listeden silindi

        if(_Cemberler.Count != 0)
        {
            bosOlanSoket--;
            _Cemberler[_Cemberler.Count - 1].GetComponent<Cember>().HareketEdebilirMi = true;
        }
        else
        {
            bosOlanSoket = 0;
        }


    }

    public void CemberleriKontrolEt()
    {
        //Renk açısından çemberleri kontrol edeceğiz

        if (_Cemberler.Count == 4)
        {
            //Soketteki çember sayısı 4 ise

            string Renk = _Cemberler[0].GetComponent<Cember>().Renk; //Standtaki en alttaki çemberin rengini alıyoruz referans olarak

            foreach (var item in _Cemberler)
            {
               if(Renk == item.GetComponent<Cember>().Renk)
                {
                    //Çemberdeki tüm renkler eşit ise

                    CemberTamamlanmaSayisi++;//Aynı renkteki çember

                }
            }

            if(CemberTamamlanmaSayisi == 4)
            {
                _GameManager.StandTamamlandi();
                TamamlanmisStandIslemleri();
            }
            else
            {
                
                CemberTamamlanmaSayisi = 0;
            }
        }
        
    }

    //Renk olarak tamamlanan standlar üzerinde işlemler
    void TamamlanmisStandIslemleri()
    {
        foreach (var item in _Cemberler)
        {
            item.GetComponent<Cember>().HareketEdebilirMi = false;//Eğer renkler tamamlandıysa daha hareket yapılmasın


            //Cemberin alfa kanalını değiştiriyoruz
            Color32 color = item.GetComponent<MeshRenderer>().material.GetColor("_Color");//Materyalin rengini aldık
            color.a = 150;//alınan rengin alfa kanalını 150 yaptık

            item.GetComponent<MeshRenderer>().material.SetColor("_Color", color);//çembere değiştirilen rengi tekrar verdik

            gameObject.tag = "TamamlanmisStand";//Eğer renk tamamlandıysa stand a artık tıklanamasın
        }
    }
}

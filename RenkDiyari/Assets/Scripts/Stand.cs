using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{

    public GameObject HareketPozisyonu;//Hareket pozisyonuna ulaşmak için
    public GameObject[] skoteler; //Standta olacak olan soket sistemi
    public int bosOlanSoket;//Standta bulunan soketin indexi
    public List<GameObject> _Cemberler;//Cember limit listemi için 4 den fazla çember gelemez

    [SerializeField]
    private GameManager _GameManager;//Bununla çember hareketleri kontrol edilecek

   

   public GameObject EnUsttekiCemberiVer()
    {
        //Standtaki en son yani en üstteki çember yakalamak için listenin son elemanını almak ile olur

        return _Cemberler[_Cemberler.Count - 1];
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
}

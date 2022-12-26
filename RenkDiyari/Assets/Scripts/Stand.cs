using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{

    public GameObject HareketPozisyonu;//Hareket pozisyonuna ulaşmak için
    public GameObject[] skoteler; //Standta olacak olan soket sistemi
    public int bosOlanSkoet;//Standta hangi soket boş ona göre çember oraya oturacak.
    public List<GameObject> _Cemberler;//Cember limit listemi için 4 den fazla çember gelemez

    [SerializeField]
    private GameManager _GameManager;//Bununla çember hareketleri kontrol edilecek

    private void Awake()
    {
        _Cemberler = new List<GameObject>();
    }

   public GameObject EnUsttekiCemberiVer()
    {
        //Standtaki en son yani en üstteki çember yakalamak için listenin son elemanını almak ile olur

        return _Cemberler[_Cemberler.Count - 1];
    }
}

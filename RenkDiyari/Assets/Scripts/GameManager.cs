using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    GameObject SeciliObje;//Secilen cember
    GameObject SeciliStand;//Cemberi ait olduğu stand
    Cember _cember;//Secilen cemberin script dosyasına ulaşacağımız değişken
    public bool HareketVar;//Birden fazla çember seçilmesini önlemek için

    public AudioSource cemberSecme;
    public AudioSource cemberGirme;

    public GameObject Panel;

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

                    if(SeciliObje != null && SeciliStand!= hit.collider.gameObject)
                    {
                        cemberSecme.Play();
                        //Seçili bir çember var ve çemberin gitmesini istediğimiz stand da seçili ve çemberin ait olduğu standtan farklı ise
                        //Çember Hareket edecek

                        Stand _Stand = hit.collider.GetComponent<Stand>();//Tıkladığım yeni standı alıyorum.

                        //Seçtiğim stand 4 çembere sahipse dolu ise gitmesin
                        if(_Stand._Cemberler.Count != 4 && _Stand._Cemberler.Count != 0)
                        {
                            //Soketin 4 ü de dolu değilse ya da tamamen boş değilse

                            
                            //Renk kontrolü
                            if (_cember.Renk == _Stand._Cemberler[_Stand._Cemberler.Count - 1].GetComponent<Cember>().Renk )
                            {
                                
                                Debug.Log("Caliştim");
                                //Seçilen Cemberin rengi ile seçilen stand daki en üstteli çemberin rengi aynıysa

                                SeciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(SeciliObje);//Çemberi seçtiğimiz Standtan seçtiğimiz çemberi çıkarıyoruz.

                                _cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);//Cember artık hareket edecek o yüzden işlem pozisyon değiştir. Gidelecek Stand ekranda dokunduğumuz stand., gidilecek soket müsait olan yani en üstteki boş soket, gidilecek pozisyonda seçilen standın hareketpozisyonu.

                                _Stand.bosOlanSoket++;//Sokete çember eklediğimiz için bossoket bir arttırılmalı. (Boş soket dizi indexi)
                                _Stand._Cemberler.Add(SeciliObje);//Standtaki çemberler listesine gönderilen çemberi ekliyoruz.

                                _Stand.CemberleriKontrolEt();//Cemberlerin renk işlemi tamamlandı mı

                                //Seçili obje(Cember) ve seçili stand ı işlem tamamlandığı için boşaltıyoruz
                                SeciliObje = null;
                                SeciliStand = null;


                            }
                            else
                            {

                                
                                //renkler aynı değil stand a geri otur

                                _cember.HareketEt("SoketeGeriGit");

                                //Cemberi standa geri oturtunca secimleri temizle
                                SeciliObje = null;
                                SeciliStand = null;
                            }

                           

                        }
                        else if(_Stand._Cemberler.Count == 0)
                        {
                            //Soketin boş ise
                            

                            SeciliStand.GetComponent<Stand>().SoketDegistirmeIslemleri(SeciliObje);//Çemberi seçtiğimiz Standtan seçtiğimiz çemberi çıkarıyoruz.

                            _cember.HareketEt("PozisyonDegistir", hit.collider.gameObject, _Stand.MusaitSoketiVer(), _Stand.HareketPozisyonu);//Cember artık hareket edecek o yüzden işlem pozisyon değiştir. Gidelecek Stand ekranda dokunduğumuz stand., gidilecek soket müsait olan yani en üstteki boş soket, gidilecek pozisyonda seçilen standın hareketpozisyonu.

                            _Stand.bosOlanSoket++;//Sokete çember eklediğimiz için bossoket bir arttırılmalı. (Boş soket dizi indexi)
                            _Stand._Cemberler.Add(SeciliObje);//Standtaki çemberler listesine gönderilen çemberi ekliyoruz.

                            _Stand.CemberleriKontrolEt();//Cemberlerin renk işlemi tamamlandı mı

                            //Seçili obje(Cember) ve seçili stand ı işlem tamamlandığı için boşaltıyoruz
                            SeciliObje = null;
                            SeciliStand = null;
                        }
                        else
                        {
                           
                            //dolu stand seçilmiş çemberi stand a geri oturt.

                            _cember.HareketEt("SoketeGeriGit");

                            //Cemberi standa geri oturtunca secimleri temizle
                            SeciliObje = null;
                            SeciliStand = null;
                        }




                       
                        
                    }
                    else if(SeciliStand == hit.collider.gameObject)
                    {

                       
                        //aynı stand tekrar seçilmiş çemberi stand a geri oturt.
                        _cember.HareketEt("SoketeGeriGit");

                        //Cemberi standa geri oturtunca secimleri temizle
                        SeciliObje = null;
                        SeciliStand = null;
                    }
                    else
                    {
                        cemberGirme.Play();

                        //Stand seçme işlemi ve çember seçme işlemi
                        Stand _Stand = hit.collider.GetComponent<Stand>();//Stand scriptine ulaşmak için

                        SeciliObje = _Stand.EnUsttekiCemberiVer();//Stand ın en üstteki çemberini seciliobje değişkenine atadık
                        _cember = SeciliObje.GetComponent<Cember>();//Seçtiğimiz cemberin Cember adlı scriptine ulaştık.
                        HareketVar = true;//Hareket başladı

                        if (_cember.HareketEdebilirMi)
                        {
                            //Cember hareket edebiliyorsa
                            //Cemberi hareket ettir. Parametre olarak işlem: Seçim, Stand: gerekyok stand, sokete gerek yok, Gidilecek obje cemberin ait olduğu standta bulunan hareket objesi
                            _cember.HareketEt("Secim", null, null, _cember._AitOlduguStand.GetComponent<Stand>().HareketPozisyonu);
                            SeciliStand = _cember._AitOlduguStand;//Secilen Standa cemberin ait olduğu standtı veriyoruz.
                        } 

                    }
                }
            }
        }

    }


    //Standtaki renkler tamamlanınca çalışacak
    public void StandTamamlandi()
    {
        TamamlananStandSayisi++;

        if(TamamlananStandSayisi == HedefStandSayisi)
        {
            Panel.SetActive(true);
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void NextLevel()
    {
        Panel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

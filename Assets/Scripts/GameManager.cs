using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public SpawnerManager spawner;
   public BoardManager board;

   private ShapeManager aktifSekil;

   [Header("Sayaclar")]
   [Range(0.02f,1f)]
   [SerializeField] private float asagiInmeSuresi = .1f;
   private float asagiInmeSayac;
   [Range(0.02f, 1f)] 
   [SerializeField] private float sagSolTusaBasmaSuresi = 0.25f;
   private float SagSolTusaBasmaSayac;
   [Range(0.02f, 1f)] 
   [SerializeField] private float sagSolTusaDonmeSuresi = 0.25f;
   private float SagSolTusaDonmeSayac;
   [Range(0.02f, 1f)] 
   [SerializeField] private float asagiTusaBasmaSuresi = 0.25f;
   private float asagiTusaBasmaSayac;

   public bool gameOver = false;

   public bool saatYonumu = true;
   public IconAckapaManager rotateIcon;
   
      
   private void Start()
   {
      if (spawner)
      {
         if (aktifSekil==null)
         {
            aktifSekil = spawner.SekilOlusturFNC();
            aktifSekil.transform.position = vectoruIntYapFNC(aktifSekil.transform.position);
         }
      }
   }

   private void Update()
   {
      if (!board || !spawner || !aktifSekil || gameOver)
      {
         return;
      }
       
      GirisKontrolFNC();
      
      
   }

   void GirisKontrolFNC()
   {
      if ((Input.GetKey("right") && Time.time>SagSolTusaBasmaSayac) || Input.GetKeyDown("right"))
      {
         aktifSekil.SagaHareketFNC();
         SagSolTusaBasmaSayac = Time.time + sagSolTusaBasmaSuresi;
         
         if (!board.GecerliPozisyondami(aktifSekil))
         {
            SoundManager.instance.SesEfektiCikar(1);
            aktifSekil.SolaHareketFNC();
         }
         else
         {
            SoundManager.instance.SesEfektiCikar(2);
         }
       
      }
      else if((Input.GetKey("left") && Time.time>SagSolTusaBasmaSayac) || Input.GetKeyDown("left"))
      {
            aktifSekil.SolaHareketFNC();
            SagSolTusaBasmaSayac = Time.time + sagSolTusaBasmaSuresi;
            
            if (!board.GecerliPozisyondami(aktifSekil))
            {
               SoundManager.instance.SesEfektiCikar(1);
               aktifSekil.SagaHareketFNC();
            }
            else
            {
               SoundManager.instance.SesEfektiCikar(2);
            }
      }
      else if ((Input.GetKey("up") && Time.time>SagSolTusaDonmeSayac))
      {
         aktifSekil.SagaDonFNC();
         SagSolTusaBasmaSayac = Time.time + sagSolTusaDonmeSuresi;
         
         if (!board.GecerliPozisyondami(aktifSekil))
         {
            SoundManager.instance.SesEfektiCikar(1);
            aktifSekil.SolaHareketFNC();
         }
         else
         {
            saatYonumu = !saatYonumu;

            if (rotateIcon)
            {
               rotateIcon.IconAcKapatFNc(saatYonumu);
            }
            SoundManager.instance.SesEfektiCikar(2);
         }
      }
      else if((Input.GetKey("down") && Time.time>asagiTusaBasmaSayac) || Time.time>asagiInmeSayac)
      {
         asagiInmeSayac = Time.time + asagiInmeSuresi;
         asagiTusaBasmaSayac = Time.time + asagiTusaBasmaSuresi;
         
         if (aktifSekil)
         {
            aktifSekil.AsagiHareketFNC();
               
            if (!board.GecerliPozisyondami(aktifSekil))
            {
               if (board.DisariTastimiFNC(aktifSekil))
               {
                  aktifSekil.YukariHareketFNC();
                  gameOver = true;
                  SoundManager.instance.SesEfektiCikar(5);
               }
               else
               {
                  YerlestiFNC();
               }
               
            }
         }
      }
      
   }

   private void YerlestiFNC()
   {

      SagSolTusaBasmaSayac = Time.time;
      asagiTusaBasmaSayac = Time.time;
      sagSolTusaDonmeSuresi = Time.time;
      
      aktifSekil.YukariHareketFNC();

      board.SekliIzgaraIcineAlFNC(aktifSekil);
      SoundManager.instance.SesEfektiCikar(4);

      if (spawner)
      {
         aktifSekil = spawner.SekilOlusturFNC();
      }
      
      board.TumSatirlariTemizleFNC();

      if (board.tamamlananSatir>0)
      {
         if (board.tamamlananSatir>1)
         {
            SoundManager.instance.VocalSesiCikar();
            
         }
         else
         {
            SoundManager.instance.SesEfektiCikar(4);
         }
      }
      
   }

   Vector2 vectoruIntYapFNC(Vector2 vector)
   {
      return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
      
   }

   public void RotationIconYonuFNC()
   {
      saatYonumu = !saatYonumu;
      
      aktifSekil.SaatYonundeDonsunmu(saatYonumu);

      if (!board.GecerliPozisyondami(aktifSekil))
      {
         aktifSekil.SaatYonundeDonsunmu(!saatYonumu);
         SoundManager.instance.SesEfektiCikar(2);
      }
      else
      {
         if (rotateIcon)
         {
            rotateIcon.IconAcKapatFNc(saatYonumu);
            SoundManager.instance.SesEfektiCikar(1);
         }
      }
   }
}



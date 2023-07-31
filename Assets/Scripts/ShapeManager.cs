using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeManager : MonoBehaviour
{
   [SerializeField] private bool donebilirmi=true;
   

   public void SolaHareketFNC()
   {
      transform.Translate(Vector3.left,Space.World);
   }
   public void SagaHareketFNC()
   {
      transform.Translate(Vector3.right,Space.World);
   }
   public void AsagiHareketFNC()
   {
      transform.Translate(Vector3.down,Space.World);
   }
   public void YukariHareketFNC()
   {
      transform.Translate(Vector3.up,Space.World);
   }

   public void SagaDonFNC()
   {
      if (donebilirmi)
      {
         transform.Rotate(0,0,-90);
      }
   }
   public void SolaDonFNC()
   {
      if (donebilirmi)
      {
         transform.Rotate(0,0,+90);
      }
   }

   IEnumerator HareketRoutine()
   {
      while (true)
      {
         AsagiHareketFNC();
         yield return new WaitForSeconds(.25f);
      }
   }

   public void SaatYonundeDonsunmu(bool saatYonumu)
   {
      if (saatYonumu)
      {
         SagaDonFNC();
      }
      else
      {
         SolaDonFNC();
      }
   }

}

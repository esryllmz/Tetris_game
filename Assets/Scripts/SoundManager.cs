
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
   public static SoundManager instance;

   [SerializeField] private AudioClip[] musicClips;
   [SerializeField] private AudioSource musicSource;
   
   [SerializeField] private AudioSource[] sesEfektleri;
   [SerializeField] private AudioSource[] vocalsClips;
   
   

   private AudioClip rastgeleMusicClip;

   public bool musicCalsinmi = true;
   public bool efektCalsinmi = true;

   public IconAckapaManager musicIcon;
   public IconAckapaManager FxIcon;
   
   private void Awake()
   {
      instance = this;
         
   }

   private void Start()
   {
      rastgeleMusicClip = RastgeleClipSec(musicClips);
      BackgroundMusicCal(rastgeleMusicClip);
   }


   public void VocalSesiCikar()
   {
      if (efektCalsinmi)
      {
         AudioSource source = vocalsClips[Random.Range(0, vocalsClips.Length)];
         source.Stop();
         source.Play();
      }
      
   }
   

   public void SesEfektiCikar(int hangiSes)
   {
      if (efektCalsinmi && hangiSes<sesEfektleri.Length)
      {
         sesEfektleri[hangiSes].Stop();
         sesEfektleri[hangiSes].Play();
      }
   }

   AudioClip RastgeleClipSec(AudioClip[] clips)
   {
      AudioClip rastgeleClip = clips[Random.Range(0, clips.Length)];
      return rastgeleClip;
   }

   public void BackgroundMusicCal(AudioClip musicClip)
   {
      if (!musicClip || !musicSource || !musicCalsinmi)
      {
         return;
      }

      musicSource.clip = musicClip;
      musicSource.Play();
   }

   void MusicGuncelleFNC()
   {
      if (musicSource.isPlaying != musicCalsinmi)
      {
         if (musicCalsinmi)
         {
            rastgeleMusicClip = RastgeleClipSec(musicClips);
            BackgroundMusicCal(rastgeleMusicClip);
         }
         else
         {
            musicSource.Stop();
         }
      }
   }

   public void MusicAcKapa()
   {
      musicCalsinmi = !musicCalsinmi;
      MusicGuncelleFNC();
      musicIcon.IconAcKapatFNc(musicCalsinmi);
   }

   public void FxAcKapatFNC()
   {
      efektCalsinmi = !efektCalsinmi;
      FxIcon.IconAcKapatFNc(efektCalsinmi);
   }
}

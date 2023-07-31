using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]  private Transform TilePrefab;

    public int yukseklik = 22;
    public int genislik = 10;
    
    private Transform[,] izgara;
    
    public int tamamlananSatir=0;

    private void Awake()
    {
        izgara = new Transform[genislik, yukseklik];
    }

    private void Start()
    {
        BosKareleriOlusturFNC();
    }

    bool BoardIcindemi(int x, int y)
    {
        return (x >= 0 && x < genislik && y >= 0);
    }

    bool KareDolumu(int x, int y, ShapeManager shape)
    {
        return (izgara[x, y] != null && izgara[x, y].parent != shape.transform);
    }
 
    public bool GecerliPozisyondami(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = vectoruIntYapFNC(child.position);

            if (!BoardIcindemi((int)pos.x,(int)pos.y))
            {
                return false;
            }

            if (pos.y<yukseklik)
            {
                if (KareDolumu((int)pos.x,(int)pos.y,shape))
                {
                    return false;
                }
            }

            
        }

        return true;
    }
    void BosKareleriOlusturFNC()
    {
        for (int y = 0; y < yukseklik; y++)
        {
            for (int x = 0; x < genislik; x++)
            {
                Transform tile = Instantiate(TilePrefab, new Vector3(x, y, 0), quaternion.identity);
                tile.name = "x" + x.ToString() + " ," + "y" + y.ToString();
                tile.parent = this.transform;

            }
        }
    }

    public void SekliIzgaraIcineAlFNC(ShapeManager shape)
    {
        if (shape==null)
        
            return;
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = vectoruIntYapFNC(child.position);
            izgara[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool SatirTamamlandimiFNC(int y)
    {
        for (int x = 0; x<genislik ; ++x)
        {
            if (izgara[x,y]==null)
            {
                return false;
            }
        }

        return true;
    }

    void SatiriTemizleFNC(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x,y] !=null)
            {
                Destroy(izgara[x,y].gameObject);
            }

            izgara[x,y] = null;
            
        }
    }

    void BirSatirAsagiİndirFNC(int y)
    {
        for (int x = 0; x < genislik; ++x)
        {
            if (izgara[x,y]!=null)
            {
                izgara[x, y - 1] = izgara[x, y];
                izgara[x, y] = null;
                izgara[x,y-1].position+=Vector3.down;
                
            }
            
        }
    }

    void TumSatirlariAsagiİndirFNC(int baslangicY)
    {
        for (int i = baslangicY; i < yukseklik; ++i)
        {
            BirSatirAsagiİndirFNC(i);
        }
    }

    public void TumSatirlariTemizleFNC()
    {
        tamamlananSatir = 0;
        for (int y = 0; y < yukseklik; y++)
        {
            if (SatirTamamlandimiFNC(y))
            {
                tamamlananSatir++;
                SatiriTemizleFNC(y);
                TumSatirlariAsagiİndirFNC(y+1);
                y--;
            }
        }
    }

    public bool DisariTastimiFNC(ShapeManager shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y>=yukseklik-1)
            {
                return true;
                
            }
        }

        return false;
    }
    Vector2 vectoruIntYapFNC(Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
      
    }  
    
    
}

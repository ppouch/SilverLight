﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Generator : MonoBehaviour
{
   
    int MapWidth;
    int MapHeight;
    public Texture2D Map2D1;
    public GameObject TilePrefab;//Le prefab des thuiles generes
    public Transform MapHolder;
    public Dictionary<TileInfo, GameObject> mapTItoGO = new Dictionary<TileInfo, GameObject>();//map les info vers le visuel de la thuil
    public Dictionary<GameObject, TileInfo> mapGOtoTI = new Dictionary<GameObject, TileInfo>();//map le visuel vers les info
    public Dictionary<string, TileInfo> mapCootoTI = new Dictionary<string, TileInfo>();//map les coo vers les tileinfo
    public List<TileInfo> mapinfo = new List<TileInfo>();//Contiens toutes les info de la map
    //TurnManagingThings
    public bool IsPlayerTurn = true;
    public int TurnNumber=0;
    void GenerateMap1()
    {
        MapWidth = Map2D1.width;
        MapHeight = Map2D1.height;
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                Color P = Map2D1.GetPixel(x, y);
                TileInfo TI = new TileInfo(x,y,P,0);//Tile info
                GameObject TIGO = Instantiate(TilePrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder);//Tile PRefab
                Mapping(TIGO, TI,x,y);//Mapping des deux dans mes dictionaires
                
                UpdateGo(TI);//Update le tile prefab attribue le ti au go et le go au ti
                StartLocation(x, y, TI);
                UpdateGo(TI);
            }
        }
        //killallChildren();
    }
    void killallChildren()
    {
       TileUpdater[] Children=FindObjectsOfType<TileUpdater>();
        foreach (TileUpdater child in Children)
        {
            Destroy(child);
        }
    }
    void StartLocation(int x,int y,TileInfo ti)
    {
        if(x==18&&y==37)
        {
            mapTItoGO[ti].GetComponent<TileUpdater>().StartLocation = true; 
        }
    }
    public string CootoString(int x,int y)
    {
        return x.ToString() +","+ y.ToString();
    }
    void Mapping(GameObject go, TileInfo ti,int x,int y)
    {
        mapGOtoTI.Add(go, ti);
        mapTItoGO.Add(ti, go);
        mapinfo.Add(ti);
        string coo = CootoString(x, y);

        mapCootoTI.Add(coo, ti);

    }
    void UpdateGo(TileInfo ti)
    {
        mapTItoGO[ti].GetComponent<TileUpdater>().UpdateMe(ti);
    }
    private void Start()
    {
        GenerateMap1();
    }

    public void ResetEnemyTurn()//TurnManagingThing
    {
        DetectionEnemy[] Enemies= FindObjectsOfType<DetectionEnemy>();
        foreach (DetectionEnemy  E in Enemies)
        {
            E.T = 0;
        }
    }

}

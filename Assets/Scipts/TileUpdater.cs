﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TileUpdater : MonoBehaviour
{
    TileInfo myInfo;
    public Material[] mats =new Material[7];
    PlayerController player;
    public bool StartLocation = false;
    Generator GM;
    public Material PasDansLaNeigeMat;
    public bool HasPasDansLaNeige;
    public int PasDansLaNeigeTimer=5;
    GameObject PasDansLaNeige;
    public int DirectionDePasDansLaNeige = 0;
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GM = FindObjectOfType<Generator>();
    }


    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        if(GM.IsPlayerTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                print("TileClickedis" + myInfo.PositionX + "," + myInfo.PositionY);
                player.TileClicked = this.gameObject;
                player.tiClicked = myInfo;
                //player.MoveMan();
                //player.PathFinding();
            }
        }
        
    }
    
    public void UpdateMeCurrent()
    {
        if (HasPasDansLaNeige) 
        {

            Instantiate(PasDansLaNeige, new Vector3(myInfo.PositionX, 0.2f, myInfo.PositionY), Quaternion.identity);


        }
    }
    public void UpdateMe(TileInfo ti)
    {
        this.transform.position = new Vector3(ti.PositionX, 0, ti.PositionY);
        //print(ti.type);
        myInfo = ti;
        ti.myGo = this.gameObject;

        if(ti.type==20)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[0];
        }
        if (ti.type == 60)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[1];
        }
        if (ti.type == 80)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[2];
        }
        if (ti.type == 100)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[3];
        }
        if (ti.type == 120)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[4];
        }
        if (ti.type == 200)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[5];
        }
        if (ti.type == 220)
        {
            GetComponentInChildren<MeshRenderer>().material = mats[6];
        }
        if (StartLocation)
        {
            player = FindObjectOfType<PlayerController>();
           
            player.myPresentTileInfo = ti;
           
        }
    }


}

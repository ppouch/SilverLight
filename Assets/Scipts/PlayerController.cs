﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IA.PathFinding.mapCarre;
using Procedural.Carre;
public class PlayerController : MonoBehaviour
{
    /* clic gauche pour choisir notre destination, ca nous indique le chemin
     * clic droit pour se deplacer,
     * quand on se deplace, la camera change pour nous montrer l<animation de deplacement
     * quand on a fini de se deplacer, la camera zoom out
     * on peut appuyer sur espace pour passer un tour sur place*/
    // Use this for initialization

    public GameObject TileClicked;
    public GameObject LastTileClicked;
    public GameObject MyTile;
    public GameObject OverlayTiles;
    public Camera[] Cams;
    public GameObject[] Terrains;
    public GameObject[] Overlays;
    int HeightView = 0;
    public delegate void Onclick();
    public Onclick Clicked;
    Pathfinding PF;
    int coox=23;
    int cooy=2;
    public bool DoingTurn = false;
    int clicCount=0;
    public Material[] DistanceColor = new Material[3];
    void Start ()
    {
        

    }
   
	// Update is called once per frame
	void Update ()
    {

        if(MyTile==null)
        {
            
            if(GameController.GM!=null)
            {
                MyTile = GameController.GM.mapTItoGO[GameController.GM.FindTile(coox, cooy,0)];
                PF = new Pathfinding(GameController.GM);
                PF.Legal = IllegalesMoves;
                Clicked = PathFinder;
                Clicked += ClickCounter;
                PF.AllowUpAndDown = true;
                PF.TimeOut = 10000;
            }
            
        }
        
        if(Input.GetKeyDown(KeyCode.F1))
        {
            ShowHideOverLayTiles();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            UnitViewMode();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            foreach (TileInfo ti in GameController.GM.mapinfo)
            {
                ti.MyVisual.GetComponentInChildren<TextMesh>().text = ti.R256.ToString()+" , "+ti.G256.ToString() + " , " + ti.G256.ToString();
            }
        }
        if(Input.GetKeyDown(KeyCode.PageUp))
        {
            
            HeightView++;
            if(HeightView>2)
            {
                HeightView = 2;
            }
            
            
            
            Terrains[HeightView].SetActive(true);
           
        }
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            if(HeightView!=0)
            {
                Terrains[HeightView].SetActive(false);
                
            }
            
            HeightView--;
            if (HeightView < 0)
            {
                HeightView = 0;
            }
            
        }
        if (DoingTurn)
        {
            DoMoves();
        }
	}
    
    void UnitViewMode()
    {
        
        foreach (Camera c in Cams)
        {
            c.gameObject.SetActive(false);
            
        }
        Cams[1].gameObject.SetActive(true);
    }
    void MainViewMode()
    {
        foreach (Camera c in Cams)
        {
            c.gameObject.SetActive(false);

        }
        Cams[0].gameObject.SetActive(true);
    }
    bool isActive = true;
    bool OverlayClick = false;
    public void ShowHideOverLayTiles()
    {
        isActive = !isActive;
        OverlayTiles.SetActive(isActive);
        clicCount = 0;
        OverlayClick = true;
    }
    void ClickCounter()
    {
        if(OverlayClick)
        {
            OverlayClick = false;
            return;
        }
        if(LastTileClicked==null)
        {
            clicCount++;
            LastTileClicked = TileClicked;
            return;
        }
        if(TileClicked!=LastTileClicked)
        {
            clicCount = 0;
            LastTileClicked = TileClicked;
            return;
        }
        else
        {
            clicCount++;
            LastTileClicked = TileClicked;
            return;
        }
    }
    public void ClickControl()
    {
        if(clicCount<2)
        {
            Clicked = PathFinder;
            Clicked += ClickCounter;
        }
        else
        {
            Clicked = OnClickTwice;
            clicCount = 0;
        }
        Clicked();
    }
    
    bool IllegalesMoves(Action a)
    {
        //return true si l<action est legal
        if (a != null && a.To != null)
        {
            if(a.To.R256==255&&a.To.G256==0&&a.To.B256==0)
            {
                
                return false;
                
            }
            if(a.To.CooH!=a.From.CooH)
            {
                if (a.To.CooH == a.From.CooH + 1)//Monte de 1 etage
                {
                    if (a.From.G256 == 60 && a.From.R256 == 0 && a.From.B256 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (a.To.CooH == a.From.CooH + 2)//Monte de 2 etage
                {
                    if (a.From.G256 == 70 && a.From.R256 == 0 && a.From.B256 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (a.To.CooH == a.From.CooH -1)//Descend de 1 etage
                {
                    if (a.From.G256 == 10 && a.From.R256 == 0 && a.From.B256 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (a.To.CooH == a.From.CooH - 2)//Descend de 2 etage
                {
                    if (a.From.G256 == 20 && a.From.R256 == 0 && a.From.B256 == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }
        else
        {
            return false;
        }
    }
    void PathFinder()
    {
        //Quand on clic sur une thuile, on appel la fonction Astar et on allume les thuile
        //On indique le nombre de tours pour s<y rendre
        //seulement si cest le tour du joueur
        if(GameController.TM.IsPlayerTurn)
        {
            //Allume toutes les tiles sur le chemin
            Path.Clear();
            //List des Actions (from,to)
            foreach (TileInfo ti in GameController.GM.mapinfo)
            {
                GameController.GM.VisualUpdate(ti);
                ti.MyVisual.GetComponentInChildren<TextMesh>().text = "";
                
            }
            int Distance = 0;
            foreach (Action a in PF.AStar(GameController.GM.mapGOtoTI[MyTile], GameController.GM.mapGOtoTI[TileClicked]))
            {
                Distance++;
                if(Distance<=5)
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = "1";
                    GameController.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[0];
                    Path.Add(a);
                }
                else if(Distance <= 10)
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = "2";
                    GameController.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[1];
                }
                else
                {
                    a.To.MyVisual.GetComponentInChildren<TextMesh>().text = ((int)((Distance+0.5f)/5)).ToString();
                    GameController.GM.mapTItoGO[a.To].GetComponentInChildren<MeshRenderer>().material = DistanceColor[2];
                }
                
            }
            if(Path.Count==1)
            {
                if(Path[0].To==Path[0].From)
                {
                    clicCount = 0;
                }
            }
            
        }

    }
    List<Action> Path = new List<Action>();
    bool StartMoves = false;
    public void OnClickTwice()//Appeler quand on clic deux fois
    {
        if(GameController.TM.IsPlayerTurn)
        {
 
            DoingTurn = true;//active le update
            GameController.TM.IsPlayerTurn = false;
            StartMoves = true;//s<execute une fois au debut de la fonction DoMoves
        }
        
    }
    void DoMoves()
    {
        //J<ai une liste d<action 
        //Chaque Action contien un from et un to
        //J<executes seulements les actions "Vertes", les actions que je peux faire ce tour ci bref
        //Je me deplace de une thuile a la fois et lorsque ma liste est fini, je end mon tour
        //Actions remaning?
        if(StartMoves)
        {
            foreach (GameObject overlay in Overlays)
            {
                overlay.SetActive(false);
            }
            
            UnitViewMode();
            Frac = 0;//timer du lerp
            foreach (TileInfo ti in GameController.GM.mapinfo)
            {
                GameController.GM.VisualUpdate(ti);
                ti.MyVisual.GetComponentInChildren<TextMesh>().text = "";
            }
            StartMoves = false;
        }
        if(Path.Count>0)
        {
            DoAction(Path[0]);
            if (Frac >= 1)
            {
                Path.Remove(Path[0]);
                Frac = 0;
            }
        }
        else
        {
            foreach (GameObject overlay in Overlays)
            {
                overlay.SetActive(true);
            }
            MainViewMode();
            DoingTurn = false;
            GameController.TM.IsPlayerTurn = true;
        }
    }
    public float Frac = 0;

   
    void DoAction(Action a)
    {
        
        if(a.isAttack==false)
        {
            Frac += Time.deltaTime / 2;
            Vector3 Start = new Vector3(a.From.Coox, a.From.Height, a.From.Cooy);
            Vector3 end = new Vector3(a.To.Coox, a.To.Height, a.To.Cooy);
            this.transform.position = Vector3.Lerp(Start, end, Frac);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation,Quaternion.Euler(new Vector3(0, 90 * a.Direction, 0)), Frac);
            this.MyTile = GameController.GM.mapTItoGO[a.To];
            this.coox = a.To.Coox;
            this.cooy = a.To.Cooy;
        }
        
    }
    
}



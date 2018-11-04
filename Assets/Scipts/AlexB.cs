using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexB : MonoBehaviour {

	public GameObject BLCornerPrefab;
	public GameObject BRCornerPrefab;
	public GameObject FLCornerPrefab;
	public GameObject FRCornerPrefab;
	public GameObject FWallPrefab;
	public GameObject LWallPrefab;
	public GameObject BWallPrefab;
	public GameObject RWallPrefab;
	public GameObject TreePrefab;
	public GameObject PillarPrefab;
	public GameObject BWindowPrefab;
	public GameObject FWindowPrefab;
	public GameObject LWindowPrefab;
	public GameObject RWindowPrefab;

	public GameObject BDoorPrefab;
	public GameObject FDoorPrefab;
	public GameObject LDoorPrefab;
	public GameObject RDoorPrefab;

	public Transform MapHolder;
	public Generator GM;
	void Start () {
		GM = FindObjectOfType<Generator> ();
	}


	public void AjouterPrefabs(int MapWidth, int MapHeight) {
		for (int x = 1; x < MapWidth - 1; x++)
		{
			for (int y = 1; y < MapHeight - 1; y++)
			{
				if (GM.mapCootoTI[GM.CootoString(x,y)].type == 100)
				{

					if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x,y+1)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x+1,y+1)].type == 80) 
					{Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x,y-1)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x+1,y-1)].type == 80) 
					{Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x,y+1)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x-1,y+1)].type == 80) 
					{Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 80
						&& GM.mapCootoTI[GM.CootoString(x,y-1)].type == 80
						&&  GM.mapCootoTI[GM.CootoString(x-1,y-1)].type == 80)
					{Instantiate(PillarPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }





					else if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x,y+1)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x+1,y+1)].type == 80) 
					{Instantiate(BLCornerPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x,y-1)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x+1,y-1)].type == 80) 
					{Instantiate(BRCornerPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x,y+1)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x-1,y+1)].type == 80) 
					{Instantiate(FLCornerPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 100
						&& GM.mapCootoTI[GM.CootoString(x,y-1)].type == 100
						&&  GM.mapCootoTI[GM.CootoString(x-1,y-1)].type == 80)
					{Instantiate(FRCornerPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }



					else if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 80) 
					{Instantiate(BWallPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x,y+1)].type == 80) 
					{Instantiate(LWallPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 80) 
					{Instantiate(FWallPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }

					else if (GM.mapCootoTI[GM.CootoString(x,y-1)].type == 80) 
					{Instantiate(RWallPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }





				}


				if (GM.mapCootoTI[GM.CootoString(x,y)].type == 120)
				{
					if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 80)
					{Instantiate(BDoorPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 80)
					{Instantiate(FDoorPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x,y+1)].type == 80)
					{Instantiate(LDoorPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x,y-1)].type == 80)
					{Instantiate(RDoorPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
				}


				if (GM.mapCootoTI[GM.CootoString(x,y)].type == 200)
				{
					if (GM.mapCootoTI[GM.CootoString(x+1,y)].type == 80)
					{Instantiate(BWindowPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 80)
					{Instantiate(FWindowPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x,y+1)].type == 80)
					{Instantiate(LWindowPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
					if (GM.mapCootoTI[GM.CootoString(x,y-1)].type == 80)
					{Instantiate(RWindowPrefab, new Vector3(x, 0, y), Quaternion.identity,MapHolder); }
				}




				if (GM.mapCootoTI[GM.CootoString(x,y)].type == 220)
				{
					if (GM.mapCootoTI[GM.CootoString(x-1,y)].type == 220
						&& GM.mapCootoTI[GM.CootoString(x,y-1)].type == 220
						&&  GM.mapCootoTI[GM.CootoString(x-1,y-1)].type == 220)
					{Instantiate(TreePrefab, new Vector3(x - 0.5f, 0, y - 0.5f), Quaternion.identity,MapHolder); }
				}


			}
		}
	}




}
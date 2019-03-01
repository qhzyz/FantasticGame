using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMap : MonoBehaviour {
    //默认世界坐标原点为首块位置
    public string fileName;
    GameObject M_normal, M_water, M_wood, M_wind, M_fire;
    MapData mapData;
    public GameObject[] mapSurface;
    // Use this for initialization
    void Start() {
        LoadPlace();
        mapData = transform.GetComponent<MapData>();
        mapData.LoadFromFile(fileName);
        mapSurface = new GameObject[MapData.MAX_NUM];
        CreateMap();
    }

    void GotoPosition(MapUnit ld,int lheight)
    {
        switch (ld.kind)
        {
            case Place_Kind.PLACE_NORMAL:
                M_normal.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE*MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WATER:
                M_water.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WOOD:
                M_wood.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_WIND:
                M_wind.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
            case Place_Kind.PLACE_FIRE:
                M_fire.transform.position = new Vector3(ld.x * MapData.UNIT_SIZE,
                                                          lheight * MapData.UNIT_SIZE * MapData.Y_SCALE,
                                                          ld.y * MapData.UNIT_SIZE);
                break;
        }
    }
    GameObject InstantiateMap(Place_Kind lkind) {
        switch (lkind) {
            case Place_Kind.PLACE_NORMAL:
                return Instantiate(M_normal, transform);
            case Place_Kind.PLACE_WATER:
                return Instantiate(M_water, transform);
            case Place_Kind.PLACE_WOOD:
                return Instantiate(M_wood, transform);
            case Place_Kind.PLACE_WIND:
                return Instantiate(M_wind, transform);
            case Place_Kind.PLACE_FIRE:
                return Instantiate(M_fire, transform);
        }
        return null;
    }
    void LoadPlace() {
        M_normal = (GameObject)Resources.Load("normalPlace");
        M_water = (GameObject)Resources.Load("waterPlace");
        M_wood = (GameObject)Resources.Load("woodPlace");
        M_wind = (GameObject)Resources.Load("windPlace");
        M_fire = (GameObject)Resources.Load("firePlace");
    }
    void CreateMap()
    {
        for (int i = 0; i < mapData.length * mapData.width; i++)
        {
            for (int j = 0; j < mapData.DataArr[i].height; j++)
            {
                GotoPosition(mapData.DataArr[i],j);
                mapSurface[i]=InstantiateMap(mapData.DataArr[i].kind);
            }
            if (mapSurface[i]) { mapSurface[i].AddComponent<BoxCollider>(); }
        }
    }
}

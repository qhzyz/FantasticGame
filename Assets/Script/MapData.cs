using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public enum Place_Kind { PLACE_NORMAL, PLACE_WATER, PLACE_WOOD, PLACE_WIND, PLACE_FIRE ,PLACE_NONE };
public struct MapUnit
{
    public int x, y;
    public int height;
    public Place_Kind kind;//
    public bool isStart;
};

public class MapData : MonoBehaviour
{

    public const int KIND_NUM = 5;
    public const int MAX_HEIGHT = 4;
    public const int MAX_NUM = 400;//L*W
    public const int UNIT_SIZE = 1;
    public const float Y_SCALE = 0.5f;//扁扁地图
    
    public MapUnit[] DataArr =new MapUnit[MAX_NUM];
    public int length;
    public int width;

    void Start () {
        Zero();
	}
    public void LoadFromFile(string fileName) {
        StreamReader sr = new StreamReader(fileName + ".map", Encoding.Default);
        string content;
        content = sr.ReadToEnd();
        length = content[0];
        width = content[1];
        for(int i = 2,j = 0; j < length * width; j++)  {
            DataArr[j].x = content[i++];
            DataArr[j].y = content[i++];
            DataArr[j].height = content[i++];

            DataArr[j].kind = (Place_Kind)content[i++];
            if (content[i++] == 'F')
                DataArr[j].isStart = false;
            else
                DataArr[j].isStart = true;

        }
    }
    public void RandomCreate() {
        bool[,] exist = new bool[width, length];
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < length; j++) {
                exist[i,j] = false;
            }
        }

        for (int i = 0; i < MAX_NUM; i++) {
            do {
                DataArr[i].x = UnityEngine.Random.Range(0, width);
                DataArr[i].y = UnityEngine.Random.Range(0, length);
            } while (exist[DataArr[i].x, DataArr[i].y]);
            exist[DataArr[i].x, DataArr[i].y] = true;

            DataArr[i].height = UnityEngine.Random.Range(1, MAX_HEIGHT);
            DataArr[i].kind   = (Place_Kind)UnityEngine.Random.Range(0, KIND_NUM);
        }
    }
    public void SaveToFile(string fileName) {

        StreamWriter sw = new StreamWriter(fileName + ".map", false);
        sw.Write("{0}{1}", (char)length, (char)width);
        for (int i = 0; i < length * width; i++) {
            sw.Write("{0}{1}{2}{3}", (char)DataArr[i].x, (char)DataArr[i].y, (char)DataArr[i].height, (char)DataArr[i].kind);
            if (DataArr[i].isStart == false) {
                sw.Write("F");
            }
            else
                sw.Write("T");
        }
        sw.Flush();
        sw.Close();
    }
    public void Zero() {//置空
        for (int i = 0; i < MAX_NUM; i++) {
            DataArr[i].x = 0;
            DataArr[i].y = 0;
            DataArr[i].height = 0;
            DataArr[i].kind = Place_Kind.PLACE_NONE;
            DataArr[i].isStart = false;
        }
    }
}

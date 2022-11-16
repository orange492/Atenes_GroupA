using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    GameObject[,] tile;
    [SerializeField]
    GameObject tilePref;
    [SerializeField]
    GameObject unitPref;
    int[] size = new int[2];
    public int unitCnt { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitMap()
    {
        size[0] = DataManager.Instance.mapSize[0];
        size[1] = DataManager.Instance.mapSize[1];
        this.transform.Translate(new Vector3(-1, -1, 0));
        tile = new GameObject[size[0], size[1]];
        for (int i = 0; i < size[0]; i++)
        {
            for (int j = 0; j < size[1]; j++)
            {
                tile[i, j] = Instantiate(tilePref, this.transform);
                tile[i, j].transform.Translate(j, i, 0);
                tile[i, j].name = $"tile({i},{j})";
                Unit unit = Instantiate(unitPref, tile[i, j].transform).GetComponent<Unit>();
                unit.SetIndex(i,j);
            }
        }
    }

    public void CrtUnit()
    {
        if (unitCnt >= tile.Length)
        {
            return;
        }

        int index;
        while (true)
        {
            index = Random.Range(0, tile.Length);
            if (!this.transform.GetChild(index).GetChild(0).gameObject.activeSelf)
                break;
        }
        Unit unit = this.transform.GetChild(index).GetChild(0).gameObject.GetComponent<Unit>();
        int temp = Random.Range(0, 5);
        unit.Init(temp, 1);
        unitCnt++;
    }

    public int CheckGir(int[] index)
    {
        GameObject obj;
        int count = 0;
        if(index[1]-1 >=0)
        {
            obj = tile[index[0], index[1] - 1].transform.GetChild(0).gameObject;
            if (obj.GetComponent<Unit>().type == 4 && obj.activeSelf)
            {
                count++;
            }
        }
        if (index[1] + 1 < 3)
        {
            obj = tile[index[0], index[1] + 1].transform.GetChild(0).gameObject;
            if (obj.GetComponent<Unit>().type == 4 && obj.activeSelf)
            {
                count++;
            }
        }
        if (index[0] - 1 >= 0)
        {
            obj = tile[index[0] - 1, index[1]].transform.GetChild(0).gameObject;
            if (obj.GetComponent<Unit>().type == 4 && obj.activeSelf)
            {
                count++;
            }
        }
        if (index[0] + 1 < 5)
        {
            obj = tile[index[0] + 1, index[1]].transform.GetChild(0).gameObject;
            if (obj.GetComponent<Unit>().type == 4 && obj.activeSelf)
            {
                count++;
            }
        }
        return count;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    GameObject[,] tile;
    [SerializeField]
    GameObject tilePref;
    [SerializeField]
    GameObject unitPref;
    [SerializeField]
    Sprite[] spr;
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
                Instantiate(unitPref, tile[i, j].transform);
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
        unit.Init(temp,spr[temp], 1);
        unitCnt++;
    }
}

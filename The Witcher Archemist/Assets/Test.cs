using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> tiles = new List<GameObject>();
    public int width = 4;
    public int height = 4;

    public List<GameObject> matchTiles = new List<GameObject>();


    private void Start()
    {
        for (int i = 0; i < width * height; i++)
        {
            var x = i % width;
            var y = i / width;
            GameObject obj = Instantiate(prefab, new Vector2(x, -y), Quaternion.identity);//new GameObject("tile");
            obj.transform.parent = transform;

            int randomTile = Random.Range(0, 10);
            if(randomTile == 0)
            {
                TestTile block = obj.AddComponent<TestTile>();
                obj.name = "booster";
            }
            else
            {
                TestBlock block = obj.AddComponent<TestBlock>();
                block.count = Random.Range(1, 7);
            }

            
            tiles.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            clear();
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                int count = hit.collider.GetComponent<TestTile>().count;
                search(hit.collider.gameObject);
            }
        }

        
    }

    void search(GameObject obj)
    {
        var idx = tiles.FindIndex(x => x == obj);

        int count = tiles[idx].GetComponent<TestTile>().count;

        int i = idx % width;
        int j = idx / width;

        TestTile leftUp = new TestTile { pos = new Pos(i - 1, j - 1) };
        TestTile left = new TestTile { pos = new Pos(i - 1, j)};
        TestTile leftDown = new TestTile { pos = new Pos(i - 1, j + 1)};

        TestTile rightUp = new TestTile { pos = new Pos(i + 1, j - 1) };
        TestTile right = new TestTile { pos = new Pos(i + 1, j)};
        TestTile rightDown = new TestTile { pos = new Pos(i + 1, j + 1)};

        TestTile top = new TestTile { pos = new Pos(i, j - 1)};

        TestTile down = new TestTile { pos = new Pos(i, j + 1)};


        List<TestTile> tilesPos = new List<TestTile>();

        tilesPos.Add(leftUp);
        tilesPos.Add(left);
        tilesPos.Add(leftDown);
        tilesPos.Add(rightUp);
        tilesPos.Add(right);
        tilesPos.Add(rightDown);
        tilesPos.Add(top);
        tilesPos.Add(down);

        //중복 방지를 위해 오버플로 방지
        if(!matchTiles.Contains(obj))
            matchTiles.Add(obj);

        for (int k = 0; k < tilesPos.Count; k++)
        {
            if(IsVaildTile(tilesPos, k))
            {
                var index = (width * tilesPos[k].pos.y) + tilesPos[k].pos.x;

                GameObject tile = tiles[index];

                //재귀함수를 벗어날수 있게 해줍니다.
                if (tile.GetComponent<TestBlock>())
                {
                    //상하좌우 검사
                    if (count == tile.GetComponent<TestTile>().count && !matchTiles.Contains(tile))
                    {
                        search(tile);
                    }
                }
            }
        }

        //여기부터 같은 타일들을 보여주는 효과를 줍니다.
        if (matchTiles.Count > 2)
        {
            for (int l = 0; l < matchTiles.Count; l++)
            {
                int a = tiles.FindIndex(x => x == matchTiles[l]);
                matchTiles[l].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

    }

    bool IsVaildTile(List<TestTile> tilesPos, int k)
    {
        return tilesPos[k].pos.x >= 0 && tilesPos[k].pos.x < width &&
                tilesPos[k].pos.y >= 0 && tilesPos[k].pos.y < height;
    }

    void clear()
    {
        for (int i = 0; i < matchTiles.Count; i++)
        {
            matchTiles[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
        matchTiles.Clear();
    }
}

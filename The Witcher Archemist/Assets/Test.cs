using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject prefab;
    public List<GameObject> tiles = new List<GameObject>();
    public int width = 4;
    public int height = 4;


    private void Start()
    {
        for (int i = 0; i < width * height; i++)
        {
            var x = i % width;
            var y = i / width;
            GameObject obj = Instantiate(prefab, new Vector2(x, -y), Quaternion.identity);//new GameObject("tile");
            obj.transform.parent = transform;
            TestTile tile = obj.AddComponent<TestTile>();
            tile.count = Random.Range(1, 7);
            tiles.Add(obj);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
             var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                search(hit.collider.gameObject);
            }
        }
    }

    public struct Pos
    {
        public int x;
        public int y;
    }

    void search(GameObject hit)
    {
        var idx = tiles.FindIndex(x => x == hit);

        int i = idx % width;
        int j = idx / width;

        Pos origin = new Pos { x = i, y = j};
        Pos left = new Pos { x = i - 1, y = j};
        Pos right = new Pos { x = i + 1, y = j};
        Pos top = new Pos { x = i, y = j - 1};
        Pos down = new Pos { x = i, y = j + 1};

        Debug.Log(origin.x + " " + origin.y);
        Debug.Log(left.x + " " + left.y);
        Debug.Log(right.x + " " + right.y);
        Debug.Log(top.x + " " + top.y);
        Debug.Log(down.x + " " + down.y);

        List<Pos> poss = new List<Pos> { left, right, top, down };

        for (int k = 0; k < poss.Count; k++)
        {
            if(poss[k].x >= 0 && poss[k].x < width && poss[k].y >= 0 && poss[k].y < width)
                Debug.Log("반복문 " + poss[k].x + " " + poss[k].y );
        }

    }
}

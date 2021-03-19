using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public Texture2D tex;
    public Texture2D image;
    public List<Texture2D> texlist = new List<Texture2D>();
    public Sprite sp;

    public Image ee;

    public int width;
    public int height;

    public Transform parent;


    private void Start()
    {
        //image = TextureUtil.SliceTexture2(tex, 6, 6);
        //sp = Sprite.Create(image, new Rect(0, 0, image.width, image.height), new Vector2(0.5f, 0.5f));
        texlist = TextureUtil.SliceTexture3(tex, 6, 6);
        

        for (int i = 0; i < texlist.Count; i++)
        {
            GameObject a = new GameObject("tex" + i);
            a.AddComponent<RectTransform>();
            sp = Sprite.Create(texlist[i], new Rect(0, 0, texlist[i].width, texlist[i].height), new Vector2(0.5f, 0.5f));

            a.AddComponent<Image>().sprite = sp;
            a.transform.SetParent(parent);
        }

        width = tex.width;
        height = tex.height;
        ee.sprite = sp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{

    Texture2D mColorSwapTex;
    Color[] mSpriteColors;
    [SerializeField]PlayerController player;
    [SerializeField] Color toBeSet;
    public int id;

    SpriteRenderer mSpriteRenderer;

    float mHitEffectTimer = 0.0f;
    const float cHitEffectTime = 0.1f;

    void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        InitColorSwapTex();
        id = player.id;
    }



    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF;
        int g = (c >> 8) & 0x000000FF;
        int b = c & 0x000000FF;

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha;

        return ret;
    }

    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }

    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point;

        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply();

        mSpriteRenderer.material.SetTexture("_SwapTex", colorSwapTex);

        mSpriteColors = new Color[colorSwapTex.width];
        mColorSwapTex = colorSwapTex;
    }

    public void SwapColor(SwapIndex index, Color color)
    {
        mSpriteColors[(int)index] = color;
        mColorSwapTex.SetPixel((int)index, 0, color);
    }


    public void SwapColors(List<SwapIndex> indexes, List<Color> colors)
    {
        for (int i = 0; i < indexes.Count; ++i)
        {
            mSpriteColors[(int)indexes[i]] = colors[i];
            mColorSwapTex.SetPixel((int)indexes[i], 0, colors[i]);
        }
        mColorSwapTex.Apply();
    }

    void OnSetShirt0Color(Color color)
    {
        SwapColor(SwapIndex.Prim, color);
        mColorSwapTex.Apply();
    }

    void Update()
    {
        if (id == 1)
        {
        }

        if (id == 2)
        {
            SwapColor(SwapIndex.Prim, ColorFromIntRGB(177, 172, 50));
            SwapColor(SwapIndex.Back, ColorFromIntRGB(82, 75, 36));
            mColorSwapTex.Apply();
        }

        if (id == 3)
        {
            SwapColor(SwapIndex.Prim, ColorFromIntRGB(75, 105, 47));
            SwapColor(SwapIndex.Back, ColorFromIntRGB(82, 75, 36));
            mColorSwapTex.Apply();
        }

        if (id == 4)
        {
            SwapColor(SwapIndex.Prim, ColorFromIntRGB(48, 96, 130));
            SwapColor(SwapIndex.Back, ColorFromIntRGB(63, 63, 116));
            mColorSwapTex.Apply();
        }

    }

}


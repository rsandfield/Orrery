using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;

public class OrbitalElementFlag : MonoBehaviour
{
    public SVGImage flag;
    public TMP_Text text;
    [SerializeField]
    Color _flagColor;
    public Color flagColor {
        get => _flagColor;
        set
        {
            _flagColor = value;
            flag.color = value;
        }
    }
    [SerializeField]
    Color _textColor;
    public Color textColor {
        get => _textColor;
        set
        {
            _textColor = value;
            text.color = value;
        }
    }

    public static OrbitalElementFlag Instantiate(Transform parent, Vector3 position, string tag, Color? flagColor = null, Color? textColor = null)
    {
        OrbitalElementFlag prefab = Resources.Load("Prefabs/UI/Orbital Element Flag", typeof(OrbitalElementFlag)) as OrbitalElementFlag;
        OrbitalElementFlag flag = Instantiate(prefab, position, parent.rotation, parent);
        flag.flagColor = flagColor ?? flag.flagColor;
        flag.textColor = textColor ?? flag.textColor;
        flag.text.text = tag;
        return flag;
    }
    
    void Reset()
    {
        flag = GetComponentInChildren<SVGImage>();
        text = GetComponentInChildren<TMP_Text>();
        flagColor = Color.cyan;
        textColor = Color.white;
    }
}
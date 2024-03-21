using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectableCounter : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI counterText;

    public void setCounter(int num)
    {
        string text = "x" + num;
        counterText.text = text;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS_Counter : MonoBehaviour
{
    public TextMeshProUGUI _fpsText;

    private void Update()
    {
        _fpsText.SetText("FPS: " + (1f / Time.unscaledDeltaTime).ToString());
    }
}

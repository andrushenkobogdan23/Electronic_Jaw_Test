using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class ColorableObject : MonoBehaviour {

    [SerializeField]
    private string prefabName;

    [SerializeField]
    private ClickColorData clickColorData;

    [SerializeField]
    private int clickCount = 0;

    Material cachedMaterial;
    FigureSettings cachedFigureSettings;

    private void Start()
    {
        cachedMaterial = GetComponent<Renderer>().material;
        cachedFigureSettings = clickColorData.Settings.FirstOrDefault(r => r.prefabName == prefabName);

        StartCoroutine(ChangeColor());
    }    

    private IEnumerator ChangeColor()
    {

        if (cachedFigureSettings == null)
        {
            Debug.LogWarningFormat("Can't find record for {0} figure", prefabName);
            yield break;
        }

        while (true)
        {
            yield return new WaitForSeconds(cachedFigureSettings.SecondsToChageColor);
            cachedMaterial.color = UnityEngine.Random.ColorHSV();
        }
    }

    public void OnClick()
    {
        clickCount++;
        
        if (cachedFigureSettings == null)
        {
            Debug.LogWarningFormat("Can't find record for {0} figure", prefabName);
            return;
        }

        var colorRecord = cachedFigureSettings.clickColorMaping.FirstOrDefault(r => r.minClick <= clickCount && r.maxClck >= clickCount);

        if (colorRecord == null)
        {
            Debug.LogWarningFormat("Can't find color for {0} clikcs for {1} figure", clickCount, prefabName);
            return;
        }

        cachedMaterial.color = colorRecord.color;
    }
}

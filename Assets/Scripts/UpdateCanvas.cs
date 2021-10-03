using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCanvas : MonoBehaviour
{
    Canvas currCanvas;
    RectTransform canvasRect;
    // Lives Sprite
    [SerializeField] public Sprite heartTexture;
    private List<GameObject> heartList;
    [SerializeField] private float heart_scale = 0.8f;
    // Debuffs Sprite
    private List<GameObject> spriteList;
    // Start is called before the first frame update
    void Start()
    {
        currCanvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();
        heartList = new List<GameObject>();
        for (int i = 0; i < Globals.lives; i++)
        {
            GameObject newObj = new GameObject();
            
            Image heartImage = newObj.AddComponent<Image>();
            heartImage.sprite = heartTexture;
            newObj.GetComponent<RectTransform>().SetParent(currCanvas.transform);
            Vector3[] v = new Vector3[4];
            canvasRect.GetLocalCorners(v);
            heartImage.transform.localPosition = v[1] + (Vector3.down*15) + (Vector3.right*15 + Vector3.right*i*30);
            heartImage.rectTransform.sizeDelta = new Vector2(heart_scale, heart_scale);
            newObj.SetActive(true);


            heartList.Add(newObj);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.lives != heartList.Count && heartList.Count > 0)
        {
            Destroy(heartList[heartList.Count - 1]);
            heartList.RemoveAt(heartList.Count - 1);
        }
    }
}
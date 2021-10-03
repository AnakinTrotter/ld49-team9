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
    private Vector3[] canvasCorners = new Vector3[4];
    private float heart_x;
    private float heart_y;
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
            RectTransform heartRect = newObj.GetComponent<RectTransform>();
            heartImage.sprite = heartTexture;
            newObj.GetComponent<RectTransform>().SetParent(currCanvas.transform);
            canvasRect.GetLocalCorners(canvasCorners);    // get 4 corners of canvas
            heartImage.transform.localPosition = canvasCorners[1] + (Vector3.up*0.5f*heartRect.rect.y) + 
                (Vector3.left*heartRect.rect.x*0.5f + Vector3.left*i*heartRect.rect.x*0.75f);
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
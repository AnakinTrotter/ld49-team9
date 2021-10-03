using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCanvas : MonoBehaviour
{
    Canvas currCanvas;
    [SerializeField] public Sprite heartTexture;
    [SerializeField] private List<GameObject> heartList;
    [SerializeField] private float heart_y = 0.1f;
    [SerializeField] private float heart_x = 0.1f;
    [SerializeField] private float heart_scale = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        currCanvas = GetComponent<Canvas>();
        heartList = new List<GameObject>();
        for (int i = 0; i < Globals.lives; i++)
        {
            GameObject newObj = new GameObject();
            Image heartImage = newObj.AddComponent<Image>();
            heartImage.sprite = heartTexture;
            //heartImage.transform.Translate(Vector2.up * heart_y);
            //heartImage.transform.Translate(Vector2.left * heart_x);
            heartImage.rectTransform.sizeDelta = new Vector2(heart_scale, heart_scale);
            newObj.GetComponent<RectTransform>().SetParent(currCanvas.transform);
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


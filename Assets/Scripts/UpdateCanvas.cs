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
    [SerializeField] private float heart_scale = 0.5f;
    private Vector3[] canvasCorners = new Vector3[4];
    // Debuffs Sprite
    [SerializeField] private Dictionary<Globals.DebuffState, GameObject> debuffList = new Dictionary<Globals.DebuffState, GameObject>();
    private Dictionary<Globals.DebuffState, string> debuffDict = new Dictionary<Globals.DebuffState, string>
        {
        {Globals.DebuffState.slow, "Slowed!" },
        {Globals.DebuffState.fast, "Speedy!" },
        {Globals.DebuffState.invert, "Inverted Controls!" },
        {Globals.DebuffState.moon, "Moon!" },
        {Globals.DebuffState.rewind, "Rewinded!" }
        };
    // Start is called before the first frame update
    void Start()
    {
        currCanvas = GetComponent<Canvas>();
        canvasRect = GetComponent<RectTransform>();
        heartList = new List<GameObject>();
        // Add hearts to screen
        for (int i = 0; i < Globals.lives; i++)
        {
            GameObject newObj = new GameObject();
            newObj.layer = LayerMask.NameToLayer("UI");
            Image heartImage = newObj.AddComponent<Image>();
            RectTransform heartRect = newObj.GetComponent<RectTransform>();
            heartImage.sprite = heartTexture;
            newObj.GetComponent<RectTransform>().SetParent(currCanvas.transform);
            canvasRect.GetLocalCorners(canvasCorners);    // get 4 corners of canvas

            heartImage.transform.localPosition = canvasCorners[1] + (Vector3.up * 0.75f * heartRect.rect.y) +
                (Vector3.left * heartRect.rect.x * 0.75f + Vector3.left * i * heartRect.rect.x * 1.5f);
            newObj.transform.localScale = new Vector3(heart_scale, heart_scale, 1f);
            newObj.SetActive(true);
            heartList.Add(newObj);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.debuffs.Contains(Globals.DebuffState.invert))
            Debug.Log("Inverted");
        if (Globals.debuffs.Contains(Globals.DebuffState.slow))
            Debug.Log("slow");
        if (Globals.debuffs.Contains(Globals.DebuffState.moon))
            Debug.Log("moon");
        if (Globals.debuffs.Contains(Globals.DebuffState.fast))
            Debug.Log("fast");
        if (Globals.lives != heartList.Count && heartList.Count > 0)
        {
            Destroy(heartList[heartList.Count - 1]);
            heartList.RemoveAt(heartList.Count - 1);
        }
        if (Globals.debuffs.Count != debuffList.Count || Globals.debuffChanged)
        {
            // reset debuffList
            foreach (var debuff in debuffList)
            {
                Destroy(debuffList[debuff.Key]);
            }
            debuffList.Clear();
            // Find new debuffs
            List<Globals.DebuffState> newDebuffs = new List<Globals.DebuffState>();
            foreach (var debuff in Globals.debuffs)
            {
                if (!debuffList.ContainsKey(debuff))
                {
                    newDebuffs.Add(debuff);
                }
            }
            foreach (var debuff in newDebuffs)
            {
                // Add debuff text
                GameObject debuffInd = new GameObject();
                debuffInd.layer = LayerMask.NameToLayer("UI");

                Text debuffText = debuffInd.AddComponent<Text>();

                debuffText.text = debuffDict[debuff];
                debuffText.color = Color.red;
                Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
                debuffText.font = ArialFont;
                debuffText.fontSize = 25;
                debuffText.material = ArialFont.material;
                debuffText.alignment = TextAnchor.MiddleRight;
                debuffInd.transform.SetParent(currCanvas.transform);
                debuffText.transform.localPosition = canvasCorners[2] + Vector3.down * 100 + Vector3.down * 75 + Vector3.left * 150;
                debuffText.transform.localScale = new Vector3(1f, 1f, 1f);
                debuffText.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);
                debuffInd.SetActive(true);
                debuffList.Add(debuff, debuffInd);
            }
            Globals.debuffChanged = false;
        }
    }
}
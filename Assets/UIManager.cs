using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public RectTransform btnPrefab;

    private FeatureManager mgr;
    private Text descText;
    private List<Button> buttons;

	// Use this for initialization
	void Start () {
        mgr = FindObjectOfType<FeatureManager>();
        descText = transform.Find("Navigation").Find("Text").GetComponent<Text>();
        transform.Find("Navigation").Find("Previous").GetComponent<Button>().onClick.AddListener(() => mgr.PreviousChoice());
        transform.Find("Navigation").Find("Next").GetComponent<Button>().onClick.AddListener(() => mgr.NextChoice());
        InitialzeFeatureButtons(); 
    }
    
    void InitialzeFeatureButtons()
    {
        buttons = new List<Button>();
        float height = btnPrefab.rect.height;
        float width = btnPrefab.rect.width;
        for(int i = 0; i < mgr.features.Count; i++)
        {
            RectTransform temp = Instantiate<RectTransform>(btnPrefab);
            temp.name = i.ToString();
            temp.SetParent(transform.Find("Features").GetComponent<RectTransform>());
            temp.localScale = new Vector3(1, 1, 1);
            temp.localPosition = new Vector3(0, 0, 0);
            temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, width);
            temp.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);

            Button b = temp.GetComponent<Button>();
            b.onClick.AddListener(() => mgr.SetCurrent(int.Parse(temp.name)));
            buttons.Add(b);
        }
    }

    void UpdateFeatureButtons()
    {
        for(int i = 0; i < mgr.features.Count; i++)
        {
            buttons[i].transform.Find("FeatureImage").GetComponent<Image>().sprite = mgr.features[i].renderer.sprite;
        }
    }	


	// Update is called once per frame
	void Update () {
        UpdateFeatureButtons();
        descText.text = mgr.features[mgr.currFeature].ID +"#"+(mgr.features[mgr.currFeature].currIndex+1).ToString();
	}
}

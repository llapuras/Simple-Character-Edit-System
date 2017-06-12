using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class FeatureManager : MonoBehaviour {

    public List<Feature> features;
    public int currFeature;

    void OnEnable(){
        LoadFeatures();
    }

    void OnDisable()
    {
        SaveFeatures();
    }

      void LoadFeatures()
    {
        features = new List<Feature>();
        features.Add(new Feature("Face",transform.Find("Face").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Hair", transform.Find("Face").transform.Find("Hair").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Eyes", transform.Find("Face").transform.Find("Eyes").GetComponent<SpriteRenderer>()));
        features.Add(new Feature("Mouth", transform.Find("Face").transform.Find("Mouth").GetComponent<SpriteRenderer>()));
    }

    void SaveFeatures()
    {

    }


    public void SetCurrent(int index)
    {
        if (features == null)
            return;

        currFeature = index;
    }

    public void NextChoice()
    {
        if (features == null)
            return;
     
        features[currFeature].currIndex++;

        features[currFeature].UpdateFeature();
    }

    public void PreviousChoice()
    {
        if (features == null)       
            return;

        features[currFeature].currIndex--;

        features[currFeature].UpdateFeature();
    }


}

[System.Serializable]
public class Feature
{
    public string ID;                    //部件名称
    public int currIndex;                //当前部件选项编号
    public Sprite[] choices;             //部件选项数组
    public SpriteRenderer renderer;      //精灵渲染器

    public Feature(string id,SpriteRenderer rend)
    {
        ID = id;
        renderer = rend;
        UpdateFeature();
    }

    public void UpdateFeature()
    {
        choices = Resources.LoadAll<Sprite>("lapuras/" + ID);

        if (choices == null || renderer == null)
            return;

        //到数组循环
        if (currIndex < 0)
            currIndex = choices.Length - 1;
        if (currIndex >= choices.Length)
            currIndex = 0;

        renderer.sprite = choices[currIndex];

        Debug.Log("cww"+currIndex);
    }
}
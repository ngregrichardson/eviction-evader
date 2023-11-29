using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionManager : MonoBehaviour {

    #region Singleton
    public static SectionManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    public List<GameObject> sections;

    void Update()
    {
        if(sections.Count <= 0)
        {
            GameManager.instance.Lose();
        }
    }

    public List<GameObject> GetSections()
    {
        return sections;
    }

}

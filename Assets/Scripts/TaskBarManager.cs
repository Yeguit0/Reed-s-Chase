using UnityEngine;
using UnityEngine.UI;

public class TaskBarManager : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public GameObject target;  
        public Buttons data;      
        public Button button;     
    }

    [SerializeField] private Entry[] entries; 

    private void Start()
    {
        for (int i = 0; i < entries.Length; i++)
        {
            Entry e = entries[i];

            e.button.image.sprite = e.data.portada;       
            e.button.transform.SetSiblingIndex(i);       
            e.button.onClick.AddListener(() => e.target.transform.SetAsLastSibling()); 
        }
    }

    private void Update()
    {
        foreach (Entry e in entries)
        {
            bool active = e.target.activeSelf;
            if (e.button.gameObject.activeSelf != active)
                e.button.gameObject.SetActive(active);
        }
    }
}

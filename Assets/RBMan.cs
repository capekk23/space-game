using UnityEngine;
using System.Linq;

public class RBMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform[] RBs;
    void Start()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.transform).ToArray();
    }
    void updateArray()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.transform).ToArray();
    }
}
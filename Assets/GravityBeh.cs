using UnityEngine;

public class GravityBeh : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject RBMan;
    void Start()
    {
        RBMan = GameObject.Find("Awake");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using UnityEngine;
using System.Linq;
using Unity.Mathematics;

public class RBMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Transform[] RBs;
    [SerializeField]
    private ComputeShader Forces;
    void Start()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.transform).ToArray();
    }
    private void FixedUpdate()
    {
        ComputeBuffer bufferVels = new ComputeBuffer(RBs.Length, sizeof(float) * 3);
        ComputeBuffer bufferPoss = new ComputeBuffer(RBs.Length, sizeof(float) * 3);

        Forces.SetBuffer(0, "vels", bufferVels);
        Forces.SetBuffer(0, "poss", bufferPoss);
        Forces.Dispatch(0, RBs.Length/10, 1, 1);
    }
    void updateArray()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.transform).ToArray();
    }
}
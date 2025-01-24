using UnityEngine;
using System.Linq;
using Unity.Mathematics;

public class RBMan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Rigidbody[] RBs;
    [SerializeField]
    private ComputeShader Forces;
    [SerializeField]
    private float GConst;
    void Start()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.GetComponent<Rigidbody>()).ToArray();
    }
    private void FixedUpdate()
    {
        ComputeBuffer bufferVels = new ComputeBuffer(RBs.Length, sizeof(float) * 3);
        Vector3[] vels = RBs.Select(p => p.linearVelocity).ToArray();
        bufferVels.SetData(vels);
        ComputeBuffer bufferPoss = new ComputeBuffer(RBs.Length, sizeof(float) * 3);
        Vector3[] poss = RBs.Select(p => p.position).ToArray();
        bufferPoss.SetData(poss);
        ComputeBuffer bufferMass = new ComputeBuffer(RBs.Length, sizeof(float));
        float[] mass = RBs.Select(p => p.mass).ToArray();
        bufferMass.SetData(mass);
        Forces.SetBuffer(0, "vels", bufferVels);
        Forces.SetBuffer(0, "poss", bufferPoss);
        Forces.SetBuffer(0, "mass", bufferMass);
        Forces.SetFloat("GConst", GConst);
        Forces.Dispatch(0, RBs.Length/10, 1, 1);
        bufferVels.GetData(vels);
        bufferPoss.GetData(poss);
        bufferMass.GetData(mass);
    }
    void updateArray()
    {
        RBs = GameObject.FindGameObjectsWithTag("Body").Select(p => p.GetComponent<Rigidbody>()).ToArray();
    }
}
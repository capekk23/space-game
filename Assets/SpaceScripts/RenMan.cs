using Unity.Mathematics;
using UnityEngine;

public class RenMan : MonoBehaviour
{
    static uint CHUNK_SIZE = 64;
    struct Particle
    {
        float3 position;
        float3 velocity;
        bool structured;
    }
    struct _3DChunk
    {
        uint x;
        Particle[] y;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() //size : 49 408 B
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}

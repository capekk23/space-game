using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class RenMan : MonoBehaviour
{
    static uint CHUNK_SIZE = 64;
    static uint FLAG_MASK = 0xff;
    static uint FLAG_EMPTY = 0x00;
    static uint FLAG_USED = 0xff;
    static float CHUNK_LENGTH = 20.0f;
    static uint HASH_SIZE = 32;
    [SerializeField]
    TextAsset pMap;

    struct Particle
    {
        public Vector3 position;
        public Vector3 velocity;
        public bool structured;
    }
    struct _3DChunk
    {
        public uint x;
        public Particle[] y;
    }
    static uint hash(uint k, uint bufferSize)
    {
        k ^= k >> 16;
        k *= 0x85ebca6b;
        k ^= k >> 13;
        k *= 0xc2b2ae35;
        k ^= k >> 16;
        return k & (bufferSize - 1);
    }

    class HASHMAP
    {
        _3DChunk[] Map;
        public HASHMAP(TextAsset pMap)
        {
            Map = new _3DChunk[HASH_SIZE];
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i] = new _3DChunk();
                Map[i].x = 0x0;
            }
            Particle[] txtRef = pMap.GetData<Particle>().ToArray();
            for (int i = 0;i < txtRef.Length;i++)
            {
                uint key = 0xff;
                key += (uint)Mathf.Floor(txtRef[i].position.x / CHUNK_LENGTH) << 6;
                key += (uint)Mathf.Floor(txtRef[i].position.y / CHUNK_LENGTH) << 4;
                key += (uint)Mathf.Floor(txtRef[i].position.z / CHUNK_LENGTH) << 2;

                var dest = HashLookup(key, HASH_SIZE);
            }
        }
        ref _3DChunk
            HashLookup(
            uint key, uint bufferSize)
        {
            uint slot = hash(key, bufferSize);
            uint counter = 0;
            while (true)
            {
                if ((Map[slot].x == key) | ((Map[slot].x & FLAG_MASK) == FLAG_EMPTY))
                {
                    return ref Map[slot];
                }
                slot = (slot + 1) & (bufferSize - 1);
                if (counter == CHUNK_SIZE)
                {
                    throw new System.Exception();
                }
                counter++;
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() //size : 51 200 B
    {
        new ComputeBuffer(32,64*(2*3*4+1)+8);

        //HASHMAP hm = new HASHMAP();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}

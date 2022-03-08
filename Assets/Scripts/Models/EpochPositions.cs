using UnityEngine;

public class EpochPositions
{
    struct EpochPosition
    {
        public float epoch;
        public Vector3 position;
        public EpochPosition(float epoch, Vector3 position)
        {
            this.epoch = epoch;
            this.position = position;
        }
    }

    EpochPosition[] saved;
    int stalestIndex;
    int newestIndex;

    public EpochPositions(int size)
    {
        saved = new EpochPosition[size];
    }

    public void Push(float epoch, Vector3 position)
    {
        if(Get(epoch) != null) return;
        
        int index = (newestIndex + 1) % saved.Length;
        saved[index] = new EpochPosition(epoch, position);

        if(index == stalestIndex) stalestIndex = newestIndex;
        newestIndex = index;
    }

    public Vector3? Get(float epoch)
    {
        for(int i = stalestIndex; i < saved.Length + newestIndex; i++)
        {
            int index = i % saved.Length;
            EpochPosition ep = saved[index];
            if(ApproximatelyEquals(epoch, ep.epoch)) return ep.position;
        }
        return null;
    }

    bool ApproximatelyEquals(float a, float b)
    {
        float epsilon = 1e-5f;
        if(Mathf.Abs(a) < epsilon && Mathf.Abs(b) < epsilon) return true;

        if(a > b)
        {
            float temp = a;
            a = b;
            b = temp;
        }

        if((b - a) / b < epsilon) return true;

        return false;
    }
}
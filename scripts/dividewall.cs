using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dividewall : MonoBehaviour
{
    public GameObject Ddwall;

    public void divide()
    {
        Renderer rend = GetComponent<Renderer>();

        Vector3 sz = rend.bounds.size;
        Vector3 cent = rend.bounds.center;

        int size = Mathf.RoundToInt(sz.x);
        char key = 'x';
        if(sz.z > sz.x)
        {
            size = Mathf.RoundToInt(sz.z);
            key = 'z';
        }

        if(size%2 == 0 && key == 'x')
        {
            for(int i=0;i<size;i++)
            {
                Vector3 pos = cent;
                pos.x += (float)(i - (size / 2) + 0.5);

                Instantiate(Ddwall, pos, transform.rotation);
            }
        }
        if (size % 2 == 0 && key == 'z')
        {
            for (int i = 0; i < size; i++)
            {
                Vector3 pos = cent;
                pos.z += (float)(i - (size / 2) + 0.5);

                Instantiate(Ddwall, pos, transform.rotation);
            }
        }
        if (size % 2 == 1 && key == 'x')
        {
            for (int i = 0; i < size; i++)
            {
                Vector3 pos = cent;
                pos.z += (float)(i - (size / 2));

                Instantiate(Ddwall, pos, transform.rotation);
            }
        }
        if (size % 2 == 1 && key == 'z')
        {
            for (int i = 0; i < size; i++)
            {
                Vector3 pos = cent;
                pos.z += (float)(i - (size / 2));

                Instantiate(Ddwall, pos, transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}

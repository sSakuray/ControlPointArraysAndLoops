using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSort : MonoBehaviour
{
    [SerializeField] private Transform[] cubes;
    [SerializeField] private float Spacing;

    private void Awake()
    {
        SortBySize();
    } 




    private void SortBySize()
    {
        for (int i = 0; i < cubes.Length; i++)
        {
            for (int j = 0; j < cubes.Length - 1; j++)
            {
                if (cubes[j].localScale.x < cubes[j + 1].localScale.x)
                {
                    Transform temp = cubes[j];
                    cubes[j] = cubes[j + 1];
                    cubes[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < cubes.Length; i++)
        {
            cubes[i].localPosition = new Vector3(i * Spacing, 0, 0);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;

public class MainCode : MonoBehaviour
{
    public GameObject[] item;
    private List<GameObject> itemFood = new List<GameObject>();
    public GameObject[] place;

    public GameObject[] placeTable;


    private GameObject currentFood;
    private List<GameObject> foodOrder = new List<GameObject>();
    
    //Случайная генерация блюд
    public void RandomButton()
    {
        if (foodOrder.Count != 0)
        {
            for (int i = 0; i < foodOrder.Count; i++)
                Destroy(foodOrder[i]);
            
            foodOrder.Clear();
            return;
        }

        itemFood.AddRange(item);

        Debug.Log(itemFood.Count);
        for (int i = 0; i < place.Length; i++)
        {
            currentFood = itemFood[Random.Range(0, itemFood.Count)];
            itemFood.Remove(currentFood);
            GameObject food = Instantiate(currentFood, place[i].transform.position, place[i].transform.rotation);
            food.name = currentFood.name;
            foodOrder.Add(food);
        }
    }

    //Сортировка пузырьком
    public void BubbleSort()
    {
        var n = foodOrder.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1; j++)
            {
                Debug.Log(foodOrder[j].name);
                if (Convert.ToInt32(foodOrder[j].name) > Convert.ToInt32(foodOrder[j + 1].name))
                {
                    GameObject tempVar = foodOrder[j];
                    int pos = Convert.ToInt32(foodOrder[j].name);

                    foodOrder[j] = foodOrder[j + 1];
                    foodOrder[j].transform.position = place[Convert.ToInt32(foodOrder[j + 1].name)].transform.position;

                    foodOrder[j + 1] = tempVar;
                    foodOrder[j + 1].transform.position = place[pos].transform.position;
                }
            }
        }
    }

    //Сортировка вставками
    public void InsertionSort()
    {
        var n = foodOrder.Count;

        for (int i = 1; i < n; i++)
        {
            var key = foodOrder[i];
            int pos = Convert.ToInt32(key.name);
            var flag = 0;

            for (int j = i - 1; j >= 0 && flag != 1;)
            {
                if (Convert.ToInt32(key.name) < Convert.ToInt32(foodOrder[j].name))
                {
                    foodOrder[j + 1] = foodOrder[j];
                    foodOrder[j + 1].transform.position = place[Convert.ToInt32(foodOrder[j].name)].transform.position;
                    j--;
                    foodOrder[j + 1] = key;
                    foodOrder[j + 1].transform.position = place[pos].transform.position;
                }

                else flag = 1;
            }
        }
    }

    //Сортировка выбором
    public void SelectionSort()
    {
        var n = foodOrder.Count;

        for (int i = 0; i < n - 1; i++)
        {
            var smallestVal = i;

            for (int j = i + 1; j < n; j++)
            {
                if (Convert.ToInt32(foodOrder[j].name) < Convert.ToInt32(foodOrder[smallestVal].name))
                {
                    smallestVal = j;
                }
            }

            var tempVar = foodOrder[smallestVal];
            int pos = Convert.ToInt32(tempVar.name);

            foodOrder[smallestVal] = foodOrder[i];
            foodOrder[smallestVal].transform.position = place[Convert.ToInt32(foodOrder[i].name)].transform.position;

            foodOrder[i] = tempVar;
            foodOrder[i].transform.position = place[pos].transform.position;
        }
    }

    //Быстрая сортировка
    public void QuickSort()
    {
        var n = foodOrder.Count;

        SortArray(foodOrder, 0, n - 1);
    }

    private void SortArray(List<GameObject> arrayFood, int leftIndex, int rightIndex)
    {
        var i = leftIndex;
        var j = rightIndex;

        var pivot = arrayFood[leftIndex];

        while (i <= j)
        {
            while (Convert.ToInt32(arrayFood[i].name) < Convert.ToInt32(pivot.name))
            {
                i++;
            }
            
            while (Convert.ToInt32(arrayFood[j].name) > Convert.ToInt32(pivot.name))
            {
                j--;
            }

            if (i <= j)
            {
                var tempVar = arrayFood[i];
                int pos = Convert.ToInt32(tempVar.name);

                arrayFood[i] = arrayFood[j];
                arrayFood[i].transform.position = place[Convert.ToInt32(arrayFood[j].name)].transform.position;

                arrayFood[j] = tempVar;
                arrayFood[j].transform.position = place[pos].transform.position;
                i++;
                j--;
            }
        }
        
        if (leftIndex < j)
            SortArray(arrayFood, leftIndex, j);

        if (i < rightIndex)
            SortArray(arrayFood, i, rightIndex);
    }

    /*IEnumerator MoveObject(GameObject item, Vector3 end)
    {
        if (Vector2.Distance(item.transform.position, end) > 0.01f)
        {
            item.transform.position = Vector2.Lerp(item.transform.position, end, Time.deltaTime);
            yield return null;
        }
    }*/
}

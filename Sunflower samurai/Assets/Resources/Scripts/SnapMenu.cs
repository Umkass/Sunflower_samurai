using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SnapMenu : MonoBehaviour
{
    public Transform contentTransform;
    [SerializeField]
    Transform centralPoint;
    //тут будем хранить расстояние между центром и каждым элементом скролла
    [SerializeField]
    List<float> distancesToCenter = new List<float>();
    //тут храним расстояние между родительским объектом элементов скролла и всех элементов скролла
    [SerializeField]
    List<float> distancesToScroll = new List<float>();
    //индекс элемента с минимальным расстоянием до центра
    [SerializeField]
    int indexMinDist;
    //происходит ли драг скролла
    bool dragging;

    [SerializeField]
    GameObject spriteRendererArrowRight;
    [SerializeField]
    GameObject spriteRendererArrowLeft;
    Sprite ArrowRight;
    Sprite ArrowLeft;

    //это метод нужно вызвать через компонент Event Trigger -> на событие Begin Drag
    //Event Trigger нужно назначит на объект в котором компонент ScrollRect
    //этот метод будет вызван на момент начала драга
    public void BeginDrag()
    {
        dragging = true;
        distancesToCenter.Clear();
    }

    public void EndDragObject()
    {
        //вычисляем дистанцию между скролл-контент точкой и элементом
        CalcDistanceScrollToObject();

        //получаем дистанцию между центром и каждым элементом
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            //вычисляем и получаем абсолютное значение
            distancesToCenter.Add(Mathf.Abs(centralPoint.position.x - contentTransform.GetChild(i).position.x));
        }
        //вычисляем минимальный индекс к центру
        for (int i = 0; i < distancesToCenter.Count; i++)
        {
            if (distancesToCenter[i] == distancesToCenter.Min()) { indexMinDist = i; }
        }

        dragging = false;
    }
    void Snap()
    {
        //если скролл не драгается
        if (dragging == false)
        {
            //центровка самого ближнего к центру элемента
            float posX = Mathf.Lerp(contentTransform.position.x, centralPoint.position.x - distancesToScroll[indexMinDist], Time.deltaTime * 20);
            Vector2 pos = new Vector2(posX, contentTransform.position.y);
            contentTransform.position = pos;
        }
    }

    //получаем дистанцию каждого элемента до скролл-объекта
    public void CalcDistanceScrollToObject()
    {
        distancesToScroll.Clear();
        for (int i = 0; i < contentTransform.childCount; i++)
        {
            //вычисляем дистанцию между скролл-контент точкой и фото
            distancesToScroll.Add(Mathf.Abs(contentTransform.position.x - contentTransform.GetChild(i).position.x));
        }
    }

    public void ToStart()
    {
        contentTransform.position = new Vector3(0, contentTransform.position.y, contentTransform.position.z);
        indexMinDist = 0;
    }

    void Start()
    {
        spriteRendererArrowRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ArrowRight");
        spriteRendererArrowLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/ArrowLeft");
    }
    // Update is called once per frame
    void Update()
    {
        CalcDistanceScrollToObject();
        Snap();
        if (indexMinDist == 0)
        {
            spriteRendererArrowLeft.SetActive(false);
        }
        else if (indexMinDist == 7)
        {
            spriteRendererArrowRight.SetActive(false);
        }
        else
        {
            spriteRendererArrowLeft.SetActive(true);
            spriteRendererArrowRight.SetActive(true);
        }
    }
}

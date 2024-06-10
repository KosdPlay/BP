using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstruction : MonoBehaviour
{
    [SerializeField] private List<GameObject> platformsUI;
    [SerializeField] private List<Vector2> transformsPlatformsUI;
    [SerializeField] private List<GameObject> platforms;
    [SerializeField] private List<int> activePlatforms = new List<int>();

    private float yOffsetUI = 70;
    [SerializeField] private float yOffset = 2;

    private void Start()
    {
        for (int i = 0; i < platformsUI.Count-1; i++)
        {
            transformsPlatformsUI[i] = platformsUI[i].GetComponent<RectTransform>().anchoredPosition;
            platformsUI[i].SetActive(false);
            for (int j = 0; j < 3; j++)
            {
                platformsUI[i].transform.GetChild(j).gameObject.SetActive(true);
            }
        }
        platformsUI[0].SetActive(true);
    }


    public void UpperPlatform(int id)
    {
        EstablishingPosition(id, yOffsetUI, yOffset, 0);
    }

    public void AveragePlatform(int id)
    {
        EstablishingPosition(id, 0,0, 1);
    }

    public void LowerPlatform(int id)
    {
        EstablishingPosition(id, -yOffsetUI, -yOffset, 2);
    }

    public void Revert()
    {
        Debug.Log("1");
        for (int i = 0; i < 3; i++)
        {
            platformsUI[i].GetComponent<RectTransform>().anchoredPosition = transformsPlatformsUI[i];
            platformsUI[i].SetActive(false);
            for (int j = 0; j < 3; j++)
            {
                platformsUI[i].GetComponent<RectTransform>().GetChild(j).gameObject.SetActive(true);
            }

            for (int j = 0; j < 3; j++)
            {
                platforms[i].transform.GetChild(j).gameObject.SetActive(false);

            }
        }
        platformsUI[0].SetActive(true);

        activePlatforms.Clear();
    }

    private void EstablishingPosition(int id, float yUI,float y, int buttonNumber)
    {
        activePlatforms.Add(buttonNumber);
        for (int i= 0; i < 3; i++)
        {
            if (buttonNumber != i)
            {
                platformsUI[id].GetComponent<RectTransform>().GetChild(i).gameObject.SetActive(false);
            }
        }

        platformsUI[id + 1].SetActive(true);
        platformsUI[id + 1].GetComponent<RectTransform>().anchoredPosition = new Vector2(platformsUI[id + 1].GetComponent<RectTransform>().anchoredPosition.x, platformsUI[id].GetComponent<RectTransform>().anchoredPosition.y + yUI);

        for (int i = 0; i < 3; i++)
        {
            if (buttonNumber == i)
            {
                platforms[id].transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        platforms[id + 1].transform.position = new Vector2(platforms[id + 1].transform.position.x, platforms[id].transform.position.y + y);

    }

    public void Load(List<int> q)
    {
        for (int i = 0; i < 3; i++)
        {
            activePlatforms = q;
            switch (q[i])
            {
                case 0:
                    EstablishingPosition(i, yOffsetUI, yOffset, q[i]);
                    break;
                case 1:
                    EstablishingPosition(i, 0, 0, q[i]);
                    break;
                case 2:
                    EstablishingPosition(i, -yOffsetUI, -yOffset, q[i]);
                    break;
                default:

                    break;

            }
        }
    }

    public List<int> GetActivePlatforms()
    {
        return activePlatforms;
    }

}

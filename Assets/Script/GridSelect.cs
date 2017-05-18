using UnityEngine;
using System.Collections;

public class GridSelect : MonoBehaviour
{


    private void OnTriggerEnter(Collider col)
    {
        switch (col.transform.name)
        {
            case "1":
                Player.instance.ChangeColor(new Color(255f / 255f, 0f, 0f, 255f / 255f), new Color(0.2f, 0f, 0f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f / 255f, 160f / 255f, 255f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(0f, 0f, 0f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 1);
                break;
            case "2":
                Player.instance.ChangeColor(new Color(166f / 255f, 20f / 255f, 47f / 255f, 255f / 255f), new Color(0.6f, 0.1f, 0.27f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(217f / 255f, 54f / 255f, 84f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(217f / 255f, 152f / 255f, 115f / 255f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 2);
                break;
            case "3":
                Player.instance.ChangeColor(new Color(255f/255f, 97f/255f, 56f/255f, 255f/255f), new Color(1f, 0.38f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(0f, 163f / 255f, 136f / 255f, 255f / 255f));
                GameManager.instance.mainCam.backgroundColor = new Color(121f / 255f, 189f / 255f, 143f / 255f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 3);
                break;
            case "4":
                Player.instance.ChangeColor(new Color(1f, 1f, 1f,1f), new Color(0.4f, 0.4f, 0.4f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(255f/255f,  0f, 0f, 1f));
                GameManager.instance.mainCam.backgroundColor = new Color(242f / 255f, 185f / 255f, 4f / 255f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 4);
                break;
            case "5":
                Player.instance.ChangeColor(new Color(66f/255f, 75f/255f, 84f/255f, 1f), new Color(0.25f, 0.3f, 0.3f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
                GameManager.instance.mainCam.backgroundColor = new Color(14f / 255f, 21f / 255f, 37f / 255f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 5);
                break;
            case "6":
                Player.instance.ChangeColor(new Color(243f / 255f, 203f / 255f, 73f / 255f, 1f), new Color(0.96f, 0.6f, 0.2f));
                GameManager.instance.enemyMat.SetColor("_Color", new Color(117f/255f, 66f/255f, 47f/255f, 255f/255f));
                GameManager.instance.mainCam.backgroundColor = new Color(157f / 255f, 17f / 255f, 20f / 255f, 0f);
                PlayerPrefs.SetInt("COLORNUM", 6);
                break;
        }
    }
}

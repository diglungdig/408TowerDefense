﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using foodDefense;

public class generateMinions : MonoBehaviour {

    public Vector3 genePos;

    public Transform geneSprite;
    public GameObject minion1;
    public GameObject minion2;
    public GameObject minion3;
    public GameObject tower;
    public GameObject towerText;
	public GameObject capWarningText;
    public Text goldAmout;
    public AudioSource falseSound;
    public int pathIndex;
	public int minionsCount = 0;

	public int minionCap = 10;
    private bool hasTower = false;
    
	private bool buyDisable = false;
	void Start()
    {
        genePos = geneSprite.position;
    }

    public void GenerateOne(int index)
    {
		if(!buyDisable){
        if (index == 1)
        {
            //banana
            if (!costMoney(costList(food.banana)))
            {
                return;
            }

			Debug.Log (minionsCount);

			minionsCount++;
            GameObject newObject = (GameObject)Instantiate(minion1, genePos, Quaternion.Euler(0f, 0f, 0f));
            newObject.AddComponent<Ally>().thisType = food.banana;
            newObject.GetComponent<Ally>().setPathIndex(pathIndex);
            newObject.GetComponent<Ally>().mappingValue(food.banana);
        }
        else if (index == 2)
        {
            //strawberry
            if (!costMoney(costList(food.strawberry)))
            {
                return;
            }

			Debug.Log (minionsCount);

			minionsCount++;
            GameObject newObject = (GameObject)Instantiate(minion2, genePos, Quaternion.Euler(0f, 0f, 0f));
            newObject.AddComponent<Ally>().thisType = food.strawberry;
            newObject.GetComponent<Ally>().setPathIndex(pathIndex);
            newObject.GetComponent<Ally>().mappingValue(food.strawberry);
        }
        else if (index == 3)
        {
            //apple
            if (!costMoney(costList(food.apple)))
            {
                return;
            }

			Debug.Log (minionsCount);
			minionsCount++;
            GameObject newObject = (GameObject)Instantiate(minion3, genePos, Quaternion.Euler(0f, 0f, 0f));
            newObject.AddComponent<Ally>().thisType = food.apple;
            newObject.GetComponent<Ally>().setPathIndex(pathIndex);
            newObject.GetComponent<Ally>().mappingValue(food.apple);
        }

        else if (index == 4)
        {


            if (!hasTower)
            {
                if (!costMoney(costList(food.tower)))
                {
                    return;
                }
                else
                {

                    hasTower = true;
                    tower.SetActive(true);
                }

            }

                else
                {
					StartCoroutine(TextWarning(towerText));
					//goldAmout.text = (int.Parse (goldAmout.text) + 300).ToString ();
                }

        }
        else if(index == 5 && hasTower)
        {
            if (costMoney(200))
            {
                tower.GetComponent<tower>().UpdateTurret();
            }
        }
    }
	}

	IEnumerator TextWarning(GameObject textWarning)
    {
		textWarning.SetActive(true);
        yield return new WaitForSeconds(2f);
		textWarning.SetActive(false);
    }
    private bool costMoney(int amount)
    {
        if((int.Parse(goldAmout.text) - amount) < 0)
        {
            falseSound.Play();
            return false;
        }

        goldAmout.text = (int.Parse(goldAmout.text) - amount).ToString();
		GetComponent<AudioSource> ().Play ();
        return true;
    }
    private int costList(food foodType)
    {
        if (foodType == food.banana)
            return 50;
        else if (foodType == food.strawberry)
            return 100;
        else if (foodType == food.apple)
            return 150;
        else if (foodType == food.tower) return 300;
        return 0;
    }

	void Update(){

		if (GameObject.FindGameObjectsWithTag ("allyMinion").Length == minionCap) {
			buyDisable = true;
			StartCoroutine (TextWarning (capWarningText));

		} else {
			buyDisable = false;
		}
	}
}

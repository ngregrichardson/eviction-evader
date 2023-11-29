using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pages : MonoBehaviour
{

	int pageNum = 1;

	public void NextPage()
	{
		pageNum += 1;
		if (pageNum > 3)
		{
			pageNum = 3;
		}
		if(pageNum != 1)
		{
			gameObject.transform.Find("Page" + (pageNum - 1)).gameObject.SetActive(false);
		}
		gameObject.transform.Find("Page" + pageNum).gameObject.SetActive(true);
	}

	public void PrevPage()
	{
		pageNum -= 1;
		if (pageNum <= 0)
		{
			pageNum = 1;
		}
		if (pageNum != 3)
		{
			gameObject.transform.Find("Page" + (pageNum + 1)).gameObject.SetActive(false);
		}
		gameObject.transform.Find("Page" + pageNum).gameObject.SetActive(true);
	}
}

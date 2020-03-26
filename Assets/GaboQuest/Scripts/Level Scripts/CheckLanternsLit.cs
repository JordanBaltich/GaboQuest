using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLanternsLit : MonoBehaviour
{
    public List<Lantern> m_Lanterns;

    int numberOfLanternsToLight;
    int numberOfLitLanterns;


    public int playerID;

    // Start is called before the first frame update
    void Start()
    {
        numberOfLanternsToLight = m_Lanterns.Count;
        StartCoroutine(CheckIfLanternsAreLit());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CheckIfLanternsAreLit()
    {
        while (enabled)
        {
            numberOfLitLanterns = 0;

            foreach(Lantern lantern in m_Lanterns)
            {
                if(lantern.heldLibee != null)
                {
                    numberOfLitLanterns++;
                }
            }
            yield return new WaitForSeconds(.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == playerID && numberOfLitLanterns == numberOfLanternsToLight)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

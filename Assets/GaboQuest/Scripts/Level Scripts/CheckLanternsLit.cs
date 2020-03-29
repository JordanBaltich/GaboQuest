using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckLanternsLit : MonoBehaviour
{
    public List<Lantern> m_Lanterns;

    int numberOfLanternsToLight;
    int numberOfLitLanterns;

    [SerializeField] Material offMat, onMat;
    [SerializeField] MeshRenderer targetMesh;

    public int playerID;

    private void Awake()
    {
        targetMesh = GetComponent<MeshRenderer>();
    }

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

            //changes material depending on if the gate is open vs closed
            if (numberOfLitLanterns == numberOfLanternsToLight)
            {
                targetMesh.material = onMat;
            }
            else
            {
                targetMesh.material = offMat;
            }

            yield return new WaitForSeconds(.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == playerID && numberOfLitLanterns == numberOfLanternsToLight)
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}

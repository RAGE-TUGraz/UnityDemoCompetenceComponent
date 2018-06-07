using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CompetenceComponentNamespace;

public class EndSceneScript : MonoBehaviour
{

    public void Reset()
    {
        CompetenceComponent.Instance.ResetCompetenceState();
        SceneManager.LoadScene("StartScene");
    }
}

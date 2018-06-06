using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CompetenceAssessmentAssetNameSpace;

public class EndSceneScript : MonoBehaviour
{

    public void Reset()
    {
        CompetenceAssessmentAsset.Instance.resetCompetenceState();
        SceneManager.LoadScene("StartScene");
    }
}

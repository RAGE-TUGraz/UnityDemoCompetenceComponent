using AssetManagerPackage;
using CompetenceComponentNamespace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamesituationLoadingScript : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] Text evaluationState;
    [SerializeField] bool debugOutput;

    private int maxLevel = 3;

    private static bool isAssetpackCreated = false;
    string gsName;

    private void Awake()
    {
        if (!isAssetpackCreated)
        {
            // in here: create Component, determine settings
            AssetManager.Instance.Bridge = new Bridge();

            //CompetenceComponent
            CompetenceComponent competenceComponent = CompetenceComponent.Instance;
            CompetenceComponentSettings settings = new CompetenceComponentSettings();
            settings.NumberOfLevels = maxLevel;
            settings.SourceFile = "dataModel.xml";
            settings.LinearDecreasionOfCompetenceValuePerDay = 0.1f;
            competenceComponent.Settings = settings;
            

            //competenceComponent.Initialize();

            isAssetpackCreated = true;
        }

    }

    // Use this for initialization
    void Start ()
    {
        //in here: we fill the screen with content based on the current competence to learn/test

        //load current game situation
        gsName = CompetenceComponent.Instance.GetCompetenceRecommendation();



        string message = "Now the game presents a competence which needs to be trained/tested next. ";
        message += "In our case it is the competence <b><i>'" + gsName + "'</i></b>. Based on the player's performance there is evidence for (success) or against (failure) ";
        message += "the possession of this competence. Since this is a simple simulation the gameplay is simulated as a button click.";
        

        text.text = message;


        //display evaluation state
        string txt = "";
        if (debugOutput)
        {
            txt += "Competence values:\nid_value_timestamp\n\n";
            CompetenceAssessmentObject cao = CompetenceComponent.Instance.getAssessmentObject();
            foreach (AssessmentCompetence ac in cao.competences)
            {
                txt += ac.id + "_" + (Mathf.Round(100.0f * ac.value) / 100.0f).ToString() + "_" + ac.timestamp + "\n";
            }
            evaluationState.fontSize = 10;
        }
        else
        {
            txt += "Competences\nid: level\n\n";
            Dictionary<string, int> competenceLevels = CompetenceComponent.Instance.getCompetenceLevels();
            foreach (string key in competenceLevels.Keys)
            {
                txt += key + ": " + competenceLevels[key].ToString() + "/"+ (maxLevel-1).ToString() + "\n";
            }
            evaluationState.fontSize = 20;
        }
        evaluationState.text = txt;
    }
    

    private void LoadEndScene()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void UpdateCompetenceState(bool success)
    {
        //in here: the update of the current gamesituation is done based on the user input
        CompetenceComponent.Instance.Update(gsName,success);
        LoadNextGamesituation();
    }

    public void ResetEvaluationState()
    {
        CompetenceComponent.Instance.ResetCompetenceState();
        LoadNextGamesituation();
    }

    private void LoadNextGamesituation()
    {
        //Here the same Scene is loaded again and again
        //of course one could load different scenes - one for each gamesituation
        SceneManager.LoadScene("GamesituationLoadingScene");
    }
}

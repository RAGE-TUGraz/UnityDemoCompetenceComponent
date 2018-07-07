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
            settings.SourceFile = "dataModel2.xml";
            settings.LinearDecreasionOfCompetenceValuePerDay = 0.1f;
            settings.CompetencePauseTimeInSeconds = 5;
            settings.Phase = CompetenceComponentPhase.DEFAULT;
            settings.ThreasholdRecommendationSelection = 1f / (60f * 60f * 60f); // very low, nearly zero
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
        gsName = CompetenceComponent.Instance.GetGamesituationRecommendation();
        string message="";
        if (gsName != null)
        {
            message += "Now the game presents a game situation which needs to be trained/tested next. ";
            message += "In our case it is the game situation <b><i>'" + gsName + "'</i></b>. Based on the player's performance there is evidence for (success) or against (failure) ";
            message += "the mastery of the game situation. Since this is a simple simulation the gameplay is simulated as a button click.";
        }
        else
        {
            message += "Right now there is no game situation which can be played - the player should pause the game. Click success or failure to request new game situation.";
        }



        

        text.text = message;


        //display evaluation state
        string txt = "";
        if (debugOutput)
        {
            txt += "Competence values:\nid_valueA_timestampA\nid_valueL_timestampL\n\n";
            CompetenceAssessmentObject cao = CompetenceComponent.Instance.getAssessmentObject();
            foreach (AssessmentCompetence ac in cao.competences)
            {
                txt += ac.id + "_" + (Mathf.Round(100.0f * ac.valueAssessment) / 100.0f).ToString() + "_" + ac.timestampAssessment + "\n";
                txt += ac.id + "_" + (Mathf.Round(100.0f * ac.valueLearning) / 100.0f).ToString() + "_" + ac.timestampLearning + "\n";
            }
            evaluationState.fontSize = 10;
        }
        else
        {
            txt += "Competences\nid: level\n\n";
            Dictionary<string, int[]> competenceLevels = CompetenceComponent.Instance.getCompetenceLevels();
            foreach (string key in competenceLevels.Keys)
            {
                txt += key + ": A" + competenceLevels[key][0].ToString() + " L" + competenceLevels[key][1].ToString() + "/" + (maxLevel-1).ToString() + "\n";
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
        if(gsName!= null)
            CompetenceComponent.Instance.UpdateGamesituation(gsName,success);
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

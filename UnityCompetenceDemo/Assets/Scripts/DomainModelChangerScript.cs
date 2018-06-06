using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssetManagerPackage;
using CompetenceAssessmentAssetNameSpace;
using CompetenceBasedAdaptionAssetNameSpace;
using DomainModelAssetNameSpace;
using UnityEngine.SceneManagement;

public class DomainModelChangerScript : MonoBehaviour {

	public void ChangeDomainModelAndLoadNewGameSituation()
    {
        string newDataSource = ((DomainModelAssetSettings)DomainModelAsset.Instance.Settings).Source.Equals("DomainModel.xml") ? "DomainModel2.xml" : "DomainModel.xml";

        Debug.Log("change DM!");
        DomainModelAssetSettings dmas = new DomainModelAssetSettings();
        dmas.LocalSource = true;
        dmas.Source = newDataSource;
        DomainModelAsset.Instance.Settings = dmas;
        
        CompetenceBasedAdaptionAsset.Instance.resetDataSource();

        SceneManager.LoadScene("GamesituationLoadingScene");
    }

    public static void SetDomainModel(string domainModelName)
    {
        /*
        //Domain Model Asset
        DomainModelAssetSettings dmas = new DomainModelAssetSettings();
        dmas.LocalSource = true;
        dmas.Source = domainModelName;
        DomainModelAsset.Instance.Settings = dmas;

        //Competence Assement Asset
        CompetenceAssessmentAssetSettings caas = new CompetenceAssessmentAssetSettings();
        caas.TransitionProbability = 0.9;
        CompetenceAssessmentAsset.Instance.Settings = caas;
        */
    }
}

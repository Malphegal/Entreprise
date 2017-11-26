using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleFood : Item {

    /* Correspond au type de l'aliment : nourriture ou boisson */
    public enum FoodType
    {
        Food,
        Drink
    }

        // FIELDS

    public string       foodName;       // Nom de l'aliment
    public FoodType     foodType;       // Type de l'aliment

    [Space]
    [Header("Processus pourrissement")]
    public byte         rottingValue;   // La valeur de l'état de pourrissement TODO : changer le public en private à la fin des tests
    public byte         nbOfSeconds;    // Le nombre de secondes qu'il faut à l'aliment pour perdre un point
                                        // de fraîcheur (rottingValue--)

    [Space]
    [Header("Régénération faim et soif")]
    public ushort       hungerRegen;    // Valeur de régénération de la faim par tick
    public ushort       thirstRegen;    // Valeur de régénération de la soif par tick
    public byte         nbOfTicks;      // Nombre de ticks nécessaire à la consommation entière de l'aliment


        // METHODS

    void Awake ()
    {
        rottingValue = 100;
    }

	void Start () {
        StartCoroutine(RottingProcess());
	}

    // TODO: Ajouter une animation de mort de l'objet
    /* Méthode appelée par la coroutine qui se lance à la création de l'objet,
     * mettant à jour l'état de pourrissement de l'aliment jusqu'à sa décomposition
     * totale qui cause sa disparition. */
    private IEnumerator RottingProcess()
    {
        while (rottingValue > 0)
        {
            yield return new WaitForSeconds(nbOfSeconds);
            rottingValue--;
        }
    }

    /* Méthode renvoyant l'état de pourrissement de l'aliment */
    private string GetRottingState()
    {
        if (rottingValue > 89)
            return "Fresh"; // Frais
        else if (rottingValue > 49)
            return "Good"; // bon
        else if (rottingValue > 19)
            return "Stale"; // rassis
        else
            return "Rotten"; // Pourri
    }
}

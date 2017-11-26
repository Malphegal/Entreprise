using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdibleFood : MonoBehaviour {

    public enum FoodType
    {
        Food,
        Drink
    }

        // FIELDS

    public string   nameOfFood;           // Nom de l'aliment
    public FoodType foodType;       // Type de l'aliment
    public ushort   hungerRegen;    // Valeur de régénération de la faim par tick
    public ushort   thirstRegen;    // Valeur de régénération de la soif par tick
    public byte     nbOfTicks;      // Nombre de ticks nécessaire à la consommation entière de l'aliment

        // METHODS

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

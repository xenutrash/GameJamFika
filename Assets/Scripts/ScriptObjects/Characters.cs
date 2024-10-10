using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterList", menuName = "ScriptableObjects/CharacterList", order = 2)]
public class Characters : ScriptableObject
{
    
    public List<CharacterContainer> characterContainers = new();
    public GameObject defaultCharacter; 

    public GameObject GetPrefab(string Name)
    {
        foreach (var CharContainer in characterContainers)
        {
            if (CharContainer.NameOfCharacter == Name)
                return CharContainer.CharacterPrefab;
        }

        Debug.Log("Could not find a character with the name " + Name); 
        return defaultCharacter; 
    }
   
}

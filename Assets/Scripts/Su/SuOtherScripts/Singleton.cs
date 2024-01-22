using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: SU MYAT MYAT HTAY
// Date  		: 2023-05-10
// Description	: Tempalte for a generic use Singleton class
//              : From PluralSight Sword and Shovels Series that is no longer available
//---------------------------------------------------------------------------------

/// <summary>
/// Class that creates a Singleton
/// requires other class to inherit Singleton for it to work
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    #region Variables
    private static T instance;
    #endregion

    #region Getter

    public static T Instance
    {
        get { return instance; }
    }
    #endregion

    #region Unity Methods
    /// <summary>
    /// Called when script is loaded. Can be overidden
    /// </summary>
    protected virtual void Awake()
    {
        //to ensure there is only 1 instance;
        //assign current instance to the instance variable;
        //else if instance is null 
        //else if instance is null 
        if (instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a second instance of Singleton class");
            Destroy(this);
        }
        else
        {
            instance = (T)this;
        }
    }

    /// <summary>
    /// Called when script is destroyed. Can be overidden
    /// </summary>
    protected virtual void OnDestroy()
    {
        // check if destroyed object is this
        if (instance == this)
        {
            instance = null;
        }
    }
    #endregion
}
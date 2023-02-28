using Unity.Entities;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class CustomSystemGroup : ComponentSystemGroup
{
    private bool _isInitialized;
        
    protected override void OnCreate()
    {
        base.OnCreate();
        // set the flag to false, not true.
        _isInitialized = false;
    }

    protected override void OnUpdate()
    {
        if (!_isInitialized)
        {
            // check once
            if (SceneManager.GetActiveScene().isLoaded)
            {
                SubScene subScene = Object.FindObjectOfType<SubScene>();
                
                // condition to enable the system group
                // if true, the system group will be execute when OnUpdate() is called
                Enabled = subScene is not null 
                          && SubSceneName == subScene.gameObject.scene.name;

                _isInitialized = true;
            }
        }

        base.OnUpdate();
    }

    // wrap the name of the SystemGroup by SubSceneName
    // so that the system group will be enabled only when the scene is loaded
    protected abstract string SubSceneName { get; }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
public static class ResourceLoader
{
    public static void Load<T>(string addresableLabel, Dictionary<int, T> resourceDictionary, Action onComplete, Func<IResource, T> valueSelector)
    {
        Addressables.LoadAssetsAsync<IResource>(addresableLabel, resource =>
        {
            if (!resourceDictionary.ContainsKey(resource.ID))
            {
                resourceDictionary.Add(resource.ID, valueSelector(resource));
                Debug.Log($"Resource loaded; ID: {resource.ID} Name: {resource.Name}");
            }
            else
            {
                Debug.LogError($"Duplicate ID detected: {resource.ID} for resource {resource.Name}");
            }
        }).Completed += handle => 
        { 
            OnLoadCompleted(handle, resourceDictionary, addresableLabel);
            onComplete?.Invoke();
        };
    }
    private static void OnLoadCompleted<T>(AsyncOperationHandle<IList<IResource>> handle, Dictionary<int, T> resourceDictionary, string resourceType)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"Successfully loaded {resourceDictionary.Count} of {resourceType}.");
        }
    }
}

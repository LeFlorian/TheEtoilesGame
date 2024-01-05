using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(EventsCreator))]
public class EventsCreatorEditor : Editor
{
    public static class FindExtension
    {
        public static List<T> Find<T>()
        {
            List<T> interfaces = new List<T>();
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>();
                foreach (var childInterface in childrenInterfaces)
                {
                    interfaces.Add(childInterface);
                }
            }
            return interfaces;
        }

        public static IEnumerable<T> GetEnumerableOfType<T>(params object[] constructorArgs) where T : class
        {
            List<T> objects = new List<T>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                objects.Add((T)Activator.CreateInstance(type, constructorArgs));
            }
            //  objects.Sort();
            return objects;
        }

        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return a;

        }

    }
    [SerializeField]
    Color m_Color = Color.white;

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Add Command Event", GUILayout.Height(35)))
        {
            // create the menu and add items to it
            GenericMenu menu = new GenericMenu();

            var eventCommands = FindExtension.GetEnumerableOfType<EventCommand>();

            foreach (var eventCommand in eventCommands)
            {
                EventCommandInfo commandInfo = GetCommandInfo(eventCommand.GetType());

                if (commandInfo == null)
                    continue;

                if (commandInfo.Category == string.Empty)
                    AddMenuItemEventCommand(menu, commandInfo.CommandName, eventCommand);
                else
                    AddMenuItemEventCommand(menu, commandInfo.Category + "/" + commandInfo.CommandName, eventCommand);

            }


            // display the menu
            menu.ShowAsContext();
        }
    }
    void AddMenuItemEventCommand(GenericMenu menu, string menuPath, EventCommand eventCommand)
    {
        // the menu item is marked as selected if it matches the current value of m_Color
        menu.AddItem(new GUIContent(menuPath), false, OnEventCommandSelected, eventCommand);
    }

    // the GenericMenu.MenuFunction2 event handler for when a menu item is selected
    void OnEventCommandSelected(object command)
    {
        EventsCreator targetComponent = (EventsCreator)target;
        EventCommand eventCommand = (EventCommand)command;

        targetComponent.gameObject.AddComponent(eventCommand.GetType());
    }

    public static EventCommandInfo GetCommandInfo(System.Type commandType)
    {
        EventCommandInfo retval = null;

        object[] attributes = commandType.GetCustomAttributes(typeof(EventCommandInfo), false);
        foreach (object obj in attributes)
        {
            EventCommandInfo commandInfoAttr = obj as EventCommandInfo;
            if (commandInfoAttr != null)
            {
                if (retval == null)
                    retval = commandInfoAttr;
            }
        }

        return retval;
    }
}

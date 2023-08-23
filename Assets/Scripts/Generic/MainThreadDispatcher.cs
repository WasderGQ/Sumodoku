using System;
using System.Collections.Generic;
using UnityEngine;
using WasderGQ.Utility.Singleton;

namespace WasderGQ.Sudoku
{
    public class MainThreadDispatcher : MonoBehaviour
    {
        
            private static MainThreadDispatcher Instance;

            private Queue<Action> actionQueue = new Queue<Action>();

            private static bool applicationIsQuitting = false;

            private void Awake()
            {
                if (Instance != null)
                {
                    Destroy(this.gameObject);
                    return;
                }

                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

            private void OnApplicationQuit()
            {
                applicationIsQuitting = true;
            }

            private void Update()
            {
                if (applicationIsQuitting)
                {
                    return;
                }

                lock (actionQueue)
                {
                    while (actionQueue.Count > 0)
                    {
                        Action action = actionQueue.Dequeue();
                        action.Invoke();
                    }
                }
            }

            public static void RunOnMainThread(Action action)
            {
                if (Instance == null)
                {
                    Debug.LogError("MainThreadDispatcher's instance not found");
                    return;
                }

                lock (Instance.actionQueue)
                {
                    Instance.actionQueue.Enqueue(action);
                }
            }
        }
    }
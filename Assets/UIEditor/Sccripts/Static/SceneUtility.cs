using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace GameUtil 
{
    public class SceneUtility
    {
        /// <summary>
        /// 检查指定场景是否打开
        /// </summary>
        /// <param name="scenePath">路径</param>
        /// <returns></returns>
        public static bool SceneIsOpened(string scenePath) 
        {
            //获取当前激活的场景
            Scene activeScene = EditorSceneManager.GetActiveScene();
            //返回不具有扩展名的
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            return activeScene.name.Equals(sceneName);
        }

        /// <summary>
        /// 打开指定场景
        /// </summary>
        /// <param name="scenePath">路径</param>
        public static void OpenScene(string scenePath) 
        {
            //指定路径的场景没有打开才执行后续代码
            if (!SceneIsOpened(scenePath)) 
            {
                if (!EditorApplication.isPlaying)
                {
                    EditorSceneManager.OpenScene(scenePath);
                }
                else 
                {
                    //打开提示弹窗
                    if (EditorUtility.DisplayDialog("警告", "是否需要在运行时打开场景", "是", "否"))
                    {
                        //当点击是，执行运行时加载场景
                        SceneManager.LoadScene(scenePath);
                    }
                    else 
                    {
                        EditorApplication.isPlaying = false;
                        void Load(PlayModeStateChange state) 
                        {
                            if (!EditorApplication.isPlaying) 
                            {
                                EditorSceneManager.OpenScene(scenePath);
                                EditorApplication.playModeStateChanged -= Load;
                            }
                        }

                        EditorApplication.delayCall = () => 
                        {
                            if (!EditorApplication.isPlaying)
                                EditorSceneManager.OpenScene(scenePath);
                            else
                                EditorApplication.playModeStateChanged += Load;
                        };
                    }
                }
            }
        }
    }
}
using System.Collections.Generic;
using Code.Assistant;
using UnityEditor;
using UnityEngine;

namespace Code.LevelCreator
{
    [CustomEditor(typeof(LevelsConfig))]
    [CanEditMultipleObjects]
    public class LevelsConfigEditor : UnityEditor.Editor
    {
        #if UNITY_EDITOR
        private DetailsBaseConfig _detailsBase;
        private HashSet<string> _checkList;

        private string _nameObject;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _localScale;

        public override void OnInspectorGUI()
        {
            LevelsConfig levelsConfig = (LevelsConfig) target;

            _detailsBase =
                EditorGUILayout.ObjectField("DetailBase", _detailsBase, typeof(DetailsBaseConfig), true) as
                    DetailsBaseConfig;
            levelsConfig.DetailsBase = _detailsBase;

            var createButton = GUILayout.Button("Create new level");

            if (levelsConfig.CreatingDetails.Count > 0)
            {
                for (int i = 0; i < levelsConfig.CreatingDetails.Count; i++)
                {
                    EditorGUILayout.TextField("NameLevelObject", levelsConfig.CreatingDetails[i].Key);
                    EditorGUILayout.Vector3Field("Position", levelsConfig.CreatingDetails[i].Position);
                    EditorGUILayout.Vector3Field("Rotation", levelsConfig.CreatingDetails[i].Rotation.eulerAngles);
                    EditorGUILayout.Vector3Field("Size", levelsConfig.CreatingDetails[i].Position);
                }
                EditorUtility.SetDirty(levelsConfig);
            }
            else
            {
                levelsConfig.CreatingDetails = new List<DetailCreatingInfo>();
            }

            if (createButton)
            {
                levelsConfig.DetailsBase = _detailsBase;
                levelsConfig.CreatingDetails = new List<DetailCreatingInfo>();
                GetKeyList();

                var objectList = FindObjectsOfType<GameObject>();

                for (int i = 0; i < objectList.Length; i++)
                {
                    if (_checkList.Contains(objectList[i].name))
                    {
                        Debug.Log(objectList[i].name);
                        _nameObject = objectList[i].name;
                        _position = objectList[i].transform.position;
                        _rotation = objectList[i].transform.rotation;
                        _localScale = objectList[i].transform.localScale;
                        DetailCreatingInfo detailCreatingInfo =
                            new DetailCreatingInfo(_nameObject, _position, _rotation, _localScale);
                        levelsConfig.CreatingDetails.Add(detailCreatingInfo);
                    }
                }

                EditorUtility.SetDirty(levelsConfig.DetailsBase);
            }
        }

        private void GetBase()
        {
            if (_detailsBase == null)
            {
                _detailsBase = HelperExtentions.Load<DetailsBaseConfig>("ActiveObjects/DetailBaseConfig");
            }
        }

        private void GetKeyList()
        {
            GetBase();

            _checkList = new HashSet<string>();
            for (int i = 0; i < _detailsBase.DetailBase.Count; i++)
            {
                _checkList.Add(_detailsBase.DetailBase[i].name);
            }
        }
        #endif
    }
}

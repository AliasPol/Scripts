using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace BlocksSave {
    [CustomEditor(typeof(BlockSaver))]

    public class BlocksSaverEditor : Editor {

        public List<BlockSave> bloksSavePost;


        public override void OnInspectorGUI() {
            BlockSaver block = (BlockSaver)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Save All Blocks")) {
                block.SaveAllChildren();
            }

            if (GUILayout.Button("Save All Positions")) {
                Debug.LogError("WORK");
                block.SaveAllPositions();
            }
            
            if (GUILayout.Button("Set All Positions")) {
                block.SetAllPositions();
            }

        }

        void Save(List<BlockSave> list) {
            bloksSavePost = new List<BlockSave>();
            bloksSavePost = list;
        }

        
    }
}

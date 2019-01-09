using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace BlocksSave {
    public class BlockSave {
        public Vector3 position;
        public Quaternion rotation;
    };



    public class BlockSaver : MonoBehaviour {

        public List<GameObject> allBlocks;
        public List<BlockSave> allBlocksTransform;


        

        public void SaveAllChildren() {

            allBlocks = new List<GameObject>();

            foreach (Transform n in gameObject.transform) {
                allBlocks.Add(n.gameObject);
            }
        }

        public void SaveAllPositions() {
            /*
            allBlocksTransform = new List<BlockSave>();
            
            foreach (GameObject n in allBlocks) {

                BlockSave bS = new BlockSave();
                bS.position = n.transform.position;
                bS.rotation = n.transform.rotation;

                allBlocksTransform.Add(bS);
            }*/
            
            for(int i = 0; i < allBlocks.Count; i++) {
                BlockSave bs = new BlockSave();
                bs.position = allBlocks[i].transform.position;
                bs.rotation = allBlocks[i].transform.rotation;

                Save(bs, i);
            }
        }

        public void SetAllPositions() {

            for (int i = 0; i < allBlocks.Count; i++) {
                Read(i);
            }
        }
        /*
        private void Save(int index) {
            PlayerPrefs.SetFloat(index + "x", allBlocksTransform[index].position.x);
            PlayerPrefs.SetFloat(index + "y", allBlocksTransform[index].position.y);
            PlayerPrefs.SetFloat(index + "z", allBlocksTransform[index].position.z);

            Quaternion rot = allBlocksTransform[index].rotation;

            PlayerPrefs.SetFloat(index + "qx", rot.x);
            PlayerPrefs.SetFloat(index + "qy", rot.y);
            PlayerPrefs.SetFloat(index + "qz", rot.z);
            PlayerPrefs.SetFloat(index + "qw", rot.w);

            Debug.LogWarning("Saved rotation: " + rot + "   ");
        }*/

        private void Save(BlockSave bs, int index) {

            PlayerPrefs.SetFloat(index + "x", bs.position.x);
            PlayerPrefs.SetFloat(index + "y", bs.position.y);
            PlayerPrefs.SetFloat(index + "z", bs.position.z);

            Quaternion rot = bs.rotation;

            PlayerPrefs.SetFloat(index + "qx", bs.rotation.x);
            PlayerPrefs.SetFloat(index + "qy", bs.rotation.y);
            PlayerPrefs.SetFloat(index + "qz", bs.rotation.z);
            PlayerPrefs.SetFloat(index + "qw", bs.rotation.w);

            Debug.LogError(bs.rotation);
        }

        private void Read(int index) {
            Vector3 post = Vector3.zero;
            post.x = PlayerPrefs.GetFloat(index + "x", 0);
            post.y = PlayerPrefs.GetFloat(index + "y", 0);
            post.z = PlayerPrefs.GetFloat(index + "z", 0);

            allBlocks[index].transform.position = post;

            Quaternion rot = allBlocks[index].transform.rotation;
            rot.x = PlayerPrefs.GetFloat(index + "qx", 0);
            rot.y = PlayerPrefs.GetFloat(index + "qy", 0);
            rot.z = PlayerPrefs.GetFloat(index + "qz", 0);
            rot.w = PlayerPrefs.GetFloat(index + "qw", 0);

            Debug.Log("Saved rotation " + rot);
            allBlocks[index].transform.rotation = rot;
        }
        /*
        private void OnDisable() {
            if (allBlocksTransform != null) {
                for (int i = 0; i < allBlocksTransform.Count; i++) {
                    Save(i);
                }
            }
        }*/
    }

    
}
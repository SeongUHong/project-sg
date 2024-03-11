using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmallShips
{
    public class ExplosionController : MonoBehaviour {

        [Tooltip("Child game objects that should be destroyed during explosion. For that 'DestroyPart(x)' will be called from animation clip.")]
        public GameObject[] removeParts;


        Animator animator;
        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void DestroyPart(int index)
        {
            if (removeParts != null && index >= 0 && index < removeParts.Length)
                Destroy(removeParts[index]);
            else
                Debug.LogWarning("Index is out of range in DestroPart. index: " + index);
        }

        /// <summary>
        /// Call this function from animation clip in the last frame to remove GameObject.
        /// </summary>
        public void DestroyObject()
        {
            Destroy(gameObject);
        }

       
        
    }

}

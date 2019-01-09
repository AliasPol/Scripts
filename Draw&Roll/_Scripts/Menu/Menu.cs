using UnityEngine;
using System.Collections;

namespace Game.Menu
{
    [RequireComponent(typeof(Animator))]

    public class Menu : MonoBehaviour
    {
        private Animator _animator;

        
        void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }


        public bool IsOpen
        {
            get { return _animator.GetBool("IsOpen"); }
            set { _animator.SetBool("IsOpen", value); }
        }
    }
}
using System;
using System.Collections.Generic;
using Assets.FantasyMonsters.Scripts.Tweens;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.FantasyMonsters.Scripts
{
    /// <summary>
    /// The main script to control monsters.
    /// </summary>
    public class Monster : MonoBehaviour
    {
        public GameObject player;
        public SpriteRenderer Head;
        public List<Sprite> HeadSprites;
        public Animator Animator;
        public bool Variations;
        public event Action<string> OnEvent = eventName => { };
        public bool hittedInThisAttack = false;
        

        // monster information
        /// <summary>
        /// Called on Awake.
        /// </summary>
        public void Awake()
        {
            if (Variations)
            {
                var variations = GetComponents<MonsterVariation>();
                var random = UnityEngine.Random.Range(0, variations.Length + 1);

                if (random > 0)
                {
                    variations[random - 1].Apply();
                }
            }

            //GetComponent<LayerManager>().SetSortingGroupOrder((int) -transform.localPosition.y);
        }

        private void Start()
        {
            
        }
        private void Update()
        {
            if(this.gameObject.transform.position.y > player.transform.position.y)
            {
                GetComponent<SortingGroup>().sortingOrder = 0;
            } 
            if(this.gameObject.transform.position.y < player.transform.position.y)
            {
                GetComponent<SortingGroup>().sortingOrder = 10;    
            }

           
        }

        /// <summary>
        /// Set animation parameter State to control transitions. Play different state animations (except Attack).
        /// </summary>
        public void SetState(MonsterState state)
        {
            Animator.SetInteger("State", (int) state);
        }

        /// <summary>
        /// Play Attack animation.
        /// </summary>
        public void Attack()
        {
            Animator.SetTrigger("Attack");
        }

        /// <summary>
        /// Play scale spring animation.
        /// </summary>
        public virtual void Spring()
        {
            ScaleSpring.Begin(this, 1f, 1.1f, 40, 2);
        }

        // Play Die animation.
        public void Die()
        {
            SetState(MonsterState.Death);
        }

        /// <summary>
        /// Called from animation. Can be used by the game to handle animation events.
        /// </summary>
        public void Event(string eventName)
        {
            OnEvent(eventName);
        }

        /// <summary>
        /// Called from animation.
        /// </summary>
        public void SetHead(int index)
        {
            if (index < HeadSprites.Count)
            {
                Head.sprite = HeadSprites[index];
            }
        }
    }
}
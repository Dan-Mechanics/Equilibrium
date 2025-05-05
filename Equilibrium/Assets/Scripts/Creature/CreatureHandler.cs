using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OuterWilds
{
    /// <summary>
    /// https://i.pinimg.com/736x/f5/c7/51/f5c75186d5c9e47032fdac3e6c7e3964.jpg
    /// 
    /// 
    /// This class is allowed to have a direct reference to creature since it is the higher up
    /// and the creature is not allowed to know this class, only talking through abstractions and events etc etc.
    /// 
    /// I think the plan here is to just make this slay and then extract some object pool behaviour out of it.
    /// 
    /// This code is a little bit overengineerd i think.
    /// i think i can make some conrete interfaces like IClaimCallback and IDieCallback or something
    /// </summary>
    public class CreatureHandler : MonoBehaviour, IDieCallback, IClaimCallback, IUpdatable, IWritable<float>
    {
        //public event Action OnLoseRound;
        
        [SerializeField] private TerrainData terrainData = default;
        [SerializeField] private CreatureData creatureData = default;
        [SerializeField] private Spawner spawner = default;
        [SerializeField] private InspectorInterface<IReadable<Vector3[]>> terrainReader = default;
        [SerializeField] private List<InspectorInterface<IPassable<int[]>>> factionsTallyListeners = default;
        [SerializeField] private UnityEvent onLoseRound = default;

        private FixedTicks fixedTicks;
        private readonly List<Creature> creatures = new List<Creature>();
        private int[] factionsTally;
        private bool hasChangedThisFrame;
        
        private void Awake()
        {
            terrainReader.Setup();
            factionsTallyListeners.ForEach(x => x.Setup());
            factionsTally = new int[creatureData.factionsCount];
        }

        private void Start() => SendTally();

        public void DoUpdate() 
        {
            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                DoFixedFrame();
            }
        }

        private void DoFixedFrame()
        {
            hasChangedThisFrame = false;
            
            // this is more performant and also now we can add pause functionality.
            foreach (var creature in creatures)
            {
                if (!creature.gameObject.activeSelf)
                    continue;

                creature.ProcessFixedFrame();
            }

            // THIS IS AN IMPORTANT EVETN AND SHOULD BE HANDLED IN THE STATE MACHINE !!
            if (hasChangedThisFrame)
            {
                SendTally();

                for (int i = 0; i < factionsTally.Length; i++)
                {
                    if (factionsTally[i] <= 0)
                    {
                        onLoseRound?.Invoke();
                        print("L ...");
                        return;
                    }
                }
            }
        }

        [ContextMenu(nameof(Respawn))]
        public void Respawn() 
        {
            // We don't have anthing spawned yet !!
            if (creatures.Count <= 0)
                SpawnNewCreatures();

            for (int i = 0; i < factionsTally.Length; i++)
            {
                factionsTally[i] = 0;
            }

            Vector3[] verts = terrainReader.attached.Data;
            creatures.ForEach(x => ResetCreature(x, ref verts));

            SendTally();
        }

        [ContextMenu(nameof(Stop))]
        public void Stop()
        {
            creatures.ForEach(x => x.gameObject.SetActive(false));

            for (int i = 0; i < factionsTally.Length; i++)
            {
                factionsTally[i] = 0;
            }

            SendTally();
        }

        private void SendTally()
        {
            //OnNewFactionsTally?.Invoke(factionsTally);
            factionsTallyListeners.ForEach(x => x.attached.Pass(ref factionsTally));
        }

        private void SpawnNewCreatures()
        {
            for (int i = 0; i < creatureData.creatureSpawnCount; i++)
            {
                Creature creature = spawner.SpawnSingle(Vector3.zero).GetComponent<Creature>();
                creature.Setup(this, this);
                creatures.Add(creature);
            }
        }

        private void ResetCreature(Creature creature, ref Vector3[] verts)
        {
            creature.transform.position = Utils.GetRandomVertexWorldSpace(ref verts, terrainData) + spawner.Data.spawnOffset;
            TallyFaction(creature.ResetCreature(), 1); // use the int here.
        }

        public void DieCallback(int faction)
        {
            TallyFaction(faction, -1);
            //Debug.LogWarning("A BITCH DIED !!");
        }

        public void ClaimCallback(int faction)
        {
            int previousIndex = Utils.WrapIndex(faction, 1, creatureData.factionsCount);

            TallyFaction(faction, 1);
            TallyFaction(previousIndex, -1);
        }

        private void TallyFaction(int index, int direction) 
        {
            factionsTally[index] += direction;
            factionsTally[index] = Mathf.Clamp(factionsTally[index], 0, creatureData.creatureSpawnCount);

            hasChangedThisFrame = true;
        }

        public void Write(float timeScale)
        {
            fixedTicks = new FixedTicks(creatureData.processInterval * timeScale);
        }
    }
}
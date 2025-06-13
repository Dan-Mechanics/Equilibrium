using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Equilibrium
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
    public class CreatureHandler : MonoBehaviour, ICreatureCallbacks, IUpdatable, IWritable<float>, IWritable<ITerrainable>, IWritable<BaseTerrain>
    {
        [SerializeField] private CreatureSettings settings = default;
        [SerializeField] private SpawnData spawnData = default;
        [SerializeField] private InspectorInterface<IGetter<Vector3[]>> terrainReader = default;
        [SerializeField] private InspectorInterface<ISpawnable> spawner = default;  
        [SerializeField] private List<InspectorInterface<IWritable<int[]>>> factionsTallyListeners = default;

        private CreatureBehaviour[] creatures;
        private FixedTicks fixedTicks;
        private int[] factionsTally;
        private bool hasChangedThisFrame;
        private ITerrainable terrainable;
        private BaseTerrain baseTerrain;

        private void Awake()
        {
            creatures = new CreatureBehaviour[settings.creatureSpawnCount];
            terrainReader.Setup();
            factionsTallyListeners.ForEach(x => x.Setup());
            factionsTally = new int[settings.factionsCount];
            spawner.Setup();
        }

        private void Start() 
        {
            SendTally();
            UnityEngine.Random.InitState(0);
        }

        /// <summary>
        /// This must be like this because of the different processinterval than
        /// usual fixedupdaterate.
        /// </summary>
        public void Write(float timeScale) => fixedTicks = new FixedTicks(settings.processInterval * timeScale);
        public void Write(BaseTerrain baseTerrain) => this.baseTerrain = baseTerrain;
        public void Write(ITerrainable terrainable) => this.terrainable = terrainable;

        public void DoUpdate() 
        {
            for (int i = 0; i < fixedTicks.GetTicksCount(Time.deltaTime); i++)
            {
                Tick();
            }
        }

        private void Tick()
        {
            hasChangedThisFrame = false;

            for (int i = 0; i < creatures.Length; i++)
            {
                if (!creatures[i].gameObject.activeSelf)
                    continue;

                creatures[i].ProcessFixedFrame();
            }

            // THIS IS AN IMPORTANT EVETN AND SHOULD BE HANDLED IN THE STATE MACHINE !!
            if (!hasChangedThisFrame)
                return;

            SendTally();

            for (int i = 0; i < factionsTally.Length; i++)
            {
                if (factionsTally[i] > 0)
                    continue;

                EventManager.RaiseEvent(EventManager.EventType.ROUND_LOSE);
                return;
            }
        }

        //[ContextMenu(nameof(Respawn))]
        public void Respawn() 
        {
            // We don't have anthing spawned yet !!
            if (creatures[0] == null)
                SpawnNewCreatures();

            for (int i = 0; i < factionsTally.Length; i++)
            {
                factionsTally[i] = 0;
            }

            Vector3[] verts = terrainReader.attached.Get();
            for (int i = 0; i < creatures.Length; i++)
            {
                ResetCreature(creatures[i], verts);
            }

            SendTally();
        }

        public void Stop()
        {
            if (creatures == null || creatures.Length <= 0 || creatures[0] == null)
                return;

            for (int i = 0; i < creatures.Length; i++)
            {
                creatures[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < factionsTally.Length; i++)
            {
                factionsTally[i] = 0;
            }

            SendTally();
        }

        private void SendTally()
        {
            factionsTallyListeners.ForEach(x => x.attached.Write(factionsTally));
        }

        private void SpawnNewCreatures()
        {
            settings.foundColliders = new Collider[settings.creatureSearchBufferSize];

            for (int i = 0; i < creatures.Length; i++)
            {
                CreatureBehaviour newCreature = spawner.attached.SpawnSingle(spawnData).GetComponent<CreatureBehaviour>();
                newCreature.Setup(this);
                creatures[i] = newCreature;
            }
        }

        /// <summary>
        /// I dont really think ref is required here.
        /// </summary>
        private void ResetCreature(CreatureBehaviour creature, Vector3[] verts)
        {
            creature.transform.position = Utils.GetRandomVertexWorldSpace(verts, terrainable) + spawnData.spawnOffset;

            TallyFaction(creature.ResetCreature(baseTerrain.speedMod), 1); // use the int here.
        }

        public void DieCallback(int faction) => TallyFaction(faction, -1);

        public void ClaimCallback(int faction)
        {
            int previousIndex = Utils.WrapIndex(faction, 1, settings.factionsCount);

            TallyFaction(faction, 1);
            TallyFaction(previousIndex, -1);
        }

        private void TallyFaction(int index, int direction) 
        {
            factionsTally[index] += direction;
            factionsTally[index] = Mathf.Clamp(factionsTally[index], 0, settings.creatureSpawnCount);

            hasChangedThisFrame = true;
        }
    }
}
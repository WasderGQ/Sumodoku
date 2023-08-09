using UnityEngine;

namespace WasderGQ.Sudoku.Scenes.GameScene.Game
{
    public class Parsel : MonoBehaviour
    {
        [SerializeField] private Zone[] _zonesInParsel;
        [SerializeField] private int _myparselID;


        public Zone[] ZonesInParsel { get => _zonesInParsel;  }
        public void init() //Init Parsel
        {
            initMyZones();
            GiveMyIDToZones();
        }
    


        private void initMyZones()
        {
            foreach (var zone in ZonesInParsel)
            {
                zone.init();
            }

        } //Init Zones Inside This Parsel
    
    
    
        public void GiveMyIDToZones()
        {
            int counter = 0;
            foreach (var zone in ZonesInParsel)
            {
                int[] zoneParselID = new[] { _myparselID, counter };
                zone.SetParselID(zoneParselID);
                counter++;
            }
        } //Parsel giving ID to zone

        public void ParselRemovePossibleValueListOnZone(int value)
        {
            foreach (var zone in ZonesInParsel)
            {
                zone.RemoveValueFromPossibleValues(value);
            }
        }
        
    }
}

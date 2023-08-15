using UnityEngine;
using WasderGQ.Sudoku.Enums;

namespace WasderGQ.Sudoku.Scenes.GameScene.GameElement.Boards
{
    public class Boardx9 : Board
    {
        [SerializeField] public readonly GameBoards GameBoards = GameBoards.x9;

        public override void InIt()
        {
            ConvertParselZonesToZones();//
            base.ParselsInIt(); //parsels in it
            base.SetZonesID();  //set zones id
            base.StartMapCreater(); //start map creater
            base.MakeZonesDefault(); //start with null value zone
            base.SelectZoneFromBoard(0, 20); // show selected zone's value
        }


        protected override void ConvertParselZonesToZones()
        {
            try
            {
                _zones = new Zone[9, 9]

                {
                    {
                        _parsels[0].ZonesInParsel[0], _parsels[0].ZonesInParsel[1], _parsels[0].ZonesInParsel[2], /***/
                        _parsels[1].ZonesInParsel[0], _parsels[1].ZonesInParsel[1], _parsels[1].ZonesInParsel[2], /***/
                        _parsels[2].ZonesInParsel[0], _parsels[2].ZonesInParsel[1], _parsels[2].ZonesInParsel[2]
                    },
                    {
                        _parsels[0].ZonesInParsel[3], _parsels[0].ZonesInParsel[4], _parsels[0].ZonesInParsel[5], /***/
                        _parsels[1].ZonesInParsel[3], _parsels[1].ZonesInParsel[4], _parsels[1].ZonesInParsel[5], /***/
                        _parsels[2].ZonesInParsel[3], _parsels[2].ZonesInParsel[4], _parsels[2].ZonesInParsel[5]
                    },
                    {
                        _parsels[0].ZonesInParsel[6], _parsels[0].ZonesInParsel[7], _parsels[0].ZonesInParsel[8], /***/
                        _parsels[1].ZonesInParsel[6], _parsels[1].ZonesInParsel[7], _parsels[1].ZonesInParsel[8], /***/
                        _parsels[2].ZonesInParsel[6], _parsels[2].ZonesInParsel[7], _parsels[2].ZonesInParsel[8]
                    },
                    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    {
                        _parsels[3].ZonesInParsel[0], _parsels[3].ZonesInParsel[1], _parsels[3].ZonesInParsel[2], /***/
                        _parsels[4].ZonesInParsel[0], _parsels[4].ZonesInParsel[1], _parsels[4].ZonesInParsel[2], /***/
                        _parsels[5].ZonesInParsel[0], _parsels[5].ZonesInParsel[1], _parsels[5].ZonesInParsel[2]
                    },
                    {
                        _parsels[3].ZonesInParsel[3], _parsels[3].ZonesInParsel[4], _parsels[3].ZonesInParsel[5], /***/
                        _parsels[4].ZonesInParsel[3], _parsels[4].ZonesInParsel[4], _parsels[4].ZonesInParsel[5], /***/
                        _parsels[5].ZonesInParsel[3], _parsels[5].ZonesInParsel[4], _parsels[5].ZonesInParsel[5]
                    },
                    {
                        _parsels[3].ZonesInParsel[6], _parsels[3].ZonesInParsel[7], _parsels[3].ZonesInParsel[8], /***/
                        _parsels[4].ZonesInParsel[6], _parsels[4].ZonesInParsel[7], _parsels[4].ZonesInParsel[8], /***/
                        _parsels[5].ZonesInParsel[6], _parsels[5].ZonesInParsel[7], _parsels[5].ZonesInParsel[8]
                    },
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    {
                        _parsels[6].ZonesInParsel[0], _parsels[6].ZonesInParsel[1], _parsels[6].ZonesInParsel[2], /***/
                        _parsels[7].ZonesInParsel[0], _parsels[7].ZonesInParsel[1], _parsels[7].ZonesInParsel[2], /***/
                        _parsels[8].ZonesInParsel[0], _parsels[8].ZonesInParsel[1], _parsels[8].ZonesInParsel[2]
                    },
                    {
                        _parsels[6].ZonesInParsel[3], _parsels[6].ZonesInParsel[4], _parsels[6].ZonesInParsel[5], /***/
                        _parsels[7].ZonesInParsel[3], _parsels[7].ZonesInParsel[4], _parsels[7].ZonesInParsel[5], /***/
                        _parsels[8].ZonesInParsel[3], _parsels[8].ZonesInParsel[4], _parsels[8].ZonesInParsel[5]
                    },
                    {
                        _parsels[6].ZonesInParsel[6], _parsels[6].ZonesInParsel[7], _parsels[6].ZonesInParsel[8], /***/
                        _parsels[7].ZonesInParsel[6], _parsels[7].ZonesInParsel[7], _parsels[7].ZonesInParsel[8], /***/
                        _parsels[8].ZonesInParsel[6], _parsels[8].ZonesInParsel[7], _parsels[8].ZonesInParsel[8]
                    },
                };
                Debug.Log("All Zones saved to Board");
            }
            catch
            {
                Debug.LogError("All zones CAN'T saved to Board");

            }
        }



    }
}


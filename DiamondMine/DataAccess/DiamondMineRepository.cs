
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MyMicroservice.Models;
using MyMicroservice.Data;
using MyMicroservice.DataAccess;
using Hxgn.Min.Platform.Libraries;

namespace Mine.DataAccess
{
    public class DiamondMineRepository : IMineRepository
    {
        private string filePath = "./diamondMineData.json";
        private MyMicroservice.Models.DiamondMine mineState;
        public DiamondMineRepository()
        {
            Load();
            if (mineState == null)
            {
                mineState = new MyMicroservice.Models.DiamondMine();
                Save();
            }
        }

        public MyMicroservice.Models.DiamondMine GetMineState()
        {
            Load();
            return mineState;
        }

        public ExcavationResult Excavate()
        {
            ExcavationResult result = mineState.Excavate();
            Save();
            return result;
        }

        public BuyResult BuyEquipment(string equipmentName)
        {
            BuyResult buyResult = mineState.BuyEquipment(equipmentName);
            Save();
            return buyResult;
        }

        public void Load()
        {
            mineState = Helpers.Serializer.DeserializeJsonFromFile<MyMicroservice.Models.DiamondMine>(filePath);
        }

        public void Save() {
            Helpers.Serializer.SerializeJsonToFile(filePath, mineState, true);
        }
    }
}
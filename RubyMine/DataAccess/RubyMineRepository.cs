
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MyMicroservice.Models;
using MyMicroservice.Data;
using MyMicroservice.DataAccess;
using Hxgn.Min.Platform.Libraries;

namespace Mine.DataAccess
{
    public class RubyMineRepository : IMineRepository
    {
        private string filePath = "rubyMineData.json";
        private MyMicroservice.Models.RubyMine mineState;
        public RubyMineRepository()
        {
            mineState = Helpers.Serializer.DeserializeJsonFromFile<MyMicroservice.Models.RubyMine>(filePath);
            if (mineState == null)
            {
                mineState = new MyMicroservice.Models.RubyMine();
            }
        }

        public MyMicroservice.Models.RubyMine GetMineState()
        {
            return mineState;
        }

        public ExcavationResult Excavate()
        {
            ExcavationResult result = mineState.Excavate();
            Save();
            return result;
        }

        public void Save() {
            Helpers.Serializer.SerializeJsonToFile(filePath, mineState, true);
        }
    }
}
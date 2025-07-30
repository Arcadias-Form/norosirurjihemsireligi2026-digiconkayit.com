using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KatilimciTablosuIslemler : KatilimciTablosuIslemlerBase
	{
		public KatilimciTablosuIslemler() : base() { }

		public KatilimciTablosuIslemler(OleDbTransaction tran) : base (tran) { }

        public string YeniKatilimciID()
        {
            string KatilimciID;

            do
            {
                KatilimciID = Guid.NewGuid().ToString();
                VTIslem.SetCommandText("SELECT COUNT(*) FROM KatilimciTablosu WHERE KatilimciID = @KatilimciID");
                VTIslem.AddWithValue("KatilimciID", KatilimciID);
            } while (!Convert.ToInt32(VTIslem.ExecuteScalar()).Equals(0));

            return KatilimciID;
        }
    }
}

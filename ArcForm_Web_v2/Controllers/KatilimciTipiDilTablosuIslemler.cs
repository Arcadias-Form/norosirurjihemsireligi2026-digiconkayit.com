using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KatilimciTipiDilTablosuIslemler : KatilimciTipiDilTablosuIslemlerBase
	{
		public KatilimciTipiDilTablosuIslemler() : base() { }

		public KatilimciTipiDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class OdemeTipiDilTablosuIslemler : OdemeTipiDilTablosuIslemlerBase
	{
		public OdemeTipiDilTablosuIslemler() : base() { }

		public OdemeTipiDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

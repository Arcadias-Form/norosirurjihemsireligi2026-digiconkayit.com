using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KursTipiDilTablosuIslemler : KursTipiDilTablosuIslemlerBase
	{
		public KursTipiDilTablosuIslemler() : base() { }

		public KursTipiDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

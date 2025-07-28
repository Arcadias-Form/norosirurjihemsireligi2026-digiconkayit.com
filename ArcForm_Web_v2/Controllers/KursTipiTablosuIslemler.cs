using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KursTipiTablosuIslemler : KursTipiTablosuIslemlerBase
	{
		public KursTipiTablosuIslemler() : base() { }

		public KursTipiTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

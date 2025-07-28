using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class OdaTipiTablosuIslemler : OdaTipiTablosuIslemlerBase
	{
		public OdaTipiTablosuIslemler() : base() { }

		public OdaTipiTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

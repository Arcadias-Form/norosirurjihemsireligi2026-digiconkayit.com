using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class OdaTipiDilTablosuIslemler : OdaTipiDilTablosuIslemlerBase
	{
		public OdaTipiDilTablosuIslemler() : base() { }

		public OdaTipiDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

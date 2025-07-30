using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class OtelTablosuIslemler : OtelTablosuIslemlerBase
	{
		public OtelTablosuIslemler() : base() { }

		public OtelTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

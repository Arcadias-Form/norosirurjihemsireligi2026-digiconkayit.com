using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class KonaklamaTablosuIslemler : KonaklamaTablosuIslemlerBase
	{
		public KonaklamaTablosuIslemler() : base() { }

		public KonaklamaTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

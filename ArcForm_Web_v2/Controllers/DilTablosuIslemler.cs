using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class DilTablosuIslemler : DilTablosuIslemlerBase
	{
		public DilTablosuIslemler() : base() { }

		public DilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

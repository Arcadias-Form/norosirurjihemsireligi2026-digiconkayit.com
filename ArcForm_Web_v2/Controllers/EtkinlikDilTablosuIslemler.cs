using Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using VeritabaniIslemMerkeziBase;

namespace VeritabaniIslemMerkezi
{
	public partial class EtkinlikDilTablosuIslemler : EtkinlikDilTablosuIslemlerBase
	{
		public EtkinlikDilTablosuIslemler() : base() { }

		public EtkinlikDilTablosuIslemler(OleDbTransaction tran) : base (tran) { }
	}
}

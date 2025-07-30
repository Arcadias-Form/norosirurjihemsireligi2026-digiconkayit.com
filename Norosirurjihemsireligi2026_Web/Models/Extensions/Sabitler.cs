using System;

namespace Model
{
    public sealed class Sabitler
    {
        public enum Donemler
        {
            CokErkenDonem,
            ErkenDonem,
            NormalDonem
        };

        public static DateTime CokErkenTarih { get { return new DateTime(2026, 2, 15, 23, 59, 59); } }

        public static DateTime ErkenTarih { get { return new DateTime(2026, 3, 15, 23, 59, 59); } }

        public static Donemler Donem
        {
            get
            {
                if (DateTime.Now < CokErkenTarih)
                    return Donemler.CokErkenDonem;
                else if (DateTime.Now < ErkenTarih)
                    return Donemler.ErkenDonem;
                else
                    return Donemler.NormalDonem;
            }
        }

        public static string KurSimgesi = "€";
    }
}
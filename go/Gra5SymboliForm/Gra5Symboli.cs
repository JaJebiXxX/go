using System.Drawing;

namespace Gra5SymboliForm
{
    public enum PSymbol { Nic, Kółko, Krzyżyk };

    public class Gra5Symboli
    {
        private PSymbol[,] plansza = new PSymbol[19, 19];
        private PSymbol ktoTeraz = PSymbol.Krzyżyk;

        public PSymbol postawSymbol(int col, int wie)
        {
            if (plansza[col, wie] == PSymbol.Nic)
            {
                plansza[col, wie] = ktoTeraz;
                ktoTeraz = (ktoTeraz == PSymbol.Krzyżyk) ? PSymbol.Kółko : PSymbol.Krzyżyk;
                return plansza[col, wie];
            }
            return PSymbol.Nic;
        }

        public PSymbol ktoWygrał()
        {
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (plansza[i, j] != PSymbol.Nic && sprawdzWygrana(i, j))
                    {
                        return plansza[i, j];
                    }
                }
            }
            return PSymbol.Nic;
        }

        private bool sprawdzWygrana(int col, int wie)
        {
            PSymbol symbol = plansza[col, wie];
            if (symbol == PSymbol.Nic) return false;

            if (col <= 14 && symbol == plansza[col + 1, wie] && symbol == plansza[col + 2, wie] &&
                symbol == plansza[col + 3, wie] && symbol == plansza[col + 4, wie])
                return true;

            if (wie <= 14 && symbol == plansza[col, wie + 1] && symbol == plansza[col, wie + 2] &&
                symbol == plansza[col, wie + 3] && symbol == plansza[col, wie + 4])
                return true;

            if (col <= 14 && wie <= 14 && symbol == plansza[col + 1, wie + 1] &&
                symbol == plansza[col + 2, wie + 2] && symbol == plansza[col + 3, wie + 3] &&
                symbol == plansza[col + 4, wie + 4])
                return true;

            if (col >= 4 && wie <= 14 && symbol == plansza[col - 1, wie + 1] &&
                symbol == plansza[col - 2, wie + 2] && symbol == plansza[col - 3, wie + 3] &&
                symbol == plansza[col - 4, wie + 4])
                return true;

            return false;
        }

        public void reset()
        {
            plansza = new PSymbol[19, 19];
            ktoTeraz = PSymbol.Krzyżyk;
        }

        public PSymbol jakiJestTuSymbol(int col, int wie)
        {
            return plansza[col, wie];
        }
    }
}

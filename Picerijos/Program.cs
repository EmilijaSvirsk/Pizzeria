using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;


namespace Ataskaita
{
    class Kelias
    {
        private int pirmas;
        private int antras;
        private bool eitas;
        private bool sankryza;

        private int nueitaskelias;
        int veda;

        public Kelias(int pirmas, int antras)
        {
            this.pirmas = pirmas;
            this.antras = antras;
            eitas = false;
            sankryza = false;
            nueitaskelias = 0;
            veda = 0;
        }

        public int Pirmas() { return pirmas; }
        public int Antras() { return antras; }
        public bool Eitas() { return eitas; }
        public bool Sankryza() { return sankryza; }

        public void Pereitas()
        {
            eitas = true;
        }
        public void NueitasKelias(int i)
        {
            nueitaskelias = i;
        }
        public int KiekNueita()
        {
            return nueitaskelias;
        }
        public void YraSankryza()
        {
            sankryza = true;
        }
        public void Ieita(int i)
        {
            if (i == pirmas)
                veda = antras;
            else 
                veda = pirmas;
        }

        public int Veda()
        {
            return veda;
        }
    }

    class KeliuMazgas
    {
        const int didelis = 100;
        private Kelias[] k;
        private int n;

        public KeliuMazgas()
        {
            n = 0;
            k = new Kelias[didelis];
        }
        public Kelias Imti(int i)
        {
            return k[i];
        }

        public int Imti()
        {
            return n;
        }

        public void Deti(Kelias kel)
        {
            k[n++] = kel;
        }

        public void Trinti()
        {
            n = 0;
        }
    }

    class Langelis
    {
        private string simbolis;
        private int numeris;

        public Langelis(string simbolis, int numeris)
        {
            this.simbolis = simbolis;
            this.numeris = numeris;
        }
        public string Simbolis() { return simbolis; }
        public int Numeris() { return numeris; }
    }

    class Zemelapis
    {
        const int didelis = 100;
        private Langelis[,] l;
        private int n;
        private int x;
        private int y;

        public Zemelapis()
        {
            n = 0;
            l = new Langelis[didelis, didelis];
        }
        public Langelis Imti(int x, int y)
        {
            return l[x, y];
        }
        public void XY(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int Imti()
        {
            return n;
        }
        public void Deti(Langelis lang, int x, int y)
        {
            l[x, y] = lang;
            n++;
        }
        public Langelis ImtiNumeri(int numeris)
        {
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    if (l[j, i].Numeris() == numeris)
                        return l[j, i];
            return null;
        }

        public int RastiX(Langelis lang)
        {
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    if (l[j, i].Numeris() == lang.Numeris())
                        return j;
            return -1;
        }

        public int RastiY(Langelis lang)
        {
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    if (l[j, i].Numeris() == lang.Numeris())
                        return i;
            return -1;
        }

        public int X()
        {
            return x;
        }
        public int Y()
        {
            return y;
        }
    }





    class Program
    {
        static void Skaityti(Zemelapis z, string txt)
        {
            using (StreamReader reader = new StreamReader(txt))
            {
                string line;
                string[] parts;
                string simbolis;
                int numeris = 0;

                int x = 0;
                int y = 0;

                line = reader.ReadLine();
                parts = line.Split(' ');
                x = int.Parse(parts[0]);
                y = int.Parse(parts[1]);

                for (int i = 0; i < y; i++)
                {
                    line = reader.ReadLine();

                    for (int j = 0; j < x; j++)
                    {
                        simbolis = line[j].ToString();
                        Langelis l = new Langelis(simbolis, numeris);
                        z.Deti(l, j, i);
                        numeris++;
                    }
                }
                z.XY(x, y);
            }
        }

        static void Draugai(Zemelapis z)
        {
            for (int i = 0; i < z.Imti(); i++)
            {
                Langelis draugas = z.ImtiNumeri(i);
                if (draugas.Simbolis() == "D")
                {
                    Console.WriteLine("{0} {1}",
                    z.RastiX(draugas) + 1,
                    z.Y() - z.RastiY(draugas));
                }
            }
        }

        static void KeliuSudarymas(Zemelapis z, KeliuMazgas m, int priekis, int galas)
        {
            Langelis pirmas;
            Langelis antras;
            m.Trinti();

            for (int i = 0; i < z.Y(); i++)
            {
                for (int j = 0; j < z.X(); j++)
                {
                    pirmas = z.Imti(j, i);
                    if (pirmas.Simbolis() != "X"
                        && ((pirmas.Simbolis() != "P" || pirmas.Numeris() == galas)
                        || (pirmas.Simbolis() != "P" || pirmas.Numeris() == priekis)))
                    {
                        if (j - 1 >= 0)
                        {
                            antras = z.Imti(j - 1, i);
                            if (antras.Simbolis() != "X"
                                && ((antras.Simbolis() != "P" || antras.Numeris() == galas)
                                || (antras.Simbolis() != "P" || antras.Numeris() == priekis)))
                            {
                                Kelias kel = new Kelias(pirmas.Numeris(), antras.Numeris());
                                m.Deti(kel);
                            }
                        }
                        if (i - 1 >= 0)
                        {
                            antras = z.Imti(j, i - 1);
                            if (antras.Simbolis() != "X"
                                && ((antras.Simbolis() != "P" || antras.Numeris() == galas)
                                || (antras.Simbolis() != "P" || antras.Numeris() == priekis)))
                            {
                                Kelias kel = new Kelias(pirmas.Numeris(), antras.Numeris());
                                m.Deti(kel);
                            }
                        }
                    }
                }
            }
        }

        static int Ejimas(Zemelapis z, KeliuMazgas m, int priekis, int galas)
        {
            KeliuSudarymas(z, m, priekis, galas);
            Kelias kelias;

            int k = 0; //kiek nueita kelio
            bool pasiekta = false; //pazymeti kada nutraukti cikla
            int maz = int.MaxValue;
            bool neimanoma = false;//jei neimanoma

            while (pasiekta == false && neimanoma == false)
            {
                maz = int.MaxValue;
                Kelias maziausias = m.Imti(0);//cia kitaip neveikia
                bool yrasankryza = false;

                for (int i = 0; i < m.Imti(); i++)//tikrinam trumpiausia sankryza
                {
                    kelias = m.Imti(i);//cia siaip pasirenkam
                    if (kelias.Sankryza() == true && kelias.Eitas() == false)//randam sankryza
                    {
                        if (maz > kelias.KiekNueita())//palyginam ar maziausia
                        {
                            maz = kelias.KiekNueita();//pastatom maziausia
                            maziausias = kelias;
                            yrasankryza = true;//Yra sankryza!
                        }
                    }
                }

                if (yrasankryza == true)
                {
                    maziausias.Pereitas();//pazymime kad sita pasirinkome ir perejome

                    k = maz;//nauja kelio suma  
                    yrasankryza = false;

                    priekis = maziausias.Veda();//pasiziurime kur toliau veda kelias
                }

                for (int i = 0; i < m.Imti(); i++)//vel ieskome galimu sankryzu
                {
                    kelias = m.Imti(i);//cia siaip pasirenkam

                    if ((kelias.Pirmas() == priekis && kelias.Eitas() == false)
                        || (kelias.Antras() == priekis && kelias.Eitas() == false))
                    //paziurim ar pereinamas kelias
                    {
                        kelias.YraSankryza();
                        kelias.NueitasKelias(k + 1);
                        kelias.Ieita(priekis);
                    }
                }

                pasiekta = true;
                for (int i = 0; i < m.Imti(); i++)//tikrinam ar dar yra like nepereitu keliu
                    if (m.Imti(i).Eitas() == false)
                        pasiekta = false;

                if (pasiekta == false)
                {
                    neimanoma = true;
                    for (int i = 0; i < m.Imti(); i++)//tikrinam ar yra sankryzu, jei nera vadinasi nepereinama
                        if (m.Imti(i).Sankryza() == true && m.Imti(i).Eitas() == false)
                            neimanoma = false;
                }
            }

            if (neimanoma == true)
                return -1;

            maz = int.MaxValue;
            for (int i = 0; i < m.Imti(); i++)
            {
                kelias = m.Imti(i);
                if ((kelias.Pirmas() == galas || kelias.Antras() == galas)
                    && kelias.KiekNueita() < maz)
                {
                    maz = kelias.KiekNueita();
                }
            }
            return maz;
        }


        static void SusitikimoVieta(Zemelapis z, KeliuMazgas m)
        {
            int suma = 0;
            int maz = int.MaxValue;
            int mpicerija = -1;
            int msusitikimas = -1;
            for (int i = 0; i < z.Imti(); i++)
            {
                Langelis susitikimas = z.ImtiNumeri(i);
                if (susitikimas.Simbolis() == "S")//surandam galima susitikimo vieta
                {
                    for (int j = 0; j < z.Imti(); j++)//skaiciuojame suma nuo draugu
                    {
                        Langelis draugas = z.ImtiNumeri(j);
                        if (draugas.Simbolis() == "D")
                        {
                            if (Ejimas(z, m, susitikimas.Numeris(), draugas.Numeris()) == -1)
                            {
                                Console.WriteLine("Neimanoma");
                                return;
                            }
                            suma = suma + Ejimas(z, m, susitikimas.Numeris(), draugas.Numeris());
                        }
                    }

                    for (int j = 0; j < z.Imti(); j++)//skaiciuosim iki picerijos ir tada nuo picerijos iki draugu
                    {
                        Langelis picerija = z.ImtiNumeri(j);
                        if (picerija.Simbolis() == "P")
                        {
                            int ssuma = suma;
                            if (Ejimas(z, m, susitikimas.Numeris(), picerija.Numeris()) == -1)
                            {
                                Console.WriteLine("Neimanoma");
                                return;
                            }
                            ssuma = ssuma + Ejimas(z, m, susitikimas.Numeris(), picerija.Numeris());//nuo picerijos iki susitikimo vietos

                            for (int k = 0; k < z.Imti(); k++)//skaiciuojame suma nuo draugu iki picerijos
                            {
                                Langelis draugas = z.ImtiNumeri(k);
                                if (draugas.Simbolis() == "D")
                                {
                                    if (Ejimas(z, m, picerija.Numeris(), draugas.Numeris()) == -1)
                                    {
                                        Console.WriteLine("Neimanoma");
                                        return;
                                    }
                                    ssuma = ssuma + Ejimas(z, m, picerija.Numeris(), draugas.Numeris());

                                }
                            }

                            if (maz >= ssuma)//jeigu randam mazesni
                            {
                                mpicerija = picerija.Numeris();
                                msusitikimas = susitikimas.Numeris();
                                maz = ssuma;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Susitikimo vieta {0} {1}",
                 z.RastiX(z.ImtiNumeri(msusitikimas)) + 1,
                 z.Y() - z.RastiY(z.ImtiNumeri(msusitikimas)));
            Console.WriteLine("Picerija {0} {1}",
                 z.RastiX(z.ImtiNumeri(mpicerija)) + 1,
                 z.Y() - z.RastiY(z.ImtiNumeri(mpicerija)));
            Console.WriteLine("Nueita {0}", maz);
        }

        static string txt = "Ataskaita.txt";

        static void Main(string[] args)
        {
            Zemelapis z = new Zemelapis();
            KeliuMazgas m = new KeliuMazgas();

            Skaityti(z, txt);
            Draugai(z);
            SusitikimoVieta(z, m);
        }
    }
}
/*
D...P
XX...
D.S.P
XX...
D.S..
*/
/*
P.X.P
..XS.
S.X..
..X.D
D.X..
*/

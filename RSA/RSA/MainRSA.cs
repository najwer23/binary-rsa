using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// RSA 

// 0) Operacje matematyczne

// dodawanie liczb binarnych, dodatnich i ujemnych +
// odejmowanie liczb binarnych, dodatnich i ujemnych +
// mnożenie liczby binarnych dodatnich i ujemnych +
// dzielenie liczb binarnych dodatnich i ujemnych +
// operacje modulo na liczbach binarnych +
// NWD na liczbach binarnych +
// odwrotność modularna na liczbach binarnych +
// test pseudo pierwszości -> Millera-Rabina +
// szybkie potęgowanie +
// porownywanie liczb binarnych +
// rozszerzony algorytm euklidesa +

// 1) Generowanie kluczy +

// P - liczba pierwsza
// Q - liczba pierwsza 
// P i Q należy zniszczyć

// N = P*Q
// Fi(N) = (P-1)(Q-1) 

// e i N - klucz publiczny
// e względnie pierwsza z Fi(N)

// d i N - klucz prywatny
// d = e^(-1) mod Fi(N); odwrotność modularna

// 2) Szyfrowanie +

// c = m^e % N

// 3) Deszyfrowanie +

// m = c^d % N

namespace RSA
{
    //ustawic dlugosc bitow
    //ustawic dlugosc klucza
    //ustawic dlugosc liczb dla testu millera
    //liczba testow dla millera
    class MainRSA
    {

        static void Main(string[] args)
        {
            


            int k = 0;
            bool isPrime = true;
            double test = 0;
            double wynik = 0;

            NumberBin Number = new NumberBin();
            int Size = 15;

            NumberBin[] Primes = new NumberBin[2];
            Primes[0] = NumberBin.RandomBin(Size);
            Primes[1] = NumberBin.RandomBin(Size);

           

            int it = 1;
            int koniec = 5;

            for (int j = 0; j < Primes.Length; j++)
            {
                k = 0;
                it += j;
                test = 0;
                wynik = 0;
                Console.WriteLine("Szuakm Liczby pierwszej.. " + it + "/2");
                while (k < koniec)
                {

                    // test Millera Rabina
                    if (isPrime == NumberBin.MR(Primes[j]))
                    {
                        test++;
                    }

                    k++;

                    if (k == koniec - 1)
                    {
                        wynik = test / koniec;
                        if (wynik < 0.4)
                        {

                            Console.WriteLine("Nie znalazlem liczby pierwszej. Szukam innej..");
                            //Console.WriteLine("Prawdopodobienstwo ze Prime "+ Primes[j].TextNumber + " Wynik + " +wynik*100+ " %");
                            k = 0;

                            Primes[j] = NumberBin.RandomBin(Size);
                            test = 0;
                            wynik = 0;

                        }
                    }

                    if (NumberBin.Equal(Primes[0], Primes[1]))
                    {
                        Primes[1] = NumberBin.RandomBin(Size);
                    }
                }
                wynik = Math.Round(wynik * 100, 3);
                Console.WriteLine("Prime number: " + j + " -> " + Primes[j].TextNumber + " -- " + wynik + " %");
            }


            // losuj liczbe pierwsza P i Q
            NumberBin P = new NumberBin();
            P = Primes[0];
            //P.TextNumber = "011110001011";
            Console.WriteLine("\r\nP    : " + P.TextNumber);

            // liczba Q
            NumberBin Q = new NumberBin();
            Q = Primes[1];
            //Q.TextNumber = "011111011011";
            Console.WriteLine("\r\nQ    : " + Q.TextNumber);

            // liczba 1
            NumberBin One = new NumberBin();
            One.TextNumber = "01";

            // liczba N
            NumberBin N = new NumberBin();
            N = NumberBin.Mul2Binary(P, Q);
            Console.WriteLine("\r\nN    : " + N.TextNumber);

            // liczba P-1
            NumberBin P_1 = new NumberBin();
            P_1 = NumberBin.Sub2Binary(P, One);
            Console.WriteLine("\r\n(P-1): " + P_1.TextNumber);

            // liczba Q-1
            NumberBin Q_1 = new NumberBin();
            Q_1 = NumberBin.Sub2Binary(Q, One);
            Console.WriteLine("\r\n(Q-1): " + Q_1.TextNumber);

            // Phi(N) 
            NumberBin Phi = new NumberBin();
            Phi = NumberBin.Mul2Binary(P_1, Q_1);
            Console.WriteLine("\r\nPhi  : " + Phi.TextNumber);

            /////////////////////////////////////////////
            // Liczba względnie pierwsza z Phi
            NumberBin E = new NumberBin();
            NumberBin Test = new NumberBin();
            Test.TextNumber = "011";
            for (int i = 0; i < 100000; i++)
            {

                E = NumberBin.NWD(Test, Phi);

                if ((E.TextNumber.Length == 1) && (E.TextNumber == "1"))
                {
                    E.TextNumber = Test.TextNumber;
                    break;
                }
                Test = NumberBin.Add2Binary(Test, One);
            }

            // Klucz 1 - klucz prywatny
            Console.WriteLine("\r\nKlucz 1.[E,N] = [" + E.TextNumber + "," + N.TextNumber + "]");

            // Odwrotnosc modularna
            // Klucz 2 - klucz publiczny
            NumberBin D = new NumberBin();
            D = NumberBin.EEA(E, Phi);
            Console.WriteLine("\r\nKlucz 2 [D,N] = [" + D.TextNumber + "," + N.TextNumber + "]");

















            ///bonus
            NumberBin C = new NumberBin();
            NumberBin M = new NumberBin();
            NumberBin Result = new NumberBin();
            NumberBin R= new NumberBin();
            BinaryDivideClass Div = new BinaryDivideClass();
            int L = 0;


            // wiadomosc
            // wiadomosc, musi byc krotsza niz N i musi istniec liczba bitow na szybkie potegowanie              
            M.TextNumber = P.TextNumber;
            Console.WriteLine("\r\nWiadomosc");
            Console.WriteLine(M.TextNumber);



            //szyfrowanie
            //c = m ^ e % N
            R = M;
            Result.TextNumber = "01";
            L = E.TextNumber.Length;
            for (int m = L - 1; m >= 0; m--)
            {
                if (E.TextNumber[m] == '1')
                {
                    Result = NumberBin.Mul2Binary(Result, R);
                    Div = BinaryDivideClass.Div2Binary(Result, N);
                    Result.TextNumber = Div.Remainder;
                }
                R= NumberBin.Mul2Binary(R, R);
                Div = BinaryDivideClass.Div2Binary(R, N);
                R.TextNumber = Div.Remainder;
            }

            Div = BinaryDivideClass.Div2Binary(Result, N);
            Result.TextNumber = Div.Remainder;
            C = Result;
            Console.WriteLine("\r\nWiadomosc zaszyfrowana");
            Console.WriteLine(C.TextNumber);

            
            
            //deszyfrowanie
            //m = c ^ d % N
            R.TextNumber= C.TextNumber;
            Result.TextNumber = "01";
            L = D.TextNumber.Length;

            int lol = 0;
            for (int m = L-1; m>=0; m--)
            {
                if (D.TextNumber[m] == '1')
                    lol = m;
            }

            D.TextNumber = D.TextNumber.Substring(lol-1);
            L = D.TextNumber.Length;
            for (int m = L - 1; m >= 0; m--)
            {
                if (D.TextNumber[m] == '1')
                {
                    Result = NumberBin.Mul2Binary(Result, R);
                    Div = BinaryDivideClass.Div2Binary(Result, N);
                    Result.TextNumber = Div.Remainder;

                }
                R = NumberBin.Mul2Binary(R, R);
                Div = BinaryDivideClass.Div2Binary(R, N);
                R.TextNumber = Div.Remainder;
            }

            Div = BinaryDivideClass.Div2Binary(Result, N);
            Result.TextNumber = Div.Remainder;
            M.TextNumber = Result.TextNumber;

            Console.WriteLine("\r\nWiadomosc odszyfrowana");
            Console.WriteLine(M.TextNumber);



            Console.WriteLine("\r\nKoniec");
            Console.ReadKey();
  
        }
    }
}

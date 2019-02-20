using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class BinaryDivideClass : NumberBin
    {
        public string Quotient {get; set; }
        public string Remainder { get; set; }




        public static BinaryDivideClass Div2Binary(NumberBin Bin1, NumberBin Bin2)
        {
            NumberBin tempU2 = new NumberBin();
            NumberBin X = new NumberBin();
            NumberBin Y = new NumberBin();
            NumberBin R = new NumberBin();
            BinaryDivideClass Div = new BinaryDivideClass();

            X.TextNumber = Bin1.TextNumber;
            Y.TextNumber = Bin2.TextNumber;


            int CharX = 0;
            int CharY = 0;
            int distance = Bin1.Distance;


            int sizeX = distance - X.TextNumber.Length;
            for (int i = 0; i < sizeX; i++)
            {
                X.TextNumber = "0" + X.TextNumber;
            }

            int sizeY = distance - Y.TextNumber.Length;
            for (int i = 0; i < sizeY; i++)
            {
                Y.TextNumber = "0" + Y.TextNumber;
            }


            // sprawdz czy ujemna 
            if (X.TextNumber[0] == '1')
            {
                CharX = 1;   
                X = Binary2U2(X);
            }

            if (Y.TextNumber[0] == '1')
            {
                CharY = 1;
                Y = Binary2U2(Y);
            }


            for (int i=0; i<X.TextNumber.Length; i++)
            {
                R.TextNumber = R.TextNumber + X.TextNumber[i];

                // jesli mozliwe dzielenie
                if (Compare2Binary(R, Y))
                {
                    Div.Quotient = Div.Quotient + '1';
                    R = Sub2Binary(R, Y);
                }
                else
                {
                    Div.Quotient = Div.Quotient + '0';
                }
            }

            Div.Remainder = R.TextNumber;

            if ((CharY == 1 && CharX == 0) || (CharY == 0 && CharX == 1))
            {
                tempU2.TextNumber = Div.Quotient;
                tempU2 = Binary2U2(tempU2);
                Div.Quotient = tempU2.TextNumber;
            }

            if ((CharY == 0 && CharX == 1) || (CharY == 1 && CharX == 1))
            {
                tempU2.TextNumber = Div.Remainder;
                tempU2 = Binary2U2(tempU2);
                Div.Remainder = tempU2.TextNumber;
            }

            return Div;
        }
    }




    public class NumberBin
    {
        public string TextNumber { get; set; }
        public int DecNumber { get; set; }
        public int Distance { get; set; }


        public NumberBin()
        {
            TextNumber = "";
            DecNumber = 0;
            Distance = 64; // dlugosc liczby w bitach ////////////////////
        }

        public static NumberBin StringToInt(NumberBin Bin1)
        {
            int dec = Convert.ToInt32(Bin1.TextNumber, 2);
            Bin1.DecNumber = dec;
            return Bin1;
        }


        public static NumberBin RightShift(NumberBin Bin1)
        {
            NumberBin TestBin1 = new NumberBin();
            TestBin1 = Bin1;
            TestBin1.TextNumber = TestBin1.TextNumber.Substring(0, TestBin1.TextNumber.Length-1);
            return TestBin1;
        }

        public static NumberBin RandomBin(int Size)
        {
            NumberBin Num = new NumberBin();
            Num.TextNumber = "01";

            Random rnd = new Random();
            int bit = 0;
            string bitString;

            for (int i=0; i< Size -1; i++)
            {
                bit = rnd.Next(2);
                bitString = bit.ToString();
                Num.TextNumber = Num.TextNumber + bitString;
            }

            Num.TextNumber = Num.TextNumber + "1";

            return Num;
        }


        public static bool MR(NumberBin Bin1)
        {
            NumberBin Test = new NumberBin();
            NumberBin Zero = new NumberBin();
            NumberBin One = new NumberBin();
            NumberBin Two = new NumberBin();
            NumberBin Test_1 = new NumberBin();
            NumberBin CTest_1 = new NumberBin();
            NumberBin R = new NumberBin();
            NumberBin S = new NumberBin();
            NumberBin D = new NumberBin();
            NumberBin Random = new NumberBin();
            NumberBin CRandom = new NumberBin();
            NumberBin Result = new NumberBin();
            Test.TextNumber = Bin1.TextNumber;
            Zero.TextNumber = "0";
            S.TextNumber = "0";
            One.TextNumber = "01";
            Two.TextNumber = "010";

            // odejmij jedynke
            Test_1 = Sub2Binary(Test, One);
            CTest_1 = Sub2Binary(Test, One);
           
            // postac 2^s * d
            BinaryDivideClass Div = new BinaryDivideClass();

            //liczba krokow
            int s = 0;
            int j = 0;
            while (j==0)
            {
                Div = BinaryDivideClass.Div2Binary(Test_1, Two);
                R.TextNumber = Div.Remainder;
                if (Compare2Binary(Zero,R))
                {
                    s++;
                    S = Add2Binary(S, One);
                }
                else
                {
                    j = 1;
                }
                Test_1.TextNumber = Div.Quotient;
            }

            // podziel 2^s, znajdz d
            Div = BinaryDivideClass.Div2Binary(CTest_1, Mul2Binary(S,Two));
            D.TextNumber = Div.Quotient;

            //losuj liczbe 
            Random rnd = new Random();
            int Size = rnd.Next(2, 4);
            Random = RandomBin(Size); 

            CRandom.TextNumber = Random.TextNumber;

            // szybkie potegowanie
            int L = 0;
            for (int i=0; i<s; i++)
            {
                Result.TextNumber = "01";
                L = D.TextNumber.Length;
                for (int k=L-1; k>=0; k--)
                {
                    if (D.TextNumber[k] == '1')
                    {
                        Result = Mul2Binary(Result, Random);
                    }
                    Random = Mul2Binary(Random, Random);
                    Div = BinaryDivideClass.Div2Binary(Random, Test);
                    Random.TextNumber = Div.Remainder;

                }
                
                Div = BinaryDivideClass.Div2Binary(Result, Test);
                Result.TextNumber = Div.Remainder;
                //Console.WriteLine("Result S"+i+"-- " +Result.TextNumber);

                // przypisz losowa liczbe do Random;
                Random.TextNumber = CRandom.TextNumber;
                D = Add2Binary(D, D);

                if (i==0)
                {
                    if (Equal(Result, One))
                    {
                        //prime
                        return true;
                    }

                    if (Equal(Result, CTest_1))
                    {
                        // prime
                        return true;
                    }
                }
                else
                {
                    if (Equal(Result, CTest_1))
                    {
                        // prime
                        return true;
                    }
                }
            }

            return false;
        }



        public static bool Equal(NumberBin Bin1, NumberBin Bin2)
        {
            NumberBin X = new NumberBin();
            NumberBin Y = new NumberBin();
            NumberBin Eq = new NumberBin();

            X.TextNumber = Bin1.TextNumber;
            Y.TextNumber = Bin2.TextNumber;

            Eq = Sub2Binary(Y, X);
            bool equal = true;

            for (int i=Eq.TextNumber.Length-1; i>=0; i--)
            {
                if (Eq.TextNumber[i] == '1')
                {
                    equal = false;
                    break;
                }

            }
            return equal;
        }



        //Extended Euclidean algorithm
        public static NumberBin EEA(NumberBin Bin1, NumberBin Bin2)
        {
            NumberBin Fi = new NumberBin();
            NumberBin E = new NumberBin();
            NumberBin S1 = new NumberBin();
            NumberBin S2 = new NumberBin();
            NumberBin Temp = new NumberBin();
            NumberBin T1 = new NumberBin();
            NumberBin T2 = new NumberBin();
            NumberBin Q = new NumberBin();
            NumberBin R = new NumberBin();
            NumberBin One = new NumberBin();
            NumberBin Return = new NumberBin();
            NumberBin CopyFi = new NumberBin();

            E.TextNumber = Bin1.TextNumber;
            Fi.TextNumber = Bin2.TextNumber;
            CopyFi.TextNumber = Bin2.TextNumber;

            T1.TextNumber = "0";
            T2.TextNumber = "01";

            S1.TextNumber = "01";
            S2.TextNumber = "0";

            One.TextNumber = "01";

            BinaryDivideClass Div = new BinaryDivideClass();

            int i = 0;
            while (i==0)
            {
                Div = BinaryDivideClass.Div2Binary(Fi, E);

                Q.TextNumber = Div.Quotient;
                R.TextNumber = Div.Remainder;

                // skladnik Fi
                Temp = S2;
                S2 = Sub2Binary(S1, Mul2Binary(Q, S2));
                S1 = Temp;

                // skaldnik E
                Temp = T2;
                T2 = Sub2Binary(T1, Mul2Binary(Q, T2));
                T1 = Temp;

                Fi.TextNumber = E.TextNumber;
                E.TextNumber = R.TextNumber;

                if (Compare2Binary(One, R))
                {
                    i = 1;
                    if (T2.TextNumber[0] == '1')
                    {
                       T2 = Add2Binary(T2, CopyFi);
                       Return = T2;
                    }
                    else
                    {
                       Return = T2;
                    }
             
                }
            }

            return Return;
        }


        // pamietac o bicie znaku!
        // https://www.cut-the-knot.org/blue/binary.shtml
        public static NumberBin NWD(NumberBin Bin1, NumberBin Bin2)
        {
            NumberBin Result = new NumberBin();
            NumberBin Tmp = new NumberBin();
            NumberBin One = new NumberBin();
            NumberBin LastShift = new NumberBin();
            NumberBin Fi = new NumberBin();

            Fi.TextNumber = Bin2.TextNumber;
            Tmp.TextNumber = Bin1.TextNumber;

            int TSize = 0;
            int FiSize = 0;

            int itTogether = 0;
            int together2 = 0;

            int j = 1;
            while (j == 1)
            {
                TSize = Tmp.TextNumber.Length;
                FiSize = Fi.TextNumber.Length;

                int stop = 0;
                for (int k = FiSize - 1; k >= 0; k--)
                {
                    if (Fi.TextNumber[k] == '1')
                        stop = k;

                }

                if (stop == FiSize - 1)
                {
                    j = 0;
                    break;
                }

                if (stop == 0)
                {
                    j = 0;
                    break;
                }


                itTogether = 0;
                // parzysta
                if (Tmp.TextNumber[TSize - 1] == '0')
                {
                    Tmp = RightShift(Tmp);
                    itTogether++;
                }

                if (((FiSize >= 1) && Fi.TextNumber[FiSize - 1] == '0'))
                {
                    Fi = RightShift(Fi);
                    itTogether++;
                }

                // jesli obie parzyste
                if (itTogether == 2)
                {
                    together2++;
                }

                TSize = Tmp.TextNumber.Length;
                FiSize = Fi.TextNumber.Length;
               
                // jesli obie nieparzyste
                if ((FiSize >= 1) && (TSize >= 1))
                {
                    if ((Fi.TextNumber[FiSize - 1] == '1'))
                    {
                        if ((Tmp.TextNumber[TSize - 1] == '1'))
                        {
                        
                            if (Compare2Binary(Fi,Tmp))
                            {
                                Fi = Sub2Binary(Fi, Tmp);
                            }
                            else
                            {
                                Tmp = Sub2Binary(Tmp, Fi);
                            }
                        }
                    }

                }

            }

          
            LastShift = Tmp;

            //usun zera na poczatku
            int now = 0;
            for (int i=0; i<LastShift.TextNumber.Length; i++)
            {
                if (LastShift.TextNumber[i] == '1')
                {
                    now = i;
                    break;
                }
            }
            LastShift.TextNumber = LastShift.TextNumber.Substring(now);

            //przesun, zgodnie ze wzorem 2^k*a, gdzie k=together2
            for (int i = 0; i < together2; i++)
            {
                LastShift.TextNumber = LastShift.TextNumber + "0";
            }

            //zwroc NWD
            return LastShift; 
        }



        //pierwsza >= druga -> true
        //druga > pierwszej -> false
        public static bool Compare2Binary(NumberBin Bin1, NumberBin Bin2)
        {
            string x = Bin1.TextNumber;
            string y = Bin2.TextNumber;

            int distance = x.Length - y.Length;

            if (distance > 0)
            {
                for (int i = 0; i < distance; i++)
                {
                    y = "0" + y;
                }
            }

            if (distance < 0)
            {
                for (int i = 0; i < Math.Abs(distance); i++)
                {
                    x = "0" + x;
                }
            }

            int intX = 0;
            int intY = 0;

            bool check = true;
            // check
            for (int i=0; i< x.Length; i++ )
            {
                intX = x[i] - '0';
                intY = y[i] - '0';

                if ( (intX == intY) || (intX == intY) )
                {
                    continue;
                }

                if (intX > intY)
                {
                    check = true;
                    break;
                }
                else
                {
                    check = false;
                    break;
                }
            }

            return check;
        }








        // mnozenie liczb dodatnich  i ujemnych
        // http://cygnus.tele.pw.edu.pl/olek/doc/syko/www/rozdzial3_2_5.html

        public static NumberBin Mul2Binary(NumberBin Bin1, NumberBin Bin2)
        {
            string x = Bin1.TextNumber;
            string y = Bin2.TextNumber;

            int distance = Bin1.Distance;

            for (int i = 0; i < distance - y.Length; i++)
            {
                y = "0" + y;
            }

            for (int i = 0; i < distance - x.Length; i++)
            {
                x = "0" + x;
            }

            // zbedne
            int Size;
            if (x.Length > y.Length)
                Size = x.Length;
            else
                Size = y.Length;

            // ujemne
            if (x[0] == '1')
            {
                for (int i=0; i< Size; i++)
                {
                    x = "1" + x;
                    y = "0" + y;
                }
            }

            if (y[0] == '1')
            {
                for (int i = 0; i < Size; i++)
                {
                    x = "0" + x;
                    y = "1" + y;
                }
            }

            y = StringOperation.RotateString(y);
            x = StringOperation.RotateString(x);

            string firstNumber, secondNumber;

            if (x.Length >= y.Length)
            {
                firstNumber = x;
                secondNumber = y;
            }
            else
            {
                firstNumber = y;
                secondNumber = x;
            }

            string[] MulArray = new string[secondNumber.Length];
            int intFirstNumber, intSecondNumber, mul;
            string tempMul;
            string preZeros = "";

            // mnożenie
            // tablica mnozen 
            for (int i = 0; i < secondNumber.Length; i++)
            {
                tempMul = "";
                for (int j = 0; j < firstNumber.Length; j++)
                {
                    intFirstNumber = firstNumber[j] - '0';
                    intSecondNumber = secondNumber[i] - '0';

                    mul = intFirstNumber * intSecondNumber;
                    tempMul = tempMul + mul.ToString();
                }
                MulArray[i] = preZeros + tempMul;
                preZeros = preZeros + '0';
            }

            // dodawanie wierszy mnozen
            NumberBin Result = new NumberBin();
            NumberBin tempBin1 = new NumberBin();
            NumberBin tempBin2 = new NumberBin();
            

            for (int i = 0; i < MulArray.Length; i++)
            {
               
                tempBin1 = Result;
                tempBin2.TextNumber = StringOperation.RotateString(MulArray[i]);
                tempBin1.Distance = Size * 2;
                Result = Add2Binary(tempBin1, tempBin2);
            }

            Result.TextNumber = Result.TextNumber.Substring(Result.TextNumber.Length - Result.Distance);
            return Result;
        }


    



        // odejnmowanie liczb ujemnych i dodatnich

        public static NumberBin Sub2Binary(NumberBin Bin1, NumberBin Bin2)
        {
            string x = Bin1.TextNumber;
            string y = Bin2.TextNumber;

            int distance = Bin1.Distance;

            int sizeX = distance - x.Length;
            int sizeY = distance - y.Length;

            for (int i = 0; i < sizeX; i++)
            {
                x = "0" + x;
            }

            for (int i = 0; i < sizeY; i++)
            {
                y = "0" + y;
            }

            NumberBin CopyBinY = new NumberBin();
            CopyBinY.TextNumber = y;

            NumberBin Result = new NumberBin();
            Result = Binary2U2(CopyBinY);
            Result = Add2Binary(Bin1, Result);

            return Result;
        }

        //zapis liczby w postaci U2 (liczba przeciwna)
        public static NumberBin Binary2U2 (NumberBin Bin1)
        {
            NumberBin Result = new NumberBin();
            NumberBin TempBin = new NumberBin();
            TempBin = Bin1;

            NumberBin One = new NumberBin();
            One.TextNumber = "01";

            string copy = "";
            for (int i = 0; i < TempBin.TextNumber.Length; i++)
            {
                if (TempBin.TextNumber[i] == '1')
                    copy = copy + "0";
                else
                    copy = copy + "1";
            }
            TempBin.TextNumber = copy;
            Result = Add2Binary(TempBin, One);

            return Result;
        }






        // dodawanie liczb ujemnych i dodatnich
        public static NumberBin Add2Binary(NumberBin Bin1, NumberBin Bin2)
        {
            string x = Bin1.TextNumber;
            string y = Bin2.TextNumber;

            int distance = Bin1.Distance;
            int sizeX = distance - x.Length;
            int sizeY = distance - y.Length;

            for (int i = 0; i < sizeX; i++)
            {
                x = "0" + x;
            }

            for (int i = 0; i < sizeY; i++)
            {
                y = "0" + y;
            }

            y = StringOperation.RotateString(y);
            x = StringOperation.RotateString(x);


            string result = "";
            char carry = '0';

            string charResultBit;
            int intX, intY, intCarry, resultBit;

            for (int i = 0; i < distance; i++)
            {
                intY = y[i] - '0';
                intX = x[i] - '0';
                intCarry = carry - '0';

                resultBit = (intY + intX + intCarry) % 2;
                if (((resultBit == 0) && (intY != intX)) || (intY > 0 && intX > 0))
                    carry = '1';
                else
                    carry = '0';

                charResultBit = Convert.ToString(resultBit);
                result = charResultBit + result;

            }

            NumberBin Result = new NumberBin();
            Result.TextNumber = result;

            if (Result.TextNumber.Length > distance)
            {
                Result.TextNumber = Result.TextNumber.Substring(1);
            }

            if (Result.TextNumber.Length < distance)
            {
                Result.TextNumber = "1" + Result.TextNumber;
            }

            return Result;
        }

    }
}

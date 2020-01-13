using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Graph
{
    public class Components:ICloneable
    {
        public string file_name { get; set; }//имя файла
        public double[] x { get; set; }
        public List<double[]> y { get; set; }
        public double[] A1 { get; set; }
        public double[] A2 { get; set; }
        public double[] A3 { get; set; }
        public string СommentBegin { get; set; }
        public string СommentEnd { get; set; }
        public DateTime DatetimeBegin { get; set; }
        public DateTime DatetimeEnd { get; set; }
        public string LongitudetBegin { get; set; }
        public string WidhtBegin { get; set; }
        public string LongitudetEnd { get; set; }
        public string WidhtEnd { get; set; }
        public double s { get; set; } = 0d;
        public double t { get; set; } = 0d;
        public double Sec { get; set; } = 0d;
        public double MedV { get; set; } = 0d;
        public double MedS { get; set; } = 0d;
        public double SizeFile { get; set; }
        public int numError { get; set; }

        public int num { get; set; }
        //public double procent { get; set; } = 0d;

        public int n { get; set; } = 1000;


        public string[][] str2;
        public object Clone()
        {
            Components comp = (Components) MemberwiseClone();//new Components(n);
            comp.y = new List<double[]>(y);
            comp.x = x.Select(d => d).ToArray();
            //comp.DatetimeBegin = new DateTime()

            return comp;
        }

        public Components(int n)
        {
            this.n = n;
            
        }

        public void readBinFile(string path)
        {
            this.file_name = path;
            FileInfo file = new FileInfo(path);
            long size = file.Length;
            //long num = 0;
            SizeFile = file.Length / 1024.0;
           

            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open),Encoding.GetEncoding(1251)))
            {
                // пока не достигнут конец файла
                // считываем каждое значение из файла


                //считывание калибровочных коэффициентов

                int sumXor=0;






                float[] a = new float[84];
                float[] b = new float[84];
                byte b1;
                byte b2;

                while (true)
                {
                    if (reader.BaseStream.Position == size - 1) { return; }
                    b1 = reader.ReadByte();
                    b2 = reader.ReadByte();
                    if ((b1 == 0x81 && b2 == 0xFA ) ) { break; }
                    else { reader.BaseStream.Position--; }

                }

                sumXor = b1 ^ b2;





                for (int i = 0; i < 84; i++)
                {
                    b1 = reader.ReadByte();
                    b2 = reader.ReadByte();
                    sumXor = sumXor ^ b1 ^ b2;
                    float w = (Int32) BitConverter.ToInt16(new byte[2] { b2, b1 }, 0);

                    //b[i] = w;

                    b1 = reader.ReadByte();
                    b2 = reader.ReadByte();
                    sumXor = sumXor ^ b1 ^ b2;
                    float e = (float)BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 1000;
                    //a[i] = e;

                    if ((i+1) % 2 == 0)
                    {
                        b[i] = 0;//-909;
                        a[i] = 1;//6.5f;
                    }
                    else
                    {
                        b[i] = 0f;
                        a[i] = 1f;
                    }
                }
                float Bx;
                float By;
                float Bz;

                const uint shift = 32768;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                Bx = BitConverter.ToInt16(new byte[2] { b2, b1 }, 0)/10 - shift;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                By = BitConverter.ToInt16(new byte[2] { b2, b1 }, 0)/10 - shift;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                Bz = BitConverter.ToInt16(new byte[2] { b2, b1 }, 0)/10 - shift;

                float K1;
                float K2;
                float K3;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                K1 = (float) (BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 10000.0) ;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                K2 = (float)(BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 10000.0);

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                K3 = (float)(BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 10000.0);

                float A1;
                float A2;
                float A3;

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                A1 = (float)(BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 100000.0);

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                A2 = (float)(BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 100000.0);

                b1 = reader.ReadByte();
                b2 = reader.ReadByte();
                sumXor = sumXor ^ b1 ^ b2;
                A3 = (float)(BitConverter.ToInt16(new byte[2] { b2, b1 }, 0) / 100000.0);

                for (int i = 0; i < 158; i++)
                {
                    b1 = reader.ReadByte();
                    sumXor = sumXor ^ b1;
                }

                b1 = reader.ReadByte();
                if (b1 != sumXor)
                {
                    for (int i = 0; i < 84; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            b[i] = -909;
                            a[i] = 6.5f;
                        }
                        else
                        {
                            b[i] = 0f;
                            a[i] = 1f;
                        }
                    }

                    Bx = 0;
                    By = 0;
                    Bz = 0;
                    K1 = 1;
                    K2 = 1;
                    K3 = 1;
                    A1 = 0;
                    A2 = 0;
                    A3 = 0;
                }



                //reader.ReadBytes(158);

                //считывание калибровочных коэффициентов
                while (true)
                {
                  //  if (reader.BaseStream.Position + 515 >= reader.BaseStream.Length) { break; }

                    b1 = reader.ReadByte();
                    b2 = reader.ReadByte();
                    if (b1 == 0x81&& b2 == 0xFA) { break; }
                    else { reader.BaseStream.Position--; }

                }

                //считывание комментария начала
                char[] ch = new char[96];
                byte[] utfArr;
                byte[] winArr;
                try
                {
                    for (int i = 0; i < 96; i++)
                    {
                        ch[i] = reader.ReadChar();
                    }

                    utfArr = Encoding.GetEncoding(1251).GetBytes(new string(ch));
                    winArr = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1251), utfArr);

                    СommentBegin = Encoding.GetEncoding(1251).GetString(winArr);
                }
                catch
                {
                    СommentBegin = "";
                }
                //считывание комментария начала

                //считывание даты/времени
                string Days;
                string Months;
                string Years;
                string Hours;
                string Minutes;
                string Seconds;

                try
                {
                    Days = new String(new char[] { reader.ReadChar(), reader.ReadChar() });
                    Months = new String(new char[] { reader.ReadChar(), reader.ReadChar() });
                    Years = new String(new char[] { reader.ReadChar(), reader.ReadChar() });
                    Hours = new String(new char[] { reader.ReadChar(), reader.ReadChar() });
                    Minutes = new String(new char[] { reader.ReadChar(), reader.ReadChar() });
                    Seconds = new String(new char[] { reader.ReadChar(), reader.ReadChar() });


                    DatetimeBegin = new DateTime(int.Parse(Years), int.Parse(Months), int.Parse(Days), int.Parse(Hours), int.Parse(Minutes), int.Parse(Seconds));
                    DatetimeBegin = DatetimeBegin.AddYears(2000);
                }
                catch
                {
                    DatetimeBegin = new DateTime();
                }
                //считывание даты/времени


                try
                {
                    ch = new char[10];
                    for (int i = 0; i < 10; i++)
                    {
                        ch[i] = reader.ReadChar();
                    }
                    WidhtBegin = new string(ch);
                    WidhtBegin = WidhtBegin.Replace('.', ',');
                }
                catch
                {
                    WidhtBegin = "";
                }   //считывание широты

                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        ch[i] = reader.ReadChar();
                    }
                    LongitudetBegin = new string(ch);
                    LongitudetBegin = LongitudetBegin.Replace('.', ',');
                }
                catch
                {
                    LongitudetBegin = "";
                }   //считывание долготы

                //x;
                y = new List<double[]>();

                List<List<double>> arr = new List<List<double>>();
                for(int i = 0; i < 87; i++)
                {
                    arr.Add(new List<double>());
                }
                
                numError = 0;
                int j=0;
                sumXor = 0x00;
                byte sinh1 = 0x80;
                byte sinh2 = 0xfe;
                double accel1 = 0;
                double accel2 = 0;
                double accel3 = 0;

                //считывание датчиков
               for (j = 0; reader.BaseStream.Position + 515 <= reader.BaseStream.Length; j++)
                {
                    while (true)
                    {
                        if (reader.BaseStream.Position + 515 >= reader.BaseStream.Length)
                        {
                            foreach (List<double> l in arr)
                            {
                                l.RemoveAt(l.Count - 1);
                                j--;
                            }
                            break; }

                        b1 = reader.ReadByte();
                        b2 = reader.ReadByte();
                        if (b1 == sinh1 && b2 == sinh2) { break; }
                        else { reader.BaseStream.Position--; }

                    }

                   // pos = reader.BaseStream.Position;

                    sumXor = b1 ^ b2;
                    //MessageBox.Show(b1.ToString("x"));

                    
                    
                        for (int i = 0; i < 84; i++)
                        {

                            b1 = reader.ReadByte();
                            b2 = reader.ReadByte();

                            sumXor = sumXor ^ b1 ^ b2;

                            Int16 q = BitConverter.ToInt16(new byte[2] { b2, b1 }, 0);

                        arr[i].Add((q + b[i]) * a[i]); 


                        }
                        b1 = reader.ReadByte();
                        b2 = reader.ReadByte();
                        //y[84][j] = BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0);
                       arr[84].Add(BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0));
                        sumXor = sumXor ^ b1 ^ b2;

                        b1 = reader.ReadByte();
                        b2 = reader.ReadByte();
                    //y[85][j] = BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0);
                   arr[85].Add(BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0));
                        sumXor = sumXor ^ b1 ^ b2;

                        b1 = reader.ReadByte();
                        b2 = reader.ReadByte();
                    //y[86][j] = BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0);
                  arr[86].Add(BitConverter.ToUInt16(new byte[2] { b2, b1 }, 0));
                        sumXor = sumXor ^ b1 ^ b2;

                    //y[84][j] += Bx;
                    //y[85][j] += By;
                    //y[86][j] += Bz;


                        arr[84][j] += Bx;
                        arr[85][j] += By;
                        arr[86][j] += Bz;

                        accel1 = arr[84][j] * K1;
                        accel2 = arr[84][j] * A1 + arr[85][j] * K2;
                        accel3 = arr[84][j] * A2 + arr[85][j] * A3 + arr[86][j] * K3;

                        arr[84][j] = accel1;
                        arr[85][j] = accel2;
                        arr[86][j] = accel3;

                    //reader.ReadBytes(80);
                    for (int i = 0; i < 79; i++)
                    { sumXor = sumXor ^ reader.ReadByte(); }


                        byte crc = reader.ReadByte();

                        if (sumXor != crc )
                        {
                            reader.BaseStream.Position -= 254;
                            //num--;
                            foreach (List<double> l in arr)
                            {
                                l.RemoveAt(j);
                            }
                            j--;
                            numError++;

                        }
                        else
                        {
                            num++;
                        }



                }





                //считывание датчиков
                // End:

                /*for (int i = 0; i < arr.Count; i++)
                {
                    arr[i].RemoveAt(arr[i].Count - 1);
                }*/



                x = new double[ arr[0].Count()];
                y = arr.Select(l => l.ToArray()).ToList();

                //MessageBox.Show(x.Length.ToString() + " "+)

                reader.BaseStream.Position = size - 2;

                sumXor = 0x00;
                while (true)
                {
                    if (reader.BaseStream.Position == 0) { return; }
                    reader.BaseStream.Position--;
                    b1 = reader.ReadByte();
                    b2 = reader.ReadByte();
                    if (b1 == 0x81 && b2 == 0xFA) { break; }
                    reader.BaseStream.Position = reader.BaseStream.Position - 2;
                }
                sumXor = b1 ^ b2;







                try
                { 
                    ch = new char[96];
                    
                    for (int i = 0; i < 96; i++)
                    {
                        ch[i] = reader.ReadChar();
                        sumXor = sumXor ^ ch[i];
                    }

                    utfArr = Encoding.GetEncoding(1251).GetBytes(new string(ch));
                    winArr = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(1251), utfArr);

                    СommentEnd = Encoding.GetEncoding(1251).GetString(winArr);
                }
                catch
                {
                    СommentEnd = "";
                }  //комментарий конца

                try
                {
                    char sd1, sd2;
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^sd2;
                    Days = new String(new char[] { sd1, sd2 });
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^ sd2;
                    Months = new String(new char[] { sd1, sd2 });
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^ sd2;
                    Years = new String(new char[] { sd1, sd2 });
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^ sd2;
                    Hours = new String(new char[] { sd1, sd2 });
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^ sd2;
                    Minutes = new String(new char[] { sd1, sd2 });
                    sd1 = reader.ReadChar(); sd2 = reader.ReadChar(); sumXor = sumXor ^ sd1 ^ sd2;
                    Seconds = new String(new char[] { sd1, sd2 });

                    DatetimeEnd = new DateTime(int.Parse(Years), int.Parse(Months), int.Parse(Days), int.Parse(Hours), int.Parse(Minutes), int.Parse(Seconds));
                    DatetimeEnd = DatetimeEnd.AddYears(2000);
                }
                catch
                {
                    DatetimeEnd = new DateTime();
                }  //дата время конца
                try
                {
                    ch = new char[10];
                    for (int i = 0; i < 10; i++)
                    {
                        ch[i] = reader.ReadChar();
                        sumXor = sumXor ^ ch[i];
                    }
                    WidhtEnd = new string(ch);
                    WidhtEnd = WidhtEnd.Replace('.', ',');
                }
                catch
                {
                    WidhtEnd = "";
                }   //широта

                try
                {            
                    for (int i = 0; i < 10; i++)
                    {
                        ch[i] = reader.ReadChar();
                        sumXor = sumXor ^ ch[i];
                    }
                    LongitudetEnd = new string(ch);
                    LongitudetEnd = LongitudetEnd.Replace('.', ',');
                }
                catch
                {
                    LongitudetEnd = "";
                }  //долгота

                if (reader.ReadByte() != sumXor)
                {


                }




                reader.Close();

                try
                {
                    t = DatetimeEnd.Subtract(DatetimeBegin).TotalSeconds;

                    Sec = t / j;

                    double w1 = double.Parse(WidhtBegin) / 180D * Math.PI;
                    double w2 = double.Parse(WidhtEnd) / 180D * Math.PI;

                    double l1 = double.Parse(LongitudetBegin) / 180D * Math.PI;
                    double l2 = double.Parse(LongitudetEnd) / 180D * Math.PI;

                    s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((w1 - w2) / 2), 2) + Math.Cos(w1) * Math.Cos(w2) * Math.Pow(Math.Sin((l1 - l2) / 2), 2))) * 6371.0 * 1000;

                    MedS = s / j;
                    MedV = s / t;



                    for (int i = 0; i < x.Length; i++)
                    {
                        x[i] = i * MedS;
                    }

                    n = x.Length;
                }
                catch
                {

                }  //вычисление полного пути и времени

            }
        }

        public void ConvertTo_txt(string path)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    string line = "";
                    for (int i = 0; i < n; i++)
                    {
                        line = x[i].ToString("F0");
                        for (int j = 0; j < y.Count; j++)
                        {
                            line += ' ' + y[j][i].ToString("F0"); 
                        }

                        writer.WriteLine(line.Replace(',','.'));
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка записи в файл");
            }
        }



        //13
        public Components(string path)
        {
            //FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(path))
            {
                List<string[]> str = new List<string[]>();
                string line;

                int i = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    str.Add(line.Split(' '));
                    i++;
                }
                n = i;

                reader.Close();

                //MessageBox.Show(n.ToString());

                

                int m = str[0].Length;

                //List_Components = new List<Component>();
                y = new List<double[]>();
                for (i = 0; i < m; i++)
                {
                    y.Add(new double[n]);
                }

                x = new double[n];

                for (i = 0; i < n; i++)
                {
                    //List_Components[j].x[i] = double.Parse(str[i][0]);
                    x[i] = double.Parse(str[i][0]);
                }

                for (i = 0; i < n; i++)
                {
                    //List_Components[0].x[i] = double.Parse(str[i][0]);
                    for (int j = 1; j < str[i].Length; j++)
                    {
                        y[j-1][i] = double.Parse(str[i][j]);
                    }
                }
                //str2 = str;

                
                //MessageBox.Show(List_Components[0].x[999].ToString());
            }
        }

    }
}
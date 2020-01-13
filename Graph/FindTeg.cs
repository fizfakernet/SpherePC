using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    static class FindTeg
    {
        //static string constChar = "\"";
        static public string getLinkFile(string s)
        {
            int i = 0;
            string ret = "";
            for(;i<s.Length;i++)
            {
                //< a href = "test3.da" download = "" > Скачать файл </ a >
                if ((s[i] == 'h') && (s[i + 1] == 'r') && (s[i + 2] == 'e') && (s[i + 3] == 'f'))
                {
                    for (int j=i+5; (j < s.Length) && (s[j+1] != '>'); j++)
                    {
                        if(s[j]=='"')
                        {
                            break;
                        }
                        ret+=s[j] ;
                    }
                }
            }
            return ret;
        }
    }
}

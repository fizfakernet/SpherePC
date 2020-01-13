using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public static class Formulas
    {
        public static double[] binomial_mult(int n, double[] p)
        {
            int i, j;
            double[] a = new double[2 * n]; ;
            if (a == null) return (null);

            for (i = 0; i < n; ++i)
            {
                for (j = i; j > 0; --j)
                {
                    a[2 * j] += p[2 * i] * a[2 * (j - 1)] - p[2 * i + 1] * a[2 * (j - 1) + 1];
                    a[2 * j + 1] += p[2 * i] * a[2 * (j - 1) + 1] + p[2 * i + 1] * a[2 * (j - 1)];
                }
                a[0] += p[2 * i];
                a[1] += p[2 * i + 1];
            }
            return (a);
        }



        public static double[] trinomial_mult(int n, double[] b, double[] c)
        {
            int i, j;
            double[] a = new double[4 * n];

            if (a == null) return (null);

            a[2] = c[0];
            a[3] = c[1];
            a[0] = b[0];
            a[1] = b[1];

            for (i = 1; i < n; ++i)
            {
                a[2 * (2 * i + 1)] += c[2 * i] * a[2 * (2 * i - 1)] - c[2 * i + 1] * a[2 * (2 * i - 1) + 1];
                a[2 * (2 * i + 1) + 1] += c[2 * i] * a[2 * (2 * i - 1) + 1] + c[2 * i + 1] * a[2 * (2 * i - 1)];

                for (j = 2 * i; j > 1; --j)
                {
                    a[2 * j] += b[2 * i] * a[2 * (j - 1)] - b[2 * i + 1] * a[2 * (j - 1) + 1] +
                    c[2 * i] * a[2 * (j - 2)] - c[2 * i + 1] * a[2 * (j - 2) + 1];
                    a[2 * j + 1] += b[2 * i] * a[2 * (j - 1) + 1] + b[2 * i + 1] * a[2 * (j - 1)] +
                    c[2 * i] * a[2 * (j - 2) + 1] + c[2 * i + 1] * a[2 * (j - 2)];
                }

                a[2] += b[2 * i] * a[0] - b[2 * i + 1] * a[1] + c[2 * i];
                a[3] += b[2 * i] * a[1] + b[2 * i + 1] * a[0] + c[2 * i + 1];
                a[0] += b[2 * i];
                a[1] += b[2 * i + 1];
            }

            return (a);
        }


        public static double[] dcof_bwlp(int n, double fcf)
        {
            int k;            // loop variables
            double theta;     // M_PI * fcf / 2.0
            double st;        // sine of theta
            double ct;        // cosine of theta
            double parg;      // pole angle
            double sparg;     // sine of the pole angle
            double cparg;     // cosine of the pole angle
            double a;         // workspace variable
            double[] rcof = new double[2 * n];     // binomial coefficients
            double[] dcof;     // dk coefficients

            if (rcof == null) return (null);

            theta = Math.PI * fcf;
            st = Math.Sin(theta);
            ct = Math.Cos(theta);

            for (k = 0; k < n; ++k)
            {
                parg = Math.PI * (double)(2 * k + 1) / (double)(2 * n);
                sparg = Math.Sin(parg);
                cparg = Math.Cos(parg);
                a = 1.0 + st * sparg;
                rcof[2 * k] = -ct / a;
                rcof[2 * k + 1] = -st * cparg / a;
            }

            dcof = binomial_mult(n, rcof);
            //free(rcof);

            dcof[1] = dcof[0];
            dcof[0] = 1.0;
            for (k = 3; k <= n; ++k)
                dcof[k] = dcof[2 * k - 2];
            return (dcof);
        }


        public static int[] ccof_bwlp(int n)
        {
            int[] ccof = new int[n + 1];
            int m;
            int i;

            if (ccof == null) return (null);

            ccof[0] = 1;
            ccof[1] = n;
            m = n / 2;
            for (i = 2; i <= m; ++i)
            {
                ccof[i] = (n - i + 1) * ccof[i - 1] / i;
                ccof[n - i] = ccof[i];
            }
            ccof[n - 1] = n;
            ccof[n] = 1;

            return (ccof);
        }


        public static int[] ccof_bwhp(int n)
        {
            int[] ccof;
            int i;

            ccof = ccof_bwlp(n);
            if (ccof == null) return (null);

            for (i = 0; i <= n; ++i)
                if ((i % 2) == 1) ccof[i] = -ccof[i];

            return (ccof);
        }




        public static double sf_bwlp(int n, double fcf)
        {
            int m, k;         // loop variables
            double omega;     // M_PI * fcf
            double fomega;    // function of omega
            double parg0;     // zeroth pole angle
            double sf;        // scaling factor

            omega = Math.PI * fcf;
            fomega = Math.Sin(omega);
            parg0 = Math.PI / (double)(2 * n);

            m = n / 2;
            sf = 1.0;
            for (k = 0; k < n / 2; ++k)
                sf *= 1.0 + fomega * Math.Sin((double)(2 * k + 1) * parg0);

            fomega = Math.Sin(omega / 2.0);

            if ((n % 2) == 1) sf *= fomega + Math.Cos(omega / 2.0);
            sf = Math.Pow(fomega, n) / sf;

            return (sf);
        }

        /**********************************************************************
          sf_bwhp - calculates the scaling factor for a butterworth highpass filter.
          The scaling factor is what the c coefficients must be multiplied by so
          that the filter response has a maximum value of 1.

        */

        public static double sf_bwhp(int n, double fcf)
        {
            int m, k;         // loop variables
            double omega;     // M_PI * fcf
            double fomega;    // function of omega
            double parg0;     // zeroth pole angle
            double sf;        // scaling factor

            omega = Math.PI * fcf;
            fomega = Math.Sin(omega);
            parg0 = Math.PI / (double)(2 * n);

            m = n / 2;
            sf = 1.0;
            for (k = 0; k < n / 2; ++k)
                sf *= 1.0 + fomega * Math.Sin((double)(2 * k + 1) * parg0);

            fomega = Math.Cos(omega / 2.0);

            if ((n % 2) == 1) sf *= fomega + Math.Sin(omega / 2.0);
            sf = Math.Pow(fomega, n) / sf;

            return (sf);
        }


        /**********************************************************************
dcof_bwbp - calculates the d coefficients for a butterworth bandpass 
filter. The coefficients are returned as an array of doubles.

*/

        public static double[] dcof_bwbp(int n, double f1f, double f2f)
        {
            int k;            // loop variables
            double theta;     // M_PI * (f2f - f1f) / 2.0
            double cp;        // cosine of phi
            double st;        // sine of theta
            double ct;        // cosine of theta
            double s2t;       // sine of 2*theta
            double c2t;       // cosine 0f 2*theta
            double[] rcof = new double[2 * n];     // z^-2 coefficients
            double[] tcof = new double[2 * n]; ;     // z^-1 coefficients
            double[] dcof;     // dk coefficients
            double parg;      // pole angle
            double sparg;     // sine of pole angle
            double cparg;     // cosine of pole angle
            double a;         // workspace variables

            cp = Math.Cos(Math.PI * (f2f + f1f) / 2.0);
            theta = Math.PI * (f2f - f1f) / 2.0;
            st = Math.Sin(theta);
            ct = Math.Cos(theta);
            s2t = 2.0 * st * ct;        // sine of 2*theta
            c2t = 2.0 * ct * ct - 1.0;  // cosine of 2*theta

            for (k = 0; k < n; ++k)
            {
                parg = Math.PI * (double)(2 * k + 1) / (double)(2 * n);
                sparg = Math.Sin(parg);
                cparg = Math.Cos(parg);
                a = 1.0 + s2t * sparg;
                rcof[2 * k] = c2t / a;
                rcof[2 * k + 1] = s2t * cparg / a;
                tcof[2 * k] = -2.0 * cp * (ct + st * sparg) / a;
                tcof[2 * k + 1] = -2.0 * cp * st * cparg / a;
            }

            dcof = trinomial_mult(n, tcof, rcof);
            /* free(tcof);
             free(rcof);*/

            dcof[1] = dcof[0];
            dcof[0] = 1.0;
            for (k = 3; k <= 2 * n; ++k)
                dcof[k] = dcof[2 * k - 2];
            return (dcof);
        }



        /**********************************************************************
  dcof_bwbs - calculates the d coefficients for a butterworth bandstop 
  filter. The coefficients are returned as an array of doubles.

*/

        public static double[] dcof_bwbs(int n, double f1f, double f2f)
        {
            int k;            // loop variables
            double theta;     // M_PI * (f2f - f1f) / 2.0
            double cp;        // cosine of phi
            double st;        // sine of theta
            double ct;        // cosine of theta
            double s2t;       // sine of 2*theta
            double c2t;       // cosine 0f 2*theta
            double[] rcof = new double[2 * n];     // z^-2 coefficients
            double[] tcof = new double[2 * n]; ;     // z^-1 coefficients
            double[] dcof;     // dk coefficients
            double parg;      // pole angle
            double sparg;     // sine of pole angle
            double cparg;     // cosine of pole angle
            double a;         // workspace variables

            cp = Math.Cos(Math.PI * (f2f + f1f) / 2.0);
            theta = Math.PI * (f2f - f1f) / 2.0;
            st = Math.Sin(theta);
            ct = Math.Cos(theta);
            s2t = 2.0 * st * ct;        // sine of 2*theta
            c2t = 2.0 * ct * ct - 1.0;  // cosine 0f 2*theta


            for (k = 0; k < n; ++k)
            {
                parg = Math.PI * (double)(2 * k + 1) / (double)(2 * n);
                sparg = Math.Sin(parg);
                cparg = Math.Cos(parg);
                a = 1.0 + s2t * sparg;
                rcof[2 * k] = c2t / a;
                rcof[2 * k + 1] = -s2t * cparg / a;
                tcof[2 * k] = -2.0 * cp * (ct + st * sparg) / a;
                tcof[2 * k + 1] = 2.0 * cp * st * cparg / a;
            }

            dcof = trinomial_mult(n, tcof, rcof);
            /*free(tcof);
            free(rcof);*/

            dcof[1] = dcof[0];
            dcof[0] = 1.0;
            for (k = 3; k <= 2 * n; ++k)
                dcof[k] = dcof[2 * k - 2];
            return (dcof);
        }


        /**********************************************************************
          ccof_bwbp - calculates the c coefficients for a butterworth bandpass 
          filter. The coefficients are returned as an array of integers.

        */

        public static int[] ccof_bwbp(int n)
        {
            int[] tcof;
            int[] ccof = new int[2 * n + 1];
            int i;
            if (ccof == null) return (null);

            tcof = ccof_bwhp(n);
            if (tcof == null) return (null);

            for (i = 0; i < n; ++i)
            {
                ccof[2 * i] = tcof[i];
                ccof[2 * i + 1] = 0;
            }
            ccof[2 * n] = tcof[n];

            //(tcof);
            return (ccof);
        }

        /**********************************************************************
          ccof_bwbs - calculates the c coefficients for a butterworth bandstop 
          filter. The coefficients are returned as an array of integers.

        */

        public static double[] ccof_bwbs(int n, double f1f, double f2f)
        {
            double alpha;
            double[] ccof = new double[2 * n + 1];
            int i, j;

            alpha = -2.0 * Math.Cos(Math.PI * (f2f + f1f) / 2.0) / Math.Cos(Math.PI * (f2f - f1f) / 2.0);


            ccof[0] = 1.0;

            ccof[2] = 1.0;
            ccof[1] = alpha;

            for (i = 1; i < n; ++i)
            {
                ccof[2 * i + 2] += ccof[2 * i];
                for (j = 2 * i; j > 1; --j)
                    ccof[j + 1] += alpha * ccof[j] + ccof[j - 1];

                ccof[2] += alpha * ccof[1] + 1.0;
                ccof[1] += alpha;
            }

            return (ccof);
        }


        /**********************************************************************
  sf_bwbp - calculates the scaling factor for a butterworth bandpass filter.
  The scaling factor is what the c coefficients must be multiplied by so
  that the filter response has a maximum value of 1.

*/

        public static double sf_bwbp(int n, double f1f, double f2f)
        {
            int k;            // loop variables
            double ctt;       // cotangent of theta
            double sfr, sfi;  // real and imaginary parts of the scaling factor
            double parg;      // pole angle
            double sparg;     // sine of pole angle
            double cparg;     // cosine of pole angle
            double a, b, c;   // workspace variables

            ctt = 1.0 / Math.Tan(Math.PI * (f2f - f1f) / 2.0);
            sfr = 1.0;
            sfi = 0.0;

            for (k = 0; k < n; ++k)
            {
                parg = Math.PI * (double)(2 * k + 1) / (double)(2 * n);
                sparg = ctt + Math.Sin(parg);
                cparg = Math.Cos(parg);
                a = (sfr + sfi) * (sparg - cparg);
                b = sfr * sparg;
                c = -sfi * cparg;
                sfr = b - c;
                sfi = a - b - c;
            }

            return (1.0 / sfr);
        }

        /**********************************************************************
          sf_bwbs - calculates the scaling factor for a butterworth bandstop filter.
          The scaling factor is what the c coefficients must be multiplied by so
          that the filter response has a maximum value of 1.

        */

        public static double sf_bwbs(int n, double f1f, double f2f)
        {
            int k;            // loop variables
            double tt;        // tangent of theta
            double sfr, sfi;  // real and imaginary parts of the scaling factor
            double parg;      // pole angle
            double sparg;     // sine of pole angle
            double cparg;     // cosine of pole angle
            double a, b, c;   // workspace variables

            tt = Math.Tan(Math.PI * (f2f - f1f) / 2.0);
            sfr = 1.0;
            sfi = 0.0;

            for (k = 0; k < n; ++k)
            {
                parg = Math.PI * (double)(2 * k + 1) / (double)(2 * n);
                sparg = tt + Math.Sin(parg);
                cparg = Math.Cos(parg);
                a = (sfr + sfi) * (sparg - cparg);
                b = sfr * sparg;
                c = -sfi * cparg;
                sfr = b - c;
                sfi = a - b - c;
            }

            return (1.0 / sfr);
        }




    }
}





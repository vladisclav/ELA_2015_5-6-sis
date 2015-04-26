using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CalculatorRemote
{
    public class Calculator:MarshalByRefObject
    {
        public string getResult(string request)
        {
            string ans;
            ans = new DataTable().Compute(request, null).ToString();
            return ans;
        }
    }
}

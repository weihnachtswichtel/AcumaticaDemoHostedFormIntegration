using PX.CCProcessingBase.Interfaces.V2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcumaticaDummyCreditCardPlugin
{
    public static class ADCPHelper
    {

        public static Dictionary<string, CCCardType> MapCardType = new Dictionary<string, CCCardType>
        {
            {"VIS", CCCardType.Visa},
            {"MSC", CCCardType.MasterCard},
            {"AME", CCCardType.AmericanExpress},
            {"UNI", CCCardType.UnionPay }
        };
    }
}

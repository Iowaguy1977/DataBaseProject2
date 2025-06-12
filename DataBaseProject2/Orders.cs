using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace DataBaseProject2
{
    [XmlRoot("Orders")]
    public class Orders
    {
        [XmlElement("order")]
        public List<order> orderlist { get; set; }
    }
    public class order
    {
       
        public int Order_ID;
        public string Item_Type;
        public string Item_Name;   
        public int Qty;
        public double Price_Each;
        public int Customer_ID;
    }
}

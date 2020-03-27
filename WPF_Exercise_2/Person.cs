using System;

namespace WPF_Exercise_2
{

  public class Person
   {
       public Int64 ID { get; set; }
       public String FirstName { get; set; }
       public String LastName { get; set; }
       public Int64 Age { get; set; }
       public String Email { get; set; }
       public Int64 IsMember { get; set; }
       public String OrderValue { get; set; }
       public Int64 OrderStatus { get; set; }


       public Person( String fIRSTNAME, String lASTNAME, Int64 aGE, String eMAIL, Int64 iSMEMBER, String oRDERVALUE, Int64 oRDERSTATUS)
       {
           FirstName = fIRSTNAME;
           LastName = lASTNAME;
           Age = aGE;
           Email = eMAIL;
           IsMember = iSMEMBER;
           OrderValue = oRDERVALUE;
           OrderStatus = oRDERSTATUS;
       }
       public Person()
       {
           FirstName = "-=DEFAULT_STRING=-";
           LastName = "-=DEFAULT_STRING=-";
           Age = -9999;
           Email = "-=DEFAULT_STRING=-";
           IsMember = -9999;
           OrderValue = "-=DEFAULT_STRING=-";
           OrderStatus = -9999;
       }



        public static bool operator ==(Person c1, Person c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null)) return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            //return c1.OrderValue == c2.OrderValue;
            return c1.FirstName == c2.FirstName;
        }

        public static bool operator !=(Person c1, Person c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            var thisType = obj as Person;
            return this == thisType;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(this.ID);
        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach(var s in this.GetType().GetProperties())
               str += $"{ s.Name}: { s.GetValue(this)}\n";

            return str;
        }
   }
}

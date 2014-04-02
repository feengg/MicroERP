using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Interface
{
    public class BusinessLayer
    {
        new DataAccessLayer dal = new DataAccessLayer();
        

        public List<Contact> searchContacts(string text)
        {
            return dal.searchContacts(text);
        }

    }
}

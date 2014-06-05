using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interface
{
    public class BusinessLayer
    {
        new Contact_DAL c_dal = new Contact_DAL();
        new Invoice_DAL i_dal = new Invoice_DAL();

        #region SearchContacts
        public List<Contact> searchContacts(string text)
        {
            return c_dal.searchContacts(text);
        }
        #endregion

        #region SearchID
        public List<Contact> searchID(int id)
        {
            return c_dal.searchID(id);
        }
        #endregion

        #region UpdateContacts
        public void UpdateContacts(Contact list)
        {
            c_dal.UpdateContacts(list);
        }
        #endregion

        #region NewContacts
        public void NewContacts(Contact list)
        {
            c_dal.NewContacts(list);
        }
        #endregion

        #region NewFirm 
        public void NewFirm(Contact list)
        {
            c_dal.NewFirm(list);
        }
        #endregion

    }
}

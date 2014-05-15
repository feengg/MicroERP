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

        #region SearchContacts
        public List<Contact> searchContacts(string text)
        {
            return dal.searchContacts(text);
        }
        #endregion

        #region SearchID
        public List<Contact> searchID(int id)
        {
            return dal.searchID(id);
        }
        #endregion

        #region UpdateContacts
        public void UpdateContacts(Contact list)
        {
            dal.UpdateContacts(list);
        }
        #endregion

        #region NewContacts
        public void NewContacts(Contact list)
        {
            dal.NewContacts(list);
        }
        #endregion

        #region NewFirm 
        public void NewFirm(Contact list)
        {
            dal.NewFirm(list);
        }
        #region

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interface
{
    public class BusinessLayer
    {
        new DAL dal = new DAL();

        //Contact

        #region SearchContacts
        public ContactsList searchContacts(string text)
        {
            return dal.searchContacts(text);
        }
        #endregion

        #region SearchID
        public ContactsList searchID(int id)
        {
            return dal.searchID(id);
        }
        #endregion

        #region UpdateContacts
        public void UpdateContacts(ContactsList list)
        {
            dal.UpdateContacts(list);
        }
        #endregion

        #region NewContacts
        public void NewContacts(ContactsList list)
        {
            dal.NewContacts(list);
        }
        #endregion

        //Firm

        #region NewFirm
        public void NewFirm(Firmlist list)
        {
            dal.NewFirm(list);
        }
        #endregion

        #region SearchFirm
        public Firmlist searchFirm(string text)
        {
            return dal.searchFirm(text);
        }
        #endregion

        #region Search Firm ID
        public Firmlist searchFirmID(int id)
        {
            return dal.searchFirmID(id);
        }
        #endregion

        #region UpdateFirm
        public void UpdateFirm(Firmlist list)
        {
            dal.UpdateFirm(list);
        }
    }
}


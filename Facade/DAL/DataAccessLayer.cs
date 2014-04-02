using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interface;
using WebLibrary;
using System.Net.Sockets;
using System.Data.SqlClient;


namespace Interface
{
    class DataAccessLayer
    {
        //Datenbankverbindung
         //private string strCon = @"Data Source=(local);" + "Initial Catalog=MicroERP;Integrated Security=true;";
          private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=MicroERP;Integrated Security=true;";

          #region searchContact
          public List<Contact> searchContacts(string text)
          {
              try
              {
                  List<Contact> list = new List<Contact>();
                  using (SqlConnection db = new SqlConnection(strCon))
                  {
                      db.Open();
                      //SQL Statement zum auslesen
                      string query = "SELECT ID_Person, Titel, Vorname, Nachname, Suffix, Geburtsdatum, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Person on ID_Kontakte = FK_Kontakte WHERE [Vorname] = @text or [Nachname] = @text";
                      

                      SqlCommand cmdSelect = new SqlCommand(query, db);
                      cmdSelect.Parameters.AddWithValue("@text", text);

                      using (SqlDataReader rd = cmdSelect.ExecuteReader())
                      {
                          // Daten holen
                          while (rd.Read())
                          {
                              Console.WriteLine("ID:{0} & Vorname:{1}", rd["ID_Person"], rd["Vorname"]);

                              Contact instance = new Contact();
                              instance.ID = rd.GetInt32(0);
                              instance.Titel = rd.GetString(1);
                              instance.Vorname = rd.GetString(2);
                              instance.Nachname = rd.GetString(3);
                              instance.Suffix = rd.GetString(4);
                              instance.Geburtsdatum = rd.GetDateTime(5);
                              instance.Adresse = rd.GetString(6);
                              instance.Rechnungsadresse = rd.GetString(7);
                              instance.Lieferadresse = rd.GetString(8);

                              list.Add(instance);
                              //list db null
                          }
                          // DataReader schließen 
                          rd.Close();
                      }

                      // Verbindung schließen 
                      db.Close();

                      return list;

                  }

              }
              catch (Exception)
              {
                  throw new Exception("Connection to Database failed");
              }
          #endregion

          }
    }
}

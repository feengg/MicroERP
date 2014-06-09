using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;


namespace Interface
{
    class Contact_DAL
    {
        //Datenbankverbindung
        //private string strCon = @"Data Source=(local);" + "Initial Catalog=MicroERP;Integrated Security=true;";
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=MicroERP;Integrated Security=true;";
        //private string strCon = global::Facade.Properties.Settings.Default.ConnectionString;

        #region Variablen

        string firmname;
        string UID;
        string adress;
        string billingadress;
        string deliveryadress;
        int id;
        string title;
        string firstname;
        string lastname;
        string suffix;
        DateTime birthday;

        #endregion

        #region searchContact
        public ContactsList searchContacts(string text)
        {

            try
            {
                ContactsList list = new ContactsList();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "SELECT ID_Person, Titel, Vorname, Nachname, Suffix, Geburtsdatum, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Person on ID_Kontakte = FK_Kontakte WHERE [Vorname] = @text or [Nachname] = @text";

                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@text", text);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            Console.WriteLine("ID:{0} & Vorname:{1}", rd["ID_Person"], rd["Vorname"]);
                            Contact contact = new Contact();
                            
                            contact.ID = rd.GetInt32(0);
                            contact.Titel = rd.GetString(1);
                            contact.Vorname = rd.GetString(2);
                            contact.Nachname = rd.GetString(3);
                            contact.Suffix = rd.GetString(4);
                            contact.Geburtsdatum = rd.GetDateTime(5);
                            contact.Adresse = rd.GetString(6);
                            contact.Rechnungsadresse = rd.GetString(7);
                            contact.Lieferadresse = rd.GetString(8);

                            list.contact.Add(contact);
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
        }
        #endregion

        #region SearchID
        public ContactsList searchID(int id)
        {
            try
            {
                ContactsList list = new ContactsList();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "SELECT ID_Person, Titel, Vorname, Nachname, Suffix, Geburtsdatum, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Person on ID_Kontakte = FK_Kontakte WHERE [ID_Person] = @id";


                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@id", id);

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

                            list.contact.Add(instance);
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
        }
        #endregion

        #region UpdateContacts
        public void UpdateContacts(ContactsList list)
        {

            foreach (var obj in list.contact)
            {
                id = obj.ID;
                title = obj.Titel;
                firstname = obj.Vorname;
                lastname = obj.Nachname;
                suffix = obj.Suffix;
                birthday = obj.Geburtsdatum;
                adress = obj.Adresse;
                billingadress = obj.Rechnungsadresse;
                deliveryadress = obj.Lieferadresse;
            }

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "UPDATE Person SET Titel = @title, Vorname = @firstname, Nachname = @lastname, Suffix = @suffix, Geburtsdatum = @birthday WHERE ID_Person = @id";
                    //string query = "UPDATE Person SET Titel = @title, Vorname = @firstname, Nachname = @lastname, Suffix = @suffix WHERE ID_Person = @id";
                    string query2 = "UPDATE Kontakte SET Adresse = @adress, Rechnungsadresse = @billingadress, Lieferadresse = @deliveryadress WHERE ID_Kontakte = (SELECT FK_Kontakte FROM Person WHERE ID_Person = @id)";


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@title", title);
                    cmdUpdate1.Parameters.AddWithValue("@firstname", firstname);
                    cmdUpdate1.Parameters.AddWithValue("@lastname", lastname);
                    cmdUpdate1.Parameters.AddWithValue("@suffix", suffix);
                    cmdUpdate1.Parameters.AddWithValue("@birthday", birthday);
                    cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

                    cmdUpdate1.ExecuteNonQuery();
                    cmdUpdate2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Contact failed.");
            }
        }
        #endregion

        #region NewContacts
        public void NewContacts(ContactsList list)
        {

            foreach (var obj in list.contact)
            {
                id = obj.ID;
                title = obj.Titel;
                firstname = obj.Vorname;
                lastname = obj.Nachname;
                suffix = obj.Suffix;
                birthday = obj.Geburtsdatum;
                adress = obj.Adresse;
                billingadress = obj.Rechnungsadresse;
                deliveryadress = obj.Lieferadresse;
            }

            try
            {
                //List<Contact> list = new List<Contact>();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "INSERT INTO Person(Titel, Vorname, Nachname, Suffix, Geburtsdatum) VALUES (@title, @firstname, @lastname, @suffix, @birthday)";
                    string query2 = "INSERT INTO Kontakte(Adresse, Rechnungsadresse, Lieferadresse) VALUES(@adress, @billingadress, @deliveryadress)";


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate1.Parameters.AddWithValue("@title", title);
                    cmdUpdate1.Parameters.AddWithValue("@firstname", firstname);
                    cmdUpdate1.Parameters.AddWithValue("@lastname", lastname);
                    cmdUpdate1.Parameters.AddWithValue("@suffix", suffix);
                    cmdUpdate1.Parameters.AddWithValue("@birthday", birthday);
                    cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

                    cmdUpdate1.ExecuteNonQuery();
                    cmdUpdate2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Contact failed.");
            }
        }
        #endregion

        #region NewFirm
        public void NewFirm(Firmlist list)
        {
            foreach (var obj in list.firma)
            {

                firmname = obj.Name;
                UID = obj.UID;
                adress = obj.Adresse;
                billingadress = obj.Lieferadresse;
                deliveryadress = obj.Rechnungsadresse;

            }

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "INSERT INTO Firma(Name, UID) VALUES (@firmname, @UID)";
                    string query2 = "INSERT INTO Kontakte(Adresse, Rechnungsadresse, Lieferadresse) VALUES(@adress, @billingadress, @deliveryadress)";

                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    SqlCommand cmdInsert2 = new SqlCommand(query2, db);
                    cmdInsert1.Parameters.AddWithValue("@firmname", firmname);
                    cmdInsert1.Parameters.AddWithValue("@UID", UID);
                    cmdInsert2.Parameters.AddWithValue("@adress", adress);
                    cmdInsert2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdInsert2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

                    cmdInsert1.ExecuteNonQuery();
                    cmdInsert2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Firm failed.");
            }
        }
        #endregion

        #region searchFirm
        public Firmlist searchFirm(string text)
        {

            try
            {
                Firmlist list = new Firmlist();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "SELECT ID_Firma, Name, UID, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Person on ID_Firma = FK_Firma WHERE [Name] = @text";

                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@text", text);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            
                            Firma firm = new Firma();
                            firm.ID = rd.GetString(0);
                            firm.Name = rd.GetString(1);
                            firm.UID = rd.GetString(2);
                            firm.Adresse = rd.GetString(3);
                            firm.Rechnungsadresse = rd.GetString(4);
                            firm.Lieferadresse = rd.GetString(5);

                            list.firma.Add(firm);
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
        }
        #endregion

    }
}

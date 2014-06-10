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
    class DAL
    {
        //Datenbankverbindung
        //private string strCon = @"Data Source=(local);" + "Initial Catalog=MicroERP;Integrated Security=true;";
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=MicroERP;Integrated Security=true;";
        //private string strCon = global::Facade.Properties.Settings.Default.ConnectionString;

        #region Variablen

        int id;
        //Firma
        string firmname;
        string UID;
        //Adresse
        string adress;
        string billingadress;
        string deliveryadress;
        //Kontakte
        string title;
        string firstname;
        string lastname;
        string suffix;
        DateTime birthday;
        //Invoice
        DateTime paymentDate;
        int idContact;
        int idRechnung;
        string number;
        string comment;
        string message;
        string article1;
        int amount1;
        int Ust1;
        int stk1;
        string article2;
        int amount2;
        int Ust2;
        int stk2;
        string article3;
        int amount3;
        int Ust3;
        int stk3;

        #endregion

        //Contact

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

        //Firma

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

                    string query = "SELECT ID_Firma,Name, UID, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Firma on ID_Kontakte = FK_Kontakte WHERE [Name] = @text";

                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@text", text);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {

                            Firma firm = new Firma();

                            firm.ID = rd.GetInt32(0);
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

        #region search Firm ID
        public Firmlist searchFirmID(int id)
        {
            try
            {
                Firmlist list = new Firmlist();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "SELECT ID_Firma, Name, UID Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Firma on ID_Kontakte = FK_Kontakte WHERE [ID_Firma] = @id";


                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            Firma firm = new Firma();

                            firm.ID = rd.GetInt32(0);
                            firm.Name = rd.GetString(1);
                            firm.UID = rd.GetString(2);
                            firm.Adresse = rd.GetString(3);
                            firm.Rechnungsadresse = rd.GetString(4);
                            firm.Lieferadresse = rd.GetString(5);

                            list.firma.Add(firm);
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

        #region UpdateFirm

        public void UpdateFirm(Firmlist list)
        {

            foreach (var obj in list.firma)
            {
                id = obj.ID;
                firmname = obj.Name;
                UID = obj.UID;
                adress = obj.Adresse;
                billingadress = obj.Rechnungsadresse;
                deliveryadress = obj.Lieferadresse;
            }

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "UPDATE Firma SET Name = @firmname, UID = @UID WHERE ID_Firma = @id";
                    //string query = "UPDATE Person SET Titel = @title, Vorname = @firstname, Nachname = @lastname, Suffix = @suffix WHERE ID_Person = @id";
                    string query2 = "UPDATE Kontakte SET Adresse = @adress, Rechnungsadresse = @billingadress, Lieferadresse = @deliveryadress WHERE ID_Kontakte = (SELECT FK_Kontakte FROM Firma WHERE ID_Firma = @id)";


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@firmname", firmname);
                    cmdUpdate1.Parameters.AddWithValue("@UID", UID);
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
                throw new Exception("Updating Firm failed.");
            }
        }

        #endregion

        //Invoice

        #region SearchIDInvoice
        public InvoiceList searchIDInvoice(int id)
        {

            try
            {
                InvoiceList list = new InvoiceList();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "SELECT ID_Rechnungen, Datum, Faelligkeit, Rechnungsnummer, Kommentar, Nachricht FROM Rechnungen WHERE [ID_Rechungen] = @id";

                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            Invoice invoice = new Invoice();

                            invoice.ID = rd.GetInt32(0);
                            invoice.Datum = rd.GetDateTime(1);
                            invoice.Faelligkeit = rd.GetString(2);
                            invoice.Nummer = rd.GetString(3);
                            invoice.Kommentar = rd.GetString(4);
                            invoice.Nachricht = rd.GetString(5);

                            list.invoice.Add(invoice);
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

        #region NewInvoice
        public void NewInvoice(InvoiceList list)
        {
            foreach (var obj in list.invoice)
            {

                //Rechnungen
                paymentDate = obj.Faelligkeit;
                number = obj.Nummer;
                comment = obj.Kommentar;
                message = obj.Nachricht;

                idContact = obj.IDKontakt;
                billingadress = obj.Rechnungsadresse; //Kontakte

                //Rechnungszeile
                article1 = obj.Artikel1;
                amount1 = obj.Menge1;
                Ust1 = obj.Ust1;
                stk1 = obj.Stueckpreis1;
                article2 = obj.Artikel2;
                amount2 = obj.Menge2;
                Ust2 = obj.Ust2;
                stk2 = obj.Stueckpreis2;
                article3 = obj.Artikel3;
                amount3 = obj.Menge3;
                Ust3 = obj.Ust3;
                stk3 = obj.Stueckpreis3;

            }

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "INSERT INTO Rechnungen(FK_Person, Datum, Faellikeit, Rechnungsnummer, Kommentar, Nachricht) VALUES (@idContact, GETDATE(), @paymentDate, @number, @comment, @message)";
                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    cmdInsert1.Parameters.AddWithValue("@idContact", idContact);
                    cmdInsert1.Parameters.AddWithValue("@paymentDate", paymentDate);
                    cmdInsert1.Parameters.AddWithValue("@number", number);
                    cmdInsert1.Parameters.AddWithValue("@comment", comment);
                    cmdInsert1.Parameters.AddWithValue("@message", message);
                    cmdInsert1.ExecuteNonQuery();

                    string query2 = "SELECT ID_Rechnungen FROM Rechnungen WHERE [Rechnungsnummer] = @number";
                    SqlCommand cmdSelect = new SqlCommand(query2, db);
                    cmdSelect.Parameters.AddWithValue("@number", number);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        // Daten holen
                        while (rd.Read())
                        {
                            idRechnung = rd.GetInt32(0);
                        }
                        // DataReader schließen 
                        rd.Close();
                    }


                    string query3 = "INSERT INTO Rechnungszeile(FK_Rechnungen, Artikelname, Menge,  Ust, Preis) VALUES (@idRechnung, @article, @amount, @Ust, @stk)";
                    SqlCommand cmdInsert2 = new SqlCommand(query3, db);
                    cmdInsert2.Parameters.AddWithValue("@idRechnung", idRechnung);
                    cmdInsert2.Parameters.AddWithValue("@article", article1);
                    cmdInsert2.Parameters.AddWithValue("@amount", amount1);
                    cmdInsert2.Parameters.AddWithValue("@Ust", Ust1);
                    cmdInsert2.Parameters.AddWithValue("@stk", stk1);
                    cmdInsert2.ExecuteNonQuery();

                    if(article2 != "")
                    {
                        string query4 = "INSERT INTO Rechnungszeile(FK_Rechnungen, Artikelname, Menge,  Ust) VALUES (@idRechnung, @article, @amount, @Ust)";
                        SqlCommand cmdInsert3 = new SqlCommand(query4, db);
                        cmdInsert3.Parameters.AddWithValue("@idRechnung", idRechnung);
                        cmdInsert3.Parameters.AddWithValue("@article", article2);
                        cmdInsert3.Parameters.AddWithValue("@amount", amount2);
                        cmdInsert3.Parameters.AddWithValue("@Ust", Ust2);
                        cmdInsert3.Parameters.AddWithValue("@stk", stk2);
                        cmdInsert3.ExecuteNonQuery();
                    }

                    if(article3 != "")
                    {
                        string query5 = "INSERT INTO Rechnungszeile(FK_Rechnungen, Artikelname, Menge,  Ust) VALUES (@idRechnung, @article, @amount, @Ust)";
                        SqlCommand cmdInsert4 = new SqlCommand(query5, db);
                        cmdInsert4.Parameters.AddWithValue("@idRechnung", idRechnung);
                        cmdInsert4.Parameters.AddWithValue("@article", article3);
                        cmdInsert4.Parameters.AddWithValue("@amount", amount3);
                        cmdInsert4.Parameters.AddWithValue("@Ust", Ust3);
                        cmdInsert4.Parameters.AddWithValue("@stk", stk3);
                        cmdInsert4.ExecuteNonQuery();
                    }

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Invoice failed.");
            }
        }

        #endregion


    }
}

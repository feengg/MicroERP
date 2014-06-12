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
        int number;
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

        int ID;
        DateTime Datum;
        #endregion

        //Contact

        #region NewContacts
        public void NewContacts(ContactsList list)
        {

            foreach (var obj in list.contact)
            {

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

                    string query = "INSERT INTO Kontakte(Adresse, Rechnungsadresse, Lieferadresse) VALUES(@adress, @billingadress, @deliveryadress)";
                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    cmdInsert1.Parameters.AddWithValue("@adress", adress);
                    cmdInsert1.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdInsert1.Parameters.AddWithValue("@deliveryadress", deliveryadress);
                    cmdInsert1.ExecuteNonQuery();

                    db.Close();
                    Person(adress, title, firstname, lastname, suffix, birthday);

                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Contact failed.");
            }
        }
        public void Person(string adress, string title, string firstname, string lastname, string suffix, DateTime birthday)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "INSERT INTO Person(FK_Kontakte, Titel, Vorname, Nachname, Suffix, Geburtsdatum) VALUES ((SELECT ID_Kontakte FROM Kontakte WHERE Adresse = @adress), @title, @firstname, @lastname, @suffix, @birthday)";
                    SqlCommand cmdInsert2 = new SqlCommand(query2, db);
                    cmdInsert2.Parameters.AddWithValue("@adress", adress);
                    cmdInsert2.Parameters.AddWithValue("@title", title);
                    cmdInsert2.Parameters.AddWithValue("@firstname", firstname);
                    cmdInsert2.Parameters.AddWithValue("@lastname", lastname);
                    cmdInsert2.Parameters.AddWithValue("@suffix", suffix);
                    cmdInsert2.Parameters.AddWithValue("@birthday", birthday);

                    cmdInsert2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Contact failed.");
            }
        }
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
                throw new Exception("Searching Contact failed");
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
                        }
                        rd.Close();
                    }

                    db.Close();

                    return list;

                }

            }
            catch (Exception)
            {
                throw new Exception("Searching Contact ID failed");
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
                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@title", title);
                    cmdUpdate1.Parameters.AddWithValue("@firstname", firstname);
                    cmdUpdate1.Parameters.AddWithValue("@lastname", lastname);
                    cmdUpdate1.Parameters.AddWithValue("@suffix", suffix);
                    cmdUpdate1.Parameters.AddWithValue("@birthday", birthday);
                    cmdUpdate1.ExecuteNonQuery();
                    db.Close();
                    Adresse(id, adress, billingadress, deliveryadress);

                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Contact failed.");
            }


        }
        public void Adresse(int id, string adress, string billingadress, string deliveryadress)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "UPDATE Kontakte SET Adresse = @adress, Rechnungsadresse = @billingadress, Lieferadresse = @deliveryadress WHERE ID_Kontakte = (SELECT FK_Kontakte FROM Person WHERE ID_Person = @id)";
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate2.Parameters.AddWithValue("@id", id);
                    cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

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

                    string query = "INSERT INTO Kontakte(Adresse, Rechnungsadresse, Lieferadresse) VALUES(@adress, @billingadress, @deliveryadress)";

                    SqlCommand cmdInsert1 = new SqlCommand(query, db);

                    cmdInsert1.Parameters.AddWithValue("@adress", adress);
                    cmdInsert1.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdInsert1.Parameters.AddWithValue("@deliveryadress", deliveryadress);

                    cmdInsert1.ExecuteNonQuery();
                    db.Close();
                    Firma(adress, firmname, UID);
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Firm failed.");
            }
        }
        public void Firma(string adress, string firmname, string UID)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = " INSERT INTO Firma(FK_Kontakte, Name, UID) VALUES ((SELECT ID_Kontakte FROM Kontakte WHERE Adresse = @adress), @firmname, @UID)";

                    SqlCommand cmdInsert2 = new SqlCommand(query2, db);
                    cmdInsert2.Parameters.AddWithValue("@adress", adress);
                    cmdInsert2.Parameters.AddWithValue("@firmname", firmname);
                    cmdInsert2.Parameters.AddWithValue("@UID", UID);

                    cmdInsert2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Contact failed.");
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
                    string query = "SELECT ID_Firma, Name, UID, Adresse, Rechnungsadresse, Lieferadresse FROM Kontakte inner join Firma on ID_Kontakte = FK_Kontakte WHERE [ID_Firma] = @id";


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
                        rd.Close();
                    }

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


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@firmname", firmname);
                    cmdUpdate1.Parameters.AddWithValue("@UID", UID);

                    cmdUpdate1.ExecuteNonQuery();

                    db.Close();
                    KontaktFirma(id, adress, billingadress, deliveryadress);
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Firm failed.");
            }
        }
        public void KontaktFirma(int id, string adress, string billingadress, string deliveryadress)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "UPDATE Kontakte SET Adresse = @adress, Rechnungsadresse = @billingadress, Lieferadresse = @deliveryadress WHERE ID_Kontakte = (SELECT FK_Kontakte FROM Firma WHERE ID_Firma = @id)";
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate2.Parameters.AddWithValue("@id", id);
                    cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

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

        //Invoice

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

                    string query = "INSERT INTO Rechnungen(FK_Person, Datum, Faelligkeit, Rechnungsnummer, Kommentar, Nachricht) VALUES (@idContact, GETDATE(), @paymentDate, @number, @comment, @message)";
                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    cmdInsert1.Parameters.AddWithValue("@idContact", idContact);
                    cmdInsert1.Parameters.AddWithValue("@paymentDate", paymentDate);
                    cmdInsert1.Parameters.AddWithValue("@number", number);
                    cmdInsert1.Parameters.AddWithValue("@comment", comment);
                    cmdInsert1.Parameters.AddWithValue("@message", message);

                    cmdInsert1.ExecuteNonQuery();
                    db.Close();

                    SaveID(number);

                    SaveInvoiceRow(idRechnung, article1, amount1, Ust1, stk1, article2, amount2, Ust2, stk2, article3, amount3, Ust3, stk3);

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Invoice failed.");
            }
        }
        #region SaveID
        public void SaveID(int number)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "SELECT ID_Rechnungen FROM Rechnungen WHERE [Rechnungsnummer] = @number";
                    SqlCommand cmdSelect = new SqlCommand(query2, db);
                    cmdSelect.Parameters.AddWithValue("@number", number);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            idRechnung = rd.GetInt32(0);
                        }
                        rd.Close();
                    }
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Selecting InvoiceID failed.");
            }
        }
        #endregion

        #region SaveInvoiceRow
        public void SaveInvoiceRow(int idRechnung, string article1, int amount1, int Ust1, int stk1, string article2, int amount2, int Ust2, int stk2, string article3, int amount3, int Ust3, int stk3)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query3 = "INSERT INTO Rechnungszeile(FK_Rechnungen, Artikelname1, Menge1,  Ust1, Preis1, Artikelname2, Menge2,  Ust2, Preis2, Artikelname3, Menge3,  Ust3, Preis3) VALUES (@idRechnung, @article1, @amount1, @Ust1, @stk1, @article2, @amount2, @Ust2, @stk2, @article3, @amount3, @Ust3, @stk3)";
                    SqlCommand cmdInsert2 = new SqlCommand(query3, db);
                    cmdInsert2.Parameters.AddWithValue("@idRechnung", idRechnung);
                    cmdInsert2.Parameters.AddWithValue("@article1", article1);
                    cmdInsert2.Parameters.AddWithValue("@amount1", amount1);
                    cmdInsert2.Parameters.AddWithValue("@Ust1", Ust1);
                    cmdInsert2.Parameters.AddWithValue("@stk1", stk1);
                    cmdInsert2.Parameters.AddWithValue("@article2", article2);
                    cmdInsert2.Parameters.AddWithValue("@amount2", amount2);
                    cmdInsert2.Parameters.AddWithValue("@Ust2", Ust2);
                    cmdInsert2.Parameters.AddWithValue("@stk2", stk2);
                    cmdInsert2.Parameters.AddWithValue("@article3", article3);
                    cmdInsert2.Parameters.AddWithValue("@amount3", amount3);
                    cmdInsert2.Parameters.AddWithValue("@Ust3", Ust3);
                    cmdInsert2.Parameters.AddWithValue("@stk3", stk3);
                    cmdInsert2.ExecuteNonQuery();

                    db.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Inserting InvoiceRow1 failed.");
            }
        }

        #endregion
        #endregion

        #region SearchIDInvoice
        public InvoiceList searchIDInvoice(int id)
        {

            try
            {
                InvoiceList list = new InvoiceList();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query = "SELECT ID_Rechnungen, Datum, Faelligkeit, Rechnungsnummer, Rechnungsadresse, Vorname, Nachname, Kommentar, Nachricht FROM Rechnungen inner join Person on FK_Person = ID_Person inner join Kontakte on FK_Kontakte = ID_Kontakte WHERE [ID_Rechnungen] = @id";
                    SqlCommand cmdSelect1 = new SqlCommand(query, db);
                    cmdSelect1.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader rd = cmdSelect1.ExecuteReader())
                    {
                        while (rd.Read())
                        {

                            ID = rd.GetInt32(0);
                            Datum = rd.GetDateTime(1);
                            paymentDate = rd.GetDateTime(2);
                            number = rd.GetInt32(3);
                            billingadress = rd.GetString(4);
                            firstname = rd.GetString(5);
                            lastname = rd.GetString(6);
                            comment = rd.GetString(7);
                            message = rd.GetString(8);

                        }

                        rd.Close();
                    }

                    string query2 = "SELECT Artikelname1, Menge1, Ust1, Preis1, Artikelname2, Menge2, Ust2, Preis2, Artikelname3, Menge3, Ust3, Preis3 FROM Rechnungszeile WHERE [FK_Rechnungen] = @id";
                    SqlCommand cmdSelect2 = new SqlCommand(query2, db);
                    cmdSelect2.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader rd = cmdSelect2.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Invoice invoice = new Invoice();

                            invoice.ID = ID;
                            invoice.Datum = Datum;
                            invoice.Faelligkeit = paymentDate;
                            invoice.Nummer = number;
                            invoice.Rechnungsadresse = billingadress;
                            invoice.Vorname = firstname;
                            invoice.Nachname = lastname;
                            invoice.Kommentar = comment;
                            invoice.Nachricht = message;
                            invoice.Artikel1 = rd.GetString(0);
                            invoice.Menge1 = rd.GetInt32(1);
                            invoice.Ust1 = rd.GetInt32(2);
                            invoice.Stueckpreis1 = rd.GetInt32(3);
                            invoice.Artikel2 = rd.GetString(4);
                            invoice.Menge2 = rd.GetInt32(5);
                            invoice.Ust2 = rd.GetInt32(6);
                            invoice.Stueckpreis2 = rd.GetInt32(6);
                            invoice.Artikel3 = "";
                            //if (rd.IsDBNull(7)) invoice.Artikel3 = "";
                            //invoice.Artikel3 = rd.GetString(7);
                            //invoice.Menge3 = rd.GetInt32(8);
                            //invoice.Ust3 = rd.GetInt32(9);
                            //invoice.Stueckpreis3 = rd.GetInt32(10);

                            list.invoice.Add(invoice);
                        }
                        rd.Close();
                    }


                    db.Close();

                    return list;

                }

            }
            catch (Exception)
            {
                throw new Exception("Seacrhing Invoice ID failed");
            }
        }
        #endregion

        #region searchInvoice
        public InvoiceList searchInvoiceByName(string searchKontakt)
        {

            try
            {
                InvoiceList list = new InvoiceList();

                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();

                    string query = "SELECT ID_Rechnungen, Datum, Faelligkeit, Rechnungsnummer, Rechnungsadresse, Vorname, Nachname, Kommentar, Nachricht FROM Rechnungen inner join Person on FK_Person = ID_Person inner join Kontakte on FK_Kontakte = ID_Kontakte WHERE [Vorname] = @searchKontakt or [Nachname] = @searchKontakt";

                    SqlCommand cmdSelect = new SqlCommand(query, db);
                    cmdSelect.Parameters.AddWithValue("@searchKontakt", searchKontakt);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            ID = rd.GetInt32(0);
                            Datum = rd.GetDateTime(1);
                            paymentDate = rd.GetDateTime(2);
                            number = rd.GetInt32(3);
                            billingadress = rd.GetString(4);
                            firstname = rd.GetString(5);
                            lastname = rd.GetString(6);
                            comment = rd.GetString(7);
                            message = rd.GetString(8);
                        }

                        rd.Close();
                    }

                    string query2 = "SELECT Artikelname1, Menge1, Ust1, Preis1, Artikelname2, Menge2, Ust2, Preis2, Artikelname3, Menge3, Ust3, Preis3 FROM Rechnungszeile WHERE [FK_Rechnungen] = @id";
                    SqlCommand cmdSelect1 = new SqlCommand(query2, db);
                    cmdSelect1.Parameters.AddWithValue("@id", ID);

                    using (SqlDataReader rd = cmdSelect1.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            Invoice invoice = new Invoice();
                            invoice.ID = ID;
                            invoice.Datum = Datum;
                            invoice.Faelligkeit = paymentDate;
                            invoice.Nummer = number;
                            invoice.Rechnungsadresse = billingadress;
                            invoice.Vorname = firstname;
                            invoice.Nachname = lastname;
                            invoice.Kommentar = comment;
                            invoice.Nachricht = message;
                            invoice.Artikel1 = rd.GetString(0);
                            invoice.Menge1 = rd.GetInt32(1);
                            invoice.Ust1 = rd.GetInt32(2);
                            invoice.Stueckpreis1 = rd.GetInt32(3);
                            invoice.Artikel2 = rd.GetString(4);
                            invoice.Menge2 = rd.GetInt32(5);
                            invoice.Ust2 = rd.GetInt32(6);
                            invoice.Stueckpreis2 = rd.GetInt32(6);
                            invoice.Artikel3 = "";
                            //if (rd.IsDBNull(7)) invoice.Artikel3 = "";
                            //invoice.Artikel3 = rd.GetString(7);
                            //invoice.Menge3 = rd.GetInt32(8);
                            //invoice.Ust3 = rd.GetInt32(9);
                            //invoice.Stueckpreis3 = rd.GetInt32(10);

                            list.invoice.Add(invoice);
                        }
                        rd.Close();
                    }

                    db.Close();

                    return list;

                }


            }
            catch (Exception)
            {
                throw new Exception("Search Invoice by Contact failed");
            }
        }

        #region SaveID
        public void SaveIDKontakt(string searchKontakt)
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "SELECT ID_Rechnungen FROM Rechnungen inner join Person on FK_Person = ID_Person WHERE [Vorname] = @searchKontakt or [Nachname] = @searchKontakt";
                    SqlCommand cmdSelect = new SqlCommand(query2, db);
                    cmdSelect.Parameters.AddWithValue("@searchKontakt", searchKontakt);

                    using (SqlDataReader rd = cmdSelect.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            idRechnung = rd.GetInt32(0);
                        }
                        rd.Close();
                    }
                    db.Close();
                }
            }
            catch (Exception)
            {
                throw new Exception("Selecting InvoiceIDKontakt failed.");
            }
        }
        #endregion
        #endregion

        #region Update Invoice
        public void UpdateInvoice(InvoiceList list)
        {

            foreach (var obj in list.invoice)
            {
                //Invoice
                id = obj.ID;
                comment = obj.Kommentar;
                message = obj.Nachricht;
                //InvoiceLine
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

                    string query = "UPDATE Rechnungen SET Kommentar = @comment, Nachricht = @message WHERE [ID_Rechnungen] = @id";


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@number", number);
                    cmdUpdate1.Parameters.AddWithValue("@comment", comment);
                    cmdUpdate1.Parameters.AddWithValue("@message", message);

                    cmdUpdate1.ExecuteNonQuery();

                    db.Close();
                    Rechnungszeile(id, article1, amount1, Ust1, stk1, article2, amount2, Ust2, stk2, article3, amount3, Ust3, stk3);
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Invoice failed.");
            }
        }

        #region Rechnungszeile
        public void Rechnungszeile(int id, string article1, int amount1, int Ust1, int stk1, string article2, int amount2, int Ust2, int stk2, string article3, int amount3, int Ust3, int stk3 )
        {
            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    string query2 = "UPDATE Rechnungszeile SET Artikelname1 = @article1, Menge1 = @amount1, Ust1 = @Ust1, Preis1 = @stk1, Artikelname2 = @article2, Menge2 = @amount2, Ust2 = @Ust2, Preis2 = @stk2, Artikelname3 = @article3, Menge3 = @amount3, Ust3 = @Ust3, Preis3 = @stk3 WHERE [FK_Rechnungen] = @id";
                    SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate2.Parameters.AddWithValue("@id", id);
                    cmdUpdate2.Parameters.AddWithValue("@article1", article1);
                    cmdUpdate2.Parameters.AddWithValue("@amount1", amount1);
                    cmdUpdate2.Parameters.AddWithValue("@Ust1", Ust1);
                    cmdUpdate2.Parameters.AddWithValue("@stk1", stk1);
                    cmdUpdate2.Parameters.AddWithValue("@article2", article2);
                    cmdUpdate2.Parameters.AddWithValue("@amount2", amount2);
                    cmdUpdate2.Parameters.AddWithValue("@Ust2", Ust2);
                    cmdUpdate2.Parameters.AddWithValue("@stk2", stk2);
                    cmdUpdate2.Parameters.AddWithValue("@article3", article3);
                    cmdUpdate2.Parameters.AddWithValue("@amount3", amount3);
                    cmdUpdate2.Parameters.AddWithValue("@Ust3", Ust3);
                    cmdUpdate2.Parameters.AddWithValue("@stk3", stk3);


                    cmdUpdate2.ExecuteNonQuery();

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Updating Invoice Line failed.");
            }

        }
        #endregion 
        #endregion

    }
}

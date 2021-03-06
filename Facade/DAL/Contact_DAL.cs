﻿using System;
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
    class Contact_DAL
    {
        //Datenbankverbindung
        //private string strCon = @"Data Source=(local);" + "Initial Catalog=MicroERP;Integrated Security=true;";
        private string strCon = @"Data Source=.\sqlexpress;" + "Initial Catalog=MicroERP;Integrated Security=true;";
        //private string strCon = global::Facade.Properties.Settings.Default.ConnectionString;

        bool Firma = false;

        #region checkFirma
        //private void checkFirma(string text)
        //{
        //    try
        //    {
        //        using (SqlConnection db = new SqlConnection(strCon))
        //        {
        //            db.Open();

        //            string query = "SELECT Vorname, Nachname FROM Kontakte inner join Person on ID_Kontakte = FK_Kontakte WHERE [Vorname] = @text or [Nachname] = @text";

        //            SqlCommand cmdSelect = new SqlCommand(query, db);
        //            cmdSelect.Parameters.AddWithValue("@text", text);

        //            using (SqlDataReader rd = cmdSelect.ExecuteReader())
        //            {
        //               if (rd["Vorname"] != DBNull.Value)
        //                {
        //                    Firma = false;
        //                }

        //               else
        //               {
        //                   Firma = true;
        //               }
        //                rd.Close();
        //            }

        //            // Verbindung schließen 
        //            db.Close();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("connection to database failed");
        //    }

        //}
        #endregion

        #region searchContact
        public List<Contact> searchContacts(string text)
        {
            //checkFirma(text);

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
                            //var value = rd["Suffix"];
                            //if (value != "")
                            //{
                                instance.Suffix = rd.GetString(4);
                            //}
                            //else
                            //{
                            //    Console.WriteLine("null");
                            //}
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
        }
        #endregion

        #region SearchID
        public List<Contact> searchID(int id)
        {
            try
            {
                List<Contact> list = new List<Contact>();
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
        }
        #endregion

        #region UpdateContacts
        public void UpdateContacts(Contact list)
        {
            int id = list.ID;
            string title = list.Titel;
            string firstname = list.Vorname;
            string lastname = list.Nachname;
            string suffix = list.Suffix;
            DateTime birthday = list.Geburtsdatum;
            string adress = list.Adresse;
            string billingadress = list.Rechnungsadresse;
            string deliveryadress = list.Lieferadresse;
            

            try
            {
                //List<Contact> list = new List<Contact>();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "UPDATE Person SET Titel = @title, Vorname = @firstname, Nachname = @lastname, Suffix = @suffix, Geburtsdatum = @birthday WHERE ID_Person = @id";
                    //string query = "UPDATE Person SET Titel = @title, Vorname = @firstname, Nachname = @lastname, Suffix = @suffix WHERE ID_Person = @id";
                    //string query2 = "UPDATE Kontakte SET Adresse = @adress, Rechnungsadresse = @billingadress, Lieferadresse = @deliveryadress WHERE ID_Kontakte = (SELECT FK_Kontakte FROM Person WHERE ID_Person = @id)";


                    SqlCommand cmdUpdate1 = new SqlCommand(query, db);
                    //SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdUpdate1.Parameters.AddWithValue("@id", id);
                    cmdUpdate1.Parameters.AddWithValue("@title", title);
                    cmdUpdate1.Parameters.AddWithValue("@firstname", firstname);
                    cmdUpdate1.Parameters.AddWithValue("@lastname", lastname);
                    cmdUpdate1.Parameters.AddWithValue("@suffix", suffix);
                    cmdUpdate1.Parameters.AddWithValue("@birthday", birthday);
                    //cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    //cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    //cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

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
        public void NewContacts(Contact list)
        {
            
            string title = list.Titel;
            string firstname = list.Vorname;
            string lastname = list.Nachname;
            string suffix = list.Suffix;
            DateTime birthday = list.Geburtsdatum;
            string adress = list.Adresse;
            string billingadress = list.Rechnungsadresse;
            string deliveryadress = list.Lieferadresse;

            try
            {
                //List<Contact> list = new List<Contact>();
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                     string query = "INSERT INTO Person VALUES (@title, @firstname, @lastname, @suffix, @birthday)";
                    //string query2 = "INSERT INTO ";


                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    //SqlCommand cmdUpdate2 = new SqlCommand(query2, db);
                    cmdInsert1.Parameters.AddWithValue("@title", title);
                    cmdInsert1.Parameters.AddWithValue("@firstname", firstname);
                    cmdInsert1.Parameters.AddWithValue("@lastname", lastname);
                    cmdInsert1.Parameters.AddWithValue("@suffix", suffix);
                    cmdInsert1.Parameters.AddWithValue("@birthday", birthday);
                    //cmdUpdate2.Parameters.AddWithValue("@adress", adress);
                    //cmdUpdate2.Parameters.AddWithValue("@billingadress", billingadress);
                    //cmdUpdate2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

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
        public void NewFirm(Contact list)
        {

            string firmname = list.Name;
            string UID = list.UID;
            string adress = list.Adresse;
            string billingadress = list.Rechnungsadresse;
            string deliveryadress = list.Lieferadresse;

            try
            {
                using (SqlConnection db = new SqlConnection(strCon))
                {
                    db.Open();
                    //SQL Statement zum auslesen
                    string query = "INSERT INTO Firma VALUES (@firmname, @UID, @lastname, @suffix, @birthday)";
                    string query2 = "INSERT INTO Kontakte VALUES(@adress, @billingadress, @deliveryadress)";

                    SqlCommand cmdInsert1 = new SqlCommand(query, db);
                    SqlCommand cmdInsert2 = new SqlCommand(query2, db);
                    cmdInsert1.Parameters.AddWithValue("@firmname", firmname);
                    cmdInsert1.Parameters.AddWithValue("@UID", UID);
                    cmdInsert2.Parameters.AddWithValue("@adress", adress);
                    cmdInsert2.Parameters.AddWithValue("@billingadress", billingadress);
                    cmdInsert2.Parameters.AddWithValue("@deliveryadress", deliveryadress);

                    db.Close();
                }

            }
            catch (Exception)
            {
                throw new Exception("Inserting Firm failed.");
            }
        }
        #endregion
    }
}

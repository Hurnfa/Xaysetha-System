using Microsoft.Reporting.WinForms;
using Npgsql;
/*using Org.BouncyCastle.Math;*/
using System;
using System.Numerics;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class book_printing : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;

        public book_printing()
        {
            InitializeComponent();
            reportViewer1.LocalReport.EnableExternalImages = true;
            cn.getConnect();
        }

        DateTime birthDay, famBookIssueDate, issueDate, expDate;
        ReportParameterCollection rp = new ReportParameterCollection();

        string gender;
        public void loadDataToReport(BigInteger tenantID, string name, string place)
        {
            if (name == null)
            {
                //cmd = new NpgsqlCommand("select * from tb_tenant inner join \"tb_residentialBook\" on tb_tenant.\"tenantID\" = \"tb_residentialBook\".\"tenantID\" where tb_tenant.\"tenantID\"=@tenantID;", cn.conn);
                cmd = new NpgsqlCommand("select * from tb_tenant join \"tb_residentialBook\" on tb_tenant.\"tenantID\" = \"tb_residentialBook\".\"tenantID\" join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_tenant.\"tenantID\" = @tenantID;", cn.conn);


                try
                {
                    cmd.Parameters.AddWithValue("@tenantID", tenantID);
                    //cmd.Parameters.AddWithValue("@firstname", name);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        rp.Add(new ReportParameter("tenantName", reader["firstname"].ToString()));
                        rp.Add(new ReportParameter("tenantSurname", reader["lastname"].ToString()));

                        switch (reader["gender"].ToString())
                        {
                            case "ຊາຍ":

                                gender = "ທ້າວ";

                                break;

                            case "ຍິງ":

                                gender = "ນາງ";

                                break;

                            default:

                                gender = "ທ່ານ";

                                break;
                        }

                        rp.Add(new ReportParameter("tenantGender", gender));

                        birthDay = DateTime.Parse(reader["dob"].ToString());

                        rp.Add(new ReportParameter("tenantRace", "N/A"));
                        rp.Add(new ReportParameter("tenantReligion", reader["religion"].ToString()));
                        rp.Add(new ReportParameter("tenantJobs", reader["occupation"].ToString()));
                        rp.Add(new ReportParameter("tenantWorkplace", "N/A"));

                        famBookIssueDate = DateTime.Parse(reader["famBookIssueDate"].ToString());

                        rp.Add(new ReportParameter("tenantFamBookID", reader["fambookID"].ToString()));

                        rp.Add(new ReportParameter("village", reader["village"].ToString()));
                        rp.Add(new ReportParameter("district", reader["district"].ToString()));
                        rp.Add(new ReportParameter("province", "N/A"));
                        rp.Add(new ReportParameter("tenantNationality", reader["nationality"].ToString()));
                        rp.Add(new ReportParameter("tenantEthnic", reader["ethnics"].ToString()));

                        //if (reader["tenantpics"] == DBNull.Value)
                        //{
                        //    rp.Add(new ReportParameter("tenantPics", "0"));

                        //}
                        //else
                        //{
                        //    byte[] imageData = (byte[])reader["tenantpics"];
                        //    /*string base64String = Convert.ToBase64String(imageData);
                        //    string imageDataUrl = $"data:image/jpeg;base64,{base64String}";*/
                        //    rp.Add(new ReportParameter("tenantPics", imageData.ToString()));
                        //}




                        rp.Add(new ReportParameter("duration", reader["duration"].ToString()));
                        expDate = DateTime.Parse(reader["expDate"].ToString());
                        issueDate = DateTime.Parse(reader["issueDate"].ToString());

                    }

                    reader.Close();

                    rp.Add(new ReportParameter("tenantID", tenantID.ToString()));
                    rp.Add(new ReportParameter("tenantDadName", "N/A"));
                    rp.Add(new ReportParameter("tenantMomName", "N/A"));
                    rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));
                    rp.Add(new ReportParameter("tenantFamBookIssueDate", famBookIssueDate.ToString("dd/MM/yyyy")));

                    //book section

                    rp.Add(new ReportParameter("tenantPurpose", "."));




                    rp.Add(new ReportParameter("expDate", expDate.ToString("dd/MM/yyyy")));



                    rp.Add(new ReportParameter("issueDate", issueDate.ToString("dd/MM/yyyy")));


                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tenantID == 0)
            {
                cmd = new NpgsqlCommand("select * from tb_tenant join \"tb_residentialBook\" on tb_tenant.\"tenantID\" = \"tb_residentialBook\".\"tenantID\" " +
                    "join tb_payment on tb_tenant.\"tenantID\" = tb_payment.tenant_id where tb_tenant.firstname = @firstname;", cn.conn);

                try
                {
                    //cmd.Parameters.AddWithValue("@tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@firstname", name);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        rp.Add(new ReportParameter("tenantID", reader["tenantID"].ToString()));
                        //rp.Add(new ReportParameter("tenantName", reader["firstname"].ToString()));
                        rp.Add(new ReportParameter("tenantSurname", reader["lastname"].ToString()));

                        switch (reader["gender"].ToString())
                        {
                            case "ຊາຍ":

                                gender = "ທ້າວ";

                                break;

                            case "ຍິງ":

                                gender = "ນາງ";

                                break;

                            default:

                                gender = "ທ່ານ";

                                break;
                        }

                        rp.Add(new ReportParameter("tenantGender", gender));

                        birthDay = DateTime.Parse(reader["dob"].ToString());

                        rp.Add(new ReportParameter("tenantRace", "N/A"));
                        rp.Add(new ReportParameter("tenantReligion", reader["religion"].ToString()));
                        rp.Add(new ReportParameter("tenantJobs", reader["occupation"].ToString()));
                        rp.Add(new ReportParameter("tenantWorkplace", "N/A"));

                        famBookIssueDate = DateTime.Parse(reader["famBookIssueDate"].ToString());

                        rp.Add(new ReportParameter("tenantFamBookID", reader["fambookID"].ToString()));

                        rp.Add(new ReportParameter("village", reader["village"].ToString()));
                        rp.Add(new ReportParameter("district", reader["district"].ToString()));
                        rp.Add(new ReportParameter("province", "N/A"));
                        rp.Add(new ReportParameter("tenantNationality", reader["nationality"].ToString()));
                        rp.Add(new ReportParameter("tenantEthnic", reader["ethnics"].ToString()));

                        rp.Add(new ReportParameter("duration", reader["duration"].ToString()));
                        expDate = DateTime.Parse(reader["expDate"].ToString());
                        issueDate = DateTime.Parse(reader["issueDate"].ToString());

                        //if (reader["tenantpics"] == DBNull.Value)
                        //{
                        //    rp.Add(new ReportParameter("tenantPics", ""));
                        //}
                        //else
                        //{
                        //    byte[] imageData = (byte[])reader["tenantpics"];
                        //    /*                            string base64String = Convert.ToBase64String(imageData);
                        //                                rp.Add(new ReportParameter("tenantPics", base64String));*/
                        //    rp.Add(new ReportParameter("tenantPics", imageData.ToString()));
                        //}
                    }

                    reader.Close();

                    rp.Add(new ReportParameter("tenantName", name));
                    rp.Add(new ReportParameter("tenantDadName", "N/A"));
                    rp.Add(new ReportParameter("tenantMomName", "N/A"));
                    rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));
                    rp.Add(new ReportParameter("tenantFamBookIssueDate", famBookIssueDate.ToString("dd/MM/yyyy")));

                    //book section

                    rp.Add(new ReportParameter("tenantPurpose", "."));




                    rp.Add(new ReportParameter("expDate", expDate.ToString("dd/MM/yyyy")));



                    rp.Add(new ReportParameter("issueDate", issueDate.ToString("dd/MM/yyyy")));

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //display place section


            rp.Add(new ReportParameter("placeName", place));

            cmd = new NpgsqlCommand("select * from tb_place inner join tb_citizen on tb_place.\"citizentID\" = tb_citizen.\"citizenID\" where \"placeName\"=@placeName", cn.conn);

            try
            {
                cmd.Parameters.AddWithValue("@placeName", place);

                NpgsqlDataReader reader1 = cmd.ExecuteReader();

                while (reader1.Read())
                {
                    rp.Add(new ReportParameter("houseNo", reader1["placeHouseNumber"].ToString()));
                    rp.Add(new ReportParameter("houseUnit", reader1["placeHouseUnit"].ToString()));
                    //village missing value

                    switch (reader1["gender"].ToString())
                    {
                        case "ຊາຍ":

                            gender = "ທ້າວ";

                            break;

                        case "ຍິງ":

                            gender = "ນາງ";

                            break;

                        default:

                            gender = "ທ່ານ";

                            break;
                    }

                    rp.Add(new ReportParameter("citizenGender", gender));
                    rp.Add(new ReportParameter("citizenName", reader1["name"].ToString()));
                    rp.Add(new ReportParameter("citizenSurname", reader1["surname"].ToString()));

                }

                reader1.Close();

                rp.Add(new ReportParameter("placeVillage", "ນາໄຊ"));

            }
            catch (Exception ex)
            {
                MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            reportViewer1.LocalReport.SetParameters(rp);
            reportViewer1.RefreshReport();
        }

        public void loadPlace()
        {

        }

        private void book_printing_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}

